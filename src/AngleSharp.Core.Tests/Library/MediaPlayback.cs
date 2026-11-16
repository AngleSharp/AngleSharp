namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Browser;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using AngleSharp.Media;
    using AngleSharp.Media.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class MediaPlaybackTests
    {
        [Test]
        public async Task MediaIsPausedWithoutDurationBeforeItLoads()
        {
            var (document, _) = await LoadAsync("<audio></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            Assert.IsTrue(audio.IsPaused);
            Assert.IsTrue(Double.IsNaN(audio.Duration));
            Assert.AreEqual(MediaReadyState.Nothing, audio.ReadyState);
        }

        [Test]
        public async Task StateSetBeforeTheResourceArrivesIsHandedToItsController()
        {
            var (document, controllers) = await LoadAsync("<audio></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var events = Record(audio);
            audio.Volume = 0.25;
            audio.IsMuted = true;
            audio.CurrentTime = 3;
            _ = audio.PlayAsync();
            Assert.IsFalse(audio.IsPaused);
            audio.Source = "sound.wav";
            var controller = await WaitForControllerAsync(audio);
            Assert.AreSame(controllers.Single(), controller);
            Assert.AreEqual(0.25, controller.Volume);
            Assert.IsTrue(controller.IsMuted);
            Assert.AreEqual(3.0, controller.CurrentTime);
            Assert.AreEqual(1, controller.PlayCount);
            CollectionAssert.IsSubsetOf(new[] { "volumechange", "play" }, events);
        }

        [Test]
        public async Task PlayResolvesOnceTheControllerPlays()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            var events = Record(audio);
            var play = audio.PlayAsync();
            Assert.IsFalse(play.IsCompleted);
            controller.Raise("playing");
            await play;
            CollectionAssert.AreEqual(new[] { "play", "playing" }, events);
            Assert.IsTrue(audio.PlayAsync().IsCompleted);
        }

        [Test]
        public async Task PauseRejectsAPendingPlayWithAnAbortError()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            var events = Record(audio);
            var play = audio.PlayAsync();
            audio.Pause();
            Assert.IsTrue(audio.IsPaused);
            Assert.AreEqual(1, controller.PauseCount);
            var error = await CatchAsync(play);
            Assert.AreEqual((Int32)DomError.Abort, error.Code);
            CollectionAssert.AreEqual(new[] { "play", "timeupdate", "pause" }, events);
        }

        [Test]
        public async Task EndedPausesTheMediaAndFiresPauseAndEnded()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            _ = audio.PlayAsync();
            controller.Raise("playing");
            var events = Record(audio);
            controller.State = MediaControllerPlaybackState.Ended;
            controller.Raise("ended");
            Assert.IsTrue(audio.IsPaused);
            Assert.IsTrue(audio.IsEnded);
            CollectionAssert.AreEqual(new[] { "pause", "ended" }, events);
        }

        [Test]
        public async Task PlayingAgainAfterTheEndStartsOver()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            controller.CurrentTime = 5;
            controller.State = MediaControllerPlaybackState.Ended;
            _ = audio.PlayAsync();
            Assert.AreEqual(0.0, controller.CurrentTime);
            Assert.AreEqual(1, controller.PlayCount);
        }

        [Test]
        public async Task LoopingMediaStartsOverInsteadOfEnding()
        {
            var (document, _) = await LoadAsync("<audio loop src=sound.wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            _ = audio.PlayAsync();
            controller.Raise("playing");
            var events = Record(audio);
            controller.CurrentTime = 5;
            controller.State = MediaControllerPlaybackState.Ended;
            controller.Raise("ended");
            Assert.IsFalse(audio.IsPaused);
            Assert.IsFalse(audio.IsEnded);
            Assert.AreEqual(0.0, controller.CurrentTime);
            Assert.AreEqual(2, controller.PlayCount);
            CollectionAssert.IsEmpty(events);
        }

        [Test]
        public async Task AutoplayMediaPlaysOnceItsResourceArrives()
        {
            var (document, _) = await LoadAsync("<audio autoplay src=sound.wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            Assert.AreEqual(1, controller.PlayCount);
            Assert.IsFalse(audio.IsPaused);
        }

        [Test]
        public async Task PlayRejectedByTheControllerKeepsTheMediaPaused()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio>", controller => controller.Refusal = new DomException("NotAllowedError"));
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            await WaitForControllerAsync(audio);
            var events = Record(audio);
            var error = await CatchAsync(audio.PlayAsync());
            Assert.AreEqual("NotAllowedError", error.Name);
            Assert.IsTrue(audio.IsPaused);
            CollectionAssert.IsEmpty(events);
        }

        [Test]
        public async Task UnsupportedResourceGivesAnErrorAndRejectsPendingPlays()
        {
            var (document, _) = await LoadAsync("<audio></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var failed = new TaskCompletionSource<Boolean>();
            audio.AddEventListener("error", (s, e) => failed.TrySetResult(true));
            var play = audio.PlayAsync();
            audio.Source = "page.html";
            var error = await CatchAsync(play);
            Assert.AreEqual((Int32)DomError.NotSupported, error.Code);
            await WithTimeout(failed.Task);
            Assert.AreEqual(MediaErrorCode.SourceNotSupported, audio.MediaError.Code);
        }

        [Test]
        public async Task FailedRequestGivesAnErrorAndRejectsPendingPlays()
        {
            var (document, _) = await LoadAsync("<audio></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var failed = new TaskCompletionSource<Boolean>();
            audio.AddEventListener("error", (s, e) => failed.TrySetResult(true));
            var play = audio.PlayAsync();
            audio.Source = "missing.wav";
            var error = await CatchAsync(play);
            Assert.AreEqual((Int32)DomError.NotSupported, error.Code);
            await WithTimeout(failed.Task);
            Assert.AreEqual(MediaErrorCode.SourceNotSupported, audio.MediaError.Code);
        }

        [Test]
        public async Task SourceOfAnUnsupportedTypeIsSkipped()
        {
            var (document, _) = await LoadAsync("<audio><source src=sound.ogg type=audio/ogg><source src=sound.wav type=audio/wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            Assert.AreEqual("http://localhost/sound.wav", audio.CurrentSource);
            var controller = await WaitForControllerAsync(audio);
            Assert.IsNotNull(controller);
            Assert.AreEqual("maybe", audio.CanPlayType("audio/wav"));
            Assert.AreEqual("", audio.CanPlayType("audio/ogg"));
        }

        [Test]
        public async Task SourceInsertedByAScriptLoads()
        {
            var (document, _) = await LoadAsync("<audio></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var source = document.CreateElement<IHtmlSourceElement>();
            source.Source = "sound.wav";
            audio.AppendChild(source);
            var controller = await WaitForControllerAsync(audio);
            Assert.IsNotNull(controller);
        }

        [Test]
        public async Task MediaRemovedFromItsDocumentPauses()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio>", withLoop: true);
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            _ = audio.PlayAsync();
            audio.Remove();
            Assert.IsFalse(audio.IsPaused);

            for (var i = 0; i < 500 && !audio.IsPaused; i++)
            {
                await Task.Delay(10);
            }

            Assert.IsTrue(audio.IsPaused);
            Assert.AreEqual(1, controller.PauseCount);
        }

        [Test]
        public async Task MediaMovedWithinItsDocumentKeepsPlaying()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio><div></div>", withLoop: true);
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            _ = audio.PlayAsync();
            document.QuerySelector("div").AppendChild(audio);
            await Task.Delay(200);
            Assert.IsFalse(audio.IsPaused);
            Assert.AreEqual(0, controller.PauseCount);
        }

        [Test]
        public async Task ReadinessOfTheControllerIsAnnouncedOnceItArrives()
        {
            var (document, _) = await LoadAsync("<audio></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var events = new List<String>();

            foreach (var name in new[] { "durationchange", "loadedmetadata", "loadeddata", "canplay", "canplaythrough" })
            {
                audio.AddEventListener(name, (s, e) => events.Add(e.Type));
            }

            audio.Source = "sound.wav";
            await WaitForControllerAsync(audio);

            for (var i = 0; i < 500 && events.Count < 5; i++)
            {
                await Task.Delay(10);
            }

            CollectionAssert.AreEqual(new[] { "durationchange", "loadedmetadata", "loadeddata", "canplay", "canplaythrough" }, events);
        }

        [Test]
        public async Task ControllerEventsReachTheElement()
        {
            var (document, _) = await LoadAsync("<audio src=sound.wav></audio>");
            var audio = document.QuerySelector<IHtmlAudioElement>("audio");
            var controller = await WaitForControllerAsync(audio);
            var events = Record(audio);
            controller.Raise("loadedmetadata");
            controller.Raise("durationchange");
            controller.Raise("canplaythrough");
            controller.Raise("timeupdate");
            CollectionAssert.AreEqual(new[] { "loadedmetadata", "durationchange", "canplaythrough", "timeupdate" }, events);
        }

        private static async Task<(IDocument, List<MockMediaController>)> LoadAsync(String html, Action<MockMediaController> setup = null, Boolean withLoop = false)
        {
            var controllers = new List<MockMediaController>();
            var service = new ResourceService<IAudioInfo>("audio/wav", response =>
            {
                var controller = new MockMediaController();
                setup?.Invoke(controller);
                controllers.Add(controller);
                return new MockAudioInfo { Source = response.Address, Controller = controller };
            });
            var config = Configuration.Default
                .WithVirtualRequester(request =>
                {
                    var path = request.Address.Path;

                    if (path.StartsWith("missing"))
                    {
                        throw new System.Net.Http.HttpRequestException("No connection.");
                    }

                    var type = path.EndsWith(".wav") ? "audio/wav" : path.EndsWith(".ogg") ? "audio/ogg" : "text/html";
                    return VirtualResponse.Create(m => m.Address(request.Address).Header(HeaderNames.ContentType, type).Content("data"));
                })
                .With(service);

            if (withLoop)
            {
                config = config.With<IEventLoop>(_ => new StandingEventLoop());
            }

            var document = await BrowsingContext.New(config).OpenAsync(m => m.Address("http://localhost/").Content(html));
            return (document, controllers);
        }

        private static async Task<MockMediaController> WaitForControllerAsync(IHtmlMediaElement element)
        {
            for (var i = 0; i < 500 && element.Controller is null; i++)
            {
                await Task.Delay(10);
            }

            return element.Controller as MockMediaController;
        }

        private static List<String> Record(IHtmlMediaElement element)
        {
            var events = new List<String>();
            var names = new[] { "play", "playing", "pause", "ended", "timeupdate", "volumechange", "loadedmetadata", "durationchange", "canplaythrough" };

            foreach (var name in names)
            {
                element.AddEventListener(name, (s, e) => events.Add(e.Type));
            }

            return events;
        }

        private static async Task WithTimeout(Task task)
        {
            if (await Task.WhenAny(task, Task.Delay(5000)) != task)
            {
                Assert.Fail("The task did not complete.");
            }

            await task;
        }

        private static async Task<DomException> CatchAsync(Task task)
        {
            try
            {
                await WithTimeout(task);
            }
            catch (DomException ex)
            {
                return ex;
            }

            Assert.Fail("The task did not fail.");
            return null;
        }

        sealed class MockAudioInfo : IAudioInfo
        {
            public Url Source { get; set; }

            public IMediaController Controller { get; set; }
        }

        sealed class MockMediaController : IMediaController
        {
            public Int32 PlayCount;
            public Int32 PauseCount;

            public DomException Refusal { get; set; }

            public MediaControllerPlaybackState State { get; set; }

            public ITimeRanges BufferedTime => null;

            public ITimeRanges SeekableTime => null;

            public ITimeRanges PlayedTime => null;

            public Double Duration => 10;

            public Double CurrentTime { get; set; }

            public Double DefaultPlaybackRate { get; set; } = 1;

            public Double PlaybackRate { get; set; } = 1;

            public Double Volume { get; set; } = 1;

            public Boolean IsMuted { get; set; }

            public Boolean IsPaused => State != MediaControllerPlaybackState.Playing;

            public MediaReadyState ReadyState => MediaReadyState.EnoughData;

            public MediaControllerPlaybackState PlaybackState => State;

            public void Play()
            {
                if (Refusal != null)
                {
                    throw Refusal;
                }

                PlayCount++;
                State = MediaControllerPlaybackState.Playing;
            }

            public void Pause()
            {
                PauseCount++;
                State = MediaControllerPlaybackState.Waiting;
            }

            public void Raise(String name)
            {
                var handler = name switch
                {
                    "playing" => Playing,
                    "ended" => Ended,
                    "pause" => Paused,
                    "loadedmetadata" => LoadedMetadata,
                    "durationchange" => DurationChanged,
                    "canplaythrough" => CanPlayThrough,
                    "timeupdate" => TimeUpdated,
                    _ => null,
                };
                handler?.Invoke(this, new Event(name));
            }

#pragma warning disable CS0067
            public event DomEventHandler Emptied;
            public event DomEventHandler LoadedMetadata;
            public event DomEventHandler LoadedData;
            public event DomEventHandler CanPlay;
            public event DomEventHandler CanPlayThrough;
            public event DomEventHandler Ended;
            public event DomEventHandler Waiting;
            public event DomEventHandler DurationChanged;
            public event DomEventHandler TimeUpdated;
            public event DomEventHandler Paused;
            public event DomEventHandler Played;
            public event DomEventHandler Playing;
            public event DomEventHandler RateChanged;
            public event DomEventHandler VolumeChanged;
#pragma warning restore CS0067
        }
    }
}

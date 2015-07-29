// Tests from https://github.com/NV/CSSOM/blob/gh-pages/spec/parse.spec.js
module.exports = [
	{
		input: "/* fuuuu */",
		result: {
			cssRules: [],
			parentStyleSheet: null
		}
	},
	{
		input: "/**/",
		result: {
			cssRules: [],
			parentStyleSheet: null
		}
	},
	{
		input: "/*a {content: '* {color:#000}'}*/",
		result: {
			cssRules: [],
			parentStyleSheet: null
		}
	},
	{
		input: "a {color: red}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "a",
						style: {
							0: "color",
							color: "red",
							__starts: 2,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 14
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: ".left {float: left;}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: ".left",
						style: {
							0: "float",
							'float': "left",
							__starts: 6,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 20
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "h1 {font-family: 'Times New Roman', Helvetica Neue, sans-serif }",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "h1",
						style: {
							0: "font-family",
							"font-family": "'Times New Roman', Helvetica Neue, sans-serif",
							__starts: 3,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 64
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "h2 {font: normal\n1.6em\r\nTimes New Roman,\tserif  ;}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "h2",
						style: {
							0: "font",
							font: "normal\n1.6em\r\nTimes New Roman,\tserif",
							__starts: 3,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 50
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "h3 {font-family: 'times new roman'} ",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "h3",
						style: {
							0: "font-family",
							'font-family': "'times new roman'",
							__starts: 3,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 35
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: ".icon>*{background-image: url(../images/ramona_strong.gif);}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: ".icon>*",
						style: {
							0: "background-image",
							"background-image": "url(../images/ramona_strong.gif)",
							__starts: 7,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 60
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "*/**/{}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "*",
						style: {
							__starts: 5,
							length: 0
						},
						parentRule: null,
						__starts: 0,
						__ends: 7
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "/**/*{}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "*",
						style: {
							__starts: 5,
							length: 0
						},
						parentRule: null,
						__starts: 4,
						__ends: 7
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "* /**/*{}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "* *",
						style: {
							__starts: 7,
							length: 0
						},
						parentRule: null,
						__starts: 0,
						__ends: 9
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "*/*/*/ *{}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "* *",
						style: {
							__starts: 8,
							length: 0
						},
						parentRule: null,
						__starts: 0,
						__ends: 10
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "#a {b:c;}\n#d {e:f}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "#a",
						style: {
							0: "b",
							b: "c",
							__starts: 3,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 9
					}, {
						selectorText: "#d",
						style: {
							0: "e",
							e: "f",
							__starts: 13,
							length: 1
						},
						parentRule: null,
						__starts: 10,
						__ends: 18
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[1].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			result.cssRules[1].style.parentRule = result.cssRules[1];
			return result;
		})()
	},
	{
		input: "* {	border:	none	} \n#foo {font-size: 12px; background:#fff;}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "*",
						style: {
							0: "border",
							border: "none",
							__starts: 2,
							length: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 18
					},
					{
						selectorText: "#foo",
						style: {
							0: "font-size",
							"font-size": "12px",
							1: "background",
							background: "#fff",
							__starts: 25,
							length: 2
						},
						parentRule: null,
						__starts: 20,
						__ends: 60
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[1].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			result.cssRules[1].style.parentRule = result.cssRules[1];
			return result;
		})()
	},
	{
		input: "span {display: inline-block !important; vertical-align: middle !important} .error{color:red!important;}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "span",
						style: {
							0: "display",
							1: "vertical-align",
							display: "inline-block",
							"vertical-align": "middle",
							__starts: 5,
							length: 2
						},
						parentRule: null,
						__starts: 0,
						__ends: 74
					},
					{
						selectorText: ".error",
						style: {
							0: "color",
							color: "red",
							__starts: 81,
							length: 1
						},
						parentRule: null,
						__starts: 75,
						__ends: 103
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[1].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			result.cssRules[1].style.parentRule = result.cssRules[1];
			return result;
		})()
	},
	{
		input: 'img:not(/*)*/[src]){background:url(data:image/png;base64,FooBar)}',
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: 'img:not([src])',
						parentRule: null,
						style: {
							0: 'background',
							background: 'url(data:image/png;base64,FooBar)',
							length: 1
						}
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: ".gradient{background: -moz-linear-gradient(/*);*/top, #1E5799 0%, #7db9e8 100%)}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: '.gradient',
						parentRule: null,
						style: {
							0: 'background',
							background: '-moz-linear-gradient(top, #1E5799 0%, #7db9e8 100%)',
							length: 1
						}
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "@media handheld, only screen and (max-device-width: 480px) {body{max-width:480px}}",
		result: (function() {
			var result = {
				cssRules: [
					{
						media: {
							0: "handheld",
							1: "only screen and (max-device-width: 480px)",
							length: 2
						},
						cssRules: [
							{
								selectorText: "body",
								style: {
									0: "max-width",
									"max-width": "480px",
									__starts: 64,
									length: 1
								},
								__starts: 60,
								__ends: 81
							}
						],
						parentRule: null,
						__starts: 0,
						__ends: 82
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		input: "@media screen, screen, screen {/* Match Firefox and Opera behavior here rather than WebKit. \nSane person shouldn't write like this anyway. */}",
		result: (function() {
			var result = {
				cssRules: [
					{
						media: {
							0: "screen",
							1: "screen",
							2: "screen",
							length: 3
						},
						cssRules: [],
						parentRule: null,
						__starts: 0,
						__ends: 142
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			return result;
		})()
	},
	{
		input: "@media/**/print {*{background:#fff}}",
		result: (function() {
			var result = {
				cssRules: [
					{
						media: {
							0: "print",
							length: 1
						},
						cssRules: [
							{
								selectorText: "*",
								style: {
									0: "background",
									background: "#fff",
									__starts: 18,
									length: 1
								},
								__starts: 17,
								__ends: 35
							}
						],
						parentRule: null,
						__starts: 0,
						__ends: 36
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0];
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];
			return result;
		})()
	},
	{
		input: "a{}@media all{b{color:#000}}",
		result: (function() {
			var result = {
				cssRules: [
					{
						selectorText: "a",
						style: {
							__starts: 1,
							length: 0
						},
						parentRule: null,
						__starts: 0,
						__ends: 3
					},
					{
						media: {
							0: "all",
							length: 1
						},
						cssRules: [
							{
								selectorText: "b",
								style: {
									0: "color",
									color: "#000",
									__starts: 15,
									length: 1
								},
								__starts: 14,
								__ends: 27
							}
						],
						parentRule: null,
						__starts: 3,
						__ends: 28
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[1].parentStyleSheet = result.cssRules[1].cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			result.cssRules[1].cssRules[0].parentRule = result.cssRules[1];
			result.cssRules[1].cssRules[0].style.parentRule = result.cssRules[1].cssRules[0];
			return result;
		})()
	},
	{
		input: "@mediaall {}",
		result: {
			cssRules: [],
			parentStyleSheet: null
		}
	},
	{
		input: "some invalid junk @media projection {body{background:black}}",
		result: (function() {
			var result = {
				cssRules: [
					{
						media: {
							0: "projection",
							length: 1
						},
						cssRules: [
							{
								selectorText: "body",
								style: {
									0: "background",
									background: "black",
									__starts: 41,
									length: 1
								},
								__starts: 37,
								__ends: 59
							}
						],
						parentRule: null,
						__starts: 18,
						__ends: 60
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0];
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];
			return result;
		})()
	},
	{
		input: '@import url(partial.css);\ni {font-style: italic}',
		result: (function() {
			var result = {
				cssRules: [
					{
						href: 'partial.css',
						media: {
							length: 0
						},
						parentRule: null,
						styleSheet: {
							cssRules: []
						}
					},
					{
						selectorText: "i",
						parentRule: null,
						style: {
							0: 'font-style',
							'font-style': 'italic',
							length: 1
						}
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].styleSheet.parentStyleSheet = result.cssRules[1].parentStyleSheet = result;
			result.cssRules[1].style.parentRule = result.cssRules[1];
			return result;
		})()
	},
	{
		input: '@import "partial.css";\ni {font-style: italic}',
		result: (function() {
			var result = {
				cssRules: [
					{
						href: 'partial.css',
						media: {
							length: 0
						},
						parentRule: null,
						styleSheet: {
							cssRules: []
						}
					},
					{
						selectorText: "i",
						parentRule: null,
						style: {
							0: 'font-style',
							'font-style': 'italic',
							length: 1
						}
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].styleSheet.parentStyleSheet = result.cssRules[1].parentStyleSheet = result;
			result.cssRules[1].style.parentRule = result.cssRules[1];
			result.cssRules[0].styleSheet.parentStyleSheet = result;
			return result;
		})()
	},
	{
		input: "@import 'partial.css';\ni {font-style: italic}",
		result: (function() {
			var result = {
				cssRules: [
					{
						href: 'partial.css',
						media: {
							length: 0
						},
						parentRule: null,
						styleSheet: {
							cssRules: []
						}
					},
					{
						selectorText: "i",
						parentRule: null,
						style: {
							0: 'font-style',
							'font-style': 'italic',
							length: 1
						}
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].styleSheet.parentStyleSheet = result.cssRules[1].parentStyleSheet = result;
			result.cssRules[1].style.parentRule = result.cssRules[1];
			return result;
		})()
	},
	{
		input: "@font-face { font-family: Delicious; font-weight: bold; src: url('Delicious-Bold.otf'); }",
		result: (function() {
			var result = {
				cssRules: [
					{
						parentRule: null,
						style: {
							0: 'font-family',
							1: 'font-weight',
							2: 'src',
							'font-family': 'Delicious',
							'font-weight': 'bold',
							'src': 'url(\'Delicious-Bold.otf\')',
							length: 3
						}
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result;
			result.cssRules[0].style.parentRule = result.cssRules[0];
			return result;
		})()
	},
	{
		// Non-vendor prefixed @keyframes rule, from Twitter Bootstrap (progress-bars):
		input: '@keyframes progress-bar-stripes {\n  from  { background-position: 0 0; }\n  to    { background-position: 40px 0; }\n}',
		result: (function () {
			var result = {
				cssRules: [
					{
						name: "progress-bar-stripes",
						_vendorPrefix: undefined,
						cssRules: [
							{
								keyText: "from",
								style: {
									0: "background-position",
									'background-position': "0 0",
									length: 1
								}
							},
							{
								keyText: "to",
								style: {
									0: "background-position",
									'background-position': "40px 0",
									length: 1
								}
							}
						],
						parentRule: null
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[1].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0].cssRules[1].parentRule = result.cssRules[0];
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];
			result.cssRules[0].cssRules[1].style.parentRule = result.cssRules[0].cssRules[1];
			return result;
		})()
	},
	{
		// @keyframes with invalid vendor prefix followed by a valid one (make sure that the RegExp.lastIndex trick works as expected):
		input: '@-moz-keyframes foo {} @--keyframes bar {} @-webkit-keyframes quux {}',
		result: (function () {
			var result = {
				cssRules: [
					{
						name: "foo",
						_vendorPrefix: "-moz-",
						cssRules: [],
						parentRule: null
					},
					{
						selectorText: "@--keyframes bar",
						style: {
							length: 0
						},
						parentRule: null,
						__starts: 0,
						__ends: 14
					},
					{
						name: "quux",
						_vendorPrefix: "-webkit-",
						cssRules: [],
						parentRule: null
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[1].parentStyleSheet = result.cssRules[2].parentStyleSheet = result;
			result.cssRules[1].style.parentRule = result.cssRules[1];
			return result;
		})()
	},
	{
		input: "@-some-ridiculously-long-vendor-prefix-that-must-be-supported-keyframes therulename /*comment*/{0%{top:0px; left:0px; background:red;}100% {top:4em; left:40px; background:maroon;}}",
		result: (function() {
			var result = {
				cssRules: [
					{
						name: "therulename",
						_vendorPrefix: '-some-ridiculously-long-vendor-prefix-that-must-be-supported-',
						cssRules: [
							{
								keyText: "0%",
								style: {
									0: "top",
									1: "left",
									2: "background",
									top: "0px",
									left: "0px",
									background: "red",
									length: 3
								}
							},
							{
								keyText: "100%",
								style: {
									0: "top",
									1: "left",
									2: "background",
									top: "4em",
									left: "40px",
									background: "maroon",
									length: 3
								}
							}
						],
						parentRule: null
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[1].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0].cssRules[1].parentRule = result.cssRules[0];
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];
			result.cssRules[0].cssRules[1].style.parentRule = result.cssRules[0].cssRules[1];
			return result;
		})()
	},
	{
		input: "@-webkit-keyframes mymove {\nfrom {top:0px}\nto {top:200px}\n}",
		result: (function() {
			var result = {
				cssRules: [
					{
						name: "mymove",
						_vendorPrefix: '-webkit-',
						cssRules: [
							{
								keyText: "from",
								style: {
									0: "top",
									top: "0px",
									length: 1
								}
							},
							{
								keyText: "to",
								style: {
									0: "top",
									top: "200px",
									length: 1
								}
							}
						],
						parentRule: null
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[1].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0].cssRules[1].parentRule = result.cssRules[0];
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];
			result.cssRules[0].cssRules[1].style.parentRule = result.cssRules[0].cssRules[1];
			return result;
		})()
	},
	{
		input: "@-webkit-keyframes mymovepercent {\n0% {top:0px;}\n50% {top:200px;}\n100% {top:300px;}}",
		result: (function() {
			var result = {
				cssRules: [
					{
						name: "mymovepercent",
						_vendorPrefix: '-webkit-',
						cssRules: [
							{
								keyText: "0%",
								style: {
									0: "top",
									top: "0px",
									length: 1
								}
							},
							{
								keyText: "50%",
								style: {
									0: "top",
									top: "200px",
									length: 1
								}
							},
							{
								keyText: "100%",
								style: {
									0: "top",
									top: "300px",
									length: 1
								}
							}
						],
						parentRule: null
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[1].parentStyleSheet = result.cssRules[0].cssRules[2].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0].cssRules[1].parentRule = result.cssRules[0].cssRules[2].parentRule = result.cssRules[0];
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];
			result.cssRules[0].cssRules[1].style.parentRule = result.cssRules[0].cssRules[1];
			result.cssRules[0].cssRules[2].style.parentRule = result.cssRules[0].cssRules[2];
			return result;
		})()
	},
	{
		input: "@-moz-document url(http://www.w3.org/), url-prefix(http://www.w3.org/Style/), domain(mozilla.org), regexp(\"https:.*\")\n{\n/*comments*/\nbody { color: purple; background: yellow; }\n}",
		result: (function() {
			var result = {
				cssRules: [
					{
						matcher: {
							0: "url(http://www.w3.org/)",
							1: "url-prefix(http://www.w3.org/Style/)",
							2: "domain(mozilla.org)",
							3: "regexp(\"https:.*\")",
							length: 4
						},
						cssRules: [
							{
								selectorText: "body",
								style: {
									0: "color",
									1: "background",
									length: 2,
									__starts: 138,
									color: "purple",
									background: "yellow"
								},
								__starts: 133,
								__ends: 176
							}
						],
						parentRule: null,
						__starts: 0,
						__ends: 178
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[0].cssRules[0].parentStyleSheet = result;
			result.cssRules[0].cssRules[0].parentRule = result.cssRules[0];
			result.cssRules[0].cssRules[0].style.parentRule = result.cssRules[0].cssRules[0];

			return result;
		})()
	},
	{
		input: "a{}@-moz-document/**/url-prefix(http://www.w3.org/Style/){body { color: purple; background: yellow; }}",
		result: (function(){
			var result = {
				cssRules: [
					{
						selectorText: 'a',
						style: {
							length: 0,
							__starts: 1
						},
						parentRule: null,
						__starts: 0,
						__ends: 3
					},
					{
						matcher: {
							0: "url-prefix(http://www.w3.org/Style/)",
							length: 1
						},
						cssRules: [
							{
								selectorText: "body",
								style: {
									0: "color",
									1: "background",
									length: 2,
									__starts: 64,
									color: "purple",
									background: "yellow"
								},
								__starts: 59,
								__ends: 102
							}
						],
						parentRule: null,
						__starts: 3,
						__ends: 103
					}
				],
				parentStyleSheet: null
			};
			result.cssRules[0].parentStyleSheet = result.cssRules[1].parentStyleSheet = result.cssRules[1].cssRules[0].parentStyleSheet = result;
			result.cssRules[1].cssRules[0].parentRule = result.cssRules[1];
			result.cssRules[1].cssRules[0].style.parentRule = result.cssRules[1].cssRules[0];
			result.cssRules[0].style.parentRule = result.cssRules[0];

			return result;
		})()
	}
];
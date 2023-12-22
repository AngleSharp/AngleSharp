#nullable enable
namespace AngleSharp.Benchmarks;

public class StaticHtml
{
    public const string HtmlTable = """
                                    <table>
                                        <tr>
                                            <th>Company</th>
                                            <th>Contact</th>
                                            <th>Country</th>
                                        </tr>
                                        <tr>
                                            <td>Alfreds Futterkiste</td>
                                            <td>Maria Anders</td>
                                            <td>Germany</td>
                                        </tr>
                                        <tr>
                                            <td>Centro comercial Moctezuma</td>
                                            <td>Francisco Chang</td>
                                            <td>Mexico</td>
                                        </tr>
                                    </table>
                                    """;

    public const string HtmlTableTabbed = """
                                                                                                                      <table>
                                                                                                                          <tr>
                                                                                                                              <th>Company</th>
                                                                                                                              <th>Contact</th>
                                                                                                                              <th>Country</th>
                                                                                                                          </tr>
                                                                                                                          <tr>
                                                                                                                              <td>Alfreds Futterkiste</td>
                                                                                                                              <td>Maria Anders</td>
                                                                                                                              <td>Germany</td>
                                                                                                                          </tr>
                                                                                                                          <tr>
                                                                                                                              <td>Centro comercial Moctezuma</td>
                                                                                                                              <td>Francisco Chang</td>
                                                                                                                              <td>Mexico</td>
                                                                                                                          </tr>
                                                                                                                      </table>
                                          """;


        public const string HtmlTableTabbedSoMuch = """
                                                                                                                                                                                                                                                                              <table>
                                                                                                                                                                                                                                                                                  <tr>
                                                                                                                                                                                                                                                                                      <th>Company</th>
                                                                                                                                                                                                                                                                                      <th>Contact</th>
                                                                                                                                                                                                                                                                                      <th>Country</th>
                                                                                                                                                                                                                                                                                  </tr>
                                                                                                                                                                                                                                                                                  <tr>
                                                                                                                                                                                                                                                                                      <td>Alfreds Futterkiste</td>
                                                                                                                                                                                                                                                                                      <td>Maria Anders</td>
                                                                                                                                                                                                                                                                                      <td>Germany</td>
                                                                                                                                                                                                                                                                                  </tr>
                                                                                                                                                                                                                                                                                  <tr>
                                                                                                                                                                                                                                                                                      <td>Centro comercial Moctezuma</td>
                                                                                                                                                                                                                                                                                      <td>Francisco Chang</td>
                                                                                                                                                                                                                                                                                      <td>Mexico</td>
                                                                                                                                                                                                                                                                                  </tr>
                                                                                                                                                                                                                                                                              </table>
                                          """;

    public const string Github =
        """







        <!DOCTYPE html>
        <html lang="en" data-color-mode="auto" data-light-theme="light" data-dark-theme="dark"  data-a11y-animated-images="system" data-a11y-link-underlines="true">




          <head>
            <meta charset="utf-8">
          <link rel="dns-prefetch" href="https://github.githubassets.com">
          <link rel="dns-prefetch" href="https://avatars.githubusercontent.com">
          <link rel="dns-prefetch" href="https://github-cloud.s3.amazonaws.com">
          <link rel="dns-prefetch" href="https://user-images.githubusercontent.com/">
          <link rel="preconnect" href="https://github.githubassets.com" crossorigin>
          <link rel="preconnect" href="https://avatars.githubusercontent.com">



          <link crossorigin="anonymous" media="all" rel="stylesheet" href="https://github.githubassets.com/assets/light-38f1bf52eeeb.css" /><link crossorigin="anonymous" media="all" rel="stylesheet" href="https://github.githubassets.com/assets/dark-56010aa53a8f.css" /><link data-color-theme="dark_dimmed" crossorigin="anonymous" media="all" rel="stylesheet" data-href="https://github.githubassets.com/assets/dark_dimmed-b2e1b478d5b4.css" /><link data-color-theme="dark_high_contrast" crossorigin="anonymous" media="all" rel="stylesheet" data-href="https://github.githubassets.com/assets/dark_high_contrast-e7f12ffa82f3.css" /><link data-color-theme="dark_colorblind" crossorigin="anonymous" media="all" rel="stylesheet" data-href="https://github.githubassets.com/assets/dark_colorblind-ddca79c20026.css" /><link data-color-theme="light_colorblind" crossorigin="anonymous" media="all" rel="stylesheet" data-href="https://github.githubassets.com/assets/light_colorblind-8017b9c4037b.css" /><link data-color-theme="light_high_contrast" crossorigin="anonymous" media="all" rel="stylesheet" data-href="https://github.githubassets.com/assets/light_high_contrast-3ce2d3d8a4d3.css" /><link data-color-theme="light_tritanopia" crossorigin="anonymous" media="all" rel="stylesheet" data-href="https://github.githubassets.com/assets/light_tritanopia-02059c141ad5.css" /><link data-color-theme="dark_tritanopia" crossorigin="anonymous" media="all" rel="stylesheet" data-href="https://github.githubassets.com/assets/dark_tritanopia-870ee47909bf.css" />
            <link crossorigin="anonymous" media="all" rel="stylesheet" href="https://github.githubassets.com/assets/primer-primitives-971c6be3ec9f.css" />
            <link crossorigin="anonymous" media="all" rel="stylesheet" href="https://github.githubassets.com/assets/primer-fb122a21966c.css" />
            <link crossorigin="anonymous" media="all" rel="stylesheet" href="https://github.githubassets.com/assets/global-81fb22f50318.css" />
            <link crossorigin="anonymous" media="all" rel="stylesheet" href="https://github.githubassets.com/assets/github-9ed33716809f.css" />
          <link crossorigin="anonymous" media="all" rel="stylesheet" href="https://github.githubassets.com/assets/repository-2e5f0ce2e282.css" />



          <script type="application/json" id="client-env">{"locale":"en","featureFlags":["copilot_conversational_ux_streaming","failbot_handle_non_errors","geojson_azure_maps","image_metric_tracking","repository_suggester_elastic_search","turbo_experiment_risky","sample_network_conn_type","no_character_key_shortcuts_in_inputs"]}</script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/wp-runtime-5efffdf015ba.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_dompurify_dist_purify_js-6890e890956f.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_stacktrace-parser_dist_stack-trace-parser_esm_js-node_modules_github_bro-a4c183-79f9611c275b.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_hydro-analytics-client_dist_analytics-client_js-node_modules_gith-6a10dd-8837a7c17569.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/ui_packages_soft-nav_soft-nav_ts-6a5fadd2ef71.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/environment-599072e1b80d.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_selector-observer_dist_index_esm_js-9f960d9b217c.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_primer_behaviors_dist_esm_focus-zone_js-26c21c341c6b.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_relative-time-element_dist_index_js-c6fd49e3fd28.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_fzy_js_index_js-node_modules_github_combobox-nav_dist_index_js-node_modu-344bff-421f7a8c1008.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_delegated-events_dist_index_js-node_modules_github_details-dialog-elemen-29dc30-a2a71f11a507.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_filter-input-element_dist_index_js-node_modules_github_remote-inp-59c459-d0c49521eb35.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_file-attachment-element_dist_index_js-node_modules_primer_view-co-eb424d-2c2d25f8d174.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/github-elements-dfbd749920b3.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/element-registry-d3bbcbf532d5.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_catalyst_lib_index_js-node_modules_github_hydro-analytics-client_-978abc0-15861e0630b6.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_lit-html_lit-html_js-5b376145beff.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_mini-throttle_dist_index_js-node_modules_github_alive-client_dist-bf5aa2-1b562c29ab8e.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_morphdom_dist_morphdom-esm_js-5bff297a06de.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_turbo_dist_turbo_es2017-esm_js-c91f4ad18b62.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_color-convert_index_js-72c9fbde5ad4.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_remote-form_dist_index_js-node_modules_scroll-anchoring_dist_scro-231ccf-aa129238d13b.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_session-resume_dist_index_js-node_modules_primer_behaviors_dist_e-ac74c6-c3eb71941f78.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_primer_behaviors_dist_esm_dimensions_js-node_modules_github_jtml_lib_index_js-95b84ee6bc34.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_paste-markdown_dist_index_esm_js-node_modules_github_quote-select-618d6c-59676cf880fb.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/app_assets_modules_github_updatable-content_ts-c49b3c18a21f.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/app_assets_modules_github_behaviors_task-list_ts-app_assets_modules_github_onfocus_ts-app_ass-079b43-f06ea5f0a52c.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/app_assets_modules_github_sticky-scroll-into-view_ts-b452ca178b7e.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/app_assets_modules_github_behaviors_ajax-error_ts-app_assets_modules_github_behaviors_include-2e2258-178d980b559e.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/app_assets_modules_github_behaviors_commenting_edit_ts-app_assets_modules_github_behaviors_ht-83c235-b85e9f4f1304.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/behaviors-012abd5da1e4.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_delegated-events_dist_index_js-node_modules_github_catalyst_lib_index_js-d0256ebff5cd.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/notifications-global-99d196517b1b.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_mini-throttle_dist_index_js-node_modules_primer_behaviors_dist_es-d96db1-cf664ff86616.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/issues-983da490775c.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/structured-issues-03cd9b0f51e3.js"></script>


          <title>Utf8JsonReader/JsonStreamReader: Add reading values as Stream · Issue #30402 · dotnet/runtime</title>



          <meta name="route-pattern" content="/_view_fragments/issues/show/:user_id/:repository/:id/issue_layout(.:format)">


          <meta name="current-catalog-service-hash" content="81bb79d38c15960b92d99bca9288a9108c7a47b18f2423d0f6438c5b7bcd2114">


          <meta name="request-id" content="F9BC:1A17BE:17B31A29:18001745:657F4DF8" data-turbo-transient="true" /><meta name="html-safe-nonce" content="4a539651034da3780a9cbe1983ef7ffc2f2a5754948dd219d657ebf5f8ad3239" data-turbo-transient="true" /><meta name="visitor-payload" content="eyJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LmJpbmcuY29tLyIsInJlcXVlc3RfaWQiOiJGOUJDOjFBMTdCRToxN0IzMUEyOToxODAwMTc0NTo2NTdGNERGOCIsInZpc2l0b3JfaWQiOiIzOTY5OTgyOTQ1MDYwOTExMzYyIiwicmVnaW9uX2VkZ2UiOiJmcmEiLCJyZWdpb25fcmVuZGVyIjoiaWFkIn0=" data-turbo-transient="true" /><meta name="visitor-hmac" content="b0449eebbc49a26f1e91e125805dbaa299cbc6022dcbaa3e76f4e8a29f2a9547" data-turbo-transient="true" />


            <meta name="hovercard-subject-tag" content="issue:558472377" data-turbo-transient>


          <meta name="github-keyboard-shortcuts" content="repository,issues" data-turbo-transient="true" />


          <meta name="selected-link" value="repo_issues" data-turbo-transient>
          <link rel="assets" href="https://github.githubassets.com/">

            <meta name="google-site-verification" content="c1kuD-K2HIVF635lypcsWPoD4kilo5-jA_wBFyT4uMY">
          <meta name="google-site-verification" content="KT5gs8h0wvaagLKAVWq8bbeNwnZZK1r1XQysX3xurLU">
          <meta name="google-site-verification" content="ZzhVyEFwb7w3e0-uOTltm8Jsck2F5StVihD0exw2fsA">
          <meta name="google-site-verification" content="GXs5KoUUkNCoaAZn7wPN-t01Pywp9M3sEjnt_3_ZWPc">
          <meta name="google-site-verification" content="Apib7-x98H0j5cPqHWwSMm6dNU4GmODRoqxLiDzdx9I">

        <meta name="octolytics-url" content="https://collector.github.com/github/collect" /><meta name="octolytics-actor-id" content="1162823" /><meta name="octolytics-actor-login" content="dv00d00" /><meta name="octolytics-actor-hash" content="1ba485967bbad1838cdb050e1b1517ade778bcff66869ac717d8a519490d088d" />

          <meta name="analytics-location" content="/&lt;user-name&gt;/&lt;repo-name&gt;/voltron/issues_fragments/issue_layout" data-turbo-transient="true" />








            <meta name="user-login" content="dv00d00">

          <link rel="sudo-modal" href="/sessions/sudo_modal">

            <meta name="viewport" content="width=device-width">

              <meta name="description" content="Consider the following (real) scenario: a web service returns a binary content (file) of arbitrary length (up to hundreds of MB) as a base64-encoded value of a JSON property of a JSON document. With current implementation I have to read ...">
              <link rel="search" type="application/opensearchdescription+xml" href="/opensearch.xml" title="GitHub">
            <link rel="fluid-icon" href="https://github.com/fluidicon.png" title="GitHub">
            <meta property="fb:app_id" content="1401488693436528">
            <meta name="apple-itunes-app" content="app-id=1477376905, app-argument=https://github.com/_view_fragments/issues/show/dotnet/runtime/30402/issue_layout" />
              <meta name="twitter:image:src" content="https://opengraph.githubassets.com/76e298fed4356a7044a84c30806e2b80d9a3fc494d8633b6b714b342698123bf/dotnet/runtime/issues/30402" /><meta name="twitter:site" content="@github" /><meta name="twitter:card" content="summary_large_image" /><meta name="twitter:title" content="Utf8JsonReader/JsonStreamReader: Add reading values as Stream · Issue #30402 · dotnet/runtime" /><meta name="twitter:description" content="Consider the following (real) scenario: a web service returns a binary content (file) of arbitrary length (up to hundreds of MB) as a base64-encoded value of a JSON property of a JSON document. Wit..." />
              <meta property="og:image" content="https://opengraph.githubassets.com/76e298fed4356a7044a84c30806e2b80d9a3fc494d8633b6b714b342698123bf/dotnet/runtime/issues/30402" /><meta property="og:image:alt" content="Consider the following (real) scenario: a web service returns a binary content (file) of arbitrary length (up to hundreds of MB) as a base64-encoded value of a JSON property of a JSON document. Wit..." /><meta property="og:image:width" content="1200" /><meta property="og:image:height" content="600" /><meta property="og:site_name" content="GitHub" /><meta property="og:type" content="object" /><meta property="og:title" content="Utf8JsonReader/JsonStreamReader: Add reading values as Stream · Issue #30402 · dotnet/runtime" /><meta property="og:url" content="https://github.com/dotnet/runtime/issues/30402" /><meta property="og:description" content="Consider the following (real) scenario: a web service returns a binary content (file) of arbitrary length (up to hundreds of MB) as a base64-encoded value of a JSON property of a JSON document. Wit..." /><meta property="og:author:username" content="andriysavin" />


              <link rel="shared-web-socket" href="wss://alive.github.com/_sockets/u/1162823/ws?session=eyJ2IjoiVjMiLCJ1IjoxMTYyODIzLCJzIjoxMjQ3MTYyMDA1LCJjIjo2ODgyMTA0NzcsInQiOjE3MDI4NDE4NDh9--008b4091f314393175fe17bb7665fa6e18325e184c68a8e3d2a86dc3f2cfc7ee" data-refresh-url="/_alive" data-session-id="6f8dfc6f177f4bef614e992d3a3692b8df139291f8dabbcca774338dd66b4d1a">
              <link rel="shared-web-socket-src" href="/assets-cdn/worker/socket-worker-9cc1149b224c.js">


                <meta name="hostname" content="github.com">


              <meta name="keyboard-shortcuts-preference" content="all">

                <meta name="expected-hostname" content="github.com">


          <meta http-equiv="x-pjax-version" content="fda05fae463eafa33a74c3fd04e4712f7c9cefeff126176f559dcd1c88f552ad" data-turbo-track="reload">
          <meta http-equiv="x-pjax-csp-version" content="611e3beaf6df2ba8f98070845c8e5ef70f0ffc535519af6685e8341fcd41c235" data-turbo-track="reload">
          <meta http-equiv="x-pjax-css-version" content="7777559fa4a547914da31e84695e266e6aa534a61e3fe235e241f340ef249c40" data-turbo-track="reload">
          <meta http-equiv="x-pjax-js-version" content="ce1e0dd104042430cbc81c3d41f32abc84b780e41089f570c5996beddf65ffc7" data-turbo-track="reload">

          <meta name="turbo-cache-control" content="no-preview" data-turbo-transient="">

                <meta name="voltron-timing" value="713">

          <meta name="go-import" content="github.com/dotnet/runtime git https://github.com/dotnet/runtime.git">

          <meta name="octolytics-dimension-user_id" content="9141961" /><meta name="octolytics-dimension-user_login" content="dotnet" /><meta name="octolytics-dimension-repository_id" content="210716005" /><meta name="octolytics-dimension-repository_nwo" content="dotnet/runtime" /><meta name="octolytics-dimension-repository_public" content="true" /><meta name="octolytics-dimension-repository_is_fork" content="false" /><meta name="octolytics-dimension-repository_network_root_id" content="210716005" /><meta name="octolytics-dimension-repository_network_root_nwo" content="dotnet/runtime" />



          <meta name="turbo-body-classes" content="logged-in env-production page-responsive">


          <meta name="browser-stats-url" content="https://api.github.com/_private/browser/stats">

          <meta name="browser-errors-url" content="https://api.github.com/_private/browser/errors">

          <meta name="browser-optimizely-client-errors-url" content="https://api.github.com/_private/browser/optimizely_client/errors">

          <link rel="mask-icon" href="https://github.githubassets.com/assets/pinned-octocat-093da3e6fa40.svg" color="#000000">
          <link rel="alternate icon" class="js-site-favicon" type="image/png" href="https://github.githubassets.com/favicons/favicon.png">
          <link rel="icon" class="js-site-favicon" type="image/svg+xml" href="https://github.githubassets.com/favicons/favicon.svg">

        <meta name="theme-color" content="#1e2327">
        <meta name="color-scheme" content="light dark" />

          <meta name="msapplication-TileImage" content="/windows-tile.png">
          <meta name="msapplication-TileColor" content="#ffffff">

          <link rel="manifest" href="/manifest.json" crossOrigin="use-credentials">

          </head>

          <body class="logged-in env-production page-responsive" style="word-wrap: break-word;">
            <div data-turbo-body class="logged-in env-production page-responsive" style="word-wrap: break-word;">



            <div class="position-relative js-header-wrapper ">
              <a href="#start-of-content" class="p-3 color-bg-accent-emphasis color-fg-on-emphasis show-on-focus js-skip-to-content">Skip to content</a>
              <span data-view-component="true" class="progress-pjax-loader Progress position-fixed width-full">
            <span style="width: 0%;" data-view-component="true" class="Progress-item progress-pjax-loader-bar left-0 top-0 color-bg-accent-emphasis"></span>
        </span>

          <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/react-lib-1fbfc5be2c18.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_primer_octicons-react_dist_index_esm_js-node_modules_primer_react_lib-es-2e8e7c-8c382c96424c.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_primer_react_lib-esm_Box_Box_js-ebfceb11fb57.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_primer_react_lib-esm_Button_Button_js-8dba6638f78f.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_primer_react_lib-esm_ActionList_index_js-64637eb4b092.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_primer_react_lib-esm_Button_IconButton_js-node_modules_primer_react_lib--23bcad-842c4ce949ee.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/ui_packages_react-core_create-browser-history_ts-ui_packages_react-core_deferred-registry_ts--ebbb92-f862877dad23.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/keyboard-shortcuts-dialog-9214edae6316.js"></script>

        <react-partial
          partial-name="keyboard-shortcuts-dialog"
          data-ssr="false"
        >

          <script type="application/json" data-target="react-partial.embeddedData">{"props":{}}</script>
          <div data-target="react-partial.reactRoot"></div>
        </react-partial>





                <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_allex_crc32_lib_crc32_esm_js-node_modules_github_mini-throttle_dist_deco-b38cad-748e74df23ce.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_github_clipboard-copy-element_dist_index_esm_js-node_modules_delegated-e-b37f7d-2f24d321a3fb.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/app_assets_modules_github_command-palette_items_help-item_ts-app_assets_modules_github_comman-48ad9d-8e2f5c99dc7d.js"></script>
        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/command-palette-bc4e74ff360d.js"></script>

                    <header class="AppHeader">


            <div class="AppHeader-globalBar pb-2 js-global-bar">
              <div class="AppHeader-globalBar-start">
                  <deferred-side-panel data-url="/_side-panels/global">
          <include-fragment data-target="deferred-side-panel.fragment">
              <button aria-label="Open global navigation menu" data-action="click:deferred-side-panel#loadPanel click:deferred-side-panel#panelOpened" data-show-dialog-id="dialog-5ba1af6b-83c1-4efd-8d48-a69ca0c1990b" id="dialog-show-dialog-5ba1af6b-83c1-4efd-8d48-a69ca0c1990b" type="button" data-view-component="true" class="Button Button--iconOnly Button--secondary Button--medium AppHeader-button color-bg-transparent p-0 color-fg-muted">  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-three-bars Button-visual">
            <path d="M1 2.75A.75.75 0 0 1 1.75 2h12.5a.75.75 0 0 1 0 1.5H1.75A.75.75 0 0 1 1 2.75Zm0 5A.75.75 0 0 1 1.75 7h12.5a.75.75 0 0 1 0 1.5H1.75A.75.75 0 0 1 1 7.75ZM1.75 12h12.5a.75.75 0 0 1 0 1.5H1.75a.75.75 0 0 1 0-1.5Z"></path>
        </svg>
        </button>

        <div class="Overlay--hidden Overlay-backdrop--side Overlay-backdrop--placement-left" data-modal-dialog-overlay>
          <modal-dialog data-target="deferred-side-panel.panel" role="dialog" id="dialog-5ba1af6b-83c1-4efd-8d48-a69ca0c1990b" aria-modal="true" aria-disabled="true" aria-labelledby="dialog-5ba1af6b-83c1-4efd-8d48-a69ca0c1990b-title" aria-describedby="dialog-5ba1af6b-83c1-4efd-8d48-a69ca0c1990b-description" data-view-component="true" class="Overlay Overlay-whenNarrow Overlay--size-small-portrait Overlay--motion-scaleFade SidePanel">
            <div styles="flex-direction: row;" data-view-component="true" class="Overlay-header">
          <div class="Overlay-headerContentWrap">
            <div class="Overlay-titleWrap">
              <h1 class="Overlay-title sr-only" id="dialog-5ba1af6b-83c1-4efd-8d48-a69ca0c1990b-title">
                Global navigation
              </h1>
                    <div data-view-component="true" class="d-flex">
              <div data-view-component="true" class="AppHeader-logo position-relative">
                <svg aria-hidden="true" height="24" viewBox="0 0 16 16" version="1.1" width="24" data-view-component="true" class="octicon octicon-mark-github">
            <path d="M8 0c4.42 0 8 3.58 8 8a8.013 8.013 0 0 1-5.45 7.59c-.4.08-.55-.17-.55-.38 0-.27.01-1.13.01-2.2 0-.75-.25-1.23-.54-1.48 1.78-.2 3.65-.88 3.65-3.95 0-.88-.31-1.59-.82-2.15.08-.2.36-1.02-.08-2.12 0 0-.67-.22-2.2.82-.64-.18-1.32-.27-2-.27-.68 0-1.36.09-2 .27-1.53-1.03-2.2-.82-2.2-.82-.44 1.1-.16 1.92-.08 2.12-.51.56-.82 1.28-.82 2.15 0 3.06 1.86 3.75 3.64 3.95-.23.2-.44.55-.51 1.07-.46.21-1.61.55-2.33-.66-.15-.24-.6-.83-1.23-.82-.67.01-.27.38.01.53.34.19.73.9.82 1.13.16.45.68 1.31 2.69.94 0 .67.01 1.3.01 1.49 0 .21-.15.45-.55.38A7.995 7.995 0 0 1 0 8c0-4.42 3.58-8 8-8Z"></path>
        </svg>
        </div></div>
            </div>
            <div class="Overlay-actionWrap">
              <button data-close-dialog-id="dialog-5ba1af6b-83c1-4efd-8d48-a69ca0c1990b" aria-label="Close" type="button" data-view-component="true" class="close-button Overlay-closeButton"><svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg></button>
            </div>
          </div>
        </div>
              <div data-view-component="true" class="Overlay-body d-flex flex-column px-2">    <div data-view-component="true" class="d-flex flex-column mb-3">
                <nav aria-label="Site navigation" data-view-component="true" class="ActionList">

          <nav-list>
            <ul data-view-component="true" class="ActionListWrap">


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-hotkey="g d" data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;HOME&quot;,&quot;label&quot;:null}" id="item-7af7c235-262d-4a20-8f0d-e482a32710e8" href="/dashboard" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-home">
            <path d="M6.906.664a1.749 1.749 0 0 1 2.187 0l5.25 4.2c.415.332.657.835.657 1.367v7.019A1.75 1.75 0 0 1 13.25 15h-3.5a.75.75 0 0 1-.75-.75V9H7v5.25a.75.75 0 0 1-.75.75h-3.5A1.75 1.75 0 0 1 1 13.25V6.23c0-.531.242-1.034.657-1.366l5.25-4.2Zm1.25 1.171a.25.25 0 0 0-.312 0l-5.25 4.2a.25.25 0 0 0-.094.196v7.019c0 .138.112.25.25.25H5.5V8.25a.75.75 0 0 1 .75-.75h3.5a.75.75 0 0 1 .75.75v5.25h2.75a.25.25 0 0 0 .25-.25V6.23a.25.25 0 0 0-.094-.195Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Home
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-hotkey="g i" data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;ISSUES&quot;,&quot;label&quot;:null}" id="item-7e4137ba-697a-4a9e-835b-f9be8ff2f336" href="/issues" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-issue-opened">
            <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path><path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Issues
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-hotkey="g p" data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;PULL_REQUESTS&quot;,&quot;label&quot;:null}" id="item-c49e4d32-3386-4cfa-9fd6-fd134ac2720d" href="/pulls" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-git-pull-request">
            <path d="M1.5 3.25a2.25 2.25 0 1 1 3 2.122v5.256a2.251 2.251 0 1 1-1.5 0V5.372A2.25 2.25 0 0 1 1.5 3.25Zm5.677-.177L9.573.677A.25.25 0 0 1 10 .854V2.5h1A2.5 2.5 0 0 1 13.5 5v5.628a2.251 2.251 0 1 1-1.5 0V5a1 1 0 0 0-1-1h-1v1.646a.25.25 0 0 1-.427.177L7.177 3.427a.25.25 0 0 1 0-.354ZM3.75 2.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm0 9.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm8.25.75a.75.75 0 1 0 1.5 0 .75.75 0 0 0-1.5 0Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Pull requests
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-item-id="projects" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;PROJECTS&quot;,&quot;label&quot;:null}" id="item-e7bcf416-a792-4717-9f4d-e790d1b92f75" href="/projects" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-table">
            <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25ZM6.5 6.5v8h7.75a.25.25 0 0 0 .25-.25V6.5Zm8-1.5V1.75a.25.25 0 0 0-.25-.25H6.5V5Zm-13 1.5v7.75c0 .138.112.25.25.25H5v-8ZM5 5V1.5H1.75a.25.25 0 0 0-.25.25V5Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Projects
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;DISCUSSIONS&quot;,&quot;label&quot;:null}" id="item-e3041abd-f806-40d3-9d4d-03a0aeaac57a" href="/discussions" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-comment-discussion">
            <path d="M1.75 1h8.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 10.25 10H7.061l-2.574 2.573A1.458 1.458 0 0 1 2 11.543V10h-.25A1.75 1.75 0 0 1 0 8.25v-5.5C0 1.784.784 1 1.75 1ZM1.5 2.75v5.5c0 .138.112.25.25.25h1a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h3.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25Zm13 2a.25.25 0 0 0-.25-.25h-.5a.75.75 0 0 1 0-1.5h.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 14.25 12H14v1.543a1.458 1.458 0 0 1-2.487 1.03L9.22 12.28a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215l2.22 2.22v-2.19a.75.75 0 0 1 .75-.75h1a.25.25 0 0 0 .25-.25Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Discussions
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;CODESPACES&quot;,&quot;label&quot;:null}" id="item-646562e5-4c8e-4ba5-a8a7-7dbdb8f9b768" href="https://github.com/codespaces" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-codespaces">
            <path d="M0 11.25c0-.966.784-1.75 1.75-1.75h12.5c.966 0 1.75.784 1.75 1.75v3A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm2-9.5C2 .784 2.784 0 3.75 0h8.5C13.216 0 14 .784 14 1.75v5a1.75 1.75 0 0 1-1.75 1.75h-8.5A1.75 1.75 0 0 1 2 6.75Zm1.75-.25a.25.25 0 0 0-.25.25v5c0 .138.112.25.25.25h8.5a.25.25 0 0 0 .25-.25v-5a.25.25 0 0 0-.25-.25Zm-2 9.5a.25.25 0 0 0-.25.25v3c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25v-3a.25.25 0 0 0-.25-.25Z"></path><path d="M7 12.75a.75.75 0 0 1 .75-.75h4.5a.75.75 0 0 1 0 1.5h-4.5a.75.75 0 0 1-.75-.75Zm-4 0a.75.75 0 0 1 .75-.75h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1-.75-.75Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Codespaces
        </span></a>


        </li>


                  <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;EXPLORE&quot;,&quot;label&quot;:null}" id="item-bcdc9bb7-9286-4822-8485-ca4ecab04b44" href="/explore" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-telescope">
            <path d="M14.184 1.143v-.001l1.422 2.464a1.75 1.75 0 0 1-.757 2.451L3.104 11.713a1.75 1.75 0 0 1-2.275-.702l-.447-.775a1.75 1.75 0 0 1 .53-2.32L11.682.573a1.748 1.748 0 0 1 2.502.57Zm-4.709 9.32h-.001l2.644 3.863a.75.75 0 1 1-1.238.848l-1.881-2.75v2.826a.75.75 0 0 1-1.5 0v-2.826l-1.881 2.75a.75.75 0 1 1-1.238-.848l2.049-2.992a.746.746 0 0 1 .293-.253l1.809-.87a.749.749 0 0 1 .944.252ZM9.436 3.92h-.001l-4.97 3.39.942 1.63 5.42-2.61Zm3.091-2.108h.001l-1.85 1.26 1.505 2.605 2.016-.97a.247.247 0 0 0 .13-.151.247.247 0 0 0-.022-.199l-1.422-2.464a.253.253 0 0 0-.161-.119.254.254 0 0 0-.197.038ZM1.756 9.157a.25.25 0 0 0-.075.33l.447.775a.25.25 0 0 0 .325.1l1.598-.769-.83-1.436-1.465 1Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Explore
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;MARKETPLACE&quot;,&quot;label&quot;:null}" id="item-d43ea972-0d96-4104-b7b3-d44a94ef55fc" href="/marketplace" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-gift">
            <path d="M2 2.75A2.75 2.75 0 0 1 4.75 0c.983 0 1.873.42 2.57 1.232.268.318.497.668.68 1.042.183-.375.411-.725.68-1.044C9.376.42 10.266 0 11.25 0a2.75 2.75 0 0 1 2.45 4h.55c.966 0 1.75.784 1.75 1.75v2c0 .698-.409 1.301-1 1.582v4.918A1.75 1.75 0 0 1 13.25 16H2.75A1.75 1.75 0 0 1 1 14.25V9.332C.409 9.05 0 8.448 0 7.75v-2C0 4.784.784 4 1.75 4h.55c-.192-.375-.3-.8-.3-1.25ZM7.25 9.5H2.5v4.75c0 .138.112.25.25.25h4.5Zm1.5 0v5h4.5a.25.25 0 0 0 .25-.25V9.5Zm0-4V8h5.5a.25.25 0 0 0 .25-.25v-2a.25.25 0 0 0-.25-.25Zm-7 0a.25.25 0 0 0-.25.25v2c0 .138.112.25.25.25h5.5V5.5h-5.5Zm3-4a1.25 1.25 0 0 0 0 2.5h2.309c-.233-.818-.542-1.401-.878-1.793-.43-.502-.915-.707-1.431-.707ZM8.941 4h2.309a1.25 1.25 0 0 0 0-2.5c-.516 0-1 .205-1.43.707-.337.392-.646.975-.879 1.793Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Marketplace
        </span></a>


        </li>

        </ul>  </nav-list>
        </nav>

                <div data-view-component="true" class="my-3 d-flex flex-justify-center height-full">
                  <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="16" height="16" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
        </div>
        </div>
              <div data-view-component="true" class="flex-1"></div>


              <div data-view-component="true" class="px-2">      <p class="color-fg-subtle text-small text-light">&copy; 2023 GitHub, Inc.</p>

              <div data-view-component="true" class="d-flex text-small text-light">
                  <a target="_blank" href="/about" data-view-component="true" class="Link mr-2">About</a>
                  <a target="_blank" href="https://github.blog" data-view-component="true" class="Link mr-2">Blog</a>
                  <a target="_blank" href="https://docs.github.com/site-policy/github-terms/github-terms-of-service" data-view-component="true" class="Link mr-2">Terms</a>
                  <a target="_blank" href="https://docs.github.com/site-policy/privacy-policies/github-privacy-statement" data-view-component="true" class="Link mr-2">Privacy</a>
                  <a target="_blank" href="/security" data-view-component="true" class="Link mr-2">Security</a>
                <a target="_blank" href="https://www.githubstatus.com/" data-view-component="true" class="Link mr-3">Status</a>
        </div></div>
        </div>

        </modal-dialog></div>

          </include-fragment>
        </deferred-side-panel>

                <a
                  class="AppHeader-logo ml-2"
                  href="https://github.com/"
                  data-hotkey="g d"
                  aria-label="Homepage "
                  data-turbo="false"
                  data-analytics-event="{&quot;category&quot;:&quot;Header&quot;,&quot;action&quot;:&quot;go to dashboard&quot;,&quot;label&quot;:&quot;icon:logo&quot;}"
                >
                  <svg height="32" aria-hidden="true" viewBox="0 0 16 16" version="1.1" width="32" data-view-component="true" class="octicon octicon-mark-github v-align-middle color-fg-default">
            <path d="M8 0c4.42 0 8 3.58 8 8a8.013 8.013 0 0 1-5.45 7.59c-.4.08-.55-.17-.55-.38 0-.27.01-1.13.01-2.2 0-.75-.25-1.23-.54-1.48 1.78-.2 3.65-.88 3.65-3.95 0-.88-.31-1.59-.82-2.15.08-.2.36-1.02-.08-2.12 0 0-.67-.22-2.2.82-.64-.18-1.32-.27-2-.27-.68 0-1.36.09-2 .27-1.53-1.03-2.2-.82-2.2-.82-.44 1.1-.16 1.92-.08 2.12-.51.56-.82 1.28-.82 2.15 0 3.06 1.86 3.75 3.64 3.95-.23.2-.44.55-.51 1.07-.46.21-1.61.55-2.33-.66-.15-.24-.6-.83-1.23-.82-.67.01-.27.38.01.53.34.19.73.9.82 1.13.16.45.68 1.31 2.69.94 0 .67.01 1.3.01 1.49 0 .21-.15.45-.55.38A7.995 7.995 0 0 1 0 8c0-4.42 3.58-8 8-8Z"></path>
        </svg>
                </a>

                  <div class="AppHeader-context" >
          <div class="AppHeader-context-compact">
              <button aria-expanded="false" aria-haspopup="dialog" aria-label="Page context: dotnet / runtime" id="dialog-show-context-region-dialog" data-show-dialog-id="context-region-dialog" type="button" data-view-component="true" class="AppHeader-context-compact-trigger Truncate Button--secondary Button--medium Button box-shadow-none">  <span class="Button-content">
            <span class="Button-label"><span class="AppHeader-context-compact-lead">
                        <span class="AppHeader-context-compact-parentItem">dotnet</span>
                        <span class="no-wrap">&nbsp;/</span>

                    </span>

                    <strong class="AppHeader-context-compact-mainItem d-flex flex-items-center Truncate" >
          <span class="Truncate-text ">runtime</span>

        </strong></span>
          </span>
        </button>

        <div class="Overlay--hidden Overlay-backdrop--center" data-modal-dialog-overlay>
          <modal-dialog role="dialog" id="context-region-dialog" aria-modal="true" aria-disabled="true" aria-labelledby="context-region-dialog-title" aria-describedby="context-region-dialog-description" data-view-component="true" class="Overlay Overlay-whenNarrow Overlay--size-medium Overlay--motion-scaleFade">
            <div data-view-component="true" class="Overlay-header">
          <div class="Overlay-headerContentWrap">
            <div class="Overlay-titleWrap">
              <h1 class="Overlay-title " id="context-region-dialog-title">
                Navigate back to
              </h1>
            </div>
            <div class="Overlay-actionWrap">
              <button data-close-dialog-id="context-region-dialog" aria-label="Close" type="button" data-view-component="true" class="close-button Overlay-closeButton"><svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg></button>
            </div>
          </div>
        </div>
              <div data-view-component="true" class="Overlay-body">          <ul role="list" class="list-style-none" >
            <li>
              <a data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;context_region_crumb&quot;,&quot;label&quot;:&quot;dotnet&quot;,&quot;screen_size&quot;:&quot;compact&quot;}" href="/dotnet" data-view-component="true" class="Link--primary Truncate d-flex flex-items-center py-1">
                <span class="AppHeader-context-item-label Truncate-text ">
                    <svg aria-hidden="true" height="12" viewBox="0 0 16 16" version="1.1" width="12" data-view-component="true" class="octicon octicon-organization mr-1">
            <path d="M1.75 16A1.75 1.75 0 0 1 0 14.25V1.75C0 .784.784 0 1.75 0h8.5C11.216 0 12 .784 12 1.75v12.5c0 .085-.006.168-.018.25h2.268a.25.25 0 0 0 .25-.25V8.285a.25.25 0 0 0-.111-.208l-1.055-.703a.749.749 0 1 1 .832-1.248l1.055.703c.487.325.779.871.779 1.456v5.965A1.75 1.75 0 0 1 14.25 16h-3.5a.766.766 0 0 1-.197-.026c-.099.017-.2.026-.303.026h-3a.75.75 0 0 1-.75-.75V14h-1v1.25a.75.75 0 0 1-.75.75Zm-.25-1.75c0 .138.112.25.25.25H4v-1.25a.75.75 0 0 1 .75-.75h2.5a.75.75 0 0 1 .75.75v1.25h2.25a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25ZM3.75 6h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5ZM3 3.75A.75.75 0 0 1 3.75 3h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 3 3.75Zm4 3A.75.75 0 0 1 7.75 6h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 7 6.75ZM7.75 3h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5ZM3 9.75A.75.75 0 0 1 3.75 9h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 3 9.75ZM7.75 9h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5Z"></path>
        </svg>

                  dotnet
                </span>

        </a>
            </li>
            <li>
              <a data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;context_region_crumb&quot;,&quot;label&quot;:&quot;runtime&quot;,&quot;screen_size&quot;:&quot;compact&quot;}" href="/dotnet/runtime" data-view-component="true" class="Link--primary Truncate d-flex flex-items-center py-1">
                <span class="AppHeader-context-item-label Truncate-text ">
                    <svg aria-hidden="true" height="12" viewBox="0 0 16 16" version="1.1" width="12" data-view-component="true" class="octicon octicon-repo mr-1">
            <path d="M2 2.5A2.5 2.5 0 0 1 4.5 0h8.75a.75.75 0 0 1 .75.75v12.5a.75.75 0 0 1-.75.75h-2.5a.75.75 0 0 1 0-1.5h1.75v-2h-8a1 1 0 0 0-.714 1.7.75.75 0 1 1-1.072 1.05A2.495 2.495 0 0 1 2 11.5Zm10.5-1h-8a1 1 0 0 0-1 1v6.708A2.486 2.486 0 0 1 4.5 9h8ZM5 12.25a.25.25 0 0 1 .25-.25h3.5a.25.25 0 0 1 .25.25v3.25a.25.25 0 0 1-.4.2l-1.45-1.087a.249.249 0 0 0-.3 0L5.4 15.7a.25.25 0 0 1-.4-.2Z"></path>
        </svg>

                  runtime
                </span>

        </a>
            </li>
        </ul>

        </div>

        </modal-dialog></div>
          </div>

          <div class="AppHeader-context-full">
            <nav role="navigation" aria-label="Page context">
              <ul role="list" class="list-style-none" >
            <li>
              <a data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;context_region_crumb&quot;,&quot;label&quot;:&quot;dotnet&quot;,&quot;screen_size&quot;:&quot;full&quot;}" data-hovercard-type="organization" data-hovercard-url="/orgs/dotnet/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/dotnet" data-view-component="true" class="AppHeader-context-item">
                <span class="AppHeader-context-item-label  ">

                  dotnet
                </span>

        </a>
                <span class="AppHeader-context-item-separator">/</span>
            </li>
            <li>
              <a data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;context_region_crumb&quot;,&quot;label&quot;:&quot;runtime&quot;,&quot;screen_size&quot;:&quot;full&quot;}" href="/dotnet/runtime" data-view-component="true" class="AppHeader-context-item">
                <span class="AppHeader-context-item-label  ">

                  runtime
                </span>

        </a>
            </li>
        </ul>

            </nav>
          </div>
        </div>

              </div>
              <div class="AppHeader-globalBar-end">
                  <div class="AppHeader-search" >



        <qbsearch-input class="search-input" data-scope="repo:dotnet/runtime" data-custom-scopes-path="/search/custom_scopes" data-delete-custom-scopes-csrf="_ueeIeok_T-65lLoYxmxNaju1AbIDl7-b0_P-jCqlulXdjJGOVkVfkpOhweJOOX-Ed7fN5N9rzFbNpV6sSZgew" data-max-custom-scopes="10" data-header-redesign-enabled="true" data-initial-value="" data-blackbird-suggestions-path="/search/suggestions" data-jump-to-suggestions-path="/_graphql/GetSuggestedNavigationDestinations" data-current-repository="dotnet/runtime" data-current-org="dotnet" data-current-owner="" data-logged-in="true" data-copilot-chat-enabled="false" data-blackbird-indexed-repo-csrf="<input type=&quot;hidden&quot; value=&quot;mZG7urCpaa1zo-A20BxrkikTDpVepdnIHu9yDiHGKYoH45MlZkzJ0OaGPZ1j-lOu_ad7wBFQXk5k08zWl3slgw&quot; data-csrf=&quot;true&quot; />">
          <div
            class="search-input-container search-with-dialog position-relative d-flex flex-row flex-items-center height-auto color-bg-transparent border-0 color-fg-subtle mx-0"
            data-action="click:qbsearch-input#searchInputContainerClicked"
          >

                    <button type="button" data-action="click:qbsearch-input#handleExpand" class="AppHeader-button AppHeader-search-whenNarrow" aria-label="Search or jump to…" aria-expanded="false" aria-haspopup="dialog">
                    <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-search">
            <path d="M10.68 11.74a6 6 0 0 1-7.922-8.982 6 6 0 0 1 8.982 7.922l3.04 3.04a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215ZM11.5 7a4.499 4.499 0 1 0-8.997 0A4.499 4.499 0 0 0 11.5 7Z"></path>
        </svg>
                  </button>


        <div class="AppHeader-search-whenRegular">
          <div class="AppHeader-search-wrap AppHeader-search-wrap--hasTrailing">
            <div class="AppHeader-search-control">
              <label
                for="AppHeader-searchInput"
                aria-label="Search or jump to…"
                class="AppHeader-search-visual--leading"
              >
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-search">
            <path d="M10.68 11.74a6 6 0 0 1-7.922-8.982 6 6 0 0 1 8.982 7.922l3.04 3.04a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215ZM11.5 7a4.499 4.499 0 1 0-8.997 0A4.499 4.499 0 0 0 11.5 7Z"></path>
        </svg>
              </label>

                        <button
                    type="button"
                    data-target="qbsearch-input.inputButton"
                    data-action="click:qbsearch-input#handleExpand"
                    class="AppHeader-searchButton form-control input-contrast text-left color-fg-subtle no-wrap"
                    data-hotkey="s,/"
                    data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;SEARCH&quot;,&quot;label&quot;:null}"
                  >
                    <div class="overflow-hidden">
                      <span id="qb-input-query" data-target="qbsearch-input.inputButtonText">
                          Type <kbd class="AppHeader-search-kbd">/</kbd> to search
                      </span>
                    </div>
                  </button>

            </div>


              <button type="button" id="AppHeader-commandPalette-button" class="AppHeader-search-action--trailing js-activate-command-palette" data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;command_palette&quot;,&quot;label&quot;:&quot;open command palette&quot;}">
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-command-palette">
            <path d="m6.354 8.04-4.773 4.773a.75.75 0 1 0 1.061 1.06L7.945 8.57a.75.75 0 0 0 0-1.06L2.642 2.206a.75.75 0 0 0-1.06 1.061L6.353 8.04ZM8.75 11.5a.75.75 0 0 0 0 1.5h5.5a.75.75 0 0 0 0-1.5h-5.5Z"></path>
        </svg>
              </button>

              <tool-tip id="tooltip-4124f0c5-274b-49de-8ccd-ad00595be942" for="AppHeader-commandPalette-button" popover="manual" data-direction="s" data-type="label" data-view-component="true" class="sr-only position-absolute">Command palette</tool-tip>
          </div>
        </div>

            <input type="hidden" name="type" class="js-site-search-type-field">


        <div class="Overlay--hidden " data-modal-dialog-overlay>
          <modal-dialog data-action="close:qbsearch-input#handleClose cancel:qbsearch-input#handleClose" data-target="qbsearch-input.searchSuggestionsDialog" role="dialog" id="search-suggestions-dialog" aria-modal="true" aria-labelledby="search-suggestions-dialog-header" data-view-component="true" class="Overlay Overlay--width-medium Overlay--height-auto">
              <h1 id="search-suggestions-dialog-header" class="sr-only">Search code, repositories, users, issues, pull requests...</h1>
            <div class="Overlay-body Overlay-body--paddingNone">

                  <div data-view-component="true">        <div class="search-suggestions position-absolute width-full color-shadow-large border color-fg-default color-bg-default overflow-hidden d-flex flex-column query-builder-container"
                  style="border-radius: 12px;"
                  data-target="qbsearch-input.queryBuilderContainer"
                  hidden
                >
                  <!-- '"` --><!-- </textarea></xmp> --></option></form><form id="query-builder-test-form" action="" accept-charset="UTF-8" method="get">
          <query-builder data-target="qbsearch-input.queryBuilder" id="query-builder-query-builder-test" data-filter-key=":" data-view-component="true" class="QueryBuilder search-query-builder">
            <div class="FormControl FormControl--fullWidth">
              <label id="query-builder-test-label" for="query-builder-test" class="FormControl-label sr-only">
                Search
              </label>
              <div
                class="QueryBuilder-StyledInput width-fit "
                data-target="query-builder.styledInput"
              >
                  <span id="query-builder-test-leadingvisual-wrap" class="FormControl-input-leadingVisualWrap QueryBuilder-leadingVisualWrap">
                    <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-search FormControl-input-leadingVisual">
            <path d="M10.68 11.74a6 6 0 0 1-7.922-8.982 6 6 0 0 1 8.982 7.922l3.04 3.04a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215ZM11.5 7a4.499 4.499 0 1 0-8.997 0A4.499 4.499 0 0 0 11.5 7Z"></path>
        </svg>
                  </span>
                <div data-target="query-builder.styledInputContainer" class="QueryBuilder-StyledInputContainer">
                  <div
                    aria-hidden="true"
                    class="QueryBuilder-StyledInputContent"
                    data-target="query-builder.styledInputContent"
                  ></div>
                  <div class="QueryBuilder-InputWrapper">
                    <div aria-hidden="true" class="QueryBuilder-Sizer" data-target="query-builder.sizer"></div>
                    <input id="query-builder-test" name="query-builder-test" value="" autocomplete="off" type="text" role="combobox" spellcheck="false" aria-expanded="false" aria-describedby="validation-645f5ad0-7b8c-4fd3-8a29-cd86bd50238d" data-target="query-builder.input" data-action="
                  input:query-builder#inputChange
                  blur:query-builder#inputBlur
                  keydown:query-builder#inputKeydown
                  focus:query-builder#inputFocus
                " data-view-component="true" class="FormControl-input QueryBuilder-Input FormControl-medium" />
                  </div>
                </div>
                  <span class="sr-only" id="query-builder-test-clear">Clear</span>
                  <button role="button" id="query-builder-test-clear-button" aria-labelledby="query-builder-test-clear query-builder-test-label" data-target="query-builder.clearButton" data-action="
                        click:query-builder#clear
                        focus:query-builder#clearButtonFocus
                        blur:query-builder#clearButtonBlur
                      " variant="small" hidden="hidden" type="button" data-view-component="true" class="Button Button--iconOnly Button--invisible Button--medium mr-1 px-2 py-0 d-flex flex-items-center rounded-1 color-fg-muted">  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x-circle-fill Button-visual">
            <path d="M2.343 13.657A8 8 0 1 1 13.658 2.343 8 8 0 0 1 2.343 13.657ZM6.03 4.97a.751.751 0 0 0-1.042.018.751.751 0 0 0-.018 1.042L6.94 8 4.97 9.97a.749.749 0 0 0 .326 1.275.749.749 0 0 0 .734-.215L8 9.06l1.97 1.97a.749.749 0 0 0 1.275-.326.749.749 0 0 0-.215-.734L9.06 8l1.97-1.97a.749.749 0 0 0-.326-1.275.749.749 0 0 0-.734.215L8 6.94Z"></path>
        </svg>
        </button>

              </div>
              <template id="search-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-search">
            <path d="M10.68 11.74a6 6 0 0 1-7.922-8.982 6 6 0 0 1 8.982 7.922l3.04 3.04a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215ZM11.5 7a4.499 4.499 0 1 0-8.997 0A4.499 4.499 0 0 0 11.5 7Z"></path>
        </svg>
        </template>

        <template id="code-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-code">
            <path d="m11.28 3.22 4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734L13.94 8l-3.72-3.72a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215Zm-6.56 0a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L2.06 8l3.72 3.72a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L.47 8.53a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
        </template>

        <template id="file-code-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-file-code">
            <path d="M4 1.75C4 .784 4.784 0 5.75 0h5.586c.464 0 .909.184 1.237.513l2.914 2.914c.329.328.513.773.513 1.237v8.586A1.75 1.75 0 0 1 14.25 15h-9a.75.75 0 0 1 0-1.5h9a.25.25 0 0 0 .25-.25V6h-2.75A1.75 1.75 0 0 1 10 4.25V1.5H5.75a.25.25 0 0 0-.25.25v2.5a.75.75 0 0 1-1.5 0Zm1.72 4.97a.75.75 0 0 1 1.06 0l2 2a.75.75 0 0 1 0 1.06l-2 2a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734l1.47-1.47-1.47-1.47a.75.75 0 0 1 0-1.06ZM3.28 7.78 1.81 9.25l1.47 1.47a.751.751 0 0 1-.018 1.042.751.751 0 0 1-1.042.018l-2-2a.75.75 0 0 1 0-1.06l2-2a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042Zm8.22-6.218V4.25c0 .138.112.25.25.25h2.688l-.011-.013-2.914-2.914-.013-.011Z"></path>
        </svg>
        </template>

        <template id="history-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-history">
            <path d="m.427 1.927 1.215 1.215a8.002 8.002 0 1 1-1.6 5.685.75.75 0 1 1 1.493-.154 6.5 6.5 0 1 0 1.18-4.458l1.358 1.358A.25.25 0 0 1 3.896 6H.25A.25.25 0 0 1 0 5.75V2.104a.25.25 0 0 1 .427-.177ZM7.75 4a.75.75 0 0 1 .75.75v2.992l2.028.812a.75.75 0 0 1-.557 1.392l-2.5-1A.751.751 0 0 1 7 8.25v-3.5A.75.75 0 0 1 7.75 4Z"></path>
        </svg>
        </template>

        <template id="repo-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-repo">
            <path d="M2 2.5A2.5 2.5 0 0 1 4.5 0h8.75a.75.75 0 0 1 .75.75v12.5a.75.75 0 0 1-.75.75h-2.5a.75.75 0 0 1 0-1.5h1.75v-2h-8a1 1 0 0 0-.714 1.7.75.75 0 1 1-1.072 1.05A2.495 2.495 0 0 1 2 11.5Zm10.5-1h-8a1 1 0 0 0-1 1v6.708A2.486 2.486 0 0 1 4.5 9h8ZM5 12.25a.25.25 0 0 1 .25-.25h3.5a.25.25 0 0 1 .25.25v3.25a.25.25 0 0 1-.4.2l-1.45-1.087a.249.249 0 0 0-.3 0L5.4 15.7a.25.25 0 0 1-.4-.2Z"></path>
        </svg>
        </template>

        <template id="bookmark-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-bookmark">
            <path d="M3 2.75C3 1.784 3.784 1 4.75 1h6.5c.966 0 1.75.784 1.75 1.75v11.5a.75.75 0 0 1-1.227.579L8 11.722l-3.773 3.107A.751.751 0 0 1 3 14.25Zm1.75-.25a.25.25 0 0 0-.25.25v9.91l3.023-2.489a.75.75 0 0 1 .954 0l3.023 2.49V2.75a.25.25 0 0 0-.25-.25Z"></path>
        </svg>
        </template>

        <template id="plus-circle-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-plus-circle">
            <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm7.25-3.25v2.5h2.5a.75.75 0 0 1 0 1.5h-2.5v2.5a.75.75 0 0 1-1.5 0v-2.5h-2.5a.75.75 0 0 1 0-1.5h2.5v-2.5a.75.75 0 0 1 1.5 0Z"></path>
        </svg>
        </template>

        <template id="circle-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-dot-fill">
            <path d="M8 4a4 4 0 1 1 0 8 4 4 0 0 1 0-8Z"></path>
        </svg>
        </template>

        <template id="trash-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-trash">
            <path d="M11 1.75V3h2.25a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1 0-1.5H5V1.75C5 .784 5.784 0 6.75 0h2.5C10.216 0 11 .784 11 1.75ZM4.496 6.675l.66 6.6a.25.25 0 0 0 .249.225h5.19a.25.25 0 0 0 .249-.225l.66-6.6a.75.75 0 0 1 1.492.149l-.66 6.6A1.748 1.748 0 0 1 10.595 15h-5.19a1.75 1.75 0 0 1-1.741-1.575l-.66-6.6a.75.75 0 1 1 1.492-.15ZM6.5 1.75V3h3V1.75a.25.25 0 0 0-.25-.25h-2.5a.25.25 0 0 0-.25.25Z"></path>
        </svg>
        </template>

        <template id="team-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-people">
            <path d="M2 5.5a3.5 3.5 0 1 1 5.898 2.549 5.508 5.508 0 0 1 3.034 4.084.75.75 0 1 1-1.482.235 4 4 0 0 0-7.9 0 .75.75 0 0 1-1.482-.236A5.507 5.507 0 0 1 3.102 8.05 3.493 3.493 0 0 1 2 5.5ZM11 4a3.001 3.001 0 0 1 2.22 5.018 5.01 5.01 0 0 1 2.56 3.012.749.749 0 0 1-.885.954.752.752 0 0 1-.549-.514 3.507 3.507 0 0 0-2.522-2.372.75.75 0 0 1-.574-.73v-.352a.75.75 0 0 1 .416-.672A1.5 1.5 0 0 0 11 5.5.75.75 0 0 1 11 4Zm-5.5-.5a2 2 0 1 0-.001 3.999A2 2 0 0 0 5.5 3.5Z"></path>
        </svg>
        </template>

        <template id="project-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-project">
            <path d="M1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25V1.75C0 .784.784 0 1.75 0ZM1.5 1.75v12.5c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25H1.75a.25.25 0 0 0-.25.25ZM11.75 3a.75.75 0 0 1 .75.75v7.5a.75.75 0 0 1-1.5 0v-7.5a.75.75 0 0 1 .75-.75Zm-8.25.75a.75.75 0 0 1 1.5 0v5.5a.75.75 0 0 1-1.5 0ZM8 3a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-1.5 0v-3.5A.75.75 0 0 1 8 3Z"></path>
        </svg>
        </template>

        <template id="pencil-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-pencil">
            <path d="M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Zm.176 4.823L9.75 4.81l-6.286 6.287a.253.253 0 0 0-.064.108l-.558 1.953 1.953-.558a.253.253 0 0 0 .108-.064Zm1.238-3.763a.25.25 0 0 0-.354 0L10.811 3.75l1.439 1.44 1.263-1.263a.25.25 0 0 0 0-.354Z"></path>
        </svg>
        </template>

        <template id="copilot-icon">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-copilot">
            <path d="M7.998 15.035c-4.562 0-7.873-2.914-7.998-3.749V9.338c.085-.628.677-1.686 1.588-2.065.013-.07.024-.143.036-.218.029-.183.06-.384.126-.612-.201-.508-.254-1.084-.254-1.656 0-.87.128-1.769.693-2.484.579-.733 1.494-1.124 2.724-1.261 1.206-.134 2.262.034 2.944.765.05.053.096.108.139.165.044-.057.094-.112.143-.165.682-.731 1.738-.899 2.944-.765 1.23.137 2.145.528 2.724 1.261.566.715.693 1.614.693 2.484 0 .572-.053 1.148-.254 1.656.066.228.098.429.126.612.012.076.024.148.037.218.924.385 1.522 1.471 1.591 2.095v1.872c0 .766-3.351 3.795-8.002 3.795Zm0-1.485c2.28 0 4.584-1.11 5.002-1.433V7.862l-.023-.116c-.49.21-1.075.291-1.727.291-1.146 0-2.059-.327-2.71-.991A3.222 3.222 0 0 1 8 6.303a3.24 3.24 0 0 1-.544.743c-.65.664-1.563.991-2.71.991-.652 0-1.236-.081-1.727-.291l-.023.116v4.255c.419.323 2.722 1.433 5.002 1.433ZM6.762 2.83c-.193-.206-.637-.413-1.682-.297-1.019.113-1.479.404-1.713.7-.247.312-.369.789-.369 1.554 0 .793.129 1.171.308 1.371.162.181.519.379 1.442.379.853 0 1.339-.235 1.638-.54.315-.322.527-.827.617-1.553.117-.935-.037-1.395-.241-1.614Zm4.155-.297c-1.044-.116-1.488.091-1.681.297-.204.219-.359.679-.242 1.614.091.726.303 1.231.618 1.553.299.305.784.54 1.638.54.922 0 1.28-.198 1.442-.379.179-.2.308-.578.308-1.371 0-.765-.123-1.242-.37-1.554-.233-.296-.693-.587-1.713-.7Z"></path><path d="M6.25 9.037a.75.75 0 0 1 .75.75v1.501a.75.75 0 0 1-1.5 0V9.787a.75.75 0 0 1 .75-.75Zm4.25.75v1.501a.75.75 0 0 1-1.5 0V9.787a.75.75 0 0 1 1.5 0Z"></path>
        </svg>
        </template>

                <div class="position-relative">
                        <ul
                          role="listbox"
                          class="ActionListWrap QueryBuilder-ListWrap"
                          aria-label="Suggestions"
                          data-action="
                            combobox-commit:query-builder#comboboxCommit
                            mousedown:query-builder#resultsMousedown
                          "
                          data-target="query-builder.resultsList"
                          data-persist-list=false
                          id="query-builder-test-results"
                        ></ul>
                </div>
              <div class="FormControl-inlineValidation" id="validation-645f5ad0-7b8c-4fd3-8a29-cd86bd50238d" hidden="hidden">
                <span class="FormControl-inlineValidation--visual">
                  <svg aria-hidden="true" height="12" viewBox="0 0 12 12" version="1.1" width="12" data-view-component="true" class="octicon octicon-alert-fill">
            <path d="M4.855.708c.5-.896 1.79-.896 2.29 0l4.675 8.351a1.312 1.312 0 0 1-1.146 1.954H1.33A1.313 1.313 0 0 1 .183 9.058ZM7 7V3H5v4Zm-1 3a1 1 0 1 0 0-2 1 1 0 0 0 0 2Z"></path>
        </svg>
                </span>
                <span></span>
        </div>    </div>
            <div data-target="query-builder.screenReaderFeedback" aria-live="polite" aria-atomic="true" class="sr-only"></div>
        </query-builder></form>
                  <div class="d-flex flex-row color-fg-muted px-3 text-small color-bg-default search-feedback-prompt">
                    <a target="_blank" href="https://docs.github.com/en/search-github/github-code-search/understanding-github-code-search-syntax" data-view-component="true" class="Link color-fg-accent text-normal ml-2">
                      Search syntax tips
        </a>            <div class="d-flex flex-1"></div>
                      <button data-action="click:qbsearch-input#showFeedbackDialog" type="button" data-view-component="true" class="Button--link Button--medium Button color-fg-accent text-normal ml-2">  <span class="Button-content">
            <span class="Button-label">Give feedback</span>
          </span>
        </button>
                  </div>
                </div>
        </div>

            </div>
        </modal-dialog></div>
          </div>
          <div data-action="click:qbsearch-input#retract" class="dark-backdrop position-fixed" hidden data-target="qbsearch-input.darkBackdrop"></div>
          <div class="color-fg-default">

        <div class="Overlay--hidden Overlay-backdrop--center" data-modal-dialog-overlay>
          <modal-dialog data-target="qbsearch-input.feedbackDialog" data-action="close:qbsearch-input#handleDialogClose cancel:qbsearch-input#handleDialogClose" role="dialog" id="feedback-dialog" aria-modal="true" aria-disabled="true" aria-labelledby="feedback-dialog-title" aria-describedby="feedback-dialog-description" data-view-component="true" class="Overlay Overlay-whenNarrow Overlay--size-medium Overlay--motion-scaleFade">
            <div data-view-component="true" class="Overlay-header">
          <div class="Overlay-headerContentWrap">
            <div class="Overlay-titleWrap">
              <h1 class="Overlay-title " id="feedback-dialog-title">
                Provide feedback
              </h1>
            </div>
            <div class="Overlay-actionWrap">
              <button data-close-dialog-id="feedback-dialog" aria-label="Close" type="button" data-view-component="true" class="close-button Overlay-closeButton"><svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg></button>
            </div>
          </div>
        </div>
              <div data-view-component="true" class="Overlay-body">        <!-- '"` --><!-- </textarea></xmp> --></option></form><form id="code-search-feedback-form" data-turbo="false" action="/search/feedback" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="9xXHenN3lQ7qwBv6mCBb2uf3M_Vu58XNJ19ZOQokdCPiHIkzbkbjaNpiE46iEG2YsQiCojLcWbQGD32spRBUSA" />
                  <p>We read every piece of feedback, and take your input very seriously.</p>
                  <textarea name="feedback" class="form-control width-full mb-2" style="height: 120px" id="feedback"></textarea>
                  <input name="include_email" id="include_email" aria-label="Include my email address so I can be contacted" class="form-control mr-2" type="checkbox">
                  <label for="include_email" style="font-weight: normal">Include my email address so I can be contacted</label>
        </form></div>
              <div data-view-component="true" class="Overlay-footer Overlay-footer--alignEnd">          <button data-close-dialog-id="feedback-dialog" type="button" data-view-component="true" class="btn">    Cancel
        </button>
                  <button form="code-search-feedback-form" data-action="click:qbsearch-input#submitFeedback" type="submit" data-view-component="true" class="btn-primary btn">    Submit feedback
        </button>
        </div>
        </modal-dialog></div>

            <custom-scopes data-target="qbsearch-input.customScopesManager">

        <div class="Overlay--hidden Overlay-backdrop--center" data-modal-dialog-overlay>
          <modal-dialog data-target="custom-scopes.customScopesModalDialog" data-action="close:qbsearch-input#handleDialogClose cancel:qbsearch-input#handleDialogClose" role="dialog" id="custom-scopes-dialog" aria-modal="true" aria-disabled="true" aria-labelledby="custom-scopes-dialog-title" aria-describedby="custom-scopes-dialog-description" data-view-component="true" class="Overlay Overlay-whenNarrow Overlay--size-medium Overlay--motion-scaleFade">
            <div data-view-component="true" class="Overlay-header Overlay-header--divided">
          <div class="Overlay-headerContentWrap">
            <div class="Overlay-titleWrap">
              <h1 class="Overlay-title " id="custom-scopes-dialog-title">
                Saved searches
              </h1>
                <h2 id="custom-scopes-dialog-description" class="Overlay-description">Use saved searches to filter your results more quickly</h2>
            </div>
            <div class="Overlay-actionWrap">
              <button data-close-dialog-id="custom-scopes-dialog" aria-label="Close" type="button" data-view-component="true" class="close-button Overlay-closeButton"><svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg></button>
            </div>
          </div>
        </div>
              <div data-view-component="true" class="Overlay-body">        <div data-target="custom-scopes.customScopesModalDialogFlash"></div>

                <div hidden class="create-custom-scope-form" data-target="custom-scopes.createCustomScopeForm">
                <!-- '"` --><!-- </textarea></xmp> --></option></form><form id="custom-scopes-dialog-form" data-turbo="false" action="/search/custom_scopes" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="YisObqtcx2DWcoWIFPjbXMAUAtUwdAcDXT56NB0i4bwfwgL_YxRI2awz8YgAj9kOwely_ggIUZSjzkXJORr5QA" />
                  <div data-target="custom-scopes.customScopesModalDialogFlash"></div>

                  <input type="hidden" id="custom_scope_id" name="custom_scope_id" data-target="custom-scopes.customScopesIdField">

                  <div class="form-group">
                    <label for="custom_scope_name">Name</label>
                    <auto-check src="/search/custom_scopes/check_name" required>
                      <input
                        type="text"
                        name="custom_scope_name"
                        id="custom_scope_name"
                        data-target="custom-scopes.customScopesNameField"
                        class="form-control"
                        autocomplete="off"
                        placeholder="github-ruby"
                        required
                        maxlength="50">
                      <input type="hidden" value="PSfaJ7fc5vgQVK3KVyXCLqJ24P3eI2B_V0_TKSgMSPaxw5tziaG9txl_SVwrnE4fO8WqYwaDIUCEjwOCgd2P8A" data-csrf="true" />
                    </auto-check>
                  </div>

                  <div class="form-group">
                    <label for="custom_scope_query">Query</label>
                    <input
                      type="text"
                      name="custom_scope_query"
                      id="custom_scope_query"
                      data-target="custom-scopes.customScopesQueryField"
                      class="form-control"
                      autocomplete="off"
                      placeholder="(repo:mona/a OR repo:mona/b) AND lang:python"
                      required
                      maxlength="500">
                  </div>

                  <p class="text-small color-fg-muted">
                    To see all available qualifiers, see our <a class="Link--inTextBlock" href="https://docs.github.com/en/search-github/github-code-search/understanding-github-code-search-syntax">documentation</a>.
                  </p>
        </form>        </div>

                <div data-target="custom-scopes.manageCustomScopesForm">
                  <div data-target="custom-scopes.list"></div>
                </div>

        </div>
              <div data-view-component="true" class="Overlay-footer Overlay-footer--alignEnd Overlay-footer--divided">          <button data-action="click:custom-scopes#customScopesCancel" type="button" data-view-component="true" class="btn">    Cancel
        </button>
                  <button form="custom-scopes-dialog-form" data-action="click:custom-scopes#customScopesSubmit" data-target="custom-scopes.customScopesSubmitButton" type="submit" data-view-component="true" class="btn-primary btn">    Create saved search
        </button>
        </div>
        </modal-dialog></div>
            </custom-scopes>
          </div>
        </qbsearch-input><input type="hidden" value="Nmp2Y8hCBxzcHEbqFAiUrymnVM2xmY7oqsNCZPGZCGIqBfI5va-wct_qT-MM3-zx88xP8Td1RVlI0iquc9PhwQ" data-csrf="true" class="js-data-jump-to-suggestions-path-csrf" />

                  </div>

                <div class="AppHeader-actions position-relative">
                  <action-menu data-select-variant="none" data-view-component="true">
          <focus-group direction="vertical" mnemonics retain>
            <button id="global-create-menu-button" popovertarget="global-create-menu-overlay" aria-label="Create something new" aria-controls="global-create-menu-list" aria-haspopup="true" type="button" data-view-component="true" class="AppHeader-button Button--secondary Button--small Button width-auto color-fg-muted">  <span class="Button-content">
              <span class="Button-visual Button-leadingVisual">
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-plus">
            <path d="M7.75 2a.75.75 0 0 1 .75.75V7h4.25a.75.75 0 0 1 0 1.5H8.5v4.25a.75.75 0 0 1-1.5 0V8.5H2.75a.75.75 0 0 1 0-1.5H7V2.75A.75.75 0 0 1 7.75 2Z"></path>
        </svg>
              </span>
            <span class="Button-label"><svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-triangle-down">
            <path d="m4.427 7.427 3.396 3.396a.25.25 0 0 0 .354 0l3.396-3.396A.25.25 0 0 0 11.396 7H4.604a.25.25 0 0 0-.177.427Z"></path>
        </svg></span>
          </span>
        </button><tool-tip id="tooltip-c0ac8098-512e-4078-8f51-0565b96603dc" for="global-create-menu-button" popover="manual" data-direction="s" data-type="description" data-view-component="true" class="sr-only position-absolute">Create new...</tool-tip>


        <anchored-position id="global-create-menu-overlay" anchor="global-create-menu-button" align="end" side="outside-bottom" anchor-offset="normal" popover="auto" data-view-component="true">
          <div data-view-component="true" class="Overlay Overlay--size-auto">

              <div data-view-component="true" class="Overlay-body Overlay-body--paddingNone">          <div data-view-component="true">
          <ul aria-labelledby="global-create-menu-button" id="global-create-menu-list" role="menu" data-view-component="true" class="ActionListWrap--inset ActionListWrap">
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new repository&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/new" tabindex="-1" id="item-ed300139-1677-4bf3-bcdd-c38a7ae7a114" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-repo">
            <path d="M2 2.5A2.5 2.5 0 0 1 4.5 0h8.75a.75.75 0 0 1 .75.75v12.5a.75.75 0 0 1-.75.75h-2.5a.75.75 0 0 1 0-1.5h1.75v-2h-8a1 1 0 0 0-.714 1.7.75.75 0 1 1-1.072 1.05A2.495 2.495 0 0 1 2 11.5Zm10.5-1h-8a1 1 0 0 0-1 1v6.708A2.486 2.486 0 0 1 4.5 9h8ZM5 12.25a.25.25 0 0 1 .25-.25h3.5a.25.25 0 0 1 .25.25v3.25a.25.25 0 0 1-.4.2l-1.45-1.087a.249.249 0 0 0-.3 0L5.4 15.7a.25.25 0 0 1-.4-.2Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                      New repository

        </span></a>


        </li>
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;import repository&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/new/import" tabindex="-1" id="item-9cef88cb-260b-4310-836e-89783cebe64f" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-repo-push">
            <path d="M1 2.5A2.5 2.5 0 0 1 3.5 0h8.75a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-1.5 0V1.5h-8a1 1 0 0 0-1 1v6.708A2.493 2.493 0 0 1 3.5 9h3.25a.75.75 0 0 1 0 1.5H3.5a1 1 0 0 0 0 2h5.75a.75.75 0 0 1 0 1.5H3.5A2.5 2.5 0 0 1 1 11.5Zm13.23 7.79h-.001l-1.224-1.224v6.184a.75.75 0 0 1-1.5 0V9.066L10.28 10.29a.75.75 0 0 1-1.06-1.061l2.505-2.504a.75.75 0 0 1 1.06 0L15.29 9.23a.751.751 0 0 1-.018 1.042.751.751 0 0 1-1.042.018Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        Import repository

        </span></a>


        </li>
              <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new codespace&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/codespaces/new" tabindex="-1" id="item-e9e489f8-175e-41e8-9898-ece4da550b46" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-codespaces">
            <path d="M0 11.25c0-.966.784-1.75 1.75-1.75h12.5c.966 0 1.75.784 1.75 1.75v3A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm2-9.5C2 .784 2.784 0 3.75 0h8.5C13.216 0 14 .784 14 1.75v5a1.75 1.75 0 0 1-1.75 1.75h-8.5A1.75 1.75 0 0 1 2 6.75Zm1.75-.25a.25.25 0 0 0-.25.25v5c0 .138.112.25.25.25h8.5a.25.25 0 0 0 .25-.25v-5a.25.25 0 0 0-.25-.25Zm-2 9.5a.25.25 0 0 0-.25.25v3c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25v-3a.25.25 0 0 0-.25-.25Z"></path><path d="M7 12.75a.75.75 0 0 1 .75-.75h4.5a.75.75 0 0 1 0 1.5h-4.5a.75.75 0 0 1-.75-.75Zm-4 0a.75.75 0 0 1 .75-.75h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1-.75-.75Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        New codespace

        </span></a>


        </li>
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new gist&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="https://gist.github.com/" tabindex="-1" id="item-92a31833-4cb2-4d1f-b235-41a236179fdc" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-code">
            <path d="m11.28 3.22 4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734L13.94 8l-3.72-3.72a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215Zm-6.56 0a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L2.06 8l3.72 3.72a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L.47 8.53a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        New gist

        </span></a>


        </li>
              <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>
              <li data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/account/organizations/new" tabindex="-1" data-dont-follow-via-test="true" data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new organization&quot;}" id="item-f170063d-414f-4d5b-851e-fe2284873abc" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-organization">
            <path d="M1.75 16A1.75 1.75 0 0 1 0 14.25V1.75C0 .784.784 0 1.75 0h8.5C11.216 0 12 .784 12 1.75v12.5c0 .085-.006.168-.018.25h2.268a.25.25 0 0 0 .25-.25V8.285a.25.25 0 0 0-.111-.208l-1.055-.703a.749.749 0 1 1 .832-1.248l1.055.703c.487.325.779.871.779 1.456v5.965A1.75 1.75 0 0 1 14.25 16h-3.5a.766.766 0 0 1-.197-.026c-.099.017-.2.026-.303.026h-3a.75.75 0 0 1-.75-.75V14h-1v1.25a.75.75 0 0 1-.75.75Zm-.25-1.75c0 .138.112.25.25.25H4v-1.25a.75.75 0 0 1 .75-.75h2.5a.75.75 0 0 1 .75.75v1.25h2.25a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25ZM3.75 6h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5ZM3 3.75A.75.75 0 0 1 3.75 3h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 3 3.75Zm4 3A.75.75 0 0 1 7.75 6h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 7 6.75ZM7.75 3h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5ZM3 9.75A.75.75 0 0 1 3.75 9h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 3 9.75ZM7.75 9h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        New organization

        </span></a>


        </li>
        </ul>
        </div>

        </div>

        </div></anchored-position>  </focus-group>
        </action-menu>


                  <a href="/issues" data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;ISSUES_HEADER&quot;,&quot;label&quot;:null}" id="icon-button-5961f65a-2b47-404d-a3c8-3eb35368ca31" aria-labelledby="tooltip-a19b971a-592e-4569-9559-eee72ca9d210" data-view-component="true" class="Button Button--iconOnly Button--secondary Button--medium AppHeader-button color-fg-muted">  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-issue-opened Button-visual">
            <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path><path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Z"></path>
        </svg>
        </a><tool-tip id="tooltip-a19b971a-592e-4569-9559-eee72ca9d210" for="icon-button-5961f65a-2b47-404d-a3c8-3eb35368ca31" popover="manual" data-direction="s" data-type="label" data-view-component="true" class="sr-only position-absolute">Issues</tool-tip>

                  <a href="/pulls" data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;PULL_REQUESTS_HEADER&quot;,&quot;label&quot;:null}" id="icon-button-6da2fb1b-1ec1-4508-9fe5-61910d12ffa1" aria-labelledby="tooltip-9a68ba0b-c4c4-4e6b-9b53-0f3fe9f55e86" data-view-component="true" class="Button Button--iconOnly Button--secondary Button--medium AppHeader-button color-fg-muted">  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-git-pull-request Button-visual">
            <path d="M1.5 3.25a2.25 2.25 0 1 1 3 2.122v5.256a2.251 2.251 0 1 1-1.5 0V5.372A2.25 2.25 0 0 1 1.5 3.25Zm5.677-.177L9.573.677A.25.25 0 0 1 10 .854V2.5h1A2.5 2.5 0 0 1 13.5 5v5.628a2.251 2.251 0 1 1-1.5 0V5a1 1 0 0 0-1-1h-1v1.646a.25.25 0 0 1-.427.177L7.177 3.427a.25.25 0 0 1 0-.354ZM3.75 2.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm0 9.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm8.25.75a.75.75 0 1 0 1.5 0 .75.75 0 0 0-1.5 0Z"></path>
        </svg>
        </a><tool-tip id="tooltip-9a68ba0b-c4c4-4e6b-9b53-0f3fe9f55e86" for="icon-button-6da2fb1b-1ec1-4508-9fe5-61910d12ffa1" popover="manual" data-direction="s" data-type="label" data-view-component="true" class="sr-only position-absolute">Pull requests</tool-tip>

                </div>


        <notification-indicator data-channel="eyJjIjoibm90aWZpY2F0aW9uLWNoYW5nZWQ6MTE2MjgyMyIsInQiOjE3MDI4NDE4NDh9--0f89030f2d6fd1475dd9673dae1f49254acdd11ab8b20d9d6298ecd8a9777733" data-indicator-mode="none" data-tooltip-global="You have unread notifications" data-tooltip-unavailable="Notifications are unavailable at the moment." data-tooltip-none="You have no unread notifications" data-header-redesign-enabled="true" data-fetch-indicator-src="/notifications/indicator" data-fetch-indicator-enabled="true" data-view-component="true" class="js-socket-channel">
            <a id="AppHeader-notifications-button" href="/notifications" aria-label="Notifications" data-hotkey="g n" data-target="notification-indicator.link" data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;NOTIFICATIONS_HEADER&quot;,&quot;label&quot;:null}" data-view-component="true" class="Button Button--iconOnly Button--secondary Button--medium AppHeader-button  color-fg-muted">  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-inbox Button-visual">
            <path d="M2.8 2.06A1.75 1.75 0 0 1 4.41 1h7.18c.7 0 1.333.417 1.61 1.06l2.74 6.395c.04.093.06.194.06.295v4.5A1.75 1.75 0 0 1 14.25 15H1.75A1.75 1.75 0 0 1 0 13.25v-4.5c0-.101.02-.202.06-.295Zm1.61.44a.25.25 0 0 0-.23.152L1.887 8H4.75a.75.75 0 0 1 .6.3L6.625 10h2.75l1.275-1.7a.75.75 0 0 1 .6-.3h2.863L11.82 2.652a.25.25 0 0 0-.23-.152Zm10.09 7h-2.875l-1.275 1.7a.75.75 0 0 1-.6.3h-3.5a.75.75 0 0 1-.6-.3L4.375 9.5H1.5v3.75c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25Z"></path>
        </svg>
        </a>

            <tool-tip data-target="notification-indicator.tooltip" id="tooltip-b6fc20f1-d31e-4fc0-aac5-86bd797d4587" for="AppHeader-notifications-button" popover="manual" data-direction="s" data-type="description" data-view-component="true" class="sr-only position-absolute">Notifications</tool-tip>
        </notification-indicator>



                <div class="AppHeader-user">
                  <deferred-side-panel data-url="/_side-panels/user?repository_id=210716005">
          <include-fragment data-target="deferred-side-panel.fragment">
              <user-drawer-side-panel>
            <button aria-label="Open user account menu" data-action="click:deferred-side-panel#loadPanel click:deferred-side-panel#panelOpened" data-show-dialog-id="dialog-918ab256-dc76-4c82-9b93-0b0576e801d9" id="dialog-show-dialog-918ab256-dc76-4c82-9b93-0b0576e801d9" type="button" data-view-component="true" class="AppHeader-logo Button--invisible Button--medium Button Button--invisible-noVisuals color-bg-transparent p-0">  <span class="Button-content">
            <span class="Button-label"><img src="https://avatars.githubusercontent.com/u/1162823?v=4" alt="" size="32" height="32" width="32" data-view-component="true" class="avatar circle" /></span>
          </span>
        </button>

        <div class="Overlay--hidden Overlay-backdrop--side Overlay-backdrop--placement-right" data-modal-dialog-overlay>
          <modal-dialog data-target="deferred-side-panel.panel" role="dialog" id="dialog-918ab256-dc76-4c82-9b93-0b0576e801d9" aria-modal="true" aria-disabled="true" aria-labelledby="dialog-918ab256-dc76-4c82-9b93-0b0576e801d9-title" aria-describedby="dialog-918ab256-dc76-4c82-9b93-0b0576e801d9-description" data-view-component="true" class="Overlay Overlay-whenNarrow Overlay--size-small-portrait Overlay--motion-scaleFade SidePanel">
            <div styles="flex-direction: row;" data-view-component="true" class="Overlay-header">
          <div class="Overlay-headerContentWrap">
            <div class="Overlay-titleWrap">
              <h1 class="Overlay-title sr-only" id="dialog-918ab256-dc76-4c82-9b93-0b0576e801d9-title">
                Account menu
              </h1>
                    <div data-view-component="true" class="d-flex">
              <div data-view-component="true" class="AppHeader-logo position-relative">
                <img src="https://avatars.githubusercontent.com/u/1162823?v=4" alt="" size="32" height="32" width="32" data-view-component="true" class="avatar circle" />
        </div>        <div data-view-component="true" class="overflow-hidden d-flex width-full">        <div data-view-component="true" class="lh-condensed overflow-hidden d-flex flex-column flex-justify-center ml-2 f5 mr-auto width-full">
                  <span data-view-component="true" class="Truncate text-bold">
            <span data-view-component="true" class="Truncate-text">
                    dv00d00
        </span>
        </span>          <span data-view-component="true" class="Truncate color-fg-subtle">
            <span data-view-component="true" class="Truncate-text">
                    Dmitry Kushnir
        </span>
        </span></div>
                <action-menu data-select-variant="none" data-view-component="true" class="d-sm-none d-md-none d-lg-none">
          <focus-group direction="vertical" mnemonics retain>
            <button id="user-create-menu-button" popovertarget="user-create-menu-overlay" aria-label="Create something new" aria-controls="user-create-menu-list" aria-haspopup="true" type="button" data-view-component="true" class="AppHeader-button Button--secondary Button--small Button width-auto color-fg-muted">  <span class="Button-content">
              <span class="Button-visual Button-leadingVisual">
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-plus">
            <path d="M7.75 2a.75.75 0 0 1 .75.75V7h4.25a.75.75 0 0 1 0 1.5H8.5v4.25a.75.75 0 0 1-1.5 0V8.5H2.75a.75.75 0 0 1 0-1.5H7V2.75A.75.75 0 0 1 7.75 2Z"></path>
        </svg>
              </span>
            <span class="Button-label"><svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-triangle-down">
            <path d="m4.427 7.427 3.396 3.396a.25.25 0 0 0 .354 0l3.396-3.396A.25.25 0 0 0 11.396 7H4.604a.25.25 0 0 0-.177.427Z"></path>
        </svg></span>
          </span>
        </button><tool-tip id="tooltip-8748d152-94e8-4643-a36a-c2b9faf9febd" for="user-create-menu-button" popover="manual" data-direction="s" data-type="description" data-view-component="true" class="sr-only position-absolute">Create new...</tool-tip>


        <anchored-position id="user-create-menu-overlay" anchor="user-create-menu-button" align="end" side="outside-bottom" anchor-offset="normal" popover="auto" data-view-component="true">
          <div data-view-component="true" class="Overlay Overlay--size-auto">

              <div data-view-component="true" class="Overlay-body Overlay-body--paddingNone">          <div data-view-component="true">
          <ul aria-labelledby="user-create-menu-button" id="user-create-menu-list" role="menu" data-view-component="true" class="ActionListWrap--inset ActionListWrap">
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new repository&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/new" tabindex="-1" id="item-ac1dffbb-ee5d-42b3-b112-378b9a844603" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-repo">
            <path d="M2 2.5A2.5 2.5 0 0 1 4.5 0h8.75a.75.75 0 0 1 .75.75v12.5a.75.75 0 0 1-.75.75h-2.5a.75.75 0 0 1 0-1.5h1.75v-2h-8a1 1 0 0 0-.714 1.7.75.75 0 1 1-1.072 1.05A2.495 2.495 0 0 1 2 11.5Zm10.5-1h-8a1 1 0 0 0-1 1v6.708A2.486 2.486 0 0 1 4.5 9h8ZM5 12.25a.25.25 0 0 1 .25-.25h3.5a.25.25 0 0 1 .25.25v3.25a.25.25 0 0 1-.4.2l-1.45-1.087a.249.249 0 0 0-.3 0L5.4 15.7a.25.25 0 0 1-.4-.2Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                      New repository

        </span></a>


        </li>
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;import repository&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/new/import" tabindex="-1" id="item-75b4f6df-26f8-4a70-85c4-4a97db843427" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-repo-push">
            <path d="M1 2.5A2.5 2.5 0 0 1 3.5 0h8.75a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-1.5 0V1.5h-8a1 1 0 0 0-1 1v6.708A2.493 2.493 0 0 1 3.5 9h3.25a.75.75 0 0 1 0 1.5H3.5a1 1 0 0 0 0 2h5.75a.75.75 0 0 1 0 1.5H3.5A2.5 2.5 0 0 1 1 11.5Zm13.23 7.79h-.001l-1.224-1.224v6.184a.75.75 0 0 1-1.5 0V9.066L10.28 10.29a.75.75 0 0 1-1.06-1.061l2.505-2.504a.75.75 0 0 1 1.06 0L15.29 9.23a.751.751 0 0 1-.018 1.042.751.751 0 0 1-1.042.018Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        Import repository

        </span></a>


        </li>
              <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new codespace&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/codespaces/new" tabindex="-1" id="item-19070716-3cd1-4bea-b69f-5886f9692ae8" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-codespaces">
            <path d="M0 11.25c0-.966.784-1.75 1.75-1.75h12.5c.966 0 1.75.784 1.75 1.75v3A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm2-9.5C2 .784 2.784 0 3.75 0h8.5C13.216 0 14 .784 14 1.75v5a1.75 1.75 0 0 1-1.75 1.75h-8.5A1.75 1.75 0 0 1 2 6.75Zm1.75-.25a.25.25 0 0 0-.25.25v5c0 .138.112.25.25.25h8.5a.25.25 0 0 0 .25-.25v-5a.25.25 0 0 0-.25-.25Zm-2 9.5a.25.25 0 0 0-.25.25v3c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25v-3a.25.25 0 0 0-.25-.25Z"></path><path d="M7 12.75a.75.75 0 0 1 .75-.75h4.5a.75.75 0 0 1 0 1.5h-4.5a.75.75 0 0 1-.75-.75Zm-4 0a.75.75 0 0 1 .75-.75h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1-.75-.75Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        New codespace

        </span></a>


        </li>
              <li data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new gist&quot;}" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="https://gist.github.com/" tabindex="-1" id="item-59273c28-b047-46e6-8d56-fd8e89781e4e" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-code">
            <path d="m11.28 3.22 4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734L13.94 8l-3.72-3.72a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215Zm-6.56 0a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L2.06 8l3.72 3.72a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L.47 8.53a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        New gist

        </span></a>


        </li>
              <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>
              <li data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a href="/account/organizations/new" tabindex="-1" data-dont-follow-via-test="true" data-analytics-event="{&quot;category&quot;:&quot;SiteHeaderComponent&quot;,&quot;action&quot;:&quot;add_dropdown&quot;,&quot;label&quot;:&quot;new organization&quot;}" id="item-ec69b52a-3c5c-429f-80a3-d1d31d402fa5" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-organization">
            <path d="M1.75 16A1.75 1.75 0 0 1 0 14.25V1.75C0 .784.784 0 1.75 0h8.5C11.216 0 12 .784 12 1.75v12.5c0 .085-.006.168-.018.25h2.268a.25.25 0 0 0 .25-.25V8.285a.25.25 0 0 0-.111-.208l-1.055-.703a.749.749 0 1 1 .832-1.248l1.055.703c.487.325.779.871.779 1.456v5.965A1.75 1.75 0 0 1 14.25 16h-3.5a.766.766 0 0 1-.197-.026c-.099.017-.2.026-.303.026h-3a.75.75 0 0 1-.75-.75V14h-1v1.25a.75.75 0 0 1-.75.75Zm-.25-1.75c0 .138.112.25.25.25H4v-1.25a.75.75 0 0 1 .75-.75h2.5a.75.75 0 0 1 .75.75v1.25h2.25a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25ZM3.75 6h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5ZM3 3.75A.75.75 0 0 1 3.75 3h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 3 3.75Zm4 3A.75.75 0 0 1 7.75 6h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 7 6.75ZM7.75 3h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5ZM3 9.75A.75.75 0 0 1 3.75 9h.5a.75.75 0 0 1 0 1.5h-.5A.75.75 0 0 1 3 9.75ZM7.75 9h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1 0-1.5Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                        New organization

        </span></a>


        </li>
        </ul>
        </div>

        </div>

        </div></anchored-position>  </focus-group>
        </action-menu>

        </div>
        </div>
            </div>
            <div class="Overlay-actionWrap">
              <button data-close-dialog-id="dialog-918ab256-dc76-4c82-9b93-0b0576e801d9" aria-label="Close" type="button" data-view-component="true" class="close-button Overlay-closeButton"><svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg></button>
            </div>
          </div>
        </div>
              <div data-view-component="true" class="Overlay-body d-flex flex-column px-2">    <div data-view-component="true" class="d-flex flex-column mb-3">
                <nav aria-label="User navigation" data-view-component="true" class="ActionList">

          <nav-list>
            <ul data-view-component="true" class="ActionListWrap">


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <button id="item-96e21e7c-e276-45d0-8211-f959bdfb7bcd" type="button" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <span data-view-component="true" class="d-flex flex-items-center">    <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="16" height="16" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
        </span>
                </span>

                <span data-view-component="true" class="ActionListItem-label">


          <span class="color-fg-muted">
            Loading...
          </span>

        </span></button>


        </li>


                  <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;PROFILE&quot;,&quot;label&quot;:null}" id="item-cfe229cd-52ad-4bd2-998f-0fac62651365" href="https://github.com/dv00d00" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-person">
            <path d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0ZM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Your profile
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <button id="item-d485be51-9168-4f8e-ba8b-1e546c3be52b" type="button" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <span data-view-component="true" class="d-flex flex-items-center">    <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="16" height="16" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
        </span>
                </span>

                <span data-view-component="true" class="ActionListItem-label">


          <span class="color-fg-muted">
            Loading...
          </span>

        </span></button>


        </li>


                  <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;YOUR_REPOSITORIES&quot;,&quot;label&quot;:null}" id="item-33959740-59df-4508-b249-e0efac1d3f3b" href="/dv00d00?tab=repositories" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-repo">
            <path d="M2 2.5A2.5 2.5 0 0 1 4.5 0h8.75a.75.75 0 0 1 .75.75v12.5a.75.75 0 0 1-.75.75h-2.5a.75.75 0 0 1 0-1.5h1.75v-2h-8a1 1 0 0 0-.714 1.7.75.75 0 1 1-1.072 1.05A2.495 2.495 0 0 1 2 11.5Zm10.5-1h-8a1 1 0 0 0-1 1v6.708A2.486 2.486 0 0 1 4.5 9h8ZM5 12.25a.25.25 0 0 1 .25-.25h3.5a.25.25 0 0 1 .25.25v3.25a.25.25 0 0 1-.4.2l-1.45-1.087a.249.249 0 0 0-.3 0L5.4 15.7a.25.25 0 0 1-.4-.2Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Your repositories
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;YOUR_PROJECTS&quot;,&quot;label&quot;:null}" id="item-492d1c5b-6d53-43bb-87b1-586a5288f751" href="/dv00d00?tab=projects" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-project">
            <path d="M1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25V1.75C0 .784.784 0 1.75 0ZM1.5 1.75v12.5c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25H1.75a.25.25 0 0 0-.25.25ZM11.75 3a.75.75 0 0 1 .75.75v7.5a.75.75 0 0 1-1.5 0v-7.5a.75.75 0 0 1 .75-.75Zm-8.25.75a.75.75 0 0 1 1.5 0v5.5a.75.75 0 0 1-1.5 0ZM8 3a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-1.5 0v-3.5A.75.75 0 0 1 8 3Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Your projects
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <button id="item-9c09f138-fa97-42bc-a4f5-a8c49b2fe5f5" type="button" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <span data-view-component="true" class="d-flex flex-items-center">    <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="16" height="16" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
        </span>
                </span>

                <span data-view-component="true" class="ActionListItem-label">


          <span class="color-fg-muted">
            Loading...
          </span>

        </span></button>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;YOUR_STARS&quot;,&quot;label&quot;:null}" id="item-7410157b-7277-4cac-9a1b-8195dd1e0f48" href="/dv00d00?tab=stars" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-star">
            <path d="M8 .25a.75.75 0 0 1 .673.418l1.882 3.815 4.21.612a.75.75 0 0 1 .416 1.279l-3.046 2.97.719 4.192a.751.751 0 0 1-1.088.791L8 12.347l-3.766 1.98a.75.75 0 0 1-1.088-.79l.72-4.194L.818 6.374a.75.75 0 0 1 .416-1.28l4.21-.611L7.327.668A.75.75 0 0 1 8 .25Zm0 2.445L6.615 5.5a.75.75 0 0 1-.564.41l-3.097.45 2.24 2.184a.75.75 0 0 1 .216.664l-.528 3.084 2.769-1.456a.75.75 0 0 1 .698 0l2.77 1.456-.53-3.084a.75.75 0 0 1 .216-.664l2.24-2.183-3.096-.45a.75.75 0 0 1-.564-.41L8 2.694Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Your stars
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;SPONSORS&quot;,&quot;label&quot;:null}" id="item-bab13c7d-66c9-4c25-a478-a76963e011f9" href="/sponsors/accounts" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-heart">
            <path d="m8 14.25.345.666a.75.75 0 0 1-.69 0l-.008-.004-.018-.01a7.152 7.152 0 0 1-.31-.17 22.055 22.055 0 0 1-3.434-2.414C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002ZM4.25 2.5c-1.336 0-2.75 1.164-2.75 3 0 2.15 1.58 4.144 3.365 5.682A20.58 20.58 0 0 0 8 13.393a20.58 20.58 0 0 0 3.135-2.211C12.92 9.644 14.5 7.65 14.5 5.5c0-1.836-1.414-3-2.75-3-1.373 0-2.609.986-3.029 2.456a.749.749 0 0 1-1.442 0C6.859 3.486 5.623 2.5 4.25 2.5Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Your sponsors
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;YOUR_GISTS&quot;,&quot;label&quot;:null}" id="item-151aa308-2c02-46c5-b53e-52e72cfba02b" href="https://gist.github.com/mine" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-code-square">
            <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm1.75-.25a.25.25 0 0 0-.25.25v12.5c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25Zm7.47 3.97a.75.75 0 0 1 1.06 0l2 2a.75.75 0 0 1 0 1.06l-2 2a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734L10.69 8 9.22 6.53a.75.75 0 0 1 0-1.06ZM6.78 6.53 5.31 8l1.47 1.47a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215l-2-2a.75.75 0 0 1 0-1.06l2-2a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Your gists
        </span></a>


        </li>


                  <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <button id="item-c5567de5-d791-42ea-ba46-bc0d27781219" type="button" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <span data-view-component="true" class="d-flex flex-items-center">    <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="16" height="16" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
        </span>
                </span>

                <span data-view-component="true" class="ActionListItem-label">


          <span class="color-fg-muted">
            Loading...
          </span>

        </span></button>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <button id="item-e85b179f-2ad3-4a75-a9f4-0b3ebb653161" type="button" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <span data-view-component="true" class="d-flex flex-items-center">    <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="16" height="16" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
        </span>
                </span>

                <span data-view-component="true" class="ActionListItem-label">


          <span class="color-fg-muted">
            Loading...
          </span>

        </span></button>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <button id="item-6453fd00-aef8-4d3e-a081-fc80fc40c102" type="button" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <span data-view-component="true" class="d-flex flex-items-center">    <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="16" height="16" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
        </span>
                </span>

                <span data-view-component="true" class="ActionListItem-label">


          <span class="color-fg-muted">
            Loading...
          </span>

        </span></button>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;SETTINGS&quot;,&quot;label&quot;:null}" id="item-a5a284e3-de26-4510-b46b-b5acd9ae744c" href="/settings/profile" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-gear">
            <path d="M8 0a8.2 8.2 0 0 1 .701.031C9.444.095 9.99.645 10.16 1.29l.288 1.107c.018.066.079.158.212.224.231.114.454.243.668.386.123.082.233.09.299.071l1.103-.303c.644-.176 1.392.021 1.82.63.27.385.506.792.704 1.218.315.675.111 1.422-.364 1.891l-.814.806c-.049.048-.098.147-.088.294.016.257.016.515 0 .772-.01.147.038.246.088.294l.814.806c.475.469.679 1.216.364 1.891a7.977 7.977 0 0 1-.704 1.217c-.428.61-1.176.807-1.82.63l-1.102-.302c-.067-.019-.177-.011-.3.071a5.909 5.909 0 0 1-.668.386c-.133.066-.194.158-.211.224l-.29 1.106c-.168.646-.715 1.196-1.458 1.26a8.006 8.006 0 0 1-1.402 0c-.743-.064-1.289-.614-1.458-1.26l-.289-1.106c-.018-.066-.079-.158-.212-.224a5.738 5.738 0 0 1-.668-.386c-.123-.082-.233-.09-.299-.071l-1.103.303c-.644.176-1.392-.021-1.82-.63a8.12 8.12 0 0 1-.704-1.218c-.315-.675-.111-1.422.363-1.891l.815-.806c.05-.048.098-.147.088-.294a6.214 6.214 0 0 1 0-.772c.01-.147-.038-.246-.088-.294l-.815-.806C.635 6.045.431 5.298.746 4.623a7.92 7.92 0 0 1 .704-1.217c.428-.61 1.176-.807 1.82-.63l1.102.302c.067.019.177.011.3-.071.214-.143.437-.272.668-.386.133-.066.194-.158.211-.224l.29-1.106C6.009.645 6.556.095 7.299.03 7.53.01 7.764 0 8 0Zm-.571 1.525c-.036.003-.108.036-.137.146l-.289 1.105c-.147.561-.549.967-.998 1.189-.173.086-.34.183-.5.29-.417.278-.97.423-1.529.27l-1.103-.303c-.109-.03-.175.016-.195.045-.22.312-.412.644-.573.99-.014.031-.021.11.059.19l.815.806c.411.406.562.957.53 1.456a4.709 4.709 0 0 0 0 .582c.032.499-.119 1.05-.53 1.456l-.815.806c-.081.08-.073.159-.059.19.162.346.353.677.573.989.02.03.085.076.195.046l1.102-.303c.56-.153 1.113-.008 1.53.27.161.107.328.204.501.29.447.222.85.629.997 1.189l.289 1.105c.029.109.101.143.137.146a6.6 6.6 0 0 0 1.142 0c.036-.003.108-.036.137-.146l.289-1.105c.147-.561.549-.967.998-1.189.173-.086.34-.183.5-.29.417-.278.97-.423 1.529-.27l1.103.303c.109.029.175-.016.195-.045.22-.313.411-.644.573-.99.014-.031.021-.11-.059-.19l-.815-.806c-.411-.406-.562-.957-.53-1.456a4.709 4.709 0 0 0 0-.582c-.032-.499.119-1.05.53-1.456l.815-.806c.081-.08.073-.159.059-.19a6.464 6.464 0 0 0-.573-.989c-.02-.03-.085-.076-.195-.046l-1.102.303c-.56.153-1.113.008-1.53-.27a4.44 4.44 0 0 0-.501-.29c-.447-.222-.85-.629-.997-1.189l-.289-1.105c-.029-.11-.101-.143-.137-.146a6.6 6.6 0 0 0-1.142 0ZM11 8a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM9.5 8a1.5 1.5 0 1 0-3.001.001A1.5 1.5 0 0 0 9.5 8Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Settings
        </span></a>


        </li>


                  <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;DOCS&quot;,&quot;label&quot;:null}" id="item-6d34b426-6e28-4fbe-91af-7ce093ceaa10" href="https://docs.github.com" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-book">
            <path d="M0 1.75A.75.75 0 0 1 .75 1h4.253c1.227 0 2.317.59 3 1.501A3.743 3.743 0 0 1 11.006 1h4.245a.75.75 0 0 1 .75.75v10.5a.75.75 0 0 1-.75.75h-4.507a2.25 2.25 0 0 0-1.591.659l-.622.621a.75.75 0 0 1-1.06 0l-.622-.621A2.25 2.25 0 0 0 5.258 13H.75a.75.75 0 0 1-.75-.75Zm7.251 10.324.004-5.073-.002-2.253A2.25 2.25 0 0 0 5.003 2.5H1.5v9h3.757a3.75 3.75 0 0 1 1.994.574ZM8.755 4.75l-.004 7.322a3.752 3.752 0 0 1 1.992-.572H14.5v-9h-3.495a2.25 2.25 0 0 0-2.25 2.25Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  GitHub Docs
        </span></a>


        </li>



        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;SUPPORT&quot;,&quot;label&quot;:null}" id="item-1ebf2c0e-9188-479b-897c-14256450989e" href="https://support.github.com" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-people">
            <path d="M2 5.5a3.5 3.5 0 1 1 5.898 2.549 5.508 5.508 0 0 1 3.034 4.084.75.75 0 1 1-1.482.235 4 4 0 0 0-7.9 0 .75.75 0 0 1-1.482-.236A5.507 5.507 0 0 1 3.102 8.05 3.493 3.493 0 0 1 2 5.5ZM11 4a3.001 3.001 0 0 1 2.22 5.018 5.01 5.01 0 0 1 2.56 3.012.749.749 0 0 1-.885.954.752.752 0 0 1-.549-.514 3.507 3.507 0 0 0-2.522-2.372.75.75 0 0 1-.574-.73v-.352a.75.75 0 0 1 .416-.672A1.5 1.5 0 0 0 11 5.5.75.75 0 0 1 11 4Zm-5.5-.5a2 2 0 1 0-.001 3.999A2 2 0 0 0 5.5 3.5Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  GitHub Support
        </span></a>


        </li>


                  <li role="presentation" aria-hidden="true" data-view-component="true" class="ActionList-sectionDivider"></li>


        <li data-targets="nav-list.items nav-list.items" data-item-id="" data-view-component="true" class="ActionListItem">

            <a data-analytics-event="{&quot;category&quot;:&quot;Global navigation&quot;,&quot;action&quot;:&quot;LOGOUT&quot;,&quot;label&quot;:null}" id="item-54eaf434-0a3d-481a-b0f5-cc6393ae09b0" href="/logout" data-view-component="true" class="ActionListContent">

                <span data-view-component="true" class="ActionListItem-label">
                  Sign out
        </span></a>


        </li>

        </ul>  </nav-list>
        </nav>


        </div>
        </div>

        </modal-dialog></div>
          </user-drawer-side-panel>

          </include-fragment>
        </deferred-side-panel>

                </div>

                <div class="position-absolute mt-2">

        <site-header-logged-in-user-menu>

        </site-header-logged-in-user-menu>

                </div>
              </div>
            </div>


              <div class="AppHeader-localBar" >
                <nav data-pjax="#js-repo-pjax-container" aria-label="Repository" data-view-component="true" class="js-repo-nav js-sidenav-container-pjax js-responsive-underlinenav overflow-hidden UnderlineNav">

          <ul data-view-component="true" class="UnderlineNav-body list-style-none">
              <li data-view-component="true" class="d-inline-flex">
          <a id="code-tab" href="/dotnet/runtime" data-tab-item="i0code-tab" data-selected-links="repo_source repo_downloads repo_commits repo_releases repo_tags repo_branches repo_packages repo_deployments repo_attestations /dotnet/runtime" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-hotkey="g c" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Code&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-code UnderlineNav-octicon d-none d-sm-inline">
            <path d="m11.28 3.22 4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734L13.94 8l-3.72-3.72a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215Zm-6.56 0a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L2.06 8l3.72 3.72a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L.47 8.53a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
                <span data-content="Code">Code</span>
                  <span id="code-repo-tab-count" data-pjax-replace="" data-turbo-replace="" title="Not available" data-view-component="true" class="Counter"></span>



        </a></li>
              <li data-view-component="true" class="d-inline-flex">
          <a id="issues-tab" href="/dotnet/runtime/issues" data-tab-item="i1issues-tab" data-selected-links="repo_issues repo_labels repo_milestones /dotnet/runtime/issues" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-hotkey="g i" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Issues&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-issue-opened UnderlineNav-octicon d-none d-sm-inline">
            <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path><path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Z"></path>
        </svg>
                <span data-content="Issues">Issues</span>
                  <span id="issues-repo-tab-count" data-pjax-replace="" data-turbo-replace="" title="5,000+" data-view-component="true" class="Counter">5k+</span>



        </a></li>
              <li data-view-component="true" class="d-inline-flex">
          <a id="pull-requests-tab" href="/dotnet/runtime/pulls" data-tab-item="i2pull-requests-tab" data-selected-links="repo_pulls checks /dotnet/runtime/pulls" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-hotkey="g p" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Pull requests&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-git-pull-request UnderlineNav-octicon d-none d-sm-inline">
            <path d="M1.5 3.25a2.25 2.25 0 1 1 3 2.122v5.256a2.251 2.251 0 1 1-1.5 0V5.372A2.25 2.25 0 0 1 1.5 3.25Zm5.677-.177L9.573.677A.25.25 0 0 1 10 .854V2.5h1A2.5 2.5 0 0 1 13.5 5v5.628a2.251 2.251 0 1 1-1.5 0V5a1 1 0 0 0-1-1h-1v1.646a.25.25 0 0 1-.427.177L7.177 3.427a.25.25 0 0 1 0-.354ZM3.75 2.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm0 9.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm8.25.75a.75.75 0 1 0 1.5 0 .75.75 0 0 0-1.5 0Z"></path>
        </svg>
                <span data-content="Pull requests">Pull requests</span>
                  <span id="pull-requests-repo-tab-count" data-pjax-replace="" data-turbo-replace="" title="228" data-view-component="true" class="Counter">228</span>



        </a></li>
              <li data-view-component="true" class="d-inline-flex">
          <a id="discussions-tab" href="/dotnet/runtime/discussions" data-tab-item="i3discussions-tab" data-selected-links="repo_discussions /dotnet/runtime/discussions" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-hotkey="g g" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Discussions&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-comment-discussion UnderlineNav-octicon d-none d-sm-inline">
            <path d="M1.75 1h8.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 10.25 10H7.061l-2.574 2.573A1.458 1.458 0 0 1 2 11.543V10h-.25A1.75 1.75 0 0 1 0 8.25v-5.5C0 1.784.784 1 1.75 1ZM1.5 2.75v5.5c0 .138.112.25.25.25h1a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h3.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25Zm13 2a.25.25 0 0 0-.25-.25h-.5a.75.75 0 0 1 0-1.5h.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 14.25 12H14v1.543a1.458 1.458 0 0 1-2.487 1.03L9.22 12.28a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215l2.22 2.22v-2.19a.75.75 0 0 1 .75-.75h1a.25.25 0 0 0 .25-.25Z"></path>
        </svg>
                <span data-content="Discussions">Discussions</span>
                  <span id="discussions-repo-tab-count" data-pjax-replace="" data-turbo-replace="" title="Not available" data-view-component="true" class="Counter"></span>



        </a></li>
              <li data-view-component="true" class="d-inline-flex">
          <a id="actions-tab" href="/dotnet/runtime/actions" data-tab-item="i4actions-tab" data-selected-links="repo_actions /dotnet/runtime/actions" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-hotkey="g a" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Actions&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-play UnderlineNav-octicon d-none d-sm-inline">
            <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm4.879-2.773 4.264 2.559a.25.25 0 0 1 0 .428l-4.264 2.559A.25.25 0 0 1 6 10.559V5.442a.25.25 0 0 1 .379-.215Z"></path>
        </svg>
                <span data-content="Actions">Actions</span>
                  <span id="actions-repo-tab-count" data-pjax-replace="" data-turbo-replace="" title="Not available" data-view-component="true" class="Counter"></span>



        </a></li>
              <li data-view-component="true" class="d-inline-flex">
          <a id="projects-tab" href="/dotnet/runtime/projects" data-tab-item="i5projects-tab" data-selected-links="repo_projects new_repo_project repo_project /dotnet/runtime/projects" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-hotkey="g b" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Projects&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-table UnderlineNav-octicon d-none d-sm-inline">
            <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25ZM6.5 6.5v8h7.75a.25.25 0 0 0 .25-.25V6.5Zm8-1.5V1.75a.25.25 0 0 0-.25-.25H6.5V5Zm-13 1.5v7.75c0 .138.112.25.25.25H5v-8ZM5 5V1.5H1.75a.25.25 0 0 0-.25.25V5Z"></path>
        </svg>
                <span data-content="Projects">Projects</span>
                  <span id="projects-repo-tab-count" data-pjax-replace="" data-turbo-replace="" title="38" data-view-component="true" class="Counter">38</span>



        </a></li>
              <li data-view-component="true" class="d-inline-flex">
          <a id="security-tab" href="/dotnet/runtime/security" data-tab-item="i6security-tab" data-selected-links="security overview alerts policy token_scanning code_scanning /dotnet/runtime/security" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-hotkey="g s" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Security&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-shield UnderlineNav-octicon d-none d-sm-inline">
            <path d="M7.467.133a1.748 1.748 0 0 1 1.066 0l5.25 1.68A1.75 1.75 0 0 1 15 3.48V7c0 1.566-.32 3.182-1.303 4.682-.983 1.498-2.585 2.813-5.032 3.855a1.697 1.697 0 0 1-1.33 0c-2.447-1.042-4.049-2.357-5.032-3.855C1.32 10.182 1 8.566 1 7V3.48a1.75 1.75 0 0 1 1.217-1.667Zm.61 1.429a.25.25 0 0 0-.153 0l-5.25 1.68a.25.25 0 0 0-.174.238V7c0 1.358.275 2.666 1.057 3.86.784 1.194 2.121 2.34 4.366 3.297a.196.196 0 0 0 .154 0c2.245-.956 3.582-2.104 4.366-3.298C13.225 9.666 13.5 8.36 13.5 7V3.48a.251.251 0 0 0-.174-.237l-5.25-1.68ZM8.75 4.75v3a.75.75 0 0 1-1.5 0v-3a.75.75 0 0 1 1.5 0ZM9 10.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                <span data-content="Security">Security</span>
                  <include-fragment src="/dotnet/runtime/security/overall-count" accept="text/fragment+html"></include-fragment>


        </a></li>
              <li data-view-component="true" class="d-inline-flex">
          <a id="insights-tab" href="/dotnet/runtime/pulse" data-tab-item="i7insights-tab" data-selected-links="repo_graphs repo_contributors dependency_graph dependabot_updates pulse people community /dotnet/runtime/pulse" data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" data-analytics-event="{&quot;category&quot;:&quot;Underline navbar&quot;,&quot;action&quot;:&quot;Click tab&quot;,&quot;label&quot;:&quot;Insights&quot;,&quot;target&quot;:&quot;UNDERLINE_NAV.TAB&quot;}" data-view-component="true" class="UnderlineNav-item no-wrap js-responsive-underlinenav-item js-selected-navigation-item">

                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-graph UnderlineNav-octicon d-none d-sm-inline">
            <path d="M1.5 1.75V13.5h13.75a.75.75 0 0 1 0 1.5H.75a.75.75 0 0 1-.75-.75V1.75a.75.75 0 0 1 1.5 0Zm14.28 2.53-5.25 5.25a.75.75 0 0 1-1.06 0L7 7.06 4.28 9.78a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042l3.25-3.25a.75.75 0 0 1 1.06 0L10 7.94l4.72-4.72a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042Z"></path>
        </svg>
                <span data-content="Insights">Insights</span>
                  <span id="insights-repo-tab-count" data-pjax-replace="" data-turbo-replace="" title="Not available" data-view-component="true" class="Counter"></span>



        </a></li>
        </ul>
            <div style="visibility:hidden;" data-view-component="true" class="UnderlineNav-actions js-responsive-underlinenav-overflow position-absolute pr-3 pr-md-4 pr-lg-5 right-0">      <action-menu data-select-variant="none" data-view-component="true">
          <focus-group direction="vertical" mnemonics retain>
            <button id="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-button" popovertarget="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-overlay" aria-controls="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-list" aria-haspopup="true" aria-labelledby="tooltip-36963277-4fe7-4963-9c43-295eb4087203" type="button" data-view-component="true" class="Button Button--iconOnly Button--secondary Button--medium UnderlineNav-item">  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal Button-visual">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg>
        </button><tool-tip id="tooltip-36963277-4fe7-4963-9c43-295eb4087203" for="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-button" popover="manual" data-direction="s" data-type="label" data-view-component="true" class="sr-only position-absolute">Additional navigation options</tool-tip>


        <anchored-position id="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-overlay" anchor="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-button" align="start" side="outside-bottom" anchor-offset="normal" popover="auto" data-view-component="true">
          <div data-view-component="true" class="Overlay Overlay--size-auto">

              <div data-view-component="true" class="Overlay-body Overlay-body--paddingNone">          <div data-view-component="true">
          <ul aria-labelledby="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-button" id="action-menu-2db79aa7-0377-4d9c-a6f9-203edf87e504-list" role="menu" data-view-component="true" class="ActionListWrap--inset ActionListWrap">
              <li hidden="hidden" data-menu-item="i0code-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-67acb07a-171c-49bf-9518-2b232a379d6a" href="/dotnet/runtime" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-code">
            <path d="m11.28 3.22 4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734L13.94 8l-3.72-3.72a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215Zm-6.56 0a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L2.06 8l3.72 3.72a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L.47 8.53a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Code
        </span></a>


        </li>
              <li hidden="hidden" data-menu-item="i1issues-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-1bd85c1f-ce39-4ada-8277-54546b7f0e63" href="/dotnet/runtime/issues" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-issue-opened">
            <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path><path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Issues
        </span></a>


        </li>
              <li hidden="hidden" data-menu-item="i2pull-requests-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-893203d1-3aeb-4226-8866-7e8bb6eccfe9" href="/dotnet/runtime/pulls" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-git-pull-request">
            <path d="M1.5 3.25a2.25 2.25 0 1 1 3 2.122v5.256a2.251 2.251 0 1 1-1.5 0V5.372A2.25 2.25 0 0 1 1.5 3.25Zm5.677-.177L9.573.677A.25.25 0 0 1 10 .854V2.5h1A2.5 2.5 0 0 1 13.5 5v5.628a2.251 2.251 0 1 1-1.5 0V5a1 1 0 0 0-1-1h-1v1.646a.25.25 0 0 1-.427.177L7.177 3.427a.25.25 0 0 1 0-.354ZM3.75 2.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm0 9.5a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm8.25.75a.75.75 0 1 0 1.5 0 .75.75 0 0 0-1.5 0Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Pull requests
        </span></a>


        </li>
              <li hidden="hidden" data-menu-item="i3discussions-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-1392487f-b000-46c3-a80b-e473f06f61cb" href="/dotnet/runtime/discussions" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-comment-discussion">
            <path d="M1.75 1h8.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 10.25 10H7.061l-2.574 2.573A1.458 1.458 0 0 1 2 11.543V10h-.25A1.75 1.75 0 0 1 0 8.25v-5.5C0 1.784.784 1 1.75 1ZM1.5 2.75v5.5c0 .138.112.25.25.25h1a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h3.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25Zm13 2a.25.25 0 0 0-.25-.25h-.5a.75.75 0 0 1 0-1.5h.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 14.25 12H14v1.543a1.458 1.458 0 0 1-2.487 1.03L9.22 12.28a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215l2.22 2.22v-2.19a.75.75 0 0 1 .75-.75h1a.25.25 0 0 0 .25-.25Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Discussions
        </span></a>


        </li>
              <li hidden="hidden" data-menu-item="i4actions-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-c85eca01-ca46-40f1-851d-046d064d5cdb" href="/dotnet/runtime/actions" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-play">
            <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm4.879-2.773 4.264 2.559a.25.25 0 0 1 0 .428l-4.264 2.559A.25.25 0 0 1 6 10.559V5.442a.25.25 0 0 1 .379-.215Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Actions
        </span></a>


        </li>
              <li hidden="hidden" data-menu-item="i5projects-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-2db9e4cc-55bb-4d5c-822b-4363e9f89861" href="/dotnet/runtime/projects" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-table">
            <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25ZM6.5 6.5v8h7.75a.25.25 0 0 0 .25-.25V6.5Zm8-1.5V1.75a.25.25 0 0 0-.25-.25H6.5V5Zm-13 1.5v7.75c0 .138.112.25.25.25H5v-8ZM5 5V1.5H1.75a.25.25 0 0 0-.25.25V5Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Projects
        </span></a>


        </li>
              <li hidden="hidden" data-menu-item="i6security-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-dc9b8d37-d5fa-4c9f-adf2-09a41e4af8fc" href="/dotnet/runtime/security" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-shield">
            <path d="M7.467.133a1.748 1.748 0 0 1 1.066 0l5.25 1.68A1.75 1.75 0 0 1 15 3.48V7c0 1.566-.32 3.182-1.303 4.682-.983 1.498-2.585 2.813-5.032 3.855a1.697 1.697 0 0 1-1.33 0c-2.447-1.042-4.049-2.357-5.032-3.855C1.32 10.182 1 8.566 1 7V3.48a1.75 1.75 0 0 1 1.217-1.667Zm.61 1.429a.25.25 0 0 0-.153 0l-5.25 1.68a.25.25 0 0 0-.174.238V7c0 1.358.275 2.666 1.057 3.86.784 1.194 2.121 2.34 4.366 3.297a.196.196 0 0 0 .154 0c2.245-.956 3.582-2.104 4.366-3.298C13.225 9.666 13.5 8.36 13.5 7V3.48a.251.251 0 0 0-.174-.237l-5.25-1.68ZM8.75 4.75v3a.75.75 0 0 1-1.5 0v-3a.75.75 0 0 1 1.5 0ZM9 10.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Security
        </span></a>


        </li>
              <li hidden="hidden" data-menu-item="i7insights-tab" data-targets="action-list.items action-list.items" role="none" data-view-component="true" class="ActionListItem">

            <a tabindex="-1" id="item-6177a5a7-a57e-4f5c-ba20-fae4b73816a3" href="/dotnet/runtime/pulse" role="menuitem" data-view-component="true" class="ActionListContent ActionListContent--visual16">
                <span class="ActionListItem-visual ActionListItem-visual--leading">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-graph">
            <path d="M1.5 1.75V13.5h13.75a.75.75 0 0 1 0 1.5H.75a.75.75 0 0 1-.75-.75V1.75a.75.75 0 0 1 1.5 0Zm14.28 2.53-5.25 5.25a.75.75 0 0 1-1.06 0L7 7.06 4.28 9.78a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042l3.25-3.25a.75.75 0 0 1 1.06 0L10 7.94l4.72-4.72a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042Z"></path>
        </svg>
                </span>

                <span data-view-component="true" class="ActionListItem-label">
                  Insights
        </span></a>


        </li>
        </ul>
        </div>

        </div>

        </div></anchored-position>  </focus-group>
        </action-menu></div>
        </nav>
              </div>
        </header>


              <div hidden="hidden" data-view-component="true" class="js-stale-session-flash stale-session-flash flash flash-warn flash-full mb-3">

                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                <span class="js-stale-session-flash-signed-in" hidden>You signed in with another tab or window. <a class="Link--inTextBlock" href="">Reload</a> to refresh your session.</span>
                <span class="js-stale-session-flash-signed-out" hidden>You signed out in another tab or window. <a class="Link--inTextBlock" href="">Reload</a> to refresh your session.</span>
                <span class="js-stale-session-flash-switched" hidden>You switched accounts on another tab or window. <a class="Link--inTextBlock" href="">Reload</a> to refresh your session.</span>

            <button id="icon-button-f916388f-8793-446e-bc6a-66adde78df8d" aria-labelledby="tooltip-ba29a6dd-c7b1-455f-be11-20c922787b96" type="button" data-view-component="true" class="Button Button--iconOnly Button--invisible Button--medium flash-close js-flash-close">  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x Button-visual">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
        </button><tool-tip id="tooltip-ba29a6dd-c7b1-455f-be11-20c922787b96" for="icon-button-f916388f-8793-446e-bc6a-66adde78df8d" popover="manual" data-direction="s" data-type="label" data-view-component="true" class="sr-only position-absolute">Dismiss alert</tool-tip>



        </div>

            </div>

          <div id="start-of-content" class="show-on-focus"></div>








            <div id="js-flash-container" data-turbo-replace>


              <include-fragment src="/settings/two_factor_authentication/holiday_warning_banner"></include-fragment>



          <template class="js-flash-template">

        <div class="flash flash-full   {{ className }}">
          <div class="px-2" >
            <button autofocus class="flash-close js-flash-close" type="button" aria-label="Dismiss this message">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
            </button>
            <div aria-atomic="true" role="alert" class="js-flash-alert">

              <div>{{ message }}</div>

            </div>
          </div>
        </div>
          </template>
        </div>



            <notification-shelf-watcher data-base-url="https://github.com/notifications/beta/shelf" data-channel="eyJjIjoibm90aWZpY2F0aW9uLWNoYW5nZWQ6MTE2MjgyMyIsInQiOjE3MDI4NDE4NDh9--0f89030f2d6fd1475dd9673dae1f49254acdd11ab8b20d9d6298ecd8a9777733" data-view-component="true" class="js-socket-channel"></notification-shelf-watcher>
          <div hidden data-initial data-target="notification-shelf-watcher.placeholder"></div>






              <details
          class="details-reset details-overlay details-overlay-dark js-command-palette-dialog"
          id="command-palette-pjax-container"
          data-turbo-replace
        >
          <summary aria-label="Command palette trigger" tabindex="-1"></summary>
          <details-dialog class="command-palette-details-dialog d-flex flex-column flex-justify-center height-fit" aria-label="Command palette">
            <command-palette
              class="command-palette color-bg-default rounded-3 border color-shadow-small"
              return-to=/_view_fragments/issues/show/dotnet/runtime/30402/issue_layout
              user-id="1162823"
              activation-hotkey="Mod+k,Mod+Alt+k"
              command-mode-hotkey="Mod+Shift+K"
              data-action="
                command-palette-input-ready:command-palette#inputReady
                command-palette-page-stack-updated:command-palette#updateInputScope
                itemsUpdated:command-palette#itemsUpdated
                keydown:command-palette#onKeydown
                loadingStateChanged:command-palette#loadingStateChanged
                selectedItemChanged:command-palette#selectedItemChanged
                pageFetchError:command-palette#pageFetchError
              ">

                <command-palette-mode
                  data-char="#"
                    data-scope-types="[&quot;&quot;]"
                    data-placeholder="Search issues and pull requests"
                ></command-palette-mode>
                <command-palette-mode
                  data-char="#"
                    data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                    data-placeholder="Search issues, pull requests, discussions, and projects"
                ></command-palette-mode>
                <command-palette-mode
                  data-char="!"
                    data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                    data-placeholder="Search projects"
                ></command-palette-mode>
                <command-palette-mode
                  data-char="@"
                    data-scope-types="[&quot;&quot;]"
                    data-placeholder="Search or jump to a user, organization, or repository"
                ></command-palette-mode>
                <command-palette-mode
                  data-char="@"
                    data-scope-types="[&quot;owner&quot;]"
                    data-placeholder="Search or jump to a repository"
                ></command-palette-mode>
                <command-palette-mode
                  data-char="/"
                    data-scope-types="[&quot;repository&quot;]"
                    data-placeholder="Search files"
                ></command-palette-mode>
                <command-palette-mode
                  data-char="?"
                ></command-palette-mode>
                <command-palette-mode
                  data-char="&gt;"
                    data-placeholder="Run a command"
                ></command-palette-mode>
                <command-palette-mode
                  data-char=""
                    data-scope-types="[&quot;&quot;]"
                    data-placeholder="Search or jump to..."
                ></command-palette-mode>
                <command-palette-mode
                  data-char=""
                    data-scope-types="[&quot;owner&quot;]"
                    data-placeholder="Search or jump to..."
                ></command-palette-mode>
              <command-palette-mode
                class="js-command-palette-default-mode"
                data-char=""
                data-placeholder="Search or jump to..."
              ></command-palette-mode>

              <command-palette-input placeholder="Search or jump to..."

                data-action="
                  command-palette-input:command-palette#onInput
                  command-palette-select:command-palette#onSelect
                  command-palette-descope:command-palette#onDescope
                  command-palette-cleared:command-palette#onInputClear
                "
              >
                <div class="js-search-icon d-flex flex-items-center mr-2" style="height: 26px">
                  <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-search color-fg-muted">
            <path d="M10.68 11.74a6 6 0 0 1-7.922-8.982 6 6 0 0 1 8.982 7.922l3.04 3.04a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215ZM11.5 7a4.499 4.499 0 1 0-8.997 0A4.499 4.499 0 0 0 11.5 7Z"></path>
        </svg>
                </div>
                <div class="js-spinner d-flex flex-items-center mr-2 color-fg-muted" hidden>
                  <svg aria-label="Loading" class="anim-rotate" viewBox="0 0 16 16" fill="none" width="16" height="16">
                    <circle
                      cx="8"
                      cy="8"
                      r="7"
                      stroke="currentColor"
                      stroke-opacity="0.25"
                      stroke-width="2"
                      vector-effect="non-scaling-stroke"
                    ></circle>
                    <path
                      d="M15 8a7.002 7.002 0 00-7-7"
                      stroke="currentColor"
                      stroke-width="2"
                      stroke-linecap="round"
                      vector-effect="non-scaling-stroke"
                    ></path>
                  </svg>
                </div>
                <command-palette-scope >
                  <div data-target="command-palette-scope.placeholder" hidden class="color-fg-subtle">/&nbsp;&nbsp;<span class="text-semibold color-fg-default">...</span>&nbsp;&nbsp;/&nbsp;&nbsp;</div>
                      <command-palette-token
                        data-text="dotnet"
                        data-id="MDEyOk9yZ2FuaXphdGlvbjkxNDE5NjE="
                        data-type="owner"
                        data-value="dotnet"
                        data-targets="command-palette-scope.tokens"
                        class="color-fg-default text-semibold"
                        style="white-space:nowrap;line-height:20px;"
                        >dotnet<span class="color-fg-subtle text-normal">&nbsp;&nbsp;/&nbsp;&nbsp;</span></command-palette-token>
                      <command-palette-token
                        data-text="runtime"
                        data-id="MDEwOlJlcG9zaXRvcnkyMTA3MTYwMDU="
                        data-type="repository"
                        data-value="runtime"
                        data-targets="command-palette-scope.tokens"
                        class="color-fg-default text-semibold"
                        style="white-space:nowrap;line-height:20px;"
                        >runtime<span class="color-fg-subtle text-normal">&nbsp;&nbsp;/&nbsp;&nbsp;</span></command-palette-token>
                      <command-palette-token
                        data-text="Issues #30402"
                        data-id="MDU6SXNzdWU1NTg0NzIzNzc="
                        data-type="issue"
                        data-value="Issues #30402"
                        data-targets="command-palette-scope.tokens"
                        class="color-fg-default text-semibold"
                        style="white-space:nowrap;line-height:20px;"
                        >Issues #30402<span class="color-fg-subtle text-normal">&nbsp;&nbsp;/&nbsp;&nbsp;</span></command-palette-token>
                </command-palette-scope>
                <div class="command-palette-input-group flex-1 form-control border-0 box-shadow-none" style="z-index: 0">
                  <div class="command-palette-typeahead position-absolute d-flex flex-items-center Truncate">
                    <span class="typeahead-segment input-mirror" data-target="command-palette-input.mirror"></span>
                    <span class="Truncate-text" data-target="command-palette-input.typeaheadText"></span>
                    <span class="typeahead-segment" data-target="command-palette-input.typeaheadPlaceholder"></span>
                  </div>
                  <input
                    class="js-overlay-input typeahead-input d-none"
                    disabled
                    tabindex="-1"
                    aria-label="Hidden input for typeahead"
                  >
                  <input
                    type="text"
                    autocomplete="off"
                    autocorrect="off"
                    autocapitalize="off"
                    spellcheck="false"
                    class="js-input typeahead-input form-control border-0 box-shadow-none input-block width-full no-focus-indicator"
                    aria-label="Command palette input"
                    aria-haspopup="listbox"
                    aria-expanded="false"
                    aria-autocomplete="list"
                    aria-controls="command-palette-page-stack"
                    role="combobox"
                    data-action="
                      input:command-palette-input#onInput
                      keydown:command-palette-input#onKeydown
                    "
                  >
                </div>
                  <div data-view-component="true" class="position-relative d-inline-block">
            <button aria-keyshortcuts="Control+Backspace" data-action="click:command-palette-input#onClear keypress:command-palette-input#onClear" data-target="command-palette-input.clearButton" id="command-palette-clear-button" hidden="hidden" type="button" data-view-component="true" class="btn-octicon command-palette-input-clear-button">      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x-circle-fill">
            <path d="M2.343 13.657A8 8 0 1 1 13.658 2.343 8 8 0 0 1 2.343 13.657ZM6.03 4.97a.751.751 0 0 0-1.042.018.751.751 0 0 0-.018 1.042L6.94 8 4.97 9.97a.749.749 0 0 0 .326 1.275.749.749 0 0 0 .734-.215L8 9.06l1.97 1.97a.749.749 0 0 0 1.275-.326.749.749 0 0 0-.215-.734L9.06 8l1.97-1.97a.749.749 0 0 0-.326-1.275.749.749 0 0 0-.734.215L8 6.94Z"></path>
        </svg>
        </button>    <tool-tip id="tooltip-3985b8b4-794e-48eb-9e4a-609e87b82dc1" for="command-palette-clear-button" popover="manual" data-direction="w" data-type="label" data-view-component="true" class="sr-only position-absolute">Clear Command Palette</tool-tip>
        </div>
              </command-palette-input>

              <command-palette-page-stack
                data-default-scope-id="MDU6SXNzdWU1NTg0NzIzNzc="
                data-default-scope-type="Issue"
                data-action="command-palette-page-octicons-cached:command-palette-page-stack#cacheOcticons"
              >
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type <kbd class="hx_kbd">#</kbd> to search pull requests
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type <kbd class="hx_kbd">#</kbd> to search issues
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type <kbd class="hx_kbd">#</kbd> to search discussions
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type <kbd class="hx_kbd">!</kbd> to search projects
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;owner&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type <kbd class="hx_kbd">@</kbd> to search teams
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type <kbd class="hx_kbd">@</kbd> to search people and organizations
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type <kbd class="hx_kbd">&gt;</kbd> to activate command mode
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode=""
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Go to your accessibility settings to change your keyboard shortcuts
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode="#"
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type author:@me to search your content
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode="#"
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type is:pr to filter to pull requests
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode="#"
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type is:issue to filter to issues
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode="#"
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type is:project to filter to projects
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                  <command-palette-tip
                    class="color-fg-muted f6 px-3 py-1 my-2"
                      data-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"
                    data-mode="#"
                    data-value="">
                    <div class="d-flex flex-items-start flex-justify-between">
                      <div>
                        <span class="text-bold">Tip:</span>
                          Type is:open to filter to open content
                      </div>
                      <div class="ml-2 flex-shrink-0">
                        Type <kbd class="hx_kbd">?</kbd> for help and tips
                      </div>
                    </div>
                  </command-palette-tip>
                <command-palette-tip class="mx-3 my-2 flash flash-error d-flex flex-items-center" data-scope-types="*" data-on-error>
                  <div>
                    <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                  </div>
                  <div class="px-2">
                    We’ve encountered an error and some results aren't available at this time. Type a new search or try again later.
                  </div>
                </command-palette-tip>
                <command-palette-tip class="h4 color-fg-default pl-3 pb-2 pt-3" data-on-empty data-scope-types="*" data-match-mode="[^?]|^$">
                  No results matched your search
                </command-palette-tip>

                <div hidden>

                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="arrow-right-color-fg-muted">
                      <svg height="16" class="octicon octicon-arrow-right color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M8.22 2.97a.75.75 0 0 1 1.06 0l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042l2.97-2.97H3.75a.75.75 0 0 1 0-1.5h7.44L8.22 4.03a.75.75 0 0 1 0-1.06Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="arrow-right-color-fg-default">
                      <svg height="16" class="octicon octicon-arrow-right color-fg-default" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M8.22 2.97a.75.75 0 0 1 1.06 0l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042l2.97-2.97H3.75a.75.75 0 0 1 0-1.5h7.44L8.22 4.03a.75.75 0 0 1 0-1.06Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="codespaces-color-fg-muted">
                      <svg height="16" class="octicon octicon-codespaces color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M0 11.25c0-.966.784-1.75 1.75-1.75h12.5c.966 0 1.75.784 1.75 1.75v3A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm2-9.5C2 .784 2.784 0 3.75 0h8.5C13.216 0 14 .784 14 1.75v5a1.75 1.75 0 0 1-1.75 1.75h-8.5A1.75 1.75 0 0 1 2 6.75Zm1.75-.25a.25.25 0 0 0-.25.25v5c0 .138.112.25.25.25h8.5a.25.25 0 0 0 .25-.25v-5a.25.25 0 0 0-.25-.25Zm-2 9.5a.25.25 0 0 0-.25.25v3c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25v-3a.25.25 0 0 0-.25-.25Z"></path><path d="M7 12.75a.75.75 0 0 1 .75-.75h4.5a.75.75 0 0 1 0 1.5h-4.5a.75.75 0 0 1-.75-.75Zm-4 0a.75.75 0 0 1 .75-.75h.5a.75.75 0 0 1 0 1.5h-.5a.75.75 0 0 1-.75-.75Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="copy-color-fg-muted">
                      <svg height="16" class="octicon octicon-copy color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M0 6.75C0 5.784.784 5 1.75 5h1.5a.75.75 0 0 1 0 1.5h-1.5a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-1.5a.75.75 0 0 1 1.5 0v1.5A1.75 1.75 0 0 1 9.25 16h-7.5A1.75 1.75 0 0 1 0 14.25Z"></path><path d="M5 1.75C5 .784 5.784 0 6.75 0h7.5C15.216 0 16 .784 16 1.75v7.5A1.75 1.75 0 0 1 14.25 11h-7.5A1.75 1.75 0 0 1 5 9.25Zm1.75-.25a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-7.5a.25.25 0 0 0-.25-.25Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="dash-color-fg-muted">
                      <svg height="16" class="octicon octicon-dash color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M2 7.75A.75.75 0 0 1 2.75 7h10a.75.75 0 0 1 0 1.5h-10A.75.75 0 0 1 2 7.75Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="file-color-fg-muted">
                      <svg height="16" class="octicon octicon-file color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M2 1.75C2 .784 2.784 0 3.75 0h6.586c.464 0 .909.184 1.237.513l2.914 2.914c.329.328.513.773.513 1.237v9.586A1.75 1.75 0 0 1 13.25 16h-9.5A1.75 1.75 0 0 1 2 14.25Zm1.75-.25a.25.25 0 0 0-.25.25v12.5c0 .138.112.25.25.25h9.5a.25.25 0 0 0 .25-.25V6h-2.75A1.75 1.75 0 0 1 9 4.25V1.5Zm6.75.062V4.25c0 .138.112.25.25.25h2.688l-.011-.013-2.914-2.914-.013-.011Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="gear-color-fg-muted">
                      <svg height="16" class="octicon octicon-gear color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M8 0a8.2 8.2 0 0 1 .701.031C9.444.095 9.99.645 10.16 1.29l.288 1.107c.018.066.079.158.212.224.231.114.454.243.668.386.123.082.233.09.299.071l1.103-.303c.644-.176 1.392.021 1.82.63.27.385.506.792.704 1.218.315.675.111 1.422-.364 1.891l-.814.806c-.049.048-.098.147-.088.294.016.257.016.515 0 .772-.01.147.038.246.088.294l.814.806c.475.469.679 1.216.364 1.891a7.977 7.977 0 0 1-.704 1.217c-.428.61-1.176.807-1.82.63l-1.102-.302c-.067-.019-.177-.011-.3.071a5.909 5.909 0 0 1-.668.386c-.133.066-.194.158-.211.224l-.29 1.106c-.168.646-.715 1.196-1.458 1.26a8.006 8.006 0 0 1-1.402 0c-.743-.064-1.289-.614-1.458-1.26l-.289-1.106c-.018-.066-.079-.158-.212-.224a5.738 5.738 0 0 1-.668-.386c-.123-.082-.233-.09-.299-.071l-1.103.303c-.644.176-1.392-.021-1.82-.63a8.12 8.12 0 0 1-.704-1.218c-.315-.675-.111-1.422.363-1.891l.815-.806c.05-.048.098-.147.088-.294a6.214 6.214 0 0 1 0-.772c.01-.147-.038-.246-.088-.294l-.815-.806C.635 6.045.431 5.298.746 4.623a7.92 7.92 0 0 1 .704-1.217c.428-.61 1.176-.807 1.82-.63l1.102.302c.067.019.177.011.3-.071.214-.143.437-.272.668-.386.133-.066.194-.158.211-.224l.29-1.106C6.009.645 6.556.095 7.299.03 7.53.01 7.764 0 8 0Zm-.571 1.525c-.036.003-.108.036-.137.146l-.289 1.105c-.147.561-.549.967-.998 1.189-.173.086-.34.183-.5.29-.417.278-.97.423-1.529.27l-1.103-.303c-.109-.03-.175.016-.195.045-.22.312-.412.644-.573.99-.014.031-.021.11.059.19l.815.806c.411.406.562.957.53 1.456a4.709 4.709 0 0 0 0 .582c.032.499-.119 1.05-.53 1.456l-.815.806c-.081.08-.073.159-.059.19.162.346.353.677.573.989.02.03.085.076.195.046l1.102-.303c.56-.153 1.113-.008 1.53.27.161.107.328.204.501.29.447.222.85.629.997 1.189l.289 1.105c.029.109.101.143.137.146a6.6 6.6 0 0 0 1.142 0c.036-.003.108-.036.137-.146l.289-1.105c.147-.561.549-.967.998-1.189.173-.086.34-.183.5-.29.417-.278.97-.423 1.529-.27l1.103.303c.109.029.175-.016.195-.045.22-.313.411-.644.573-.99.014-.031.021-.11-.059-.19l-.815-.806c-.411-.406-.562-.957-.53-1.456a4.709 4.709 0 0 0 0-.582c-.032-.499.119-1.05.53-1.456l.815-.806c.081-.08.073-.159.059-.19a6.464 6.464 0 0 0-.573-.989c-.02-.03-.085-.076-.195-.046l-1.102.303c-.56.153-1.113.008-1.53-.27a4.44 4.44 0 0 0-.501-.29c-.447-.222-.85-.629-.997-1.189l-.289-1.105c-.029-.11-.101-.143-.137-.146a6.6 6.6 0 0 0-1.142 0ZM11 8a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM9.5 8a1.5 1.5 0 1 0-3.001.001A1.5 1.5 0 0 0 9.5 8Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="lock-color-fg-muted">
                      <svg height="16" class="octicon octicon-lock color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M4 4a4 4 0 0 1 8 0v2h.25c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 12.25 15h-8.5A1.75 1.75 0 0 1 2 13.25v-5.5C2 6.784 2.784 6 3.75 6H4Zm8.25 3.5h-8.5a.25.25 0 0 0-.25.25v5.5c0 .138.112.25.25.25h8.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25ZM10.5 6V4a2.5 2.5 0 1 0-5 0v2Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="moon-color-fg-muted">
                      <svg height="16" class="octicon octicon-moon color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M9.598 1.591a.749.749 0 0 1 .785-.175 7.001 7.001 0 1 1-8.967 8.967.75.75 0 0 1 .961-.96 5.5 5.5 0 0 0 7.046-7.046.75.75 0 0 1 .175-.786Zm1.616 1.945a7 7 0 0 1-7.678 7.678 5.499 5.499 0 1 0 7.678-7.678Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="person-color-fg-muted">
                      <svg height="16" class="octicon octicon-person color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0ZM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="pencil-color-fg-muted">
                      <svg height="16" class="octicon octicon-pencil color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Zm.176 4.823L9.75 4.81l-6.286 6.287a.253.253 0 0 0-.064.108l-.558 1.953 1.953-.558a.253.253 0 0 0 .108-.064Zm1.238-3.763a.25.25 0 0 0-.354 0L10.811 3.75l1.439 1.44 1.263-1.263a.25.25 0 0 0 0-.354Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="issue-opened-open">
                      <svg height="16" class="octicon octicon-issue-opened open" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path><path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="git-pull-request-draft-color-fg-muted">
                      <svg height="16" class="octicon octicon-git-pull-request-draft color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M3.25 1A2.25 2.25 0 0 1 4 5.372v5.256a2.251 2.251 0 1 1-1.5 0V5.372A2.251 2.251 0 0 1 3.25 1Zm9.5 14a2.25 2.25 0 1 1 0-4.5 2.25 2.25 0 0 1 0 4.5ZM2.5 3.25a.75.75 0 1 0 1.5 0 .75.75 0 0 0-1.5 0ZM3.25 12a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5Zm9.5 0a.75.75 0 1 0 0 1.5.75.75 0 0 0 0-1.5ZM14 7.5a1.25 1.25 0 1 1-2.5 0 1.25 1.25 0 0 1 2.5 0Zm0-4.25a1.25 1.25 0 1 1-2.5 0 1.25 1.25 0 0 1 2.5 0Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="search-color-fg-muted">
                      <svg height="16" class="octicon octicon-search color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M10.68 11.74a6 6 0 0 1-7.922-8.982 6 6 0 0 1 8.982 7.922l3.04 3.04a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215ZM11.5 7a4.499 4.499 0 1 0-8.997 0A4.499 4.499 0 0 0 11.5 7Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="sun-color-fg-muted">
                      <svg height="16" class="octicon octicon-sun color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M8 12a4 4 0 1 1 0-8 4 4 0 0 1 0 8Zm0-1.5a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5Zm5.657-8.157a.75.75 0 0 1 0 1.061l-1.061 1.06a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734l1.06-1.06a.75.75 0 0 1 1.06 0Zm-9.193 9.193a.75.75 0 0 1 0 1.06l-1.06 1.061a.75.75 0 1 1-1.061-1.06l1.06-1.061a.75.75 0 0 1 1.061 0ZM8 0a.75.75 0 0 1 .75.75v1.5a.75.75 0 0 1-1.5 0V.75A.75.75 0 0 1 8 0ZM3 8a.75.75 0 0 1-.75.75H.75a.75.75 0 0 1 0-1.5h1.5A.75.75 0 0 1 3 8Zm13 0a.75.75 0 0 1-.75.75h-1.5a.75.75 0 0 1 0-1.5h1.5A.75.75 0 0 1 16 8Zm-8 5a.75.75 0 0 1 .75.75v1.5a.75.75 0 0 1-1.5 0v-1.5A.75.75 0 0 1 8 13Zm3.536-1.464a.75.75 0 0 1 1.06 0l1.061 1.06a.75.75 0 0 1-1.06 1.061l-1.061-1.06a.75.75 0 0 1 0-1.061ZM2.343 2.343a.75.75 0 0 1 1.061 0l1.06 1.061a.751.751 0 0 1-.018 1.042.751.751 0 0 1-1.042.018l-1.06-1.06a.75.75 0 0 1 0-1.06Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="sync-color-fg-muted">
                      <svg height="16" class="octicon octicon-sync color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M1.705 8.005a.75.75 0 0 1 .834.656 5.5 5.5 0 0 0 9.592 2.97l-1.204-1.204a.25.25 0 0 1 .177-.427h3.646a.25.25 0 0 1 .25.25v3.646a.25.25 0 0 1-.427.177l-1.38-1.38A7.002 7.002 0 0 1 1.05 8.84a.75.75 0 0 1 .656-.834ZM8 2.5a5.487 5.487 0 0 0-4.131 1.869l1.204 1.204A.25.25 0 0 1 4.896 6H1.25A.25.25 0 0 1 1 5.75V2.104a.25.25 0 0 1 .427-.177l1.38 1.38A7.002 7.002 0 0 1 14.95 7.16a.75.75 0 0 1-1.49.178A5.5 5.5 0 0 0 8 2.5Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="trash-color-fg-muted">
                      <svg height="16" class="octicon octicon-trash color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M11 1.75V3h2.25a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1 0-1.5H5V1.75C5 .784 5.784 0 6.75 0h2.5C10.216 0 11 .784 11 1.75ZM4.496 6.675l.66 6.6a.25.25 0 0 0 .249.225h5.19a.25.25 0 0 0 .249-.225l.66-6.6a.75.75 0 0 1 1.492.149l-.66 6.6A1.748 1.748 0 0 1 10.595 15h-5.19a1.75 1.75 0 0 1-1.741-1.575l-.66-6.6a.75.75 0 1 1 1.492-.15ZM6.5 1.75V3h3V1.75a.25.25 0 0 0-.25-.25h-2.5a.25.25 0 0 0-.25.25Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="key-color-fg-muted">
                      <svg height="16" class="octicon octicon-key color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M10.5 0a5.499 5.499 0 1 1-1.288 10.848l-.932.932a.749.749 0 0 1-.53.22H7v.75a.749.749 0 0 1-.22.53l-.5.5a.749.749 0 0 1-.53.22H5v.75a.749.749 0 0 1-.22.53l-.5.5a.749.749 0 0 1-.53.22h-2A1.75 1.75 0 0 1 0 14.25v-2c0-.199.079-.389.22-.53l4.932-4.932A5.5 5.5 0 0 1 10.5 0Zm-4 5.5c-.001.431.069.86.205 1.269a.75.75 0 0 1-.181.768L1.5 12.56v1.69c0 .138.112.25.25.25h1.69l.06-.06v-1.19a.75.75 0 0 1 .75-.75h1.19l.06-.06v-1.19a.75.75 0 0 1 .75-.75h1.19l1.023-1.025a.75.75 0 0 1 .768-.18A4 4 0 1 0 6.5 5.5ZM11 6a1 1 0 1 1 0-2 1 1 0 0 1 0 2Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="comment-discussion-color-fg-muted">
                      <svg height="16" class="octicon octicon-comment-discussion color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M1.75 1h8.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 10.25 10H7.061l-2.574 2.573A1.458 1.458 0 0 1 2 11.543V10h-.25A1.75 1.75 0 0 1 0 8.25v-5.5C0 1.784.784 1 1.75 1ZM1.5 2.75v5.5c0 .138.112.25.25.25h1a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h3.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25Zm13 2a.25.25 0 0 0-.25-.25h-.5a.75.75 0 0 1 0-1.5h.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 14.25 12H14v1.543a1.458 1.458 0 0 1-2.487 1.03L9.22 12.28a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215l2.22 2.22v-2.19a.75.75 0 0 1 .75-.75h1a.25.25 0 0 0 .25-.25Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="bell-color-fg-muted">
                      <svg height="16" class="octicon octicon-bell color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M8 16a2 2 0 0 0 1.985-1.75c.017-.137-.097-.25-.235-.25h-3.5c-.138 0-.252.113-.235.25A2 2 0 0 0 8 16ZM3 5a5 5 0 0 1 10 0v2.947c0 .05.015.098.042.139l1.703 2.555A1.519 1.519 0 0 1 13.482 13H2.518a1.516 1.516 0 0 1-1.263-2.36l1.703-2.554A.255.255 0 0 0 3 7.947Zm5-3.5A3.5 3.5 0 0 0 4.5 5v2.947c0 .346-.102.683-.294.97l-1.703 2.556a.017.017 0 0 0-.003.01l.001.006c0 .002.002.004.004.006l.006.004.007.001h10.964l.007-.001.006-.004.004-.006.001-.007a.017.017 0 0 0-.003-.01l-1.703-2.554a1.745 1.745 0 0 1-.294-.97V5A3.5 3.5 0 0 0 8 1.5Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="bell-slash-color-fg-muted">
                      <svg height="16" class="octicon octicon-bell-slash color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="m4.182 4.31.016.011 10.104 7.316.013.01 1.375.996a.75.75 0 1 1-.88 1.214L13.626 13H2.518a1.516 1.516 0 0 1-1.263-2.36l1.703-2.554A.255.255 0 0 0 3 7.947V5.305L.31 3.357a.75.75 0 1 1 .88-1.214Zm7.373 7.19L4.5 6.391v1.556c0 .346-.102.683-.294.97l-1.703 2.556a.017.017 0 0 0-.003.01c0 .005.002.009.005.012l.006.004.007.001ZM8 1.5c-.997 0-1.895.416-2.534 1.086A.75.75 0 1 1 4.38 1.55 5 5 0 0 1 13 5v2.373a.75.75 0 0 1-1.5 0V5A3.5 3.5 0 0 0 8 1.5ZM8 16a2 2 0 0 1-1.985-1.75c-.017-.137.097-.25.235-.25h3.5c.138 0 .252.113.235.25A2 2 0 0 1 8 16Z"></path></svg>
                    </div>
                    <div data-targets="command-palette-page-stack.localOcticons" data-octicon-id="paintbrush-color-fg-muted">
                      <svg height="16" class="octicon octicon-paintbrush color-fg-muted" viewBox="0 0 16 16" version="1.1" width="16" aria-hidden="true"><path d="M11.134 1.535c.7-.509 1.416-.942 2.076-1.155.649-.21 1.463-.267 2.069.34.603.601.568 1.411.368 2.07-.202.668-.624 1.39-1.125 2.096-1.011 1.424-2.496 2.987-3.775 4.249-1.098 1.084-2.132 1.839-3.04 2.3a3.744 3.744 0 0 1-1.055 3.217c-.431.431-1.065.691-1.657.861-.614.177-1.294.287-1.914.357A21.151 21.151 0 0 1 .797 16H.743l.007-.75H.749L.742 16a.75.75 0 0 1-.743-.742l.743-.008-.742.007v-.054a21.25 21.25 0 0 1 .13-2.284c.067-.647.187-1.287.358-1.914.17-.591.43-1.226.86-1.657a3.746 3.746 0 0 1 3.227-1.054c.466-.893 1.225-1.907 2.314-2.982 1.271-1.255 2.833-2.75 4.245-3.777ZM1.62 13.089c-.051.464-.086.929-.104 1.395.466-.018.932-.053 1.396-.104a10.511 10.511 0 0 0 1.668-.309c.526-.151.856-.325 1.011-.48a2.25 2.25 0 1 0-3.182-3.182c-.155.155-.329.485-.48 1.01a10.515 10.515 0 0 0-.309 1.67Zm10.396-10.34c-1.224.89-2.605 2.189-3.822 3.384l1.718 1.718c1.21-1.205 2.51-2.597 3.387-3.833.47-.662.78-1.227.912-1.662.134-.444.032-.551.009-.575h-.001V1.78c-.014-.014-.113-.113-.548.027-.432.14-.995.462-1.655.942Zm-4.832 7.266-.001.001a9.859 9.859 0 0 0 1.63-1.142L7.155 7.216a9.7 9.7 0 0 0-1.161 1.607c.482.302.889.71 1.19 1.192Z"></path></svg>
                    </div>

                    <command-palette-item-group
                      data-group-id="top"
                      data-group-title="Top result"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="0"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="commands"
                      data-group-title="Commands"
                      data-group-hint="Type &gt; to filter"
                      data-group-limits="{&quot;static_items_page&quot;:50,&quot;issue&quot;:50,&quot;pull_request&quot;:50,&quot;discussion&quot;:50}"
                      data-default-priority="1"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="global_commands"
                      data-group-title="Global Commands"
                      data-group-hint="Type &gt; to filter"
                      data-group-limits="{&quot;issue&quot;:0,&quot;pull_request&quot;:0,&quot;discussion&quot;:0}"
                      data-default-priority="2"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="this_page"
                      data-group-title="This Page"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="3"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="files"
                      data-group-title="Files"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="4"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="default"
                      data-group-title="Default"
                      data-group-hint=""
                      data-group-limits="{&quot;static_items_page&quot;:50}"
                      data-default-priority="5"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="pages"
                      data-group-title="Pages"
                      data-group-hint=""
                      data-group-limits="{&quot;repository&quot;:10}"
                      data-default-priority="6"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="access_policies"
                      data-group-title="Access Policies"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="7"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="organizations"
                      data-group-title="Organizations"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="8"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="repositories"
                      data-group-title="Repositories"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="9"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="references"
                      data-group-title="Issues, pull requests, and discussions"
                      data-group-hint="Type # to filter"
                      data-group-limits="{}"
                      data-default-priority="10"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="teams"
                      data-group-title="Teams"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="11"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="users"
                      data-group-title="Users"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="12"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="memex_projects"
                      data-group-title="Projects"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="13"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="projects"
                      data-group-title="Projects (classic)"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="14"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="footer"
                      data-group-title="Footer"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="15"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="modes_help"
                      data-group-title="Modes"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="16"
                    >
                    </command-palette-item-group>
                    <command-palette-item-group
                      data-group-id="filters_help"
                      data-group-title="Use filters in issues, pull requests, discussions, and projects"
                      data-group-hint=""
                      data-group-limits="{}"
                      data-default-priority="17"
                    >
                    </command-palette-item-group>

                    <command-palette-page
                      data-page-title="dotnet"
                      data-scope-id="MDEyOk9yZ2FuaXphdGlvbjkxNDE5NjE="
                      data-scope-type="owner"
                      data-targets="command-palette-page-stack.defaultPages"
                      hidden
                    >
                    </command-palette-page>
                    <command-palette-page
                      data-page-title="runtime"
                      data-scope-id="MDEwOlJlcG9zaXRvcnkyMTA3MTYwMDU="
                      data-scope-type="repository"
                      data-targets="command-palette-page-stack.defaultPages"
                      hidden
                    >
                    </command-palette-page>
                    <command-palette-page
                      data-page-title="Issues #30402"
                      data-scope-id="MDU6SXNzdWU1NTg0NzIzNzc="
                      data-scope-type="issue"
                      data-targets="command-palette-page-stack.defaultPages"
                      hidden
                    >
                    </command-palette-page>
                </div>

                <command-palette-page data-is-root>
                </command-palette-page>
                  <command-palette-page
                    data-page-title="dotnet"
                    data-scope-id="MDEyOk9yZ2FuaXphdGlvbjkxNDE5NjE="
                    data-scope-type="owner"
                  >
                  </command-palette-page>
                  <command-palette-page
                    data-page-title="runtime"
                    data-scope-id="MDEwOlJlcG9zaXRvcnkyMTA3MTYwMDU="
                    data-scope-type="repository"
                  >
                  </command-palette-page>
                  <command-palette-page
                    data-page-title="Issues #30402"
                    data-scope-id="MDU6SXNzdWU1NTg0NzIzNzc="
                    data-scope-type="issue"
                  >
                  </command-palette-page>
              </command-palette-page-stack>

              <server-defined-provider data-type="search-links" data-targets="command-palette.serverDefinedProviderElements"></server-defined-provider>
              <server-defined-provider data-type="help" data-targets="command-palette.serverDefinedProviderElements">
                  <command-palette-help
                    data-group="modes_help"
                      data-prefix="#"
                      data-scope-types="[&quot;&quot;]"
                  >
                    <span data-target="command-palette-help.titleElement">Search for <strong>issues</strong> and <strong>pull requests</strong></span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd">#</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="modes_help"
                      data-prefix="#"
                      data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                  >
                    <span data-target="command-palette-help.titleElement">Search for <strong>issues, pull requests, discussions,</strong> and <strong>projects</strong></span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd">#</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="modes_help"
                      data-prefix="@"
                      data-scope-types="[&quot;&quot;]"
                  >
                    <span data-target="command-palette-help.titleElement">Search for <strong>organizations, repositories,</strong> and <strong>users</strong></span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd">@</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="modes_help"
                      data-prefix="!"
                      data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                  >
                    <span data-target="command-palette-help.titleElement">Search for <strong>projects</strong></span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd">!</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="modes_help"
                      data-prefix="/"
                      data-scope-types="[&quot;repository&quot;]"
                  >
                    <span data-target="command-palette-help.titleElement">Search for <strong>files</strong></span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd">/</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="modes_help"
                      data-prefix="&gt;"
                  >
                    <span data-target="command-palette-help.titleElement">Activate <strong>command mode</strong></span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd">&gt;</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="filters_help"
                      data-prefix="# author:@me"
                  >
                    <span data-target="command-palette-help.titleElement">Search your issues, pull requests, and discussions</span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd"># author:@me</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="filters_help"
                      data-prefix="# author:@me"
                  >
                    <span data-target="command-palette-help.titleElement">Search your issues, pull requests, and discussions</span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd"># author:@me</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="filters_help"
                      data-prefix="# is:pr"
                  >
                    <span data-target="command-palette-help.titleElement">Filter to pull requests</span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd"># is:pr</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="filters_help"
                      data-prefix="# is:issue"
                  >
                    <span data-target="command-palette-help.titleElement">Filter to issues</span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd"># is:issue</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="filters_help"
                      data-prefix="# is:discussion"
                      data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                  >
                    <span data-target="command-palette-help.titleElement">Filter to discussions</span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd"># is:discussion</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="filters_help"
                      data-prefix="# is:project"
                      data-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"
                  >
                    <span data-target="command-palette-help.titleElement">Filter to projects</span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd"># is:project</kbd>
                      </span>
                  </command-palette-help>
                  <command-palette-help
                    data-group="filters_help"
                      data-prefix="# is:open"
                  >
                    <span data-target="command-palette-help.titleElement">Filter to open issues, pull requests, and discussions</span>
                      <span data-target="command-palette-help.hintElement">
                        <kbd class="hx_kbd"># is:open</kbd>
                      </span>
                  </command-palette-help>
              </server-defined-provider>

                <server-defined-provider
                  data-type="commands"
                  data-fetch-debounce="0"
                    data-src="/command_palette/commands"
                  data-supported-modes="[]"
                    data-supports-commands

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="prefetched"
                  data-fetch-debounce="0"
                    data-src="/command_palette/jump_to_page_navigation"
                  data-supported-modes="[&quot;&quot;]"
                    data-supported-scope-types="[&quot;&quot;,&quot;owner&quot;,&quot;repository&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="remote"
                  data-fetch-debounce="200"
                    data-src="/command_palette/issues"
                  data-supported-modes="[&quot;#&quot;,&quot;#&quot;]"
                    data-supported-scope-types="[&quot;owner&quot;,&quot;repository&quot;,&quot;&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="remote"
                  data-fetch-debounce="200"
                    data-src="/command_palette/jump_to"
                  data-supported-modes="[&quot;@&quot;,&quot;@&quot;]"
                    data-supported-scope-types="[&quot;&quot;,&quot;owner&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="remote"
                  data-fetch-debounce="200"
                    data-src="/command_palette/jump_to_members_only"
                  data-supported-modes="[&quot;@&quot;,&quot;@&quot;,&quot;&quot;,&quot;&quot;]"
                    data-supported-scope-types="[&quot;&quot;,&quot;owner&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="prefetched"
                  data-fetch-debounce="0"
                    data-src="/command_palette/jump_to_members_only_prefetched"
                  data-supported-modes="[&quot;@&quot;,&quot;@&quot;,&quot;&quot;,&quot;&quot;]"
                    data-supported-scope-types="[&quot;&quot;,&quot;owner&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="files"
                  data-fetch-debounce="0"
                    data-src="/command_palette/files"
                  data-supported-modes="[&quot;/&quot;]"
                    data-supported-scope-types="[&quot;repository&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="remote"
                  data-fetch-debounce="200"
                    data-src="/command_palette/discussions"
                  data-supported-modes="[&quot;#&quot;]"
                    data-supported-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="remote"
                  data-fetch-debounce="200"
                    data-src="/command_palette/projects"
                  data-supported-modes="[&quot;#&quot;,&quot;!&quot;]"
                    data-supported-scope-types="[&quot;owner&quot;,&quot;repository&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="prefetched"
                  data-fetch-debounce="0"
                    data-src="/command_palette/recent_issues"
                  data-supported-modes="[&quot;#&quot;,&quot;#&quot;]"
                    data-supported-scope-types="[&quot;owner&quot;,&quot;repository&quot;,&quot;&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="remote"
                  data-fetch-debounce="200"
                    data-src="/command_palette/teams"
                  data-supported-modes="[&quot;@&quot;,&quot;&quot;]"
                    data-supported-scope-types="[&quot;owner&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
                <server-defined-provider
                  data-type="remote"
                  data-fetch-debounce="200"
                    data-src="/command_palette/name_with_owner_repository"
                  data-supported-modes="[&quot;@&quot;,&quot;@&quot;,&quot;&quot;,&quot;&quot;]"
                    data-supported-scope-types="[&quot;&quot;,&quot;owner&quot;]"

                  data-targets="command-palette.serverDefinedProviderElements"
                  ></server-defined-provider>
            </command-palette>
          </details-dialog>
        </details>

        <div class="position-fixed bottom-0 left-0 ml-5 mb-5 js-command-palette-toasts" style="z-index: 1000">
          <div hidden class="Toast Toast--loading">
            <span class="Toast-icon">
              <svg class="Toast--spinner" viewBox="0 0 32 32" width="18" height="18" aria-hidden="true">
                <path
                  fill="#959da5"
                  d="M16 0 A16 16 0 0 0 16 32 A16 16 0 0 0 16 0 M16 4 A12 12 0 0 1 16 28 A12 12 0 0 1 16 4"
                />
                <path fill="#ffffff" d="M16 0 A16 16 0 0 1 32 16 L28 16 A12 12 0 0 0 16 4z"></path>
              </svg>
            </span>
            <span class="Toast-content"></span>
          </div>

          <div hidden class="anim-fade-in fast Toast Toast--error">
            <span class="Toast-icon">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-stop">
            <path d="M4.47.22A.749.749 0 0 1 5 0h6c.199 0 .389.079.53.22l4.25 4.25c.141.14.22.331.22.53v6a.749.749 0 0 1-.22.53l-4.25 4.25A.749.749 0 0 1 11 16H5a.749.749 0 0 1-.53-.22L.22 11.53A.749.749 0 0 1 0 11V5c0-.199.079-.389.22-.53Zm.84 1.28L1.5 5.31v5.38l3.81 3.81h5.38l3.81-3.81V5.31L10.69 1.5ZM8 4a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-1.5 0v-3.5A.75.75 0 0 1 8 4Zm0 8a1 1 0 1 1 0-2 1 1 0 0 1 0 2Z"></path>
        </svg>
            </span>
            <span class="Toast-content"></span>
          </div>

          <div hidden class="anim-fade-in fast Toast Toast--warning">
            <span class="Toast-icon">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            </span>
            <span class="Toast-content"></span>
          </div>


          <div hidden class="anim-fade-in fast Toast Toast--success">
            <span class="Toast-icon">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-check">
            <path d="M13.78 4.22a.75.75 0 0 1 0 1.06l-7.25 7.25a.75.75 0 0 1-1.06 0L2.22 9.28a.751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018L6 10.94l6.72-6.72a.75.75 0 0 1 1.06 0Z"></path>
        </svg>
            </span>
            <span class="Toast-content"></span>
          </div>

          <div hidden class="anim-fade-in fast Toast">
            <span class="Toast-icon">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-info">
            <path d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8Zm8-6.5a6.5 6.5 0 1 0 0 13 6.5 6.5 0 0 0 0-13ZM6.5 7.75A.75.75 0 0 1 7.25 7h1a.75.75 0 0 1 .75.75v2.75h.25a.75.75 0 0 1 0 1.5h-2a.75.75 0 0 1 0-1.5h.25v-2h-.25a.75.75 0 0 1-.75-.75ZM8 6a1 1 0 1 1 0-2 1 1 0 0 1 0 2Z"></path>
        </svg>
            </span>
            <span class="Toast-content"></span>
          </div>
        </div>


          <div
            class="application-main "
            data-commit-hovercards-enabled
            data-discussion-hovercards-enabled
            data-issue-and-pr-hovercards-enabled
          >
                <div itemscope itemtype="http://schema.org/SoftwareSourceCode" class="">
            <main id="js-repo-pjax-container" >

















          <div id="repository-container-header" data-turbo-replace hidden></div>




        <turbo-frame id="repo-content-turbo-frame" target="_top" data-turbo-action="advance" class="">
            <div id="repo-content-pjax-container" class="repository-content " >





            <div class="clearfix new-discussion-timeline js-check-all-container container-xl px-3 px-md-4 px-lg-5 mt-4" data-pjax="" data-turbo-frame="">


                <div id="show_issue"
                    class="js-issues-results js-socket-channel js-updatable-content"
                    data-morpheus-enabled="false"
                    data-channel="eyJjIjoiaXNzdWU6NTU4NDcyMzc3OnRpbWVsaW5lIiwidCI6MTcwMjg0MTg0OH0=--6ad0e00377a5894a47bc5efbda52a30a9f99aad4521fa4246c6c93d9558687a8">



          <div
            id="partial-discussion-header"
            class="gh-header mb-3 js-details-container Details js-socket-channel js-updatable-content issue"
            data-channel="eyJjIjoiaXNzdWU6NTU4NDcyMzc3IiwidCI6MTcwMjg0MTg0OH0=--1b64a51fa112c483acc6db2745859f2b2460bef3b558e03bdc75de1d2c8b1472"
            data-url="/dotnet/runtime/issues/30402/show_partial?partial=issues%2Ftitle&amp;sticky=true"
            data-gid="MDU6SXNzdWU1NTg0NzIzNzc=">

          <div class="gh-header-show gh-header-no-access">
            <div class="d-flex flex-column flex-md-row">
              <div class="gh-header-actions mt-0 mb-3 mb-md-2 ml-1 flex-md-order-1 flex-shrink-0 d-flex flex-items-center gap-1">



                <div class="flex-auto text-right d-block d-md-none">
                  <a href="#issue-comment-box" class="py-1">Jump to bottom</a>
                </div>
              </div>

            <h1 class="gh-header-title mb-2 lh-condensed f1 mr-0 flex-auto wb-break-word">
              <bdi class="js-issue-title markdown-title">Utf8JsonReader/JsonStreamReader: Add reading values as Stream</bdi>
              <span class="f1-light color-fg-muted">#30402</span>
            </h1>
            </div>
          </div>

          <div class="d-flex flex-items-center flex-wrap mt-0 gh-header-meta">
            <div class="flex-shrink-0 mb-2 flex-self-start flex-md-self-center">
                <span title="Status: Closed" data-view-component="true" class="State State--merged d-flex flex-items-center">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-issue-closed flex-items-center mr-1">
            <path d="M11.28 6.78a.75.75 0 0 0-1.06-1.06L7.25 8.69 5.78 7.22a.75.75 0 0 0-1.06 1.06l2 2a.75.75 0 0 0 1.06 0l3.5-3.5Z"></path><path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0Zm-1.5 0a6.5 6.5 0 1 0-13 0 6.5 6.5 0 0 0 13 0Z"></path>
        </svg>
          Closed
        </span>
            </div>

              <div class="mb-2 flex-shrink-0">
                  <div>

          </div>

              </div>


              <div class="flex-shrink-0 mb-2 flex-self-start flex-md-self-center">

              </div>

            <div class="flex-auto min-width-0 mb-2">
                <a class="author text-bold Link--secondary" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">andriysavin</a>  opened this issue
        <relative-time datetime="2019-07-28T07:24:20Z" class="no-wrap">Jul 28, 2019</relative-time>
        &middot; 10 comments

        <span data-issue-and-pr-hovercards-enabled>

        </span>

            </div>

          </div>





            <div class="js-sticky js-sticky-offset-scroll top-0 gh-header-sticky">
              <div class="sticky-content">
                <div class="d-flex flex-items-center flex-justify-between mt-2">
                  <div class="d-flex flex-row flex-items-center min-width-0">
                    <div class="mr-2 mb-2 flex-shrink-0">
                        <span title="Status: Closed" data-view-component="true" class="State State--merged d-flex flex-items-center">
          <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-issue-closed flex-items-center mr-1">
            <path d="M11.28 6.78a.75.75 0 0 0-1.06-1.06L7.25 8.69 5.78 7.22a.75.75 0 0 0-1.06 1.06l2 2a.75.75 0 0 0 1.06 0l3.5-3.5Z"></path><path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0Zm-1.5 0a6.5 6.5 0 1 0-13 0 6.5 6.5 0 0 0 13 0Z"></path>
        </svg>
          Closed
        </span>
                    </div>


                      <div class="mb-2 flex-shrink-0">
                          <div>

          </div>

                      </div>

                      <div class="mb-2 flex-shrink-0">

                      </div>

                    <div class="min-width-0 mr-2 mb-2">
                      <h1 class="d-flex text-bold f5">
          <a class="js-issue-title css-truncate css-truncate-target Link--primary width-fit markdown-title js-smoothscroll-anchor" href="#top">
            Utf8JsonReader/JsonStreamReader: Add reading values as Stream
          </a>
          <span class="gh-header-number color-fg-muted pl-1">#30402</span>
        </h1>

                      <div class="meta color-fg-muted css-truncate css-truncate-target d-block width-fit">
                          <a class="author text-bold Link--secondary" data-hovercard-z-index-override="111" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">andriysavin</a>  opened this issue
        <relative-time datetime="2019-07-28T07:24:20Z" class="no-wrap">Jul 28, 2019</relative-time>
        &middot; 10 comments

        <span data-issue-and-pr-hovercards-enabled>

        </span>

                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="gh-header-shadow color-shadow-small js-notification-shelf-offset-top"></div>
        </div>


                      <div class="d-block d-md-none border-bottom mb-4 f6">

              <div class="d-flex mb-3">
                <span class="text-bold color-fg-muted col-3 col-sm-2 flex-shrink-0">Labels</span>
                <div class="min-width-0 d-flex flex-wrap mt-n1">

        <a id="label-d83d87" href="/dotnet/runtime/labels/area-System.Text.Json" data-name="area-System.Text.Json" style="--label-r:212;--label-g:197;--label-b:249;--label-h:257;--label-s:81;--label-l:87;" data-view-component="true" class="IssueLabel hx_IssueLabel width-fit mb-1 mr-1">
                      <span class="css-truncate css-truncate-target width-fit">area-System.Text.Json</span>
        </a>
                </div>
              </div>


              <div class="d-flex mb-3">
                <span class="text-bold color-fg-muted col-3 col-sm-2 flex-shrink-0">Milestone</span>
                <div class="min-width-0">
                  <a title="Future" href="/dotnet/runtime/milestone/1" class="Link--primary text-bold no-underline">
                    <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-milestone color-fg-muted">
            <path d="M7.75 0a.75.75 0 0 1 .75.75V3h3.634c.414 0 .814.147 1.13.414l2.07 1.75a1.75 1.75 0 0 1 0 2.672l-2.07 1.75a1.75 1.75 0 0 1-1.13.414H8.5v5.25a.75.75 0 0 1-1.5 0V10H2.75A1.75 1.75 0 0 1 1 8.25v-3.5C1 3.784 1.784 3 2.75 3H7V.75A.75.75 0 0 1 7.75 0Zm4.384 8.5a.25.25 0 0 0 .161-.06l2.07-1.75a.248.248 0 0 0 0-.38l-2.07-1.75a.25.25 0 0 0-.161-.06H2.75a.25.25 0 0 0-.25.25v3.5c0 .138.112.25.25.25h9.384Z"></path>
        </svg>
                    <span class="css-truncate css-truncate-target">Future</span>
                  </a>
                </div>
              </div>
          </div>


                  <div id="discussion_bucket">
                    <div data-view-component="true" class="Layout Layout--flowRow-until-md Layout--sidebarPosition-end Layout--sidebarPosition-flowRow-end">
          <div data-view-component="true" class="Layout-main">                  <h2 class="sr-only">Comments</h2>
        <div class="js-quote-selection-container"
             data-quote-markdown=".js-comment-body"
             data-discussion-hovercards-enabled
             data-issue-and-pr-hovercards-enabled
             data-team-hovercards-enabled>

          <div
            class="js-discussion js-socket-channel ml-0 pl-0 ml-md-6 pl-md-3"
            data-channel="eyJjIjoibWFya2VkLWFzLXJlYWQ6MTE2MjgyMyIsInQiOjE3MDI4NDE4NDh9--64fbdf517f9cecff3ea284b1aaf39f45e57143fd0b2245606288db32455a64fc"
            data-channel-target="MDU6SXNzdWU1NTg0NzIzNzc="
            data-hpc
            >
              <div class="TimelineItem pt-0 js-comment-container js-socket-channel js-updatable-content "
          data-gid="MDU6SXNzdWU1NTg0NzIzNzc="
          data-url="/dotnet/runtime/issues/30402/partials/body?issue=30402"
          data-channel="eyJjIjoiaXNzdWU6NTU4NDcyMzc3IiwidCI6MTcwMjg0MTg0OH0=--1b64a51fa112c483acc6db2745859f2b2460bef3b558e03bdc75de1d2c8b1472">

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/12996633?s=80&amp;u=cf6a6d9ded07facc249a9e31b51d128d126364ef&amp;v=4" width="40" height="40" alt="@andriysavin" /></a>

        </div>

          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issue-558472377">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="9d030b9b305ff922e9098b4aa8ed97a91b6400007cf8b08988788ddceba8fcb7">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issues/30402/actions_menu?gid=MDU6SXNzdWU1NTg0NzIzNzc%3D&amp;href=%23issue-558472377"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">








          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">andriysavin</a>


              </strong>



              commented


                <a href="#issue-558472377" id="issue-558472377-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-28T07:24:20Z" class="no-wrap">Jul 28, 2019</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto">Consider the following (real) scenario: a web service returns a binary content (file) of arbitrary length (up to hundreds of MB) as a base64-encoded <strong>value of a JSON property</strong> of a JSON document. With current implementation I have to read whole <strong>property value</strong> into memory before I can start processing it. Needless to explain what the consequences of this are, especially related to LOH (though, memory pooling inside the library may make this less painful). What I'd like to have is ability to open a <strong>property value</strong> as a stream (or, maybe, some lower lever object such as pipe reader) and read the value in a streamed fashion as bytes without allocating huge amount of memory. I'm not sure if this should go into <code class="notranslate">Utf8JsonReader</code> or (planned?) <code class="notranslate">JsonStreamReader</code>, but you got the idea. The same should probably be considered for writers.</p>
              </td>
            </tr>
              <tr class="d-block pl-3 pr-3 pb-3 js-comment-body-error" hidden>
                <td class="d-block">
                  <div class="flash flash-warn" role="alert">
                    <p class="mb-1">
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-info">
            <path d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8Zm8-6.5a6.5 6.5 0 1 0 0 13 6.5 6.5 0 0 0 0-13ZM6.5 7.75A.75.75 0 0 1 7.25 7h1a.75.75 0 0 1 .75.75v2.75h.25a.75.75 0 0 1 0 1.5h-2a.75.75 0 0 1 0-1.5h.25v-2h-.25a.75.75 0 0 1-.75-.75ZM8 6a1 1 0 1 1 0-2 1 1 0 0 1 0 2Z"></path>
        </svg>
                      The text was updated successfully, but these errors were encountered:
                    </p>
                      <ol class="mb-0 pl-4 ml-4">
                      </ol>
                  </div>
                </td>
              </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container has-reactions d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="BUI5rEPYLtVkmM_4XXqd4_6Z7WzMFtF0XTv2kO9eSJ3GtusImw0LOhjjw1_uwRWSmRMy124lWqvgKrY7M1XOLw" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDU6SXNzdWU1NTg0NzIzNzc=">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
                  <button name="input[content]" id="reactions--reaction_button_component-d3dd85" value="THUMBS_UP react" data-button-index-position="0" data-reaction-label="+1" data-reaction-content="+1" aria-pressed="false" aria-label="react with thumbs up" type="submit" disabled="disabled" data-view-component="true" class="social-reaction-summary-item js-reaction-group-button btn-link d-flex no-underline color-fg-muted flex-items-baseline mr-2">    <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji">👍</g-emoji>
                    <span class="js-discussion-reaction-group-count">2</span>
        </button>  <tool-tip id="tooltip-7508bf08-957d-4712-8c71-0b8f25eaff68" for="reactions--reaction_button_component-d3dd85" popover="manual" data-direction="n" data-type="description" data-view-component="true" class="sr-only position-absolute">GSPP and nesterenko-kv reacted with thumbs up emoji</tool-tip>
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                      <li class="dropdown-item" aria-label="GSPP and nesterenko-kv reacted with thumbs up emoji">
                        <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji mr-2">👍</g-emoji>
                          <span>2 reactions</span>
                      </li>
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issue-558472377-edit-form" data-turbo="false" action="/dotnet/runtime/issues/30402" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="mppJ43T6bixL7OiAXa18RgXbMOfUXIwH6TJw1Ckf2KLiV2C4Qf3sS75HBx71emwnGxDcKKBlFzRPr5_aqcTdIg" /></form>    </div>
        </div>

        </div>


              <div>
            <div id="js-timeline-progressive-loader" data-timeline-item-src="dotnet/runtime/timeline_focused_item?after_cursor=Y3Vyc29yOnYyOpPPAAABdlacmMABqjQxMDM5ODc0Mjk%3D&amp;id=MDU6SXNzdWU1NTg0NzIzNzc%3D" ></div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUxNTgwMzQyNQ==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUxNTgwMzQyNQ=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUxNTgwMzQyNQ==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/scalablecory/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/scalablecory"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/4665835?s=80&amp;u=f9f41cb0ba09f0bd34fe3961103f7abea9cbfa15&amp;v=4" width="40" height="40" alt="@scalablecory" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-515803425">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="a8a0673f82bc1328197f7d56f6059d54ce787191e6357d2c50d71aa3e7f9f5fc">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/515803425/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUxNTgwMzQyNQ%3D%3D&amp;href=%23issuecomment-515803425"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">




          <span aria-label="This user has previously committed to the runtime repository." data-view-component="true" class="tooltipped tooltipped-n">
            <span data-view-component="true" class="Label ml-1">Contributor</span>
        </span>



          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/scalablecory/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/scalablecory">scalablecory</a>


              </strong>



              commented


                <a href="#issuecomment-515803425" id="issuecomment-515803425-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-28T23:16:57Z" class="no-wrap">Jul 29, 2019</relative-time></a>


              <span class="js-comment-edit-history">
          <span class="d-inline-block color-fg-muted">&#8226;</span>

          <details class="details-overlay details-reset d-inline-block dropdown hx_dropdown-fullscreen">
            <summary class="btn-link no-underline color-fg-muted js-notice">
              <div class="position-relative">
                <span>
                  edited

                </span>
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-triangle-down v-align-middle">
            <path d="m4.427 7.427 3.396 3.396a.25.25 0 0 0 .354 0l3.396-3.396A.25.25 0 0 0 11.396 7H4.604a.25.25 0 0 0-.177.427Z"></path>
        </svg>
              </div>
            </summary>
            <details-menu
              class="dropdown-menu dropdown-menu-s width-auto py-0 js-comment-edit-history-menu"
              style="max-width: 352px; z-index: 99;"
              src="/user_content_edits/show_edit_history_log/MDEyOklzc3VlQ29tbWVudDUxNTgwMzQyNQ=="
              preload
            >
              <include-fragment class="my-3" style="min-width: 100px;" aria-label="Loading...">
                <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="mx-auto d-block anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
              </include-fragment>
            </details-menu>
          </details>
        </span>

            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto"><code class="notranslate">Utf8JsonReader</code> is resumable, allowing you to use it with <code class="notranslate">Stream</code>.</p>
        <p dir="auto">Once <code class="notranslate">Utf8JsonReader.Read()</code> returns <code class="notranslate">false</code>, save the <code class="notranslate">CurrentState</code> and read more from a stream. Then, create a new <code class="notranslate">Utf8JsonReader</code> this time passing in the previous state.</p>
        <p dir="auto">Sample here: <a href="https://github.com/scalablecory/system-text-json-samples/blob/master/json-test/JsonParser.ParseSimpleAsync.cs">https://github.com/scalablecory/system-text-json-samples/blob/master/json-test/JsonParser.ParseSimpleAsync.cs</a></p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container has-reactions d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="necvptDEcFbzZAeXTnSXRHlFiCfTBnLqFTbyKg3MZo9eE_0CCBFVuY8fCzD9zx81Hs9XnHE1-TWoJ7KB0cfgPQ" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUxNTgwMzQyNQ==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
                  <button name="input[content]" id="reactions--reaction_button_component-3cc8a6" value="THUMBS_UP react" data-button-index-position="0" data-reaction-label="+1" data-reaction-content="+1" aria-pressed="false" aria-label="react with thumbs up" type="submit" disabled="disabled" data-view-component="true" class="social-reaction-summary-item js-reaction-group-button btn-link d-flex no-underline color-fg-muted flex-items-baseline mr-2">    <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji">👍</g-emoji>
                    <span class="js-discussion-reaction-group-count">1</span>
        </button>  <tool-tip id="tooltip-da7147b3-3fc0-427d-bd46-c68469907ec0" for="reactions--reaction_button_component-3cc8a6" popover="manual" data-direction="n" data-type="description" data-view-component="true" class="sr-only position-absolute">GSPP reacted with thumbs up emoji</tool-tip>
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                      <li class="dropdown-item" aria-label="GSPP reacted with thumbs up emoji">
                        <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji mr-2">👍</g-emoji>
                          <span>1 reaction</span>
                      </li>
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-515803425-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/515803425" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="5_ScLyCrffuiyZ_OmxjhE4cRl0-DVwQgp26o5CVsvgsZCQy68YR0DoSztMbs4VH64SM4Us7ag6avssuXVvE_ww" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/515803425/edit_form?textarea_id=issuecomment-515803425-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUxNTgyNjU3NQ==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUxNTgyNjU3NQ=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUxNTgyNjU3NQ==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/davidfowl/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/davidfowl"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/95136?s=80&amp;u=598ed17571853b315c1b21f792f2652a3513c02b&amp;v=4" width="40" height="40" alt="@davidfowl" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-515826575">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="a75ff8e944c7f384f50b6ee4ce3f2029b9dc573f114a66c8e373e52d1b30ef25">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/515826575/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUxNTgyNjU3NQ%3D%3D&amp;href=%23issuecomment-515826575"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">




          <span aria-label="This user is a member of the dotnet organization." data-view-component="true" class="tooltipped tooltipped-n">
          <span data-view-component="true" class="Label ml-1">Member</span>
        </span>



          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/davidfowl/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/davidfowl">davidfowl</a>


              </strong>



              commented


                <a href="#issuecomment-515826575" id="issuecomment-515826575-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-29T02:54:31Z" class="no-wrap">Jul 29, 2019</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto">Dupe of <a href="https://github.com/dotnet/corefx/issues/39655">https://github.com/dotnet/corefx/issues/39655</a></p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="V32oe60K74VYAQGMUKKFWH-huAsqcOclBfmbfeOjWZ6UiXrfdd_KaiR6DSvjGQ0pGCtnsIhDbPq46NvWP6jfLA" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUxNTgyNjU3NQ==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-515826575-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/515826575" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="c_KngAkLLpvLezm2Mcm3oX865YPUTGVwSGoYS9uePyycHNukFktdoay_PMO6i35KL7JJMz-7i94FvpTjUky5bw" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/515826575/edit_form?textarea_id=issuecomment-515826575-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUxNTg4OTMzNA==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUxNTg4OTMzNA=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUxNTg4OTMzNA==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/12996633?s=80&amp;u=cf6a6d9ded07facc249a9e31b51d128d126364ef&amp;v=4" width="40" height="40" alt="@andriysavin" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-515889334">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="901e11b1ac480a9cab777f95b8f14d86e2508259e6ff95e8c0ebba27b33d1d15">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/515889334/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUxNTg4OTMzNA%3D%3D&amp;href=%23issuecomment-515889334"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">







        <span aria-label="This user is the author of this issue." data-view-component="true" class="tooltipped tooltipped-n">
          <span data-view-component="true" class="Label ml-1">Author</span>
        </span>

          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">andriysavin</a>


              </strong>



              commented


                <a href="#issuecomment-515889334" id="issuecomment-515889334-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-29T08:05:06Z" class="no-wrap">Jul 29, 2019</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto"><a class="user-mention notranslate" data-hovercard-type="user" data-hovercard-url="/users/davidfowl/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="https://github.com/davidfowl">@davidfowl</a> I don't think this is a dup of that issue. That issue is about reading JSON <strong>from</strong> a stream. My issue is about reading <strong>part</strong> of JSON <strong>as</strong> a stream. Or maybe you're suggesting that while reading <strong>from</strong> a stream at some point (e.g. we encountered property value) a portion of that stream could be returned <strong>as</strong> a stream?</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container has-reactions d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="XEnsNVaTssHaE9it_BTd1TbQA_-jnmIvwYHQE6e0FMefvT6RjkaXLqZo1ApPr1WkUVrcRAGt6fB8kJC4e7-SdQ" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUxNTg4OTMzNA==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
                  <button name="input[content]" id="reactions--reaction_button_component-1a693f" value="THUMBS_UP react" data-button-index-position="0" data-reaction-label="+1" data-reaction-content="+1" aria-pressed="false" aria-label="react with thumbs up" type="submit" disabled="disabled" data-view-component="true" class="social-reaction-summary-item js-reaction-group-button btn-link d-flex no-underline color-fg-muted flex-items-baseline mr-2">    <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji">👍</g-emoji>
                    <span class="js-discussion-reaction-group-count">1</span>
        </button>  <tool-tip id="tooltip-e28d0f4a-8994-458a-b464-4fc38c260524" for="reactions--reaction_button_component-1a693f" popover="manual" data-direction="n" data-type="description" data-view-component="true" class="sr-only position-absolute">scalablecory reacted with thumbs up emoji</tool-tip>
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                      <li class="dropdown-item" aria-label="scalablecory reacted with thumbs up emoji">
                        <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji mr-2">👍</g-emoji>
                          <span>1 reaction</span>
                      </li>
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-515889334-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/515889334" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="tKQQhQykaFHyFxj2KyXYsn7qtMh8OV4EDyVDDf5Mvl53AynCWNebcc8B9SfCnVgjeFePL-m3P_-DU2lnUwHUVA" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/515889334/edit_form?textarea_id=issuecomment-515889334-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUxNTg5MzMzNw==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUxNTg5MzMzNw=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUxNTg5MzMzNw==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/davidfowl/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/davidfowl"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/95136?s=80&amp;u=598ed17571853b315c1b21f792f2652a3513c02b&amp;v=4" width="40" height="40" alt="@davidfowl" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-515893337">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="b9466685c6820639e5db67219c6c6af1d3bc533f19caa9b924902e90707b02aa">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/515893337/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUxNTg5MzMzNw%3D%3D&amp;href=%23issuecomment-515893337"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">




          <span aria-label="This user is a member of the dotnet organization." data-view-component="true" class="tooltipped tooltipped-n">
          <span data-view-component="true" class="Label ml-1">Member</span>
        </span>



          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/davidfowl/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/davidfowl">davidfowl</a>


              </strong>



              commented


                <a href="#issuecomment-515893337" id="issuecomment-515893337-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-29T08:17:34Z" class="no-wrap">Jul 29, 2019</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto">Ah my bad you just want to read a specific property <strong>value</strong> as a stream. The reader deals with buffers not streams so in its current form it wouldn't make any sense to have an API like this.</p>
        <p dir="auto">You'd need to be reading from a stream in chunks already combined with some logic to identify the beginning of the value, then you'd have to manually parse the stream to find the end. The reader can't give you a Stream for the underlying value since it has to have buffered that data to begin with (in order to identify the end and have APIs like Value/ValueSequence function).</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container has-reactions d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="2Hqq3x5wFQmGWVwpLHnNn73eFppEb2_6v4C2m1JxcsMbjnh7xqUw5voiUI6fwkXu2lTJIeZc5CUCkfYwjnr0cQ" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUxNTg5MzMzNw==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
                  <button name="input[content]" id="reactions--reaction_button_component-b4917a" value="CONFUSED react" data-button-index-position="4" data-reaction-label="Confused" data-reaction-content="thinking_face" aria-pressed="false" aria-label="react with confused" type="submit" disabled="disabled" data-view-component="true" class="social-reaction-summary-item js-reaction-group-button btn-link d-flex no-underline color-fg-muted flex-items-baseline mr-2">    <g-emoji alias="thinking_face" fallback-src="https://github.githubassets.com/assets/1f615-4bb1369c4251.png" class="social-button-emoji">😕</g-emoji>
                    <span class="js-discussion-reaction-group-count">1</span>
        </button>  <tool-tip id="tooltip-1ed8114e-cbb6-41c4-b476-51e30227edfc" for="reactions--reaction_button_component-b4917a" popover="manual" data-direction="n" data-type="description" data-view-component="true" class="sr-only position-absolute">xania reacted with confused emoji</tool-tip>
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                      <li class="dropdown-item" aria-label="xania reacted with confused emoji">
                        <g-emoji alias="thinking_face" fallback-src="https://github.githubassets.com/assets/1f615-4bb1369c4251.png" class="social-button-emoji mr-2">😕</g-emoji>
                          <span>1 reaction</span>
                      </li>
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-515893337-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/515893337" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="Yr4XNC4JPswiAGRp6ooA4gVmR277_DXYpBqMaqdA1juJSW4g3Xt2PwpBBWqhAy9UOj2E7c4895EtQ9SxT8swoA" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/515893337/edit_form?textarea_id=issuecomment-515893337-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUxNTg5NTU3MQ==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUxNTg5NTU3MQ=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUxNTg5NTU3MQ==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/12996633?s=80&amp;u=cf6a6d9ded07facc249a9e31b51d128d126364ef&amp;v=4" width="40" height="40" alt="@andriysavin" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-515895571">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="17443564457d35635346f715a8b7882885c39a7c29172cbb88f43b88b106d491">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/515895571/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUxNTg5NTU3MQ%3D%3D&amp;href=%23issuecomment-515895571"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">







        <span aria-label="This user is the author of this issue." data-view-component="true" class="tooltipped tooltipped-n">
          <span data-view-component="true" class="Label ml-1">Author</span>
        </span>

          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">andriysavin</a>


              </strong>



              commented


                <a href="#issuecomment-515895571" id="issuecomment-515895571-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-29T08:23:55Z" class="no-wrap">Jul 29, 2019</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto"><a class="user-mention notranslate" data-hovercard-type="user" data-hovercard-url="/users/davidfowl/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="https://github.com/davidfowl">@davidfowl</a> yeah, I felt that, and this is why I was also referring to <code class="notranslate">JsonStreamReader</code> which is mentioned in the roadmap. Today I can implement what I need myself as you described. But if this <code class="notranslate">JsonStreamReader</code> is still on the roadmap, it would be nice to have this feature implemented in it out of the box.</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="y_amJ7seUXfqIFu5xLSUAHDzYLGA53E76Z1_6iICdhUIAnSDY8t0mJZbVx53DxxxF3m_CiLU-uRUjD9B_gnwpw" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUxNTg5NTU3MQ==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-515895571-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/515895571" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="iOVv6AAwoXmR6AXIYh-umQBHOwdU2mg4lCQ6mlJGmbmLN2Md_3s7xQV4vKOKWNxbVtsF-OUXmy75LMRA5xCGAw" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/515895571/edit_form?textarea_id=issuecomment-515895571-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDE3OlJlbmFtZWRUaXRsZUV2ZW50MjUxNTg0ODk1Mw==">

                  <div class="TimelineItem js-targetable-element"  data-team-hovercards-enabled  id="event-2515848953">
          <div class="TimelineItem-badge ">
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-pencil color-fg-inherit">
            <path d="M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Zm.176 4.823L9.75 4.81l-6.286 6.287a.253.253 0 0 0-.064.108l-.558 1.953 1.953-.558a.253.253 0 0 0 .108-.064Zm1.238-3.763a.25.25 0 0 0-.354 0L10.811 3.75l1.439 1.44 1.263-1.263a.25.25 0 0 0 0-.354Z"></path>
        </svg>
          </div>
          <div class="TimelineItem-body">



                <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin"><img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/12996633?s=40&amp;u=cf6a6d9ded07facc249a9e31b51d128d126364ef&amp;v=4" width="20" height="20" alt="@andriysavin" /></a>
        <a class="author Link--primary text-bold" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">andriysavin</a>




              changed the title
        <del class="text-bold markdown-title">Utf8JsonReader: Add reading values as Stream</del>

        <ins class="text-bold markdown-title no-underline">Utf8JsonReader/JsonStreamReader: Add reading values as Stream</ins>


            <a href="#event-2515848953" class="Link--secondary"><relative-time datetime="2019-07-29T09:24:28Z" class="no-wrap">Jul 29, 2019</relative-time></a>

          </div>
        </div>




        </div>

              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUxNjA4NzkxNw==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUxNjA4NzkxNw=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUxNjA4NzkxNw==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/scalablecory/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/scalablecory"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/4665835?s=80&amp;u=f9f41cb0ba09f0bd34fe3961103f7abea9cbfa15&amp;v=4" width="40" height="40" alt="@scalablecory" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-516087917">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="2f824c30c4f6e81b9942add481cab532de523bf14190f161b094dc7d140bc705">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/516087917/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUxNjA4NzkxNw%3D%3D&amp;href=%23issuecomment-516087917"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">




          <span aria-label="This user has previously committed to the runtime repository." data-view-component="true" class="tooltipped tooltipped-n">
            <span data-view-component="true" class="Label ml-1">Contributor</span>
        </span>



          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/scalablecory/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/scalablecory">scalablecory</a>


              </strong>



              commented


                <a href="#issuecomment-516087917" id="issuecomment-516087917-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-29T17:32:52Z" class="no-wrap">Jul 29, 2019</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto">Ah, this makes sense! <code class="notranslate">XmlReader</code> does have a similar feature; it may be worth doing something similar here with <code class="notranslate">JsonStreamReader</code>.</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container has-reactions d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="5cn_u63sMLiEAtMgHG63qO0Joct8A7mdniowB4K3excmPS0fdTkVV_h534ev1T_ZioN-cN4wMkIjO3CsXrz9pQ" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUxNjA4NzkxNw==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
                  <button name="input[content]" id="reactions--reaction_button_component-508b92" value="THUMBS_UP react" data-button-index-position="0" data-reaction-label="+1" data-reaction-content="+1" aria-pressed="false" aria-label="react with thumbs up" type="submit" disabled="disabled" data-view-component="true" class="social-reaction-summary-item js-reaction-group-button btn-link d-flex no-underline color-fg-muted flex-items-baseline mr-2">    <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji">👍</g-emoji>
                    <span class="js-discussion-reaction-group-count">1</span>
        </button>  <tool-tip id="tooltip-862694a4-e384-4031-8a30-373e3ec33361" for="reactions--reaction_button_component-508b92" popover="manual" data-direction="n" data-type="description" data-view-component="true" class="sr-only position-absolute">andriysavin reacted with thumbs up emoji</tool-tip>
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                      <li class="dropdown-item" aria-label="andriysavin reacted with thumbs up emoji">
                        <g-emoji alias="+1" fallback-src="https://github.githubassets.com/assets/1f44d-41cb66fe1e22.png" class="social-button-emoji mr-2">👍</g-emoji>
                          <span>1 reaction</span>
                      </li>
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-516087917-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/516087917" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="W272ExfVqukANpKQWq3ykzTpB_NuNiOY7Ik3P4n-sIY9fIWJQdoRM5HYRaStgWXzhsIbgbA0e_xKazacw2njzA" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/516087917/edit_form?textarea_id=issuecomment-516087917-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUxNjEyNDkyOQ==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUxNjEyNDkyOQ=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUxNjEyNDkyOQ==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/12996633?s=80&amp;u=cf6a6d9ded07facc249a9e31b51d128d126364ef&amp;v=4" width="40" height="40" alt="@andriysavin" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-516124929">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="8003a74caa23622e37ef1b5b0b2b48f506e56db175a9c379c3f243c0f765de09">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/516124929/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUxNjEyNDkyOQ%3D%3D&amp;href=%23issuecomment-516124929"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">







        <span aria-label="This user is the author of this issue." data-view-component="true" class="tooltipped tooltipped-n">
          <span data-view-component="true" class="Label ml-1">Author</span>
        </span>

          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">andriysavin</a>


              </strong>



              commented


                <a href="#issuecomment-516124929" id="issuecomment-516124929-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-07-29T19:15:16Z" class="no-wrap">Jul 29, 2019</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto"><a class="user-mention notranslate" data-hovercard-type="user" data-hovercard-url="/users/scalablecory/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="https://github.com/scalablecory">@scalablecory</a> I guess you're talking about <code class="notranslate">ReadValueChunkAsync</code>, which seems to be the closest to the idea. But returning a Stream would be even better and ready to use functionality.</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="xDYBK5rBEgrY9d2HPffzsS_MR4dHGAybmJiRkqXS-dMHwtOPQhQ35aSO0SCOTHvASEaYPOUrh0QlidE5edl_YQ" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUxNjEyNDkyOQ==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-516124929-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/516124929" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="BE59txDUrLnCiSzwRkZpMOGWBYOdJVUnZ8HH0EfdEC2jBrsDr4StoItJpmvDd8MKZ9sn83FZgPhHkwoWlea-0Q" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/516124929/edit_form?textarea_id=issuecomment-516124929-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDUzNDY5MTM1Mw==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDUzNDY5MTM1Mw=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDUzNDY5MTM1Mw==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/xania/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/xania"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/34893682?s=80&amp;u=6aad20597012e5a49592c23d48ebd4ef4b69aec8&amp;v=4" width="40" height="40" alt="@xania" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-534691353">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="764c789f651cabb2bc98e5220a4cfa01fb273fdfdf7041ca46c6850a61d743a2">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/534691353/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDUzNDY5MTM1Mw%3D%3D&amp;href=%23issuecomment-534691353"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">








          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/xania/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/xania">xania</a>


              </strong>



              commented


                <a href="#issuecomment-534691353" id="issuecomment-534691353-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2019-09-24T18:35:33Z" class="no-wrap">Sep 24, 2019</relative-time></a>


              <span class="js-comment-edit-history">
          <span class="d-inline-block color-fg-muted">&#8226;</span>

          <details class="details-overlay details-reset d-inline-block dropdown hx_dropdown-fullscreen">
            <summary class="btn-link no-underline color-fg-muted js-notice">
              <div class="position-relative">
                <span>
                  edited

                </span>
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-triangle-down v-align-middle">
            <path d="m4.427 7.427 3.396 3.396a.25.25 0 0 0 .354 0l3.396-3.396A.25.25 0 0 0 11.396 7H4.604a.25.25 0 0 0-.177.427Z"></path>
        </svg>
              </div>
            </summary>
            <details-menu
              class="dropdown-menu dropdown-menu-s width-auto py-0 js-comment-edit-history-menu"
              style="max-width: 352px; z-index: 99;"
              src="/user_content_edits/show_edit_history_log/MDEyOklzc3VlQ29tbWVudDUzNDY5MTM1Mw=="
              preload
            >
              <include-fragment class="my-3" style="min-width: 100px;" aria-label="Loading...">
                <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="mx-auto d-block anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
              </include-fragment>
            </details-menu>
          </details>
        </span>

            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto">I guess everyone is going to write his owns extension method to read from the stream then, or am i the only one?</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="uLgjaLN4SKB_2wR704Mnq-dDUHrQyWQOGaGgF_0D-pV7TPHMa61tTwOgCNxgOK_agMmPwXL679GksOC8IQh8Jw" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDUzNDY5MTM1Mw==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-534691353-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/534691353" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="BY05Sr_bE9d7YAWBBTyAI7jI2mEz0RIgm3KeplG71g-lVKYsznhTeMA7jPyvzvDaaDRFmP5f9TVtIZNnXl0lnw" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/534691353/edit_form?textarea_id=issuecomment-534691353-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDE2OlRyYW5zZmVycmVkRXZlbnQyOTk5MDY2MDcy">

                  <div class="TimelineItem js-targetable-element"  data-team-hovercards-enabled  id="event-2999066072">
          <div class="TimelineItem-badge ">
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-arrow-right color-fg-inherit">
            <path d="M8.22 2.97a.75.75 0 0 1 1.06 0l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042l2.97-2.97H3.75a.75.75 0 0 1 0-1.5h7.44L8.22 4.03a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
          </div>
          <div class="TimelineItem-body">



                <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/msftgits/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/msftgits"><img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/9328564?s=40&amp;u=21b55f3cb635d5bd122113c7a63374604108a687&amp;v=4" width="20" height="20" alt="@msftgits" /></a>
        <a class="author Link--primary text-bold" data-hovercard-type="user" data-hovercard-url="/users/msftgits/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/msftgits">msftgits</a>




              transferred this issue from dotnet/corefx


            <a href="#event-2999066072" class="Link--secondary"><relative-time datetime="2020-02-01T03:38:15Z" class="no-wrap">Feb 1, 2020</relative-time></a>

          </div>
        </div>


          <div class="TimelineItem js-targetable-element"  data-team-hovercards-enabled  id="event-2999066081">
          <div class="TimelineItem-badge ">
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-milestone color-fg-inherit">
            <path d="M7.75 0a.75.75 0 0 1 .75.75V3h3.634c.414 0 .814.147 1.13.414l2.07 1.75a1.75 1.75 0 0 1 0 2.672l-2.07 1.75a1.75 1.75 0 0 1-1.13.414H8.5v5.25a.75.75 0 0 1-1.5 0V10H2.75A1.75 1.75 0 0 1 1 8.25v-3.5C1 3.784 1.784 3 2.75 3H7V.75A.75.75 0 0 1 7.75 0Zm4.384 8.5a.25.25 0 0 0 .161-.06l2.07-1.75a.248.248 0 0 0 0-.38l-2.07-1.75a.25.25 0 0 0-.161-.06H2.75a.25.25 0 0 0-.25.25v3.5c0 .138.112.25.25.25h9.384Z"></path>
        </svg>
          </div>
          <div class="TimelineItem-body">



                <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/msftgits/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/msftgits"><img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/9328564?s=40&amp;u=21b55f3cb635d5bd122113c7a63374604108a687&amp;v=4" width="20" height="20" alt="@msftgits" /></a>
        <a class="author Link--primary text-bold" data-hovercard-type="user" data-hovercard-url="/users/msftgits/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/msftgits">msftgits</a>




                added this to the   <a href="/dotnet/runtime/milestone/1" class="Link--primary text-bold">Future</a> milestone


            <a href="#event-2999066081" class="Link--secondary"><relative-time datetime="2020-02-01T03:38:17Z" class="no-wrap">Feb 1, 2020</relative-time></a>

          </div>
        </div>




        </div>

              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDU4OTg4NzY3NA==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDU4OTg4NzY3NA=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDU4OTg4NzY3NA==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/layomia/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/layomia"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/9868994?s=80&amp;u=933a7e45e40746c6c50069f521eb68ab221b05a2&amp;v=4" width="40" height="40" alt="@layomia" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-589887674">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="465ab0e55712a7dcb83bbfd5f5a88f819f0de6a7329138ac342913a35da1f8b1">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/589887674/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDU4OTg4NzY3NA%3D%3D&amp;href=%23issuecomment-589887674"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">




          <span aria-label="This user has previously committed to the runtime repository." data-view-component="true" class="tooltipped tooltipped-n">
            <span data-view-component="true" class="Label ml-1">Contributor</span>
        </span>



          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/layomia/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/layomia">layomia</a>


              </strong>



              commented


                <a href="#issuecomment-589887674" id="issuecomment-589887674-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2020-02-22T00:05:07Z" class="no-wrap">Feb 22, 2020</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <blockquote>
        <p dir="auto">I guess everyone is going to write his owns extension method to read from the stream then, or am i the only one?</p>
        </blockquote>
        <p dir="auto">This scenario is covered by <a class="issue-link js-issue-link" data-error-text="Failed to load title" data-id="558471474" data-permission-text="Title is private" data-url="https://github.com/dotnet/runtime/issues/30328" data-hovercard-type="issue" data-hovercard-url="/dotnet/runtime/issues/30328/hovercard" href="https://github.com/dotnet/runtime/issues/30328">#30328</a>.</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="9Uh0fSea8npEG2AU_sL21IrLFg1wATKnfQQdHuGPHXU2vKbZ_0_XlThgbLNNeX6l7UHJttIyuXjAFV21PYSbxw" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDU4OTg4NzY3NA==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-589887674-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/589887674" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="6y2QYrlc3wCrMIYXPpNRX6n7U909FHH3_cwb35DGrAzVD1HKPSkL7sM45AY8IQFwPM2OAc010sAtNXlbdvT56w" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/589887674/edit_form?textarea_id=issuecomment-589887674-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDEyOklzc3VlQ29tbWVudDU4OTg4NzczNg==">


        <div class="TimelineItem js-comment-container"
              data-gid="MDEyOklzc3VlQ29tbWVudDU4OTg4NzczNg=="
              data-url="/dotnet/runtime/comments/MDEyOklzc3VlQ29tbWVudDU4OTg4NzczNg==/partials/timeline_issue_comment"
              >

          <div class="avatar-parent-child TimelineItem-avatar d-none d-md-block">
          <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/layomia/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/layomia"><img class="avatar rounded-2 avatar-user" src="https://avatars.githubusercontent.com/u/9868994?s=80&amp;u=933a7e45e40746c6c50069f521eb68ab221b05a2&amp;v=4" width="40" height="40" alt="@layomia" /></a>

        </div>


          <div class="  timeline-comment-group js-minimizable-comment-group js-targetable-element TimelineItem-body my-0 " id="issuecomment-589887736">

            <div class="ml-n3 timeline-comment unminimized-comment comment previewable-edit js-task-list-container js-comment timeline-comment--caret reorderable-task-lists"
                data-body-version="867366737518850e8af0d5ab309594d7d77b4758002608e7d918b4be6c5deaad">
              <div class="timeline-comment-header new-comment-box-header clearfix d-flex"  data-morpheus-enabled="false">
          <div class="timeline-comment-actions flex-shrink-0 d-flex flex-items-center">
            <details class="details-overlay details-reset position-relative d-inline-block">
              <summary data-view-component="true" class="timeline-comment-action Link--secondary Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label"><svg aria-label="Show options" role="img" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-kebab-horizontal">
            <path d="M8 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3ZM1.5 9a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm13 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"></path>
        </svg></span>
          </span>
        </summary>

              <details-menu
                class="dropdown-menu dropdown-menu-sw show-more-popover color-fg-default"
                style="width:185px"
                src="/dotnet/runtime/issue_comments/589887736/comment_actions_menu?gid=MDEyOklzc3VlQ29tbWVudDU4OTg4NzczNg%3D%3D&amp;href=%23issuecomment-589887736"
                preload

              >
                  <include-fragment class="js-comment-header-actions-deferred-include-fragment">
                    <p class="text-center mt-3" data-hide-on-error>
                      <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
                    </p>
                    <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
                      <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                      Sorry, something went wrong.
                    </p>
                    <button
                      type="button"
                      class="dropdown-item btn-link js-comment-quote-reply"
                      hidden
                      data-hotkey="r"
                      role="menuitem"
                    >
                     Quote reply
                    </button>
                  </include-fragment>
              </details-menu>
            </details>
          </div>

          <div class="d-none d-sm-flex">




          <span aria-label="This user has previously committed to the runtime repository." data-view-component="true" class="tooltipped tooltipped-n">
            <span data-view-component="true" class="Label ml-1">Contributor</span>
        </span>



          </div>

          <h3 class="f5 text-normal" style="flex: 1 1 auto">
            <div>


              <strong>
                  <a class="author Link--primary text-bold css-overflow-wrap-anywhere " show_full_name="false" data-hovercard-type="user" data-hovercard-url="/users/layomia/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/layomia">layomia</a>


              </strong>



              commented


                <a href="#issuecomment-589887736" id="issuecomment-589887736-permalink" class="Link--secondary js-timestamp"><relative-time datetime="2020-02-22T00:05:22Z" class="no-wrap">Feb 22, 2020</relative-time></a>



            </div>

          </h3>
        </div>


              <div class="edit-comment-hide">

                <task-lists disabled sortable>
        <table class="d-block user-select-contain" data-paste-markdown-skip>
          <tbody class="d-block">
            <tr class="d-block">
              <td class="d-block comment-body markdown-body  js-comment-body">
                  <p dir="auto">Changes would need to be made to Utf8JsonReader to support this, which will affect performance for the common case. Closing as not feasible.</p>
              </td>
            </tr>
          </tbody>
        </table>
        </task-lists>


                <div class="d-flex">

                    <div class="pr-review-reactions">
                      <div data-view-component="true" class="comment-reactions just-bottom js-reactions-container js-reaction-buttons-container social-reactions reactions-container d-flex">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-pick-reaction" data-turbo="false" action="/dotnet/runtime/reactions" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="V0wD3OajSSkV1yk34V1ls7ZbDC0JQ5SiCs54V53fT7-UuNF4PnZsxmmsJZBS5u3C0dHTlqtwH3233zj8QdTJDQ" autocomplete="off" />
            <input type="hidden" name="input[subjectId]" value="MDEyOklzc3VlQ29tbWVudDU4OTg4NzczNg==">
              <input type="hidden" name="input[context]" value="" >
            <div  class="js-comment-reactions-options d-flex flex-items-center flex-row flex-wrap">
              <div class="js-reactions-container">
                <details class="dropdown details-reset details-overlay d-inline-block js-all-reactions-popover" hidden>
                  <summary aria-haspopup="true" data-view-component="true" class="Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">All reactions</span>
          </span>
        </summary>

                  <ul class="dropdown-menu dropdown-menu-se">
                  </ul>
                </details>
              </div>
            </div>
        </form></div>
                    </div>
                </div>
              </div>

              <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-comment-update" id="issuecomment-589887736-edit-form" data-turbo="false" action="/dotnet/runtime/issue_comments/589887736" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="e4uS6Sv2r6E2BI_g3yZ-n9Av2GemSDYf5t9wshxsA5YHHUhHkB2Fw2OyKeSY-ji14Rq7R629KoBNQXBYRwCrqQ" />
                    <include-fragment

          loading="lazy"
          src="/dotnet/runtime/issue_comments/589887736/edit_form?textarea_id=issuecomment-589887736-body&amp;comment_context="
          class="previewable-comment-form js-comment-edit-form-deferred-include-fragment"
        >
          <p class="text-center mt-3" data-hide-on-error>
            <svg aria-label="Loading..." style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
          </p>
          <p class="ml-1 mb-2 mt-2" data-show-on-error hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            Sorry, something went wrong.
          </p>
        </include-fragment>

        </form>    </div>
        </div>

        </div>


        </div>


              <div class="js-timeline-item js-timeline-progressive-focus-container" data-gid="MDExOkNsb3NlZEV2ZW50MzA2MjI5NDU3Mg==">

                  <div class="TimelineItem js-targetable-element"  data-team-hovercards-enabled  id="event-3062294572">
          <div class="TimelineItem-badge color-fg-on-emphasis color-bg-done-emphasis">
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-issue-closed color-fg-inherit">
            <path d="M11.28 6.78a.75.75 0 0 0-1.06-1.06L7.25 8.69 5.78 7.22a.75.75 0 0 0-1.06 1.06l2 2a.75.75 0 0 0 1.06 0l3.5-3.5Z"></path><path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0Zm-1.5 0a6.5 6.5 0 1 0-13 0 6.5 6.5 0 0 0 13 0Z"></path>
        </svg>
          </div>
          <div class="TimelineItem-body">



                <a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/layomia/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/layomia"><img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/9868994?s=40&amp;u=933a7e45e40746c6c50069f521eb68ab221b05a2&amp;v=4" width="20" height="20" alt="@layomia" /></a>
        <a class="author Link--primary text-bold" data-hovercard-type="user" data-hovercard-url="/users/layomia/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/layomia">layomia</a>




                  closed this as <a class="Link--secondary Link--inTextBlock" href="/dotnet/runtime/issues?q=is%3Aissue+is%3Aclosed+archived%3Afalse+reason%3Acompleted">completed</a>


            <a href="#event-3062294572" class="Link--secondary"><relative-time datetime="2020-02-22T00:05:22Z" class="no-wrap">Feb 22, 2020</relative-time></a>

          </div>
        </div>

          <div class="TimelineItem-break mb-0 height-full"></div>

          <div class="TimelineItem js-targetable-element"  data-team-hovercards-enabled  id="event-4103987429">
          <div class="TimelineItem-badge color-fg-on-emphasis color-bg-emphasis">
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-lock color-fg-inherit">
            <path d="M4 4a4 4 0 0 1 8 0v2h.25c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 12.25 15h-8.5A1.75 1.75 0 0 1 2 13.25v-5.5C2 6.784 2.784 6 3.75 6H4Zm8.25 3.5h-8.5a.25.25 0 0 0-.25.25v5.5c0 .138.112.25.25.25h8.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25ZM10.5 6V4a2.5 2.5 0 1 0-5 0v2Z"></path>
        </svg>
          </div>
          <div class="TimelineItem-body">



                <a class="d-inline-block" href="/apps/msftbot"><img class="avatar" src="https://avatars.githubusercontent.com/in/26612?s=40&amp;v=4" width="20" height="20" alt="@msftbot" /></a>
        <a class="author Link--primary text-bold" href="/apps/msftbot">msftbot</a>
        <span class="Label Label--secondary">bot</span>



              locked as <strong>resolved </strong>and limited conversation to collaborators


            <a href="#event-4103987429" class="Link--secondary"><relative-time datetime="2020-12-12T11:01:12Z" class="no-wrap">Dec 12, 2020</relative-time></a>

          </div>
        </div>




        </div>





          <!-- Rendered timeline since 2020-12-12 03:01:12 -->
          <div class="js-timeline-marker js-socket-channel js-updatable-content"
                id="partial-timeline"
                data-channel="eyJjIjoiaXNzdWU6NTU4NDcyMzc3IiwidCI6MTcwMjg0MTg0OX0=--b9c59fa88ecd271fea060c57a2a16959b846fc31add1664f97f55e9a076e5b33"
                data-url="/dotnet/runtime/issues/30402/partials/unread_timeline?issue=30402&amp;since=2020-12-12T03%3A01%3A12.000000000-08%3A00"
                data-last-modified="2020-12-12T03:01:12.000000000-08:00"
                data-morpheus-enabled="false"
                data-gid="MDU6SXNzdWU1NTg0NzIzNzc=">
            <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="d-none js-timeline-marker-form" data-turbo="false" action="/_graphql/MarkNotificationSubjectAsRead" accept-charset="UTF-8" data-remote="true" method="post"><input type="hidden" name="authenticity_token" value="OTB5CDFaipOyf3ElVipAbhS8ljpJ6jaj0NnniE8mxtbxESwoO7a6zlCNWnMaqhplFEgHXlGboCrjDB0T5wyw0g" />
              <input type="hidden" name="variables[subjectId]" value="MDU6SXNzdWU1NTg0NzIzNzc=">
        </form>  </div>
        </div>


          </div>



          <span id="issue-comment-box"></span>
          <div class="discussion-timeline-actions">
                    <div class="timeline-comment-wrapper timeline-new-comment js-comment-container js-targetable-element locked-conversation ml-0 pl-0 ml-md-6 pl-md-3" id="issuecomment-new">
          <div class=" d-none d-md-block">
          <span class="timeline-comment-avatar "><a class="d-inline-block" data-hovercard-type="user" data-hovercard-url="/users/dv00d00/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/dv00d00"><img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/1162823?s=80&amp;v=4" width="40" height="40" alt="@dv00d00" /></a></span>
          </div>
            <div class="">
              <!-- '"` --><!-- </textarea></xmp> --></option></form><form id="new_comment_form" aria-labelledby="previewable-comment-form-component-3bf4edf5-8230-4cc7-bc6c-6dfa2324e4b3-title" autocomplete="off" class="js-new-comment-form js-needs-timeline-marker-header" data-turbo="false" action="/dotnet/runtime/issue_comments" accept-charset="UTF-8" method="post"><input type="hidden" name="authenticity_token" value="SZzR-6vIqdrMRr5gl2Ljh6nzZjT6LpRU-Zgx9nnXR8VL6Di-FHVWIHPwSpeWHsx3k2y0CdmQJhku_4tAxOygQg" />
                <input type="text" name="required_field_13f3" hidden="hidden" class="form-control" /><input type="hidden" name="timestamp" value="1702841849113" autocomplete="off" class="form-control" /><input type="hidden" name="timestamp_secret" value="699ad9b20523f80ceffb177f04c3005ef63443c45e6a059709405f8b1ea65fc0" autocomplete="off" class="form-control" />
                <input type="hidden" name="issue" value="30402">

        <div class="previewable-comment-form">
          <div class="comment-form-head tabnav" aria-hidden="true">
            <div class="tabnav-tabs">
              <span class="tabnav-tab write-tab selected">Write</span>
              <span class="tabnav-tab preview-tab">Preview</span>
            </div>
          </div>
          <div data-view-component="true" class="blankslate">
            <svg aria-hidden="true" height="24" viewBox="0 0 24 24" version="1.1" width="24" data-view-component="true" class="octicon octicon-lock blankslate-icon">
            <path d="M6 9V7.25C6 3.845 8.503 1 12 1s6 2.845 6 6.25V9h.5a2.5 2.5 0 0 1 2.5 2.5v8a2.5 2.5 0 0 1-2.5 2.5h-13A2.5 2.5 0 0 1 3 19.5v-8A2.5 2.5 0 0 1 5.5 9Zm-1.5 2.5v8a1 1 0 0 0 1 1h13a1 1 0 0 0 1-1v-8a1 1 0 0 0-1-1h-13a1 1 0 0 0-1 1Zm3-4.25V9h9V7.25c0-2.67-1.922-4.75-4.5-4.75-2.578 0-4.5 2.08-4.5 4.75Z"></path>
        </svg>

            <p>This conversation has been locked as <strong>resolved</strong> and limited to collaborators.</p>

        </div></div>

        </form>    </div>

              <include-fragment src="/dotnet/runtime/sponsors-nudges"></include-fragment>

        </div>


          </div>
        </div>

        </div>
          <div data-view-component="true" class="Layout-sidebar">                  <div id="partial-discussion-sidebar"
          class="js-socket-channel js-updatable-content"
          data-channel="eyJjIjoiaXNzdWU6NTU4NDcyMzc3IiwidCI6MTcwMjg0MTg0OH0=--1b64a51fa112c483acc6db2745859f2b2460bef3b558e03bdc75de1d2c8b1472"
          data-gid="MDU6SXNzdWU1NTg0NzIzNzc="
          data-url="/dotnet/runtime/issues/30402/show_partial?partial=issues%2Fsidebar"
          data-project-hovercards-enabled>



            <div class="discussion-sidebar-item sidebar-assignee js-discussion-sidebar-item">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-issue-sidebar-form" aria-label="Select assignees" data-turbo="false" action="/dotnet/runtime/issues/30402/assignees" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="T8itS9qmQxKJzgDdellMikhVW04cdAmUonxTu8qOR4A2mJaq2xvSPFhLeugmNBJBrkAbX43jXDCqpb_5MUUPmQ" />


          <div class="discussion-sidebar-heading text-bold">
            Assignees
          </div>



        <span class="css-truncate js-issue-assignees">
            No one assigned
        </span>


        </form></div>



        <div class="discussion-sidebar-item js-discussion-sidebar-item">



          <div class="discussion-sidebar-heading text-bold">
            Labels
          </div>


            <div class="js-issue-labels d-flex flex-wrap">

        <a id="label-db30aa" href="/dotnet/runtime/labels/area-System.Text.Json" data-name="area-System.Text.Json" style="--label-r:212;--label-g:197;--label-b:249;--label-h:257;--label-s:81;--label-l:87;" data-view-component="true" class="IssueLabel hx_IssueLabel width-fit mb-1 mr-1">
            <span class="css-truncate css-truncate-target width-fit">area-System.Text.Json</span>
        </a>

        </div>

        </div>




          <div class="discussion-sidebar-item js-discussion-sidebar-item">
            <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-issue-sidebar-form" aria-label="Select projects" data-turbo="false" action="/dotnet/runtime/projects/issues/30402" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="sEt5sN-8DR8bhOqcEDGKQ9icfrCPoMtSi4_b6PmTWDMaqLeXVQYzRQYZa_nBZq9WHQAr9qFcjXrrrl6FYmdyrQ" />
                <div class="discussion-sidebar-heading text-bold">
            Projects
          </div>

                <div aria-live="polite">
        </div>
        <span class="css-truncate sidebar-progress-bar">
            None yet

        </span>

        </form>  </div>


              <div class="discussion-sidebar-item sidebar-progress-bar js-discussion-sidebar-item">
          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-issue-sidebar-form" aria-label="Select milestones" data-turbo="false" action="/dotnet/runtime/issues/30402/set_milestone?partial=issues%2Fsidebar%2Fshow%2Fmilestone" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="TY8Nmw43xsy2kO1E1VBIwaA1PP1gwjYF9sDcat4pMIDVZyFQ9l1n7xkWjTaNCEbH8Gy6CpE2itFQvBf-6iICRw" />

          <div class="discussion-sidebar-heading text-bold">
            Milestone
          </div>

              <span data-view-component="true" class="Progress mt-1 mb-2">
            <span style="width: 48.92678868552412%;" data-view-component="true" class="Progress-item color-bg-success-emphasis"></span>
        </span>  <a title="Future" href="/dotnet/runtime/milestone/1" class="Link--secondary mt-1 d-block text-bold css-truncate">
            <strong class="css-truncate-target width-fit">Future</strong>
          </a>

        </form></div>



              <create-branch
                data-default-repo="dotnet/runtime"
                data-selected-nwo="dotnet/runtime"
                data-default-source-branch="main"
                data-sidebar-url="/dotnet/runtime/issues/closing_references/partials/sidebar?source_id=558472377&amp;source_type=ISSUE"
                class="discussion-sidebar-item d-block">

        <div class="js-discussion-sidebar-item" data-target="create-branch.sidebarContainer">
          <div data-issue-and-pr-hovercards-enabled >
            <development-menu>
              <!-- '"` --><!-- </textarea></xmp> --></option></form><form data-target="create-branch.developmentForm" data-turbo="false" class="js-issue-sidebar-form" aria-label="Link issues" action="/dotnet/runtime/issues/closing_references?source_id=558472377&amp;source_type=ISSUE" accept-charset="UTF-8" method="post"><input type="hidden" name="_method" value="put" autocomplete="off" /><input type="hidden" name="authenticity_token" value="IMdVA6KnPxS-3ZJAYwgr1-Nmalhit-_kWmY-QCswpFH1BRk2jsH63RfbqN8PxFGEgITR9qutC3axjCVSqLGMaA" />

            <div class="discussion-sidebar-heading text-bold" >
            Development
          </div>



                      <p>No branches or pull requests</p>





        </form>    </development-menu>
          </div>
        </div>


              </create-branch>

              <div class="discussion-sidebar-item sidebar-notifications">
              <include-fragment loading="lazy" src="/notifications/thread_subscription?repository_id=210716005&amp;thread_class=Issue&amp;thread_id=558472377">
            <div data-hide-on-error>
                <svg style="box-sizing: content-box; color: var(--color-icon-primary);" width="32" height="32" viewBox="0 0 16 16" fill="none" data-view-component="true" class="mx-auto d-block anim-rotate">
          <circle cx="8" cy="8" r="7" stroke="currentColor" stroke-opacity="0.25" stroke-width="2" vector-effect="non-scaling-stroke" fill="none" />
          <path d="M15 8a7.002 7.002 0 00-7-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" vector-effect="non-scaling-stroke" />
        </svg>
            </div>
            <p data-show-on-error hidden>
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert mr-1">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
                Sorry, something went wrong and we weren't able to fetch your subscription status.
                <button data-retry-button="" type="button" data-view-component="true" class="Link--muted Button--link Button--medium Button">  <span class="Button-content">
            <span class="Button-label">Retry</span>
          </span>
        </button>

            </p>
          </include-fragment>

          </div>



              <div id="partial-users-participants" class="discussion-sidebar-item">
          <div class="participation">
            <div class="discussion-sidebar-heading text-bold">
              6 participants
            </div>
            <div class="participation-avatars d-flex flex-wrap">
                <a class="participant-avatar" data-hovercard-type="user" data-hovercard-url="/users/davidfowl/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/davidfowl">
                  <img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/95136?s=52&amp;v=4" width="26" height="26" alt="@davidfowl" />
        </a>        <a class="participant-avatar" data-hovercard-type="user" data-hovercard-url="/users/scalablecory/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/scalablecory">
                  <img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/4665835?s=52&amp;v=4" width="26" height="26" alt="@scalablecory" />
        </a>        <a class="participant-avatar" data-hovercard-type="user" data-hovercard-url="/users/msftgits/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/msftgits">
                  <img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/9328564?s=52&amp;v=4" width="26" height="26" alt="@msftgits" />
        </a>        <a class="participant-avatar" data-hovercard-type="user" data-hovercard-url="/users/layomia/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/layomia">
                  <img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/9868994?s=52&amp;v=4" width="26" height="26" alt="@layomia" />
        </a>        <a class="participant-avatar" data-hovercard-type="user" data-hovercard-url="/users/andriysavin/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/andriysavin">
                  <img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/12996633?s=52&amp;v=4" width="26" height="26" alt="@andriysavin" />
        </a>        <a class="participant-avatar" data-hovercard-type="user" data-hovercard-url="/users/xania/hovercard" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/xania">
                  <img class="avatar avatar-user" src="https://avatars.githubusercontent.com/u/34893682?s=52&amp;v=4" width="26" height="26" alt="@xania" />
        </a>    </div>
          </div>
        </div>













        </div>

        </div>

        </div>          </div>
                </div>


            </div>


          </div>

        </turbo-frame>


            </main>
          </div>

          </div>

                  <footer class="footer p-responsive pt-8 pb-6 f6 color-fg-muted" role="contentinfo">
          <h2 class='sr-only'>Footer</h2>


          <div class="d-flex flex-justify-center flex-items-center flex-column-reverse flex-lg-row flex-wrap flex-lg-nowrap">
            <div class="d-flex flex-items-center mx-2">
              <a aria-label="Homepage" title="GitHub" class="footer-octicon mr-2" href="https://github.com">
                <svg aria-hidden="true" height="24" viewBox="0 0 16 16" version="1.1" width="24" data-view-component="true" class="octicon octicon-mark-github">
            <path d="M8 0c4.42 0 8 3.58 8 8a8.013 8.013 0 0 1-5.45 7.59c-.4.08-.55-.17-.55-.38 0-.27.01-1.13.01-2.2 0-.75-.25-1.23-.54-1.48 1.78-.2 3.65-.88 3.65-3.95 0-.88-.31-1.59-.82-2.15.08-.2.36-1.02-.08-2.12 0 0-.67-.22-2.2.82-.64-.18-1.32-.27-2-.27-.68 0-1.36.09-2 .27-1.53-1.03-2.2-.82-2.2-.82-.44 1.1-.16 1.92-.08 2.12-.51.56-.82 1.28-.82 2.15 0 3.06 1.86 3.75 3.64 3.95-.23.2-.44.55-.51 1.07-.46.21-1.61.55-2.33-.66-.15-.24-.6-.83-1.23-.82-.67.01-.27.38.01.53.34.19.73.9.82 1.13.16.45.68 1.31 2.69.94 0 .67.01 1.3.01 1.49 0 .21-.15.45-.55.38A7.995 7.995 0 0 1 0 8c0-4.42 3.58-8 8-8Z"></path>
        </svg>
        </a>
              <span>
                &copy; 2023 GitHub, Inc.
              </span>
            </div>

            <nav aria-label="Footer">
              <h3 class="sr-only" id="sr-footer-heading">Footer navigation</h3>

              <ul class="list-style-none d-flex flex-justify-center flex-wrap mb-2 mb-lg-0" aria-labelledby="sr-footer-heading">

                  <li class="mx-2">
                    <a data-analytics-event="{&quot;category&quot;:&quot;Footer&quot;,&quot;action&quot;:&quot;go to Terms&quot;,&quot;label&quot;:&quot;text:terms&quot;}" href="https://docs.github.com/site-policy/github-terms/github-terms-of-service" data-view-component="true" class="Link--secondary Link">Terms</a>
                  </li>

                  <li class="mx-2">
                    <a data-analytics-event="{&quot;category&quot;:&quot;Footer&quot;,&quot;action&quot;:&quot;go to privacy&quot;,&quot;label&quot;:&quot;text:privacy&quot;}" href="https://docs.github.com/site-policy/privacy-policies/github-privacy-statement" data-view-component="true" class="Link--secondary Link">Privacy</a>
                  </li>

                  <li class="mx-2">
                    <a data-analytics-event="{&quot;category&quot;:&quot;Footer&quot;,&quot;action&quot;:&quot;go to security&quot;,&quot;label&quot;:&quot;text:security&quot;}" href="https://github.com/security" data-view-component="true" class="Link--secondary Link">Security</a>
                  </li>

                  <li class="mx-2">
                    <a data-analytics-event="{&quot;category&quot;:&quot;Footer&quot;,&quot;action&quot;:&quot;go to status&quot;,&quot;label&quot;:&quot;text:status&quot;}" href="https://www.githubstatus.com/" data-view-component="true" class="Link--secondary Link">Status</a>
                  </li>

                  <li class="mx-2">
                    <a data-analytics-event="{&quot;category&quot;:&quot;Footer&quot;,&quot;action&quot;:&quot;go to docs&quot;,&quot;label&quot;:&quot;text:docs&quot;}" href="https://docs.github.com" data-view-component="true" class="Link--secondary Link">Docs</a>
                  </li>

                  <li class="mx-2">
                    <a data-analytics-event="{&quot;category&quot;:&quot;Footer&quot;,&quot;action&quot;:&quot;go to contact&quot;,&quot;label&quot;:&quot;text:contact&quot;}" href="https://support.github.com?tags=dotcom-footer" data-view-component="true" class="Link--secondary Link">Contact</a>
                  </li>

                  <li>


        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/vendors-node_modules_consent-banner_dist_consent-banner_js-node_modules_github_catalyst_lib_i-f29431-4f8f4e329923.js"></script>

        <script crossorigin="anonymous" defer="defer" type="application/javascript" src="https://github.githubassets.com/assets/cookie-consent-management-b44675d8ec31.js"></script>

        <react-partial
          partial-name="cookie-consent-management"
          data-ssr="false"
        >

          <script type="application/json" data-target="react-partial.embeddedData">{"props":{"cookieConsentRequired":true,"initialCookieConsentAllowed":null}}</script>
          <div data-target="react-partial.reactRoot"></div>
        </react-partial>

                  </li>
              </ul>
            </nav>
          </div>

        </footer>





          <div id="ajax-error-message" class="ajax-error-message flash flash-error" hidden>
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-alert">
            <path d="M6.457 1.047c.659-1.234 2.427-1.234 3.086 0l6.082 11.378A1.75 1.75 0 0 1 14.082 15H1.918a1.75 1.75 0 0 1-1.543-2.575Zm1.763.707a.25.25 0 0 0-.44 0L1.698 13.132a.25.25 0 0 0 .22.368h12.164a.25.25 0 0 0 .22-.368Zm.53 3.996v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"></path>
        </svg>
            <button type="button" class="flash-close js-ajax-error-dismiss" aria-label="Dismiss error">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
            </button>
            You can’t perform that action at this time.
          </div>

            <template id="site-details-dialog">
          <details class="details-reset details-overlay details-overlay-dark lh-default color-fg-default hx_rsm" open>
            <summary role="button" aria-label="Close dialog"></summary>
            <details-dialog class="Box Box--overlay d-flex flex-column anim-fade-in fast hx_rsm-dialog hx_rsm-modal">
              <button class="Box-btn-octicon m-0 btn-octicon position-absolute right-0 top-0" type="button" aria-label="Close dialog" data-close-dialog>
                <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-x">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"></path>
        </svg>
              </button>
              <div class="octocat-spinner my-6 js-details-dialog-spinner"></div>
            </details-dialog>
          </details>
        </template>

            <div class="Popover js-hovercard-content position-absolute" style="display: none; outline: none;" tabindex="0">
          <div class="Popover-message Popover-message--bottom-left Popover-message--large Box color-shadow-large" style="width:360px;">
          </div>
        </div>

            <template id="snippet-clipboard-copy-button">
          <div class="zeroclipboard-container position-absolute right-0 top-0">
            <clipboard-copy aria-label="Copy" class="ClipboardButton btn js-clipboard-copy m-2 p-0 tooltipped-no-delay" data-copy-feedback="Copied!" data-tooltip-direction="w">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-copy js-clipboard-copy-icon m-2">
            <path d="M0 6.75C0 5.784.784 5 1.75 5h1.5a.75.75 0 0 1 0 1.5h-1.5a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-1.5a.75.75 0 0 1 1.5 0v1.5A1.75 1.75 0 0 1 9.25 16h-7.5A1.75 1.75 0 0 1 0 14.25Z"></path><path d="M5 1.75C5 .784 5.784 0 6.75 0h7.5C15.216 0 16 .784 16 1.75v7.5A1.75 1.75 0 0 1 14.25 11h-7.5A1.75 1.75 0 0 1 5 9.25Zm1.75-.25a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-7.5a.25.25 0 0 0-.25-.25Z"></path>
        </svg>
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-check js-clipboard-check-icon color-fg-success d-none m-2">
            <path d="M13.78 4.22a.75.75 0 0 1 0 1.06l-7.25 7.25a.75.75 0 0 1-1.06 0L2.22 9.28a.751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018L6 10.94l6.72-6.72a.75.75 0 0 1 1.06 0Z"></path>
        </svg>
            </clipboard-copy>
          </div>
        </template>
        <template id="snippet-clipboard-copy-button-unpositioned">
          <div class="zeroclipboard-container">
            <clipboard-copy aria-label="Copy" class="ClipboardButton btn btn-invisible js-clipboard-copy m-2 p-0 tooltipped-no-delay d-flex flex-justify-center flex-items-center" data-copy-feedback="Copied!" data-tooltip-direction="w">
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-copy js-clipboard-copy-icon">
            <path d="M0 6.75C0 5.784.784 5 1.75 5h1.5a.75.75 0 0 1 0 1.5h-1.5a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-1.5a.75.75 0 0 1 1.5 0v1.5A1.75 1.75 0 0 1 9.25 16h-7.5A1.75 1.75 0 0 1 0 14.25Z"></path><path d="M5 1.75C5 .784 5.784 0 6.75 0h7.5C15.216 0 16 .784 16 1.75v7.5A1.75 1.75 0 0 1 14.25 11h-7.5A1.75 1.75 0 0 1 5 9.25Zm1.75-.25a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-7.5a.25.25 0 0 0-.25-.25Z"></path>
        </svg>
              <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" class="octicon octicon-check js-clipboard-check-icon color-fg-success d-none">
            <path d="M13.78 4.22a.75.75 0 0 1 0 1.06l-7.25 7.25a.75.75 0 0 1-1.06 0L2.22 9.28a.751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018L6 10.94l6.72-6.72a.75.75 0 0 1 1.06 0Z"></path>
        </svg>
            </clipboard-copy>
          </div>
        </template>


            <style>
              .user-mention[href$="/dv00d00"] {
                color: var(--color-user-mention-fg);
                background-color: var(--color-user-mention-bg);
                border-radius: 2px;
                margin-left: -2px;
                margin-right: -2px;
                padding: 0 2px;
              }
            </style>


            </div>

            <div id="js-global-screen-reader-notice" class="sr-only" aria-live="polite" aria-atomic="true" ></div>
            <div id="js-global-screen-reader-notice-assertive" class="sr-only" aria-live="assertive" aria-atomic="true"></div>
          </body>
        </html>
        """;
}
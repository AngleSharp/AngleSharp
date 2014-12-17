<?xml version="1.0" encoding="GB2312"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
  <head>
    <title>ktmatu - Test page for Simplified Chinese in       GB2312</title>
 <meta name="description" content="This test page can be used to check whether the web browser is correctly configured to show Simplified Chinese characters in GB2312." /> 
 <meta name="keywords" content="Chinese, GB2312, GB, 2312, Simplified Chinese, HTML, XHTML" /> 
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <meta name="DC.Date.Created" content="2001-10-24T13:10" />
    <meta name="DC.Creator" content="Matti Tukiainen" />
    <link href="mailto:ktmatu@surfeu.fi.remove.invalid" rev="made" />
    <style type="text/css" media="screen">
    <!--
/* Common CSS definitions for ktmatu.com */

body   {
    background: white;
    color: black;
    /* font-family: serif; */
}

A:link {
    color: #000066;
    background: white;
}
A:active {
    color: #ff0000;
    background: white;
}
A:visited {
    color: #0066cc;
    background: white;
}

.fancy-header {
    color: #000066;
    background: white;
    font-family: sans-serif;
}

.fancy-hr {
    color: black;
    background: #cccccc;
}

.navi-text {
    color: black;
    background: white;
    /* font-family: serif; */
    /* font-size: small; */
}

.navi-header {
    color: black;
    background: white;
    /* font-family: serif; */
    font-size: small;
}

    -->
    </style>
  </head>
  <body>

    <!-- header-begin -->
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
      <tr valign="bottom">
        <td align="left" valign="bottom">
          <a href="http://ktmatu.com"><img src="../../pic/ktmatu-logo.gif" alt="ktmatu" width="180" height="38" border="0" /></a>
        </td>
        <td align="right" valign="bottom">
          <table border="0">
            <tr align="right" valign="bottom">
              <td><small class="navi-text"><!--navi1--><a href="../../" title="ktmatu home page">Home</a> | <a href="../../site-info/sitemap.html" title="Site map of this web site">Site Map</a> | <a href="../../site-info/site-search.html" title="Search this web site">Site Search</a><!--navi1--></small></td>
            </tr>
            <tr align="right" valign="bottom">
              <td><small class="navi-text"><a href="../../" title="ktmatu home page">Home</a> &gt; <a href="../../info/" title="Information and how to documents covering various subjects">Information</a> &gt; <a href="../../info/view-and-browse-chinese-web-pages/" title="Instructions on how to set up a web browser running on Microsoft Windows to show Chinese web pages without any extra programs">View and browse Chinese web pages</a> &gt;</small></td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td class="fancy-hr" width="10" colspan="2">
          &nbsp;
        </td>
      </tr>
    </table>
    <!-- header-end -->



    <h1 class="fancy-header">Simplified Chinese in GB2312 test</h1>

    <table border="1">
      <tr>
        <th>
          What you see
        </th>
        <th>
          What should you see
        </th>
      </tr>

      <tr>
        <td>
          <span style="font-size: xx-large">汉字</span>
          <!-- [han4 zi4] {hàn zì} /Chinese character/ -->
        </td>
        <td>
          <img src="test-simplified.png" 
           alt="Simplified Chinese image" />
        </td>
      </tr>
    </table>

    <p>&nbsp;</p>

    <!-- footer-begin -->
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td class="fancy-hr" width="10" colspan="2">
          &nbsp;
        </td>
      </tr>
      <tr>
        <td align="left">
          <small class="navi-text"><!--navi1--><a href="../../" title="ktmatu home page">Home</a> |           <a href="../../software/" title="Software for log analysis and log management, CGI scripts for webmasters, etc.">Software</a> | <a href="../../info/" title="Information and how to documents covering various subjects">Information</a> | <a href="../../etsin/" title="Etsin search - The ultimate search tool for webmasters">Etsin</a> |           <a href="../../chinese/" title="Chinese language dictionaries and vocabulary lists">Chinese</a> | <a href="../../xmas/" title="Interactive customizable advent calendars">Christmas Calendars</a> |           <a href="../../site-info/" title="Information about this web site, site search, site map, privacy statement, etc.">Site Info</a><!--navi1--></small>
        </td>
        <td align="right"><small class="navi-text">&nbsp;</small></td>
      </tr>
    </table>
    <!-- footer-end -->


  </body>
</html>

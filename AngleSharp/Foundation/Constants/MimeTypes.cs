namespace AngleSharp
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains a list of common mime-types.
    /// </summary>
    static class MimeTypes
    {
        #region Map File extensions to Mime types

        static Dictionary<String, String> _extensions = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);

        static MimeTypes()
        {
            _extensions.Add(".3dm", "x-world/x-3dmf");
            _extensions.Add(".3dmf", "x-world/x-3dmf");
            _extensions.Add(".a", "application/octet-stream");
            _extensions.Add(".aab", "application/x-authorware-bin");
            _extensions.Add(".aam", "application/x-authorware-map");
            _extensions.Add(".aas", "application/x-authorware-seg");
            _extensions.Add(".abc", "text/vnd.abc");
            _extensions.Add(".acgi", "text/html");
            _extensions.Add(".afl", "video/animaflex");
            _extensions.Add(".ai", "application/postscript");
            _extensions.Add(".aif", "audio/aiff");
            _extensions.Add(".aif", "audio/x-aiff");
            _extensions.Add(".aifc", "audio/aiff");
            _extensions.Add(".aifc", "audio/x-aiff");
            _extensions.Add(".aiff", "audio/aiff");
            _extensions.Add(".aiff", "audio/x-aiff");
            _extensions.Add(".aim", "application/x-aim");
            _extensions.Add(".aip", "text/x-audiosoft-intra");
            _extensions.Add(".ani", "application/x-navi-animation");
            _extensions.Add(".aos", "application/x-nokia-9000-communicator-add-on-software");
            _extensions.Add(".aps", "application/mime");
            _extensions.Add(".arc", "application/octet-stream");
            _extensions.Add(".arj", "application/arj");
            _extensions.Add(".arj", "application/octet-stream");
            _extensions.Add(".art", "image/x-jg");
            _extensions.Add(".asf", "video/x-ms-asf");
            _extensions.Add(".asm", "text/x-asm");
            _extensions.Add(".asp", "text/asp");
            _extensions.Add(".asx", "application/x-mplayer2");
            _extensions.Add(".asx", "video/x-ms-asf");
            _extensions.Add(".asx", "video/x-ms-asf-plugin");
            _extensions.Add(".au", "audio/basic");
            _extensions.Add(".au", "audio/x-au");
            _extensions.Add(".avi", "application/x-troff-msvideo");
            _extensions.Add(".avi", "video/avi");
            _extensions.Add(".avi", "video/msvideo");
            _extensions.Add(".avi", "video/x-msvideo");
            _extensions.Add(".avs", "video/avs-video");
            _extensions.Add(".bcpio", "application/x-bcpio");
            _extensions.Add(".bin", "application/mac-binary");
            _extensions.Add(".bin", "application/macbinary");
            _extensions.Add(".bin", "application/octet-stream");
            _extensions.Add(".bin", "application/x-binary");
            _extensions.Add(".bin", "application/x-macbinary");
            _extensions.Add(".bm", "image/bmp");
            _extensions.Add(".bmp", "image/bmp");
            _extensions.Add(".bmp", "image/x-windows-bmp");
            _extensions.Add(".boo", "application/book");
            _extensions.Add(".book", "application/book");
            _extensions.Add(".boz", "application/x-bzip2");
            _extensions.Add(".bsh", "application/x-bsh");
            _extensions.Add(".bz", "application/x-bzip");
            _extensions.Add(".bz2", "application/x-bzip2");
            _extensions.Add(".c", "text/plain");
            _extensions.Add(".c", "text/x-c");
            _extensions.Add(".c", "+	text/plain");
            _extensions.Add(".cat", "application/vnd.ms-pki.seccat");
            _extensions.Add(".cc", "text/plain");
            _extensions.Add(".cc", "text/x-c");
            _extensions.Add(".ccad", "application/clariscad");
            _extensions.Add(".cco", "application/x-cocoa");
            _extensions.Add(".cdf", "application/cdf");
            _extensions.Add(".cdf", "application/x-cdf");
            _extensions.Add(".cdf", "application/x-netcdf");
            _extensions.Add(".cer", "application/pkix-cert");
            _extensions.Add(".cer", "application/x-x509-ca-cert");
            _extensions.Add(".cha", "application/x-chat");
            _extensions.Add(".chat", "application/x-chat");
            _extensions.Add(".class", "application/java");
            _extensions.Add(".class", "application/java-byte-code");
            _extensions.Add(".class", "application/x-java-class");
            _extensions.Add(".com", "application/octet-stream");
            _extensions.Add(".com", "text/plain");
            _extensions.Add(".conf", "text/plain");
            _extensions.Add(".cpio", "application/x-cpio");
            _extensions.Add(".cpp", "text/x-c");
            _extensions.Add(".cpt", "application/mac-compactpro");
            _extensions.Add(".cpt", "application/x-compactpro");
            _extensions.Add(".cpt", "application/x-cpt");
            _extensions.Add(".crl", "application/pkcs-crl");
            _extensions.Add(".crl", "application/pkix-crl");
            _extensions.Add(".crt", "application/pkix-cert");
            _extensions.Add(".crt", "application/x-x509-ca-cert");
            _extensions.Add(".crt", "application/x-x509-user-cert");
            _extensions.Add(".csh", "application/x-csh");
            _extensions.Add(".csh", "text/x-script.csh");
            _extensions.Add(".css", "application/x-pointplus");
            _extensions.Add(".css", "text/css");
            _extensions.Add(".cxx", "text/plain");
            _extensions.Add(".dcr", "application/x-director");
            _extensions.Add(".deepv", "application/x-deepv");
            _extensions.Add(".def", "text/plain");
            _extensions.Add(".der", "application/x-x509-ca-cert");
            _extensions.Add(".dif", "video/x-dv");
            _extensions.Add(".dir", "application/x-director");
            _extensions.Add(".dl", "video/dl");
            _extensions.Add(".dl", "video/x-dl");
            _extensions.Add(".doc", "application/msword");
            _extensions.Add(".dot", "application/msword");
            _extensions.Add(".dp", "application/commonground");
            _extensions.Add(".drw", "application/drafting");
            _extensions.Add(".dump", "application/octet-stream");
            _extensions.Add(".dv", "video/x-dv");
            _extensions.Add(".dvi", "application/x-dvi");
            _extensions.Add(".dwf", "drawing/x-dwf (old)");
            _extensions.Add(".dwf", "model/vnd.dwf");
            _extensions.Add(".dwg", "application/acad");
            _extensions.Add(".dwg", "image/vnd.dwg");
            _extensions.Add(".dwg", "image/x-dwg");
            _extensions.Add(".dxf", "application/dxf");
            _extensions.Add(".dxf", "image/vnd.dwg");
            _extensions.Add(".dxf", "image/x-dwg");
            _extensions.Add(".dxr", "application/x-director");
            _extensions.Add(".el", "text/x-script.elisp");
            _extensions.Add(".elc", "application/x-bytecode.elisp (compiled elisp)");
            _extensions.Add(".elc", "application/x-elc");
            _extensions.Add(".env", "application/x-envoy");
            _extensions.Add(".eps", "application/postscript");
            _extensions.Add(".es", "application/x-esrehber");
            _extensions.Add(".etx", "text/x-setext");
            _extensions.Add(".evy", "application/envoy");
            _extensions.Add(".evy", "application/x-envoy");
            _extensions.Add(".exe", "application/octet-stream");
            _extensions.Add(".f", "text/plain");
            _extensions.Add(".f", "text/x-fortran");
            _extensions.Add(".f77", "text/x-fortran");
            _extensions.Add(".f90", "text/plain");
            _extensions.Add(".f90", "text/x-fortran");
            _extensions.Add(".fdf", "application/vnd.fdf");
            _extensions.Add(".fif", "application/fractals");
            _extensions.Add(".fif", "image/fif");
            _extensions.Add(".fli", "video/fli");
            _extensions.Add(".fli", "video/x-fli");
            _extensions.Add(".flo", "image/florian");
            _extensions.Add(".flx", "text/vnd.fmi.flexstor");
            _extensions.Add(".fmf", "video/x-atomic3d-feature");
            _extensions.Add(".for", "text/plain");
            _extensions.Add(".for", "text/x-fortran");
            _extensions.Add(".fpx", "image/vnd.fpx");
            _extensions.Add(".fpx", "image/vnd.net-fpx");
            _extensions.Add(".frl", "application/freeloader");
            _extensions.Add(".funk", "audio/make");
            _extensions.Add(".g", "text/plain");
            _extensions.Add(".g3", "image/g3fax");
            _extensions.Add(".gif", "image/gif");
            _extensions.Add(".gl", "video/gl");
            _extensions.Add(".gl", "video/x-gl");
            _extensions.Add(".gsd", "audio/x-gsm");
            _extensions.Add(".gsm", "audio/x-gsm");
            _extensions.Add(".gsp", "application/x-gsp");
            _extensions.Add(".gss", "application/x-gss");
            _extensions.Add(".gtar", "application/x-gtar");
            _extensions.Add(".gz", "application/x-compressed");
            _extensions.Add(".gz", "application/x-gzip");
            _extensions.Add(".gzip", "application/x-gzip");
            _extensions.Add(".gzip", "multipart/x-gzip");
            _extensions.Add(".h", "text/plain");
            _extensions.Add(".h", "text/x-h");
            _extensions.Add(".hdf", "application/x-hdf");
            _extensions.Add(".help", "application/x-helpfile");
            _extensions.Add(".hgl", "application/vnd.hp-hpgl");
            _extensions.Add(".hh", "text/plain");
            _extensions.Add(".hh", "text/x-h");
            _extensions.Add(".hlb", "text/x-script");
            _extensions.Add(".hlp", "application/hlp");
            _extensions.Add(".hlp", "application/x-helpfile");
            _extensions.Add(".hlp", "application/x-winhelp");
            _extensions.Add(".hpg", "application/vnd.hp-hpgl");
            _extensions.Add(".hpgl", "application/vnd.hp-hpgl");
            _extensions.Add(".hqx", "application/binhex");
            _extensions.Add(".hqx", "application/binhex4");
            _extensions.Add(".hqx", "application/mac-binhex");
            _extensions.Add(".hqx", "application/mac-binhex40");
            _extensions.Add(".hqx", "application/x-binhex40");
            _extensions.Add(".hqx", "application/x-mac-binhex40");
            _extensions.Add(".hta", "application/hta");
            _extensions.Add(".htc", "text/x-component");
            _extensions.Add(".htm", "text/html");
            _extensions.Add(".html", "text/html");
            _extensions.Add(".htmls", "text/html");
            _extensions.Add(".htt", "text/webviewhtml");
            _extensions.Add(".htx", "text/html");
            _extensions.Add(".ice", "x-conference/x-cooltalk");
            _extensions.Add(".ico", "image/x-icon");
            _extensions.Add(".idc", "text/plain");
            _extensions.Add(".ief", "image/ief");
            _extensions.Add(".iefs", "image/ief");
            _extensions.Add(".iges", "application/iges");
            _extensions.Add(".iges", "model/iges");
            _extensions.Add(".igs", "application/iges");
            _extensions.Add(".igs", "model/iges");
            _extensions.Add(".ima", "application/x-ima");
            _extensions.Add(".imap", "application/x-httpd-imap");
            _extensions.Add(".inf", "application/inf");
            _extensions.Add(".ins", "application/x-internett-signup");
            _extensions.Add(".ip", "application/x-ip2");
            _extensions.Add(".isu", "video/x-isvideo");
            _extensions.Add(".it", "audio/it");
            _extensions.Add(".iv", "application/x-inventor");
            _extensions.Add(".ivr", "i-world/i-vrml");
            _extensions.Add(".ivy", "application/x-livescreen");
            _extensions.Add(".jam", "audio/x-jam");
            _extensions.Add(".jav", "text/plain");
            _extensions.Add(".jav", "text/x-java-source");
            _extensions.Add(".java", "text/plain");
            _extensions.Add(".java", "text/x-java-source");
            _extensions.Add(".jcm", "application/x-java-commerce");
            _extensions.Add(".jfif", "image/jpeg");
            _extensions.Add(".jfif", "image/pjpeg");
            _extensions.Add(".jfif", "tbnl	image/jpeg");
            _extensions.Add(".jpe", "image/jpeg");
            _extensions.Add(".jpe", "image/pjpeg");
            _extensions.Add(".jpeg", "image/jpeg");
            _extensions.Add(".jpeg", "image/pjpeg");
            _extensions.Add(".jpg", "image/jpeg");
            _extensions.Add(".jpg", "image/pjpeg");
            _extensions.Add(".jps", "image/x-jps");
            _extensions.Add(".js", "application/x-javascript");
            _extensions.Add(".js", "application/javascript");
            _extensions.Add(".js", "application/ecmascript");
            _extensions.Add(".js", "text/javascript");
            _extensions.Add(".js", "text/ecmascript");
            _extensions.Add(".jut", "image/jutvision");
            _extensions.Add(".kar", "audio/midi");
            _extensions.Add(".kar", "music/x-karaoke");
            _extensions.Add(".ksh", "application/x-ksh");
            _extensions.Add(".ksh", "text/x-script.ksh");
            _extensions.Add(".la", "audio/nspaudio");
            _extensions.Add(".la", "audio/x-nspaudio");
            _extensions.Add(".lam", "audio/x-liveaudio");
            _extensions.Add(".latex", "application/x-latex");
            _extensions.Add(".lha", "application/lha");
            _extensions.Add(".lha", "application/octet-stream");
            _extensions.Add(".lha", "application/x-lha");
            _extensions.Add(".lhx", "application/octet-stream");
            _extensions.Add(".list", "text/plain");
            _extensions.Add(".lma", "audio/nspaudio");
            _extensions.Add(".lma", "audio/x-nspaudio");
            _extensions.Add(".log", "text/plain");
            _extensions.Add(".lsp", "application/x-lisp");
            _extensions.Add(".lsp", "text/x-script.lisp");
            _extensions.Add(".lst", "text/plain");
            _extensions.Add(".lsx", "text/x-la-asf");
            _extensions.Add(".ltx", "application/x-latex");
            _extensions.Add(".lzh", "application/octet-stream");
            _extensions.Add(".lzh", "application/x-lzh");
            _extensions.Add(".lzx", "application/lzx");
            _extensions.Add(".lzx", "application/octet-stream");
            _extensions.Add(".lzx", "application/x-lzx");
            _extensions.Add(".m", "text/plain");
            _extensions.Add(".m", "text/x-m");
            _extensions.Add(".m1v", "video/mpeg");
            _extensions.Add(".m2a", "audio/mpeg");
            _extensions.Add(".m2v", "video/mpeg");
            _extensions.Add(".m3u", "audio/x-mpequrl");
            _extensions.Add(".man", "application/x-troff-man");
            _extensions.Add(".map", "application/x-navimap");
            _extensions.Add(".mar", "text/plain");
            _extensions.Add(".mbd", "application/mbedlet");
            _extensions.Add(".mc", "	application/x-magic-cap-package-1.0");
            _extensions.Add(".mcd", "application/mcad");
            _extensions.Add(".mcd", "application/x-mathcad");
            _extensions.Add(".mcf", "image/vasa");
            _extensions.Add(".mcf", "text/mcf");
            _extensions.Add(".mcp", "application/netmc");
            _extensions.Add(".me", "application/x-troff-me");
            _extensions.Add(".mht", "message/rfc822");
            _extensions.Add(".mhtml", "message/rfc822");
            _extensions.Add(".mid", "application/x-midi");
            _extensions.Add(".mid", "audio/midi");
            _extensions.Add(".mid", "audio/x-mid");
            _extensions.Add(".mid", "audio/x-midi");
            _extensions.Add(".mid", "music/crescendo");
            _extensions.Add(".mid", "x-music/x-midi");
            _extensions.Add(".midi", "application/x-midi");
            _extensions.Add(".midi", "audio/midi");
            _extensions.Add(".midi", "audio/x-mid");
            _extensions.Add(".midi", "audio/x-midi");
            _extensions.Add(".midi", "music/crescendo");
            _extensions.Add(".midi", "x-music/x-midi");
            _extensions.Add(".mif", "application/x-frame");
            _extensions.Add(".mif", "application/x-mif");
            _extensions.Add(".mime", "message/rfc822");
            _extensions.Add(".mime", "www/mime");
            _extensions.Add(".mjf", "audio/x-vnd.audioexplosion.mjuicemediafile");
            _extensions.Add(".mjpg", "video/x-motion-jpeg");
            _extensions.Add(".mm", "application/base64");
            _extensions.Add(".mm", "application/x-meme");
            _extensions.Add(".mme", "application/base64");
            _extensions.Add(".mod", "audio/mod");
            _extensions.Add(".mod", "audio/x-mod");
            _extensions.Add(".moov", "video/quicktime");
            _extensions.Add(".mov", "video/quicktime");
            _extensions.Add(".movie", "video/x-sgi-movie");
            _extensions.Add(".mp2", "audio/mpeg");
            _extensions.Add(".mp2", "audio/x-mpeg");
            _extensions.Add(".mp2", "video/mpeg");
            _extensions.Add(".mp2", "video/x-mpeg");
            _extensions.Add(".mp2", "video/x-mpeq2a");
            _extensions.Add(".mp3", "audio/mpeg3");
            _extensions.Add(".mp3", "audio/x-mpeg-3");
            _extensions.Add(".mp3", "video/mpeg");
            _extensions.Add(".mp3", "video/x-mpeg");
            _extensions.Add(".mpa", "audio/mpeg");
            _extensions.Add(".mpa", "video/mpeg");
            _extensions.Add(".mpc", "application/x-project");
            _extensions.Add(".mpe", "video/mpeg");
            _extensions.Add(".mpeg", "video/mpeg");
            _extensions.Add(".mpg", "audio/mpeg");
            _extensions.Add(".mpg", "video/mpeg");
            _extensions.Add(".mpga", "audio/mpeg");
            _extensions.Add(".mpp", "application/vnd.ms-project");
            _extensions.Add(".mpt", "application/x-project");
            _extensions.Add(".mpv", "application/x-project");
            _extensions.Add(".mpx", "application/x-project");
            _extensions.Add(".mrc", "application/marc");
            _extensions.Add(".ms", "application/x-troff-ms");
            _extensions.Add(".mv", "video/x-sgi-movie");
            _extensions.Add(".my", "audio/make");
            _extensions.Add(".mzz", "application/x-vnd.audioexplosion.mzz");
            _extensions.Add(".nap", "image/naplps");
            _extensions.Add(".naplps", "image/naplps");
            _extensions.Add(".nc", "application/x-netcdf");
            _extensions.Add(".ncm", "application/vnd.nokia.configuration-message");
            _extensions.Add(".nif", "image/x-niff");
            _extensions.Add(".niff", "image/x-niff");
            _extensions.Add(".nix", "application/x-mix-transfer");
            _extensions.Add(".nsc", "application/x-conference");
            _extensions.Add(".nvd", "application/x-navidoc");
            _extensions.Add(".o", "application/octet-stream");
            _extensions.Add(".oda", "application/oda");
            _extensions.Add(".omc", "application/x-omc");
            _extensions.Add(".omcd", "application/x-omcdatamaker");
            _extensions.Add(".omcr", "application/x-omcregerator");
            _extensions.Add(".p", "text/x-pascal");
            _extensions.Add(".p10", "application/pkcs10");
            _extensions.Add(".p10", "application/x-pkcs10");
            _extensions.Add(".p12", "application/pkcs-12");
            _extensions.Add(".p12", "application/x-pkcs12");
            _extensions.Add(".p7a", "application/x-pkcs7-signature");
            _extensions.Add(".p7c", "application/pkcs7-mime");
            _extensions.Add(".p7c", "application/x-pkcs7-mime");
            _extensions.Add(".p7m", "application/pkcs7-mime");
            _extensions.Add(".p7m", "application/x-pkcs7-mime");
            _extensions.Add(".p7r", "application/x-pkcs7-certreqresp");
            _extensions.Add(".p7s", "application/pkcs7-signature");
            _extensions.Add(".part", "application/pro_eng");
            _extensions.Add(".pas", "text/pascal");
            _extensions.Add(".pbm", "image/x-portable-bitmap");
            _extensions.Add(".pcl", "application/vnd.hp-pcl");
            _extensions.Add(".pcl", "application/x-pcl");
            _extensions.Add(".pct", "image/x-pict");
            _extensions.Add(".pcx", "image/x-pcx");
            _extensions.Add(".pdb", "chemical/x-pdb");
            _extensions.Add(".pdf", "application/pdf");
            _extensions.Add(".pfunk", "audio/make");
            _extensions.Add(".pfunk", "audio/make.my.funk");
            _extensions.Add(".pgm", "image/x-portable-graymap");
            _extensions.Add(".pgm", "image/x-portable-greymap");
            _extensions.Add(".pic", "image/pict");
            _extensions.Add(".pict", "image/pict");
            _extensions.Add(".pkg", "application/x-newton-compatible-pkg");
            _extensions.Add(".pko", "application/vnd.ms-pki.pko");
            _extensions.Add(".pl", "text/plain");
            _extensions.Add(".pl", "text/x-script.perl");
            _extensions.Add(".plx", "application/x-pixclscript");
            _extensions.Add(".pm", "image/x-xpixmap");
            _extensions.Add(".pm", "text/x-script.perl-module");
            _extensions.Add(".pm4", "application/x-pagemaker");
            _extensions.Add(".pm5", "application/x-pagemaker");
            _extensions.Add(".png", "image/png");
            _extensions.Add(".pnm", "application/x-portable-anymap");
            _extensions.Add(".pnm", "image/x-portable-anymap");
            _extensions.Add(".pot", "application/mspowerpoint");
            _extensions.Add(".pot", "application/vnd.ms-powerpoint");
            _extensions.Add(".pov", "model/x-pov");
            _extensions.Add(".ppa", "application/vnd.ms-powerpoint");
            _extensions.Add(".ppm", "image/x-portable-pixmap");
            _extensions.Add(".pps", "application/mspowerpoint");
            _extensions.Add(".pps", "application/vnd.ms-powerpoint");
            _extensions.Add(".ppt", "application/mspowerpoint");
            _extensions.Add(".ppt", "application/powerpoint");
            _extensions.Add(".ppt", "application/vnd.ms-powerpoint");
            _extensions.Add(".ppt", "application/x-mspowerpoint");
            _extensions.Add(".ppz", "application/mspowerpoint");
            _extensions.Add(".pre", "application/x-freelance");
            _extensions.Add(".prt", "application/pro_eng");
            _extensions.Add(".ps", "application/postscript");
            _extensions.Add(".psd", "application/octet-stream");
            _extensions.Add(".pvu", "paleovu/x-pv");
            _extensions.Add(".pwz", "application/vnd.ms-powerpoint");
            _extensions.Add(".py", "text/x-script.phyton");
            _extensions.Add(".pyc", "applicaiton/x-bytecode.python");
            _extensions.Add(".qcp", "audio/vnd.qcelp");
            _extensions.Add(".qd3", "x-world/x-3dmf");
            _extensions.Add(".qd3d", "x-world/x-3dmf");
            _extensions.Add(".qif", "image/x-quicktime");
            _extensions.Add(".qt", "video/quicktime");
            _extensions.Add(".qtc", "video/x-qtc");
            _extensions.Add(".qti", "image/x-quicktime");
            _extensions.Add(".qtif", "image/x-quicktime");
            _extensions.Add(".ra", "audio/x-pn-realaudio");
            _extensions.Add(".ra", "audio/x-pn-realaudio-plugin");
            _extensions.Add(".ra", "audio/x-realaudio");
            _extensions.Add(".ram", "audio/x-pn-realaudio");
            _extensions.Add(".ras", "application/x-cmu-raster");
            _extensions.Add(".ras", "image/cmu-raster");
            _extensions.Add(".ras", "image/x-cmu-raster");
            _extensions.Add(".rast", "image/cmu-raster");
            _extensions.Add(".rexx", "text/x-script.rexx");
            _extensions.Add(".rf", "image/vnd.rn-realflash");
            _extensions.Add(".rgb", "image/x-rgb");
            _extensions.Add(".rm", "application/vnd.rn-realmedia");
            _extensions.Add(".rm", "audio/x-pn-realaudio");
            _extensions.Add(".rmi", "audio/mid");
            _extensions.Add(".rmm", "audio/x-pn-realaudio");
            _extensions.Add(".rmp", "audio/x-pn-realaudio");
            _extensions.Add(".rmp", "audio/x-pn-realaudio-plugin");
            _extensions.Add(".rng", "application/ringing-tones");
            _extensions.Add(".rng", "application/vnd.nokia.ringing-tone");
            _extensions.Add(".rnx", "application/vnd.rn-realplayer");
            _extensions.Add(".roff", "application/x-troff");
            _extensions.Add(".rp", "image/vnd.rn-realpix");
            _extensions.Add(".rpm", "audio/x-pn-realaudio-plugin");
            _extensions.Add(".rt", "text/richtext");
            _extensions.Add(".rt", "text/vnd.rn-realtext");
            _extensions.Add(".rtf", "application/rtf");
            _extensions.Add(".rtf", "application/x-rtf");
            _extensions.Add(".rtf", "text/richtext");
            _extensions.Add(".rtx", "application/rtf");
            _extensions.Add(".rtx", "text/richtext");
            _extensions.Add(".rv", "video/vnd.rn-realvideo");
            _extensions.Add(".s", "text/x-asm");
            _extensions.Add(".s3m", "audio/s3m");
            _extensions.Add(".saveme", "application/octet-stream");
            _extensions.Add(".sbk", "application/x-tbook");
            _extensions.Add(".scm", "application/x-lotusscreencam");
            _extensions.Add(".scm", "text/x-script.guile");
            _extensions.Add(".scm", "text/x-script.scheme");
            _extensions.Add(".scm", "video/x-scm");
            _extensions.Add(".sdml", "text/plain");
            _extensions.Add(".sdp", "application/sdp");
            _extensions.Add(".sdp", "application/x-sdp");
            _extensions.Add(".sdr", "application/sounder");
            _extensions.Add(".sea", "application/sea");
            _extensions.Add(".sea", "application/x-sea");
            _extensions.Add(".set", "application/set");
            _extensions.Add(".sgm", "text/sgml");
            _extensions.Add(".sgm", "text/x-sgml");
            _extensions.Add(".sgml", "text/sgml");
            _extensions.Add(".sgml", "text/x-sgml");
            _extensions.Add(".sh", "application/x-bsh");
            _extensions.Add(".sh", "application/x-sh");
            _extensions.Add(".sh", "application/x-shar");
            _extensions.Add(".sh", "text/x-script.sh");
            _extensions.Add(".shar", "application/x-bsh");
            _extensions.Add(".shar", "application/x-shar");
            _extensions.Add(".shtml", "text/html");
            _extensions.Add(".shtml", "text/x-server-parsed-html");
            _extensions.Add(".sid", "audio/x-psid");
            _extensions.Add(".sit", "application/x-sit");
            _extensions.Add(".sit", "application/x-stuffit");
            _extensions.Add(".skd", "application/x-koan");
            _extensions.Add(".skm", "application/x-koan");
            _extensions.Add(".skp", "application/x-koan");
            _extensions.Add(".skt", "application/x-koan");
            _extensions.Add(".sl", "application/x-seelogo");
            _extensions.Add(".smi", "application/smil");
            _extensions.Add(".smil", "application/smil");
            _extensions.Add(".snd", "audio/basic");
            _extensions.Add(".snd", "audio/x-adpcm");
            _extensions.Add(".sol", "application/solids");
            _extensions.Add(".spc", "application/x-pkcs7-certificates");
            _extensions.Add(".spc", "text/x-speech");
            _extensions.Add(".spl", "application/futuresplash");
            _extensions.Add(".spr", "application/x-sprite");
            _extensions.Add(".sprite", "application/x-sprite");
            _extensions.Add(".src", "application/x-wais-source");
            _extensions.Add(".ssi", "text/x-server-parsed-html");
            _extensions.Add(".ssm", "application/streamingmedia");
            _extensions.Add(".sst", "application/vnd.ms-pki.certstore");
            _extensions.Add(".step", "application/step");
            _extensions.Add(".stl", "application/sla");
            _extensions.Add(".stl", "application/vnd.ms-pki.stl");
            _extensions.Add(".stl", "application/x-navistyle");
            _extensions.Add(".stp", "application/step");
            _extensions.Add(".sv4cpio", "application/x-sv4cpio");
            _extensions.Add(".sv4crc", "application/x-sv4crc");
            _extensions.Add(".svf", "image/vnd.dwg");
            _extensions.Add(".svf", "image/x-dwg");
            _extensions.Add(".svr", "application/x-world");
            _extensions.Add(".svr", "x-world/x-svr");
            _extensions.Add(".swf", "application/x-shockwave-flash");
            _extensions.Add(".t", "application/x-troff");
            _extensions.Add(".talk", "text/x-speech");
            _extensions.Add(".tar", "application/x-tar");
            _extensions.Add(".tbk", "application/toolbook");
            _extensions.Add(".tbk", "application/x-tbook");
            _extensions.Add(".tcl", "application/x-tcl");
            _extensions.Add(".tcl", "text/x-script.tcl");
            _extensions.Add(".tcsh", "text/x-script.tcsh");
            _extensions.Add(".tex", "application/x-tex");
            _extensions.Add(".texi", "application/x-texinfo");
            _extensions.Add(".texinfo", "application/x-texinfo");
            _extensions.Add(".text", "application/plain");
            _extensions.Add(".text", "text/plain");
            _extensions.Add(".tgz", "application/gnutar");
            _extensions.Add(".tgz", "application/x-compressed");
            _extensions.Add(".tif", "image/tiff");
            _extensions.Add(".tif", "image/x-tiff");
            _extensions.Add(".tiff", "image/tiff");
            _extensions.Add(".tiff", "image/x-tiff");
            _extensions.Add(".tr", "application/x-troff");
            _extensions.Add(".tsi", "audio/tsp-audio");
            _extensions.Add(".tsp", "application/dsptype");
            _extensions.Add(".tsp", "audio/tsplayer");
            _extensions.Add(".tsv", "text/tab-separated-values");
            _extensions.Add(".turbot", "image/florian");
            _extensions.Add(".txt", "text/plain");
            _extensions.Add(".uil", "text/x-uil");
            _extensions.Add(".uni", "text/uri-list");
            _extensions.Add(".unis", "text/uri-list");
            _extensions.Add(".unv", "application/i-deas");
            _extensions.Add(".uri", "text/uri-list");
            _extensions.Add(".uris", "text/uri-list");
            _extensions.Add(".ustar", "application/x-ustar");
            _extensions.Add(".ustar", "multipart/x-ustar");
            _extensions.Add(".uu", "application/octet-stream");
            _extensions.Add(".uu", "text/x-uuencode");
            _extensions.Add(".uue", "text/x-uuencode");
            _extensions.Add(".vcd", "application/x-cdlink");
            _extensions.Add(".vcs", "text/x-vcalendar");
            _extensions.Add(".vda", "application/vda");
            _extensions.Add(".vdo", "video/vdo");
            _extensions.Add(".vew", "application/groupwise");
            _extensions.Add(".viv", "video/vivo");
            _extensions.Add(".viv", "video/vnd.vivo");
            _extensions.Add(".vivo", "video/vivo");
            _extensions.Add(".vivo", "video/vnd.vivo");
            _extensions.Add(".vmd", "application/vocaltec-media-desc");
            _extensions.Add(".vmf", "application/vocaltec-media-file");
            _extensions.Add(".voc", "audio/voc");
            _extensions.Add(".voc", "audio/x-voc");
            _extensions.Add(".vos", "video/vosaic");
            _extensions.Add(".vox", "audio/voxware");
            _extensions.Add(".vqe", "audio/x-twinvq-plugin");
            _extensions.Add(".vqf", "audio/x-twinvq");
            _extensions.Add(".vql", "audio/x-twinvq-plugin");
            _extensions.Add(".vrml", "application/x-vrml");
            _extensions.Add(".vrml", "model/vrml");
            _extensions.Add(".vrml", "x-world/x-vrml");
            _extensions.Add(".vrt", "x-world/x-vrt");
            _extensions.Add(".vsd", "application/x-visio");
            _extensions.Add(".vst", "application/x-visio");
            _extensions.Add(".vsw", "application/x-visio");
            _extensions.Add(".w60", "application/wordperfect6.0");
            _extensions.Add(".w61", "application/wordperfect6.1");
            _extensions.Add(".w6w", "application/msword");
            _extensions.Add(".wav", "audio/wav");
            _extensions.Add(".wav", "audio/x-wav");
            _extensions.Add(".wb1", "application/x-qpro");
            _extensions.Add(".wbmp", "image/vnd.wap.wbmp");
            _extensions.Add(".web", "application/vnd.xara");
            _extensions.Add(".wiz", "application/msword");
            _extensions.Add(".wk1", "application/x-123");
            _extensions.Add(".wmf", "windows/metafile");
            _extensions.Add(".wml", "text/vnd.wap.wml");
            _extensions.Add(".wmlc", "application/vnd.wap.wmlc");
            _extensions.Add(".wmls", "text/vnd.wap.wmlscript");
            _extensions.Add(".wmlsc", "application/vnd.wap.wmlscriptc");
            _extensions.Add(".word", "application/msword");
            _extensions.Add(".wp", "application/wordperfect");
            _extensions.Add(".wp5", "application/wordperfect");
            _extensions.Add(".wp5", "application/wordperfect6.0");
            _extensions.Add(".wp6", "application/wordperfect");
            _extensions.Add(".wpd", "application/wordperfect");
            _extensions.Add(".wpd", "application/x-wpwin");
            _extensions.Add(".wq1", "application/x-lotus");
            _extensions.Add(".wri", "application/mswrite");
            _extensions.Add(".wri", "application/x-wri");
            _extensions.Add(".wrl", "application/x-world");
            _extensions.Add(".wrl", "model/vrml");
            _extensions.Add(".wrl", "x-world/x-vrml");
            _extensions.Add(".wrz", "model/vrml");
            _extensions.Add(".wrz", "x-world/x-vrml");
            _extensions.Add(".wsc", "text/scriplet");
            _extensions.Add(".wsrc", "application/x-wais-source");
            _extensions.Add(".wtk", "application/x-wintalk");
            _extensions.Add(".xbm", "image/x-xbitmap");
            _extensions.Add(".xbm", "image/x-xbm");
            _extensions.Add(".xbm", "image/xbm");
            _extensions.Add(".xdr", "video/x-amt-demorun");
            _extensions.Add(".xgz", "xgl/drawing");
            _extensions.Add(".xif", "image/vnd.xiff");
            _extensions.Add(".xl", "application/excel");
            _extensions.Add(".xla", "application/excel");
            _extensions.Add(".xla", "application/x-excel");
            _extensions.Add(".xla", "application/x-msexcel");
            _extensions.Add(".xlb", "application/excel");
            _extensions.Add(".xlb", "application/vnd.ms-excel");
            _extensions.Add(".xlb", "application/x-excel");
            _extensions.Add(".xlc", "application/excel");
            _extensions.Add(".xlc", "application/vnd.ms-excel");
            _extensions.Add(".xlc", "application/x-excel");
            _extensions.Add(".xld", "application/excel");
            _extensions.Add(".xld", "application/x-excel");
            _extensions.Add(".xlk", "application/excel");
            _extensions.Add(".xlk", "application/x-excel");
            _extensions.Add(".xll", "application/excel");
            _extensions.Add(".xll", "application/vnd.ms-excel");
            _extensions.Add(".xll", "application/x-excel");
            _extensions.Add(".xlm", "application/excel");
            _extensions.Add(".xlm", "application/vnd.ms-excel");
            _extensions.Add(".xlm", "application/x-excel");
            _extensions.Add(".xls", "application/excel");
            _extensions.Add(".xls", "application/vnd.ms-excel");
            _extensions.Add(".xls", "application/x-excel");
            _extensions.Add(".xls", "application/x-msexcel");
            _extensions.Add(".xlt", "application/excel");
            _extensions.Add(".xlt", "application/x-excel");
            _extensions.Add(".xlv", "application/excel");
            _extensions.Add(".xlv", "application/x-excel");
            _extensions.Add(".xlw", "application/excel");
            _extensions.Add(".xlw", "application/vnd.ms-excel");
            _extensions.Add(".xlw", "application/x-excel");
            _extensions.Add(".xlw", "application/x-msexcel");
            _extensions.Add(".xm", "audio/xm");
            _extensions.Add(".xml", "application/xml");
            _extensions.Add(".xml", "text/xml");
            _extensions.Add(".xmz", "xgl/movie");
            _extensions.Add(".xpix", "application/x-vnd.ls-xpix");
            _extensions.Add(".xpm", "image/x-xpixmap");
            _extensions.Add(".xpm", "image/xpm");
            _extensions.Add(".x", "png	image/png");
            _extensions.Add(".xsr", "video/x-amt-showrun");
            _extensions.Add(".xwd", "image/x-xwd");
            _extensions.Add(".xwd", "image/x-xwindowdump");
            _extensions.Add(".xyz", "chemical/x-pdb");
            _extensions.Add(".z", "application/x-compress");
            _extensions.Add(".z", "application/x-compressed");
            _extensions.Add(".zip", "application/x-compressed");
            _extensions.Add(".zip", "application/x-zip-compressed");
            _extensions.Add(".zip", "application/zip");
            _extensions.Add(".zip", "multipart/x-zip");
            _extensions.Add(".zoo", "application/octet-stream");
            _extensions.Add(".zsh", "text/x-script.zsh");
        }

        /// <summary>
        /// Gets the mime type from a file extension .ext.
        /// </summary>
        /// <param name="extension">The extension (starting with a dot).</param>
        /// <returns>The mime-type of the given extension.</returns>
        public static String FromExtension(String extension)
        {
            var mime = String.Empty;

            if (_extensions.TryGetValue(extension, out mime))
                return mime;

            return Binary;
        }

        #endregion

        #region Known Mime Types

        /// <summary>
        /// Gets the mime-type for HTML text: text/html.
        /// </summary>
        public static readonly String Html = "text/html";

        /// <summary>
        /// Gets the mime-type for the text data, text/plain.
        /// </summary>
        public static readonly String Plain = "text/plain";

        /// <summary>
        /// Gets the mime-type for XML text: text/xml.
        /// </summary>
        public static readonly String Xml = "text/xml";

        /// <summary>
        /// Gets the mime-type for XML applications, application/xml.
        /// </summary>
        public static readonly String ApplicationXml = "application/xml";

        /// <summary>
        /// Gets the mime-type for XHTML / XML: application/xhtml+xml.
        /// </summary>
        public static readonly String ApplicationXHtml = "application/xhtml+xml";

        /// <summary>
        /// Gets the mime-type for binary data, application/octet-stream.
        /// </summary>
        public static readonly String Binary = "application/octet-stream";

        /// <summary>
        /// Gets the mime-type for form data, application/x-www-form-urlencoded.
        /// </summary>
        public static readonly String StandardForm = "application/x-www-form-urlencoded";

        /// <summary>
        /// Gets the mime-type for multipart form data, multipart/form-data.
        /// </summary>
        public static readonly String MultipartForm = "multipart/form-data";

        /// <summary>
        /// Gets a list of mime-types that are recognized as JavaScript.
        /// </summary>
        public static readonly String[] JavaScript = new[] 
        { 
            "application/ecmascript",
            "application/javascript",
            "application/x-ecmascript",
            "application/x-javascript",
            "text/ecmascript",
            "text/javascript",
            "text/javascript1.0",
            "text/javascript1.1",
            "text/javascript1.2",
            "text/javascript1.3",
            "text/javascript1.4",
            "text/javascript1.5",
            "text/jscript",
            "text/livescript",
            "text/x-ecmascript",
            "text/x-javascript"
        };

        #endregion
    }
}

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=big5">
<meta http-equiv="CACHE-CONTROL" content="no-cache">
<title>聯合新聞網：觸動未來 新識力</title>

<meta name="alexaVerifyID" content="YK3rXyqAS-5KMWJHilhKvkm0Wn0"/>
<meta name="application-name" content="聯合新聞網 udn.com" />
<meta name="msapplication-TileColor" content="#ee7000"/>
<meta name="msapplication-TileImage" content="/2010MAIN/img/ea73187c-473f-4489-9b6b-1a03b40abcd0.png"/>
<meta name="msapplication-tooltip" content="聯合新聞網：觸動未來 新識力。" />
<meta name="msapplication-window"  content="width=1024;height=768" />
<meta name="msapplication-task" content="name=udn 影音新聞;action-uri=http://video.udn.com/;icon-uri=http://udn.com/images/udn_favicon.ico" />
<meta name="msapplication-task" content="name=udn 活動訊息;action-uri=http://event.udn.com/index/index.html;icon-uri=http://udn.com/images/udn_favicon.ico" />
<meta name="msapplication-task" content="name=udn Facebook;action-uri=http://www.facebook.com/myudn/;icon-uri=http://udn.com/images/facebook_favicon.ico" />
<meta name="msapplication-task" content="name=udn Google+;action-uri=https://plus.google.com/s/udn?hl=zh-TW#105298219321543859712/;icon-uri=http://www.google.com/favicon.ico" />
<meta property="fb:app_id" content="111507808925636" />
<script  type="text/javascript">
window.onload = function()
{
	try {
		if (window.external.msIsSiteMode()) {
			window.external.msSiteModeClearJumplist();		
		    window.external.msSiteModeCreateJumplist('《聯合新聞網》今日必看');

				window.external.msSiteModeShowJumplist();	
				window.external.msSiteModeClearIconOverlay();
	
		}//end of if 
    }
    catch (ex) {
        // Fail silently.
    }	
}	
	//alert(0);
</script>
<link rel="alternate" type="application/rss+xml" title="最新報導" href="http://udn.com/udnrss/latest.xml" >
<link rel="alternate" type="application/rss+xml" title="發燒新聞" href="http://udn.com/udnrss/mostpopular.xml" >
<link rel="alternate" type="application/rss+xml" title="聯合影音網" href="http://video.udn.com/rss/video_rss.xml" >
<link rel="alternate" type="application/rss+xml" title="兩岸國際" href="http://udn.com/udnrss/BREAKINGNEWS4.xml" >
<link rel="alternate" type="application/rss+xml" title="國內要聞" href="http://udn.com/udnrss/BREAKINGNEWS1.xml" >
<link rel="alternate" type="application/rss+xml" title="udn焦點要聞" href="http://udn.com/udnrss/focus.xml" >
<link rel="alternate" type="application/rss+xml" title="udn政治要聞" href="http://udn.com/udnrss/politics.xml" >
<link rel="shortcut icon" type="image/ico" href="/ie9pin_favicon.ico" />

<!-- created on wm@cmdes.udn.com 2014/11/21 15:46:6  137 383 --> 


<script language="JavaScript" src="/2010MAIN/js/jquery-1.4.2.min.js" type="text/javascript"></script>
<script language="JavaScript" src="/2010MAIN/js/jquery.tools.min.js" type="text/javascript"></script>
<script type="text/javascript" src="/AD/lightbox.js"></script>
<script type="text/JavaScript"> 
<!--
function swfClose(adid) {
  document.getElementById(adid).style.display='none';
}
//-->
</script>
<script type="text/JavaScript"> 
<!--
function GetCkValue( name ) {
 var dc=document.cookie;
 var prefix=name+"=";
 var begin=dc.indexOf("; "+prefix);
 if(begin==-1) begin=dc.indexOf(prefix);
 else begin+=2;
 if(begin==-1) return "";
 var end=document.cookie.indexOf(";",begin);
 if(end==-1) end=dc.length;
 return dc.substring(begin+prefix.length,end);
 }
 sLogin=GetCkValue("udnmember");
 var snLogin = GetCkValue("fg_mail");
//-->
</script>
<script type="text/JavaScript"> 
<!--
function GetRandTabValue( tab , percentage ) {				
	total=0;
	for ( i = 0; i < percentage.length; i++)  
    {
       total = total + percentage[i];
	}			
    index = 1;		
	var randNum = Math.floor(Math.random() * (total - 1 + 1)) + 1;				
	total=0;
	for ( i = 0; i < percentage.length; i++)
    {
       total = total + percentage[i];
       if (randNum <= total) 
	   { 
         index = tab[i]-1;				  
         break;
       }
    } 
     return index;
}
//-->
</script> 
<script language="JavaScript" type="text/javascript"> 
$(document).ready(function(){
						   
	// 取消超連結的虛線框
	$('a').focus(function(){
		this.blur();
	});
        if ( $.browser.msie&&($.browser.version == "6.0")){
               $('.handle').css("display","none");
        }
});
</script>
<script type="text/javascript">

$(document).ready(function(){
/*
			var j = 1;
			 $(".handle").each(function(){
			 if($.browser.msie&&($.browser.version <= "8.0")) 
			 {
					$(this).children("p").html(j);
					j++;
			 }
			 else{
    				var i = $(this).attr("id");
					$(this).children("p").html(i);
			 }
			})
*/
})

 function getVdo() {
	jQuery.ajax({
			url: "/2010MAIN/inc/new_area_video.html",
		    cache: false,
		    //contentType: "application/x-www-form-urlencoded",
		    dataType: 'html',
		        type:'GET',
		    error: function(xhr) {
		    	 alert('系統忙碌中');
		    },
		    success: function(response) {
		    	//jQuery('#video_bg').html(response);
				document.getElementById('video_bg').innerHTML = response;
                jQuery("#window").html("<iframe width='410' height='250' src='http://www.youtube.com/embed/yV535DLrDYg?rel=0&autoplay=1&fs=1&autohide=1&showinfo=0&showsearch=0&modestbranding=1' frameborder='0' allowfullscreen></iframe>");
                // google analytic track 20121228
                _track("http://udn.com/2010MAIN/inc/new_area_video.html");
		    }
		});

 }
  function CloseMedia() {
    var iframe = $("#video_bg > iframe");

    if (typeof iframe.css("width") != "undefined") {
      if ( $.browser.msie && parseInt($.browser.version) == 9 ) {
        iframe.attr("src","");
        iframe.css("height",0);
        iframe.css("width",0);
        iframe.remove();
      }
      else {
        iframe.attr("src","");
        iframe.remove();
        iframe.css("height",0);
        iframe.css("width",0);
      }
    }
  }
function closeSlider() {
	CloseMedia();
  //  $(".video_container").css("width", "27px");
//	$(".handle").siblings(".slide").animate({width:"hide"},300);
//	$(".handle").removeClass("select");
  }

  //解決頁籤在ios的 safari 回上一頁時會無法點頁籤的問題
  function Reload() {
      try {
          var headElement = document.getElementsByTagName("head")[0];
          if (headElement && headElement.innerHTML)
                headElement.innerHTML += "<meta http-equiv=\"refresh\" content=\"1\">";
      }catch (e) {}
  }

  if ((/iphone|ipod|ipad.*os 5/gi).test(navigator.appVersion)) {
      window.onpageshow = function(evt) {
          if (evt.persisted) {
              document.body.style.display = "none";
              location.reload();
              Reload();
          }
      };
  }

</script>
<link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" >
<link href="/2010MAIN/css/main_920.css?1030" rel="stylesheet" type="text/css">
<link href="/2010MAIN/css/maintab.css?118" rel="stylesheet" type="text/css">
<!-- track -->
<!-- google analytics -->
<script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-19660006-1']);
  _gaq.push(['_setDomainName', '.udn.com']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>
<!-- google analytics -->

<!-- Begin comScore Tag -->
<script>
  var _comscore = _comscore || [];
  _comscore.push({ c1: "2", c2: "7390954" });
  (function() {
    var s = document.createElement("script"), el = document.getElementsByTagName("script")[0]; s.async = true;
    s.src = (document.location.protocol == "https:" ? "https://sb" : "http://b") + ".scorecardresearch.com/beacon.js";
    el.parentNode.insertBefore(s, el);
  })();
</script>
<noscript>
  <img src="http://b.scorecardresearch.com/p?c1=2&c2=7390954&cv=2.0&cj=1" />
</noscript>
<!-- End comScore Tag -->

<!-- Start Alexa Certify Javascript -->
<script type="text/javascript">
_atrk_opts = { atrk_acct:"aw0Di1a4ZP00aY", domain:"udn.com",dynamic: true};
(function() { var as = document.createElement('script'); as.type = 'text/javascript'; as.async = true; as.src = "https://d31qbv1cthcecs.cloudfront.net/atrk.js"; var s = document.getElementsByTagName('script')[0];s.parentNode.insertBefore(as, s); })();
</script>
<noscript><img src="https://d5nxst8fruw4z.cloudfront.net/atrk.gif?account=aw0Di1a4ZP00aY" style="display:none" height="1" width="1" alt="" /></noscript>
<!-- End Alexa Certify Javascript -->

<!-- track -->

</head>

<body>
<!--/SSI/common.html" -->
<script type="text/javascript" src="/player/swfobject2.js"></script>
<script>

$(document).ready(function(){
  if ( $.browser.msie&&($.browser.version == "6.0")) {
               $('.handle').css("top","0px");
               $('.handle').css("left","0px");
               $('.handle').css("display","none");
 }else {
       $('.handle').css("display","block");
 }
//20141031新版
 $("#search_kw").val("雪梨");
 $("#search_kw").click(function() {
  $("#search_kw").val("");
 });
}
);

  function CloseMedia() {
    var iframe = $("#window > iframe");

    if (typeof iframe.css("width") != "undefined") {
      if ( $.browser.msie && parseInt($.browser.version) == 9 ) {
        iframe.attr("src","");
        iframe.css("height",0);
        iframe.css("width",0);
        iframe.remove();
      }
      else {
        iframe.attr("src","");
        iframe.remove();
        iframe.css("height",0);
        iframe.css("width",0);
      }
    }
  }
</script>
<style type="text/css">
#searchbar {
     background-image: url("http://s.udn.com.tw/2010/img/sprite.png");
     background-position: right -39px;
     background-repeat: no-repeat;
     height: 25px;
     position: absolute;
     right: 6px;
     top: 32px;
     width: 450px;
}
#search_kw {
    background-color: transparent;
    border-style: none;
    float: left;
    font-family: Arial,helvetica,clean,"....","PMingLiU",sans-serif;
    font-size: 12px;
    height: 20px;
    line-height: 20px;
    padding-left: 5px;
    width: 227px;
}
</style>
<!-- saved from url=(0022)http://internet.e-mail -->
<style>
#sethome {
	position:relative;
	display:inline;
        z-index:9999;
}
#sethome div {
	position:absolute;
	left:0;
	top:20px;
	display:none;
	border: solid 1px #ccc;
	padding: 15px 30px;
	font-size: 15px;
	line-height:1.7;
	white-space: nowrap;
        background-color: #fff;
        z-index:9999;
        color:#333;
        text-align:left;
	box-shadow:3px 3px 1px rgba(0,0,0,0.4);
}
#sethome div b {
	display:block;
	border-bottom: solid 1px #eee;
	padding: 0 0 5px;
	margin: 0 0 5px;
}
#sethome div i {
	display: block;
	position: absolute;
	right: 0;
	top: 0;
	width: 30px;
	height: 30px;
	background-color: #aaa;
	color: #fff;
	text-align: center;
	line-height: 30px;
	font-style: normal;
	font-family:Gotham, "Helvetica Neue", Helvetica, Arial, sans-serif;
	cursor:pointer;
}
#sethome div i:hover {
	background-color: #666;
}
#sethome div u {
	background:url(http://s.udn.com.tw/2010/img/sprite.png) no-repeat right top;
	display:inline-block;
	position:relative;
	width:32px;
	height:32px;
	top:10px;
	left:5px;
}
#sethome div u.firefox {
	background-position: -260px top;
	width:54px;
	height:20px;
	top:5px;
}
</style>
<script>
(function($)
{
	$.extend(
	{
		NV:function(name)
		{
			var NV = {};
			var UA = navigator.userAgent.toLowerCase();
			try
			{
				NV.name=!-[1,]?'ie':
				(UA.indexOf("firefox")>0)?'firefox':
				(UA.indexOf("chrome")>0)?'chrome':
				window.opera?'opera':
				window.openDatabase?'safari':
				'unkonw';
			}catch(e){};
			try
			{
				NV.version=(NV.name=='ie')?UA.match(/msie ([\d.]+)/)[1]:
				(NV.name=='firefox')?UA.match(/firefox\/([\d.]+)/)[1]:
				(NV.name=='chrome')?UA.match(/chrome\/([\d.]+)/)[1]:
				(NV.name=='opera')?UA.match(/opera.([\d.]+)/)[1]:
				(NV.name=='safari')?UA.match(/version\/([\d.]+)/)[1]:
				'0';
			}catch(e){};

			switch(name)
			{
				case 'ua':
				case 'UA':br=UA;break;
				case 'name':br=NV.name;break;
				case 'version':br=NV.version;break;
				case 'shell':br=NV.shell;break;
				default:br=NV.name;
			}
			return br;
		}
	});
})(jQuery);

$(function()
{
	if( $.NV('name') == 'chrome' )
	{
		$("#sethome").html("<a href=\"#\">設為首頁</a><div><b>請參考步驟設定 聯合新聞網 為首頁：</b>1. 請點選右上方的<u class=\"chrome\"></u><br>2. 選擇「設定」<br>3. 在「外觀」項目中勾選「顯示[首頁]按鈕」，接著按下「變更」<br>4. 在「開啟此頁︰」中輸入 http://udn.com/<br>5. 按下「確定」完成設定<i>x</i></div>");
	}
	
	if( $.NV('name') == 'firefox' )
	{
		$("#sethome").html("<a href=\"#\">設為首頁</a><div><b>請參考步驟設定 聯合新聞網 為首頁：</b>1. 請點選上方的<u class=\"firefox\"></u><br>2. 選擇「選項」<br>3. 在「首頁」項目中輸入 http://udn.com/<br>4. 按下「確定」完成設定<i>x</i></div>");
	}

	if( $.NV('name') == 'chrome' || $.NV('name') == 'firefox' )
	{
		$("#sethome a , #sethome i").click(function(){
			$("#sethome div").toggle();
		});
	}
});
</script>

<!--/SSI/common.html" -->

<!--
<div id="outer">
<div id="swf_Container">
</div>
</div>
-->
<a name="top"></a>

<div id="header" style="overflow:visible">

    <a id="logo" href="http://udn.com" target="_top"></a>
    <!--首頁145*50 logo STAR-->
	<div id="LOGO14550"  style="width:160px ; height:50px ;z-index:999" >
         <iframe src="/2010MAIN/AD/UDNNEWS-LOGO145x50.html" width="160" height="50" scrolling="no" frameborder="0" allowTransparency="true"></iframe>
	</div>
    <!--首頁145*50 logo END-->
    <div id="family">
    <a href="http://vision.udn.com/" target="_blank">願景</a>│<a href="http://video.udn.com/" target="_blank">影音</a>│<a href="http://mobile.udn.com/web/index.html" target="_blank">行動</a>│<a href="http://money.udn.com/" target="_blank">財經</a>│<a href="http://stars.udn.com/" target="_blank">追星</a>│<a href="http://autos.udn.com" target="_blank">汽車</a>│<a href="http://style.udn.com/" target="_blank">時尚</a>│<a href="http://opinion.udn.com/" target="_blank"><font color=#FF0000>鳴人堂</font></a>│<a href="http://paper.udn.com/" target="_blank">電子報</a>│<a href="http://reading.udn.com/" target="_blank">數位閱讀</a>│<a href="http://udndata.com/" target="_blank">資料庫</a>│<a href="http://blog.udn.com/" target="_blank">Blog</a>│<a href="https://tickets.udn.com" target="_blank">售票</a>│<a href="http://learning.udn.com" target="_blank">進修</a>‧<a href="http://udnjob.com/" target="_blank">職場</a>│<a href="http://shopping.udn.com/mall/Cc1a00.do?sid=91&utm_source=udn.com&utm_medium=referral_familybar&utm_content=familybar&utm_campaign=201408_udn.com(sid91.94)" target="_blank"><font color=#FF0000>udn 買東西</font></a>
 
    </div>
    <!-- /family -->
<script language="JavaScript" type="text/javascript">
<!--
function chkform() {
   document.sform1.Keywords.value=document.sform1.q.value;  
  if(document.sform1.Keywords.value == "") return;
  if(document.getElementById("search_type").value == "udndata" )
	document.sform1.action = "http://data.udn.com/data/Search";
  else
	document.sform1.action = "http://udn.com/search/";
  document.sform1.submit();
}
//-->
</script>    
    <div id="searchbar"><form action="" method="get" name="sform1" id="sform1"><select name="search_type" id="search_type">
      <option value="udn">udn</option>
	  <!-- <option value="websearch">web</option> -->
      <option value="udndata">歷史新聞</option>
    </select><input type="text" onkeydown="if(event.keyCode==13) chkform()" onMouseOver="this.focus()" id="search_kw" name="q"><input id="Keywords" type="hidden" name="Keywords"><a href="javascript:chkform()" id="search_submit"></a></form></div>
    <!-- /searchbar -->
<script type="text/JavaScript"> 

						<!--

						function gbt() {

						  var tmp=document.URL;

						  var tmp2=tmp.split("//");

						  var url=tmp2[1];

						  if (tmp.substr(7,2) != "gb")

							window.open('http://gb.udn.com/gb/'+url)

						  else {}

						}
						
function logout() {
	var expires = new Date();
	expires.setTime (expires.getTime() - 1);
	document.cookie = "fg_mail=; path=/; domain=udn.com; expires=" + expires.toGMTString();
	top.location = 'https://member.udn.com/member/logout_social.jsp';
}

						//-->

</script>        
    <div id="infobar" style="overflow:visible"><script language="JavaScript" charset="utf-8" src="/2010MAIN/js/datetime_u8.js"></script>│<div id="sethome"><a onClick="this.style.behavior='url(#default#homepage)';this.setHomePage('http://udn.com');return false;" href="#">設為首頁</a></div>│<fjtignoreurl><a href="http://udn.com"><img border="0" align="bottom" alt="繁體" src="http://s.udn.com.tw/2010MAIN/img/big5.gif"></a></fjtignoreurl><img border="0" align="bottom" src="http://s.udn.com.tw/2010MAIN/img/arr.gif"><a onClick="javascript:gbt()" href="javascript:;"><img border="0" align="bottom" alt="簡體" src="http://s.udn.com.tw/2010MAIN/img/bg2312.gif"></a>│<a target="_blank" href="http://udn.com/rss/index.html?new"><img src="http://s.udn.com.tw/2010MAIN/img/rss.gif" border="0" align="bottom">RSS</a>│<a target="_blank" href="https://www.facebook.com/myudn"><img src="http://s.udn.com.tw/2010MAIN/img/facebook_14.png" width="14" height="14" border="0" align="bottom"> 粉絲團</a>
</div>
    <!-- /infobar -->
<div id="wicon" style="width:300px;left: 578px; top: 72px; width: 300px; display: block; position: absolute;"><iframe id="wiconif" style="padding:0px;display:none" witdth="200" height="25" scrolling="no" frameborder="0" allowTransparency="true"  src="/w/s.php?s" ></iframe></div>


<SCRIPT language=JavaScript>  
<!-- 
if(sLogin==""){
	//alert(snLogin);
	if (snLogin =='')
		document.write("<div id=\"memberbar\"><a href=\"http://member.udn.com/member/login.jsp?site=news&redirect=http://udn.com\">會員登入</a>│<a href=\"http://member.udn.com/member/ShowMember?site=news&redirect=http://udn.com\">註冊</a></div>");
	else
		document.write("<div id=\"memberbar\"><a href=\"javascript:logout()\">登出</a>│<a href=\"http://member.udn.com/member/login.jsp?site=news&redirect=http://udn.com\">udn 會員登入</a></div>");
}else{ 
    document.write("<div id=\"memberbar\"><a href=\"http://member.udn.com/member/ProcessLogout?redirect=http://udn.com\">會員登出</a>│<a href=\"http://member.udn.com/member/center.jsp\">會員中心</a></div>");
}
// -->
 </SCRIPT>
    <!-- /memberbar -->
    
</div>
<!-- /header -->
<!--快訊-->
<style type="text/css">
#breaknews{
margin: 0px auto;
width: 980px;
margin-bottom:10px;
}
div#abgne_marquee {
border: 1px solid #d4d4d4;
height: 38px;
overflow: hidden;
position: relative;
width: 976px;
margin-top: 10px;
background-color: #fff;
}
div#abgne_marquee h3 {
margin: 0;
height: 38px;
overflow: hidden;
width: 100px;
background: #fff url(http://udn.com/2010/images/alert.gif) no-repeat 2px 2px;
float: left;
font-size: 12px;
line-height: 38px;
color: #FFF;
text-align: center;
font-weight: normal;
}
div#abgne_marquee ul, div#abgne_marquee li {
float: left;
list-style: none;
margin: 0;
padding: 0;
}
div#abgne_marquee ul {
position: absolute;
left: 110px;
}
div#abgne_marquee ul li a {
display: block;
overflow: hidden;
width: 400px;
height: 38px;
text-decoration: none;
color: #444;
text-align: left;
font-size: 16px;
line-height: 38px;
font-weight: bold;
}


div#abgne_marquee ul li  {
display: block;
overflow: hidden;
width: 400px;
height: 38px;
text-decoration: none;
color: #444;
text-align: left;
font-size: 16px;
line-height: 38px;
font-weight: bold;
}

div#abgne_marquee ul li a b {
color: #999;
font: 12px Arial, Helvetica, sans-serif;
}
</style>
<script type="text/javascript">
    $(function(){
        // 先取得 div#abgne_marquee ul
        // 接著把 ul 中的 li 項目再重覆加入 ul 中(等於有兩組內容)
        // 再來取得 div#abgne_marquee 的高來決定每次跑馬燈移動的距離
        // 設定跑馬燈移動的速度及輪播的速度
        var $marqueeUl = $('div#abgne_marquee ul'),
            $marqueeli = $marqueeUl.append($marqueeUl.html()).children(),
            _height = $('div#abgne_marquee').height() * -1,
            scrollSpeed = 600,
            timer,
            speed = 6000 + scrollSpeed;

        // 幫左邊 $marqueeli 加上 hover 事件
        // 當滑鼠移入時停止計時器；反之則啟動
        $marqueeli.hover(function(){
            clearTimeout(timer);
        }, function(){
            if ( $marqueeli.length/2 > 2 ){
                timer = setTimeout(showad, speed);
            }
        });

        // 控制跑馬燈移動的處理函式
        function showad(){
            var _now = $marqueeUl.position().top / _height;
            _now = (_now + 1) % $marqueeli.length;

            // $marqueeUl 移動
            $marqueeUl.animate({
                top: _now * _height
            }, scrollSpeed, function(){
                // 如果已經移動到第二組時...則馬上把 top 設 0 來回到第一組
                // 藉此產生不間斷的輪播
                //因為這是首頁使用的, 一次要算二則和內容頁不同, 以下算法要調整
                // 原 /4 改為 /2
                if(_now == $marqueeli.length / 4){
                    $marqueeUl.css('top', 0);
                }
            });

            // 再啟動計時器
            if ( $marqueeli.length/2 < 3 ){
                clearTimeout(timer);
            }else{
                timer = setTimeout(showad, speed);
            }
        }

        // 啟動計時器
            if ( $marqueeli.length/2 < 3 ){
                clearTimeout(timer);
            }else{
                timer = setTimeout(showad, speed);
            }

        $('a').focus(function(){
            this.blur();
        });
    });
</script>
<div id="breaknews">
<script type="text/javascript">
    document.write('<script src="http://udn.com/NEWS/js/news_notify.js?' + Math.random() + '"></scr' + 'ipt>');
</script>
</div>
<div id="container">
    <div id="focus_box"  class="box_outline">
        <!-- container for the slides -->
        <style type="text/css">

#close {
display: block;
overflow: hidden;
margin-top: 5px;
width: 185px;
text-align: right;
}

#window {
height: 245px;
left: 13px;
position: absolute;
top: 13px;
width: 402px;
text-align: left;
}

#close {
    display: block;
    overflow: hidden;
    margin-top: 5px;
    width: 185px;
    text-align: right;
    margin-bottom: 5px;
}
</style>

<div id="main_tabs">

    <div class="tabs_body">

        <iframe src="/2010MAIN/tab/main_tab00.shtml" width="634" height="400" scrolling="no" frameborder="0" allowTransparency="true"></iframe>

        <!-- start 右方廣告區塊 -->
        <div id="ad_300x60"><iframe id="ad_300x60_iframe" src="http://udn.com/2010MAIN/AD/banner_300X45.html" width="300" height="60" scrolling="no" frameborder="0"></iframe></div>
        <!-- /#ad_300x60 -->
        <div id="ad_300x250"><iframe id="ad_300x250_iframe" src="http://udn.com/2010MAIN/AD/banner_300X250.html" width="300" height="250" scrolling="no" frameborder="0"></iframe></div>
        <!-- /#ad_300x250 -->
        <!-- end 右方廣告區塊 -->

    </div>
    <!-- /.tabs_body -->

</div>
<!-- /#main_tabs -->
<script>
//不顯示直播中的小圖
//不動 2012/10/23
//if ($('#handler').css('background-image').indexOf("play_on") >0){
  //      //換圖
  //      $('#handler').css('background-image', 'url(/2010MAIN/img/play_on2.png)');
  //      $('#handler').hover(function() {$(this).css('background-image','url(/2010MAIN/img/play_on_over2.png)');}, function() {$(this).css('background-image','url(/2010MAIN/img/play_on2.png)');});
//}

</script>



        <!-- /slidetabs_1 -->
    </div>
    <!-- /focus_box -->

    <div id="area_1_box" class="box_outline">
    <div class="box_line">
    
        <!-- container for the slides -->
        <div class="focus"><div style="display:block"></div></div>
        
        <div id="area_sort_1" class="sort">
        <dl>
        	<dd style="width: 100px"><a href="/2010MAIN/inc/area_focus.shtml">焦點要聞<b></b></a></dd>
	<dd style="width: 100px"><a href="/2010MAIN/inc/area_media.html">影音多媒體<b></b></a></dd>
	<dd style="width: 100px"><a href="/2010MAIN/inc/area_stock.shtml">股市理財<b></b></a></dd>
	<!--<dd style="width: 100px"><a href="/2010MAIN/inc/area_house.shtml">房市情報<b></b></a></dd>-->
	<dd style="width: 100px"><a href="/2010MAIN/inc/area_fidelity.shtml">基金智富<b></b></a></dd>
	<dd style="width: 100px"><a href="/2010MAIN/inc/area_car.html">發燒車訊<b></b></a></dd>
	<dd style="width: 100px"><a href="/2010MAIN/inc/area_dp.shtml">電子書城<b></b></a></dd>
	<dd style="width: 100px"><a href="/2010MAIN/inc/area_novel.shtml">讀小說<b></b></a></dd>

        </dl>
        </div>
        <!-- /area_sort_1 -->
        
        <a class="backward backward_2">prev</a>
        <a class="forward forward_2">next</a><p>
        <script>
          Tab=[1, 2, 3, 4, 5, 6, 7];

Percentage=[40, 5, 10, 10, 15, 10, 10];


        idx = GetRandTabValue( Tab , Percentage );	
        // sub tab 1
        $("#area_sort_1").tabs("div.focus > div", {
        				clickable:false, 
        				initialIndex: idx,        	
                effect: 'ajax',
                rotate: true
        }).slideshow();
        </script>

    </div>
    </div>
    <!-- /area_1_box -->

    <div id="area_2_box" class="box_outline">
    <div class="box_line">
    
        <!-- container for the slides -->
        <div class="focus"><div style="display:block"></div></div>
        
        <div id="area_sort_2" class="sort">
        <dl>
                    <dd><a href="/2010MAIN/inc/area_community.shtml">社群名人<b></b></a></dd>
            <dd><a href="/2010MAIN/inc/area_mag.html">雜誌期刊<b></b></a></dd>
            <dd><a href="/2010MAIN/inc/area_dignews.shtml">哇新聞<b></b></a></dd>
            <dd><a href="/2010MAIN/inc/area_job.shtml">職場 plus<b></b></a></dd>
            <dd><a href="/2010MAIN/inc/area_discount.html">好康快訊<b></b></a></dd>
            <dd><a href="/2010MAIN/inc/area_housefun.shtml">買好房<b></b></a></dd>
            <dd><a href="/2010MAIN/inc/area_travel.shtml">旅遊即時報<b></b></a></dd>
            <dd><a href="/2010MAIN/inc/area_uDesigner.shtml">有 . 設計<b></b></a></dd>

        </dl>
        </div>
        <!-- /area_sort_1 -->
        
        <a class="backward backward_2">prev</a>
        <a class="forward forward_2">next</a><p>
        <script>
          Tab=[1, 2, 3, 4, 5, 6, 7, 8];

Percentage=[20, 30, 10, 10, 10, 10, 10, 10];


        idx = GetRandTabValue( Tab , Percentage );
        // sub tab 2
        $("#area_sort_2").tabs("div.focus > div", {
        				clickable:false, 
        				initialIndex: idx,         	
                effect: 'ajax',
                rotate: true
        }).slideshow();
        </script>

    </div>
    </div>
    <!-- /area_2_box -->
    
    <div id="area_3_box" class="box_outline">
    <div class="box_line">
    
        <!-- container for the slides -->
        <div class="focus"><div style="display:block"></div></div>
        
        <div id="area_sort_3" class="sort">
        <dl>
            <dd><a href="/2010MAIN/inc/area_archive.shtml">歷史新聞<b></b></a></dd>
        </dl>
        </div>
        <script>
        // sub tab 3
        $("#area_sort_3").tabs("div.focus > div", {
        				clickable:false,         	
                effect: 'ajax',
                rotate: true
        }).slideshow();
        </script>
        <!-- /area_sort_1 -->

    </div>
    </div>
    <!-- /area_3_box -->
<SCRIPT language=JavaScript SRC="/NEWS/js/premenu.js?2"></SCRIPT>
<script type="text/JavaScript">
<!--
if (window.navigator.userAgent.indexOf("MSIE")>=1) {
	//如果瀏覽器是IE
	setActiveStyleSheet("sidemenu_IE.css?n");
} else {
	//如果瀏覽器是其他
	setActiveStyleSheet("sidemenu_FF.css?n");
}

function setActiveStyleSheet(title){ 
  document.write('<link href="/2010MAIN/css/'+title+'" rel="stylesheet" type="text/css">');  
}
//-->
</script>
    <div id="sidemenu_box" class="box_outline">
    <div class="box_line">
    <ul id="sidemenu">
		
    <!--start  /2010MAIN/inc/sidemenudiv.html -->
        <li class="group_4" onMouseOver="popUp(0,event);" onMouseOut="popDown(0,event);"><a href="http://udn.com/NEWS/BREAKINGNEWS/" >即時新聞</a></li>
        <li onMouseOver="popUp(1,event);" onMouseOut="popDown(1,event);"><a href="http://video.udn.com">影音多媒體</a></li>
        <li onMouseOver="popUp(2,event);" onMouseOut="popDown(2,event);"><a href="http://udn.com//NEWS/NATIONAL">國內要聞</a></li>
        <li onMouseOver="popUp(3,event);" onMouseOut="popDown(3,event);"><a href="http://udn.com//NEWS/SOCIETY">社會新聞</a></li>
        <li onMouseOver="popUp(4,event);" onMouseOut="popDown(4,event);"><a href="http://udn.com//NEWS/DOMESTIC">地方新聞</a></li>
        <li onMouseOver="popUp(5,event);" onMouseOut="popDown(5,event);"><a href="http://udn.com//NEWS/MAINLAND">兩岸台商</a></li>
        <li onMouseOver="popUp(6,event);" onMouseOut="popDown(6,event);"><a href="http://udn.com//NEWS/WORLD">全球觀察</a></li>
        <li onMouseOver="popUp(7,event);" onMouseOut="popDown(7,event);"><a href="http://udn.com//NEWS/OPINION">意見評論</a></li>
        <li onMouseOver="popUp(8,event);" onMouseOut="popDown(8,event);"><a href="http://udn.com//NEWS/FINANCE">財經產業</a></li>
        <li onMouseOver="popUp(9,event);" onMouseOut="popDown(9,event);"><a href="http://udn.com/NEWS/STOCK">股市投資</a></li>
        <li onMouseOver="popUp(10,event);" onMouseOut="popDown(10,event);"><a href="http://fund.udn.com">基金理財</a></li>
        <li onMouseOver="popUp(11,event);" onMouseOut="popDown(11,event);" class="group_2"><a href="http://udn.com//NEWS/SPORTS">運動大聯盟</a></li>
        <li onMouseOver="popUp(12,event);" onMouseOut="popDown(12,event);" class="group_2"><a href="http://mag.udn.com/mag/digital/index.jsp">數位資訊</a></li>
        <li onMouseOver="popUp(14,event);" onMouseOut="popDown(14,event);" class="group_2"><a href="http://autos.udn.com">發燒車訊</a></li>
		<li onMouseOver="popUp(13,event);" onMouseOut="popDown(13,event);" class="group_2"><a href="http://stars.udn.com">娛樂追星</a></li>
		<li onMouseOver="popUp(26,event);" onMouseOut="popDown(26,event);" class="group_2"><a href="http://style.udn.com/">時尚名人</a></li>        
        <li onMouseOver="popUp(15,event);" onMouseOut="popDown(15,event);" class="group_2"><a href="/NEWS/LIFE">生活消費</a></li>
        <li onMouseOver="popUp(17,event);" onMouseOut="popDown(17,event);" class="group_2"><a href="http://udn.com/news/cindex/1013">旅遊美食</a></li>
        <li onMouseOver="popUp(16,event);" onMouseOut="popDown(16,event);" class="group_2"><a href="http://mag.udn.com/mag/life/index.jsp">健康醫藥</a></li>
        <li onMouseOver="popUp(18,event);" onMouseOut="popDown(18,event);" class="group_2"><a href="http://udn.com/news/cindex/1012">文教職考</a></li>
        <li onMouseOver="popUp(19,event);" onMouseOut="popDown(19,event);" class="group_2"><a href="http://udn.com/news/cindex/1014">閱讀藝文</a></li>
        <li onMouseOver="popUp(20,event);" onMouseOut="popDown(20,event);" class="group_2"><a href="http://udn.com/news/cindex/1015">聯合書報攤</a></li>
        <li onMouseOver="popUp(21,event);" onMouseOut="popDown(21,event);" class="group_3"><a href="http://shopping.udn.com/mall/Cc1a00.do?sid=94&utm_source=udn.com&utm_medium=referral_L_list&utm_content=L_list&utm_campaign=201408_udn.com(sid91.94)" target=blank><font color=#FF0000>udn 買東西</font></a></li>
       	<li onMouseOver="popUp(22,event);" onMouseOut="popDown(22,event);" class="group_3"><a href="http://reading.udn.com">數位閱讀</a></li>
        <li onMouseOver="popUp(23,event);" onMouseOut="popDown(23,event);" class="group_3"><a href="http://learning.udn.com">進修線上</a></li>
        <li onMouseOver="popUp(24,event);" onMouseOut="popDown(24,event);" class="group_3"><a href="http://udnjob.com">職場行家</a></li>
        <li onMouseOver="popUp(25,event);" onMouseOut="popDown(25,event);" class="group_3"><a href="http://gmg.udn.com">活動線上</a></li>
    </ul>
                <SCRIPT>
                // main menu
                $("#sidemenu li").hover(
                  function() { $(this).addClass("iehover"); },
                  function() { $(this).removeClass("iehover"); }
                );
                </SCRIPT>

		<SCRIPT language=JavaScript>
		
		<!--
		if(isMenu){
		document.write("<SCRIPT SRC='/NEWS/2010hierSetEnvMain.js'><\/SCRIPT>");
		
		document.write("<SCRIPT SRC='/NEWS/hierArrays2005.js'><\/SCRIPT>");
		
		document.write("<SCRIPT LANGUAGE='JavaScript' SRC='/NEWS/hierIE_2010Main.js?1106'><\/SCRIPT>");
		
		}
		
		//-->
		
		</SCRIPT>
<!--end  /2010MAIN/inc/sidemenudiv.html -->
 
    <div id="sidemenu_ad">

	<!-- 120*180 BD badge start -->

<iframe src="/2010MAIN/BD/banner_120X180.html?v=4" width="130" height="190" scrolling="no" frameborder="0"></iframe>

	<!-- 120*180 BD badge start -->


	<!-- 120*350 AD start -->
<iframe src="/2010MAIN/AD/banner_120X350.html" width="120" height="350" scrolling="no" frameborder="0"></iframe>
	<!-- 120*350 AD end -->		
    </div>
    <!-- /sidemenu_ad -->
<!-- 因為多了時尚所以版位擠壓先將此 sidemenu_ad2 下線-->

	<div id="sidemenu_ad2" style="top:1200px;height:330px"> <!-- -->

	<!-- 120*350 AD start -->
<!-- leftdown corner
<iframe src="/2010MAIN/AD/banner_120X350_1.html" width="120" height="360" scrolling="no" frameborder="0"></iframe>

-->


	<!-- 120*350 AD end -->		
   </div> 
    <!-- /sidemenu_ad2 -->
 
	
    </div>
  </div>
    <!-- /sidemenu_box -->

    <div id="ads" class="box_outline">
    <div class="box_line">
        <!-- 2012/1/2 online    default is small ad(480X60)-->
<div id="ads_1"><iframe id="frm1" src="/2010MAIN/AD/banner_480X60.html?111" width="480" height="60" scrolling="no" frameborder="0"></iframe></div>
<div id="ads_3"><iframe id="frm3" src="/2010MAIN/BD/banner_300X60.html" width="300" height="60" scrolling="no" frameborder="0"></iframe></div>
 
    </div>
    </div>
    <!-- /ads -->
    
   <div id="banner_1"><iframe src="/2010MAIN/AD/banner_820X100_1.html" width="820" height="100" scrolling="no" frameborder="0"></iframe></div>
   <!-- /banner_1 -->

   <div id="banner_2"><iframe src="/2010MAIN/AD/banner_820X100_2.html" width="820" height="110" scrolling="no" frameborder="0"></iframe></div>
   <!-- /banner_2 -->

</div>
<!-- /container -->


<a id="gotop" href="#top"></a>

<div id="sitemap">
  <div id="sitemap_box">



        <div class="sitemap_head">網群與速覽</div>

        

        <div class="sitemap_body">

        

            <dl>

            
      <dd class="channel"><a href="http://video.udn.com/" target="_blank">影音</a></dd>

            <dd><a href="http://video.udn.com/general" target="_blank">時事</a></dd>
            <dd><a href="http://video.udn.com/global" target="_blank">國際</a></dd>
            <dd><a href="http://video.udn.com/life" target="_blank">生活</a></dd>
            <dd><a href="http://video.udn.com/finance" target="_blank">財經</a></dd>
            <dd><a href="http://video.udn.com/stars" target="_blank">娛樂</a></dd>
            <dd><a href="http://video.udn.com/sports" target="_blank">體育</a></dd>
            <dd><a href="http://ireporter.udn.com/ireporter/Home/HomePage.do" target="_blank">我是記者</a></dd>

            </dl>

           

  
        

            <dl>

            <dd class="channel"><a href="http://dignews.udn.com/forum/" target="_blank">哇新聞</a></dd>

            <dd><a href="http://dignews.udn.com/forum/index.jsp?type=hot" target="_blank">討論</a></dd>

            <dd><a href="http://dignews.udn.com/forum/udnnews/news.jsp" target="_blank">最新</a></dd>

            <dd><a href="http://dignews.udn.com/forum/udnnews/news_hot.jsp" target="_blank">發燒</a></dd>

            <dd><a href="http://dignews.udn.com/forum/face.jsp" target="_blank">臉譜</a></dd>

            <dd><a href="http://dignews.udn.com/forum/udnnews/news_photo.jsp" target="_blank">圖片</a></dd>

            </dl>

        

            <dl>

            <dd class="channel"><a href="http://stars.udn.com/" target="_blank">追星娛樂</a></dd>

            <dd><a href="http://stars.udn.com/newstars/collect/CollectIndex.do?c=6" target="_blank">話題</a></dd>

            <dd><a href="http://stars.udn.com/newstars/media/MediaIndex.do?m=&t=n" target="_blank">影音</a></dd>

            <dd><a href="http://stars.udn.com/newstars/slide/SlideIndex.do" target="_blank">圖庫</a></dd>

            <dd><a href="http://stars.udn.com/newstars/collect/CollectIndex.do?c=2" target="_blank">電影</a></dd>

            <dd><a href="http://stars.udn.com/newstars/collect/CollectIndex.do?c=3" target="_blank">音樂</a></dd>
			
            <dd><a href="http://stars.udn.com/newstars/movie/MovieList.do" target="_blank">查電影</a></dd>

            </dl>

         

            <dl>

            <dd class="channel"><a href="http://money.udn.com/"  target="_blank">財經</a></dd>

            <dd><a href="http://udn.megatime.com.tw/asp/realquote/weight_mcs.asp"  target="_blank">股市看盤</a></dd>

            <dd><a href="http://fund.udn.com/"  target="_blank">基金</a></dd>
	
			<dd><a href="http://udn.com/lotto/"  target="_blank" >樂透</a></dd>
			
			<dd><a href="http://udn.com/lotto/selec06.shtml"  target="_blank">統一發票</a></dd>

            </dl>

           

            <dl>

            <dd class="channel"><a href="http://udn.com/ugc/"  target="_blank">社群互動</a></dd>
            <dd><a href="http://event.udn.com/line/" target="_blank">udn Line好友</a></dd>

            <dd><a href="http://blog.udn.com/"  target="_blank">部落格</a></dd>

            <dd><a href="http://album.udn.com/"  target="_blank">相簿</a></dd>
			
           <dd><a href="http://city.udn.com/"  target="_blank">城市</a></dd>

            <dd><a href="http://mag.udn.com/mag/news/mag_votelist.jsp" target="_blank">投票</a></dd>
           
		    <dd><a href="http://ipost.udn.com" target="_blank">便利貼</a></dd>
            </dl>

        

            <dl>

            <dd class="channel"><a href="http://reading.udn.com/reading/index.do" target="_blank">數位閱讀</a></dd>

            <dd><a href="http://reading.udn.com/reading/index_enews.do" target="_blank">電子報紙</a></dd>

            <dd><a href="http://reading.udn.com/reading/index_ebook.do" target="_blank">電子書</a></dd>

            <dd><a href="http://reading.udn.com/reading/index_emag.do" target="_blank">電子雜誌</a></dd>            

            </dl>

         

            <dl>

            <dd class="channel"><a href="http://udndata.com/" target="_blank">資料庫</a></dd>

            <dd><a href="http://udndata.com/udn/" target="_blank">新聞全文</a></dd>

            <dd><a href="http://udndata.com/fullpage/" target="_blank">原版報紙</a></dd>

            <dd><a href="http://udndata.com/image/" target="_blank">影像圖庫</a></dd>

            <dd><a href="http://udndata.com/video/" target="_blank">影音知識庫</a></dd>

            <dd><a href="http://udndata.com/store/index.html">數位典藏</a></dd>

            </dl>

        

            <dl>

            <dd class="channel"><a href="http://shopping.udn.com/" target="_blank">加值服務</a></dd>

            <dd><a href="http://shopping.udn.com/" target="_blank">udn shopping</a></dd>

            <dd><a href="http://mobile.udn.com/web/index.html" target="_blank">行動網</a></dd>
			
      
			
		    <dd><a href="http://event.udn.com/index/index.html" target="_blank">活動特區</a></dd>
            </dl>
			
			          <dl>

            <dd class="channel"><a href="http://udnjob.com/" target="_blank">職場進修</a></dd>

            <dd><a href="http://pro.udnjob.com/mag2/pro/index.jsp" target="_blank">職場行家</a></dd>

            <dd><a href="http://learning.udn.com/learn/" target="_blank">進修線上</a></dd>

            <dd><a href="http://pro.udnjob.com/mag2/enter/index.jsp" target="_blank">創業加盟</a></dd>

            <dd><a href="http://pro.udnjob.com/mag2/hr/index.jsp" target="_blank">人資面面觀</a></dd>

      

            </dl>
			
			          <dl>

            <dd class="channel"><a href="http://mag.udn.com/index.htm" target="_blank">綜合</a></dd>

            <dd><a href="http://style.udn.com/" target="_blank">時尚</a></dd>
			
            <dd><a href="http://theme.udn.com/" target="_blank">時事</a></dd>

        

            <dd><a href="http://paper.udn.com/UDN/Subscribe/subscribe" target="_blank">電子報</a></dd>

            <dd><a href="http://udn.com/rss/index.html" target="_blank">RSS</a></dd>

            <dd><a href="http://autos.udn.com/" target="_blank">發燒車訊</a></dd>

            </dl>


        
</div>

        <!-- /sitemap_body -->

    

    </div>

	<!-- /sitemap_box -->

        <!-- /sitemap_body -->

    

    

	<!-- /sitemap_box -->
 
</div>
<!-- /sitemap -->


<div id="footer">
    <div id="footer_box">

    

        
  <div id="footer_links"><a href="http://udn.com/AD/ad_main.shtml" target="_blank"><font color="#FF0000">刊登網站廣告</font></a>︱<a href="http://udn.com/NEWS/SITEMAP/index.htm" target="_blank">網站總覽</a>︱<a href="http://udn.com/UDN/faq.htm" target="_blank">FAQ</a>‧<a href="http://www.udngroup.com/2c/cservice/index2.jsp?qfile=dotcom" target="_blank">客服</a>︱<a href="http://udndata.com/udnauthority.html" target="_blank">新聞授權</a>︱<a href="https://member.udn.com/member/privacy.htm" target="_blank">服務條款</a>‧<a href="http://udn.com/UDN/copyright.htm" target="_blank">著作權</a>‧<a href="http://www.udngroup.com/2c/private.jsp" target="_blank">隱私權聲明</a>︱<a href="http://www.udngroup.com/2c/cservice/index.jsp" target="_blank">聯合報系</a>︱<a href="http://www.udngroup.com/2c/promote/orderall.jsp" target="_blank">訂報紙</a>︱<a href="http://co.udn.com/CORP/" target="_blank">關於我們</a>︱<a href="http://co.udn.com/CORP/job.html" target="_blank"><font color="#FF0000">招募夥伴</font></a></div>

        <!-- /footer_links -->

        

        <div id="footer_copy">Copyright &copy; 2013 udn.com  All Rights Reserved.</div>

        <!-- /footer_copy -->

        

    </div>

    <!-- /footer_box --> 
</div>
<!-- /footer -->

	<!-- watermark ad start -->
<div id="abgne_float_ad" >	
	<script type="text/javascript" src="watermark_omo_2013.js"></script>
</div>
	<!-- watermark ad end -->
	<!-- Popunder AD Start -->

	<script language="javascript">

	<!--

	function GetCkValue( name ) {

	var dc=document.cookie;

	var prefix=name+"=";

	var begin=dc.indexOf("; "+prefix);

	if(begin==-1) begin=dc.indexOf(prefix);

	else begin+=2;

	if(begin==-1) return "";

	var end=document.cookie.indexOf(";",begin);

	if(end==-1) end=dc.length;

	return dc.substring(begin+prefix.length,end);

	}

	function setCookie(name,value,expiresOrNot,path,domain,secure,flag){

	var url=document.URL;

	var howLong=new Date();

	howLong.setFullYear(howLong.getFullYear()+1);

	var curCookie=name+"="+escape(value) +

	((expiresOrNot)?";expires="+howLong.toGMTString():"") +

	((path)?";path="+path:"") +

	((domain)?";domain="+domain:"") +

	((secure)?";secure":"");

	document.cookie=curCookie;

	if(flag!="null")history.go(0);

	}

	if(GetCkValue("popunder")!="Yes"){

	 setCookie("popunder","Yes",false,"/","","","null");

	 //popup=window.open('/NEWS/2004/AD/popunder.html','popunderwindow','resizable=no,scrollbars=no,width=400,height=400,top=3000,left=3000');popup.blur();window.focus();

	 //popup.moveTo(40,150);

	  }

	 // -->
	</script>




<script>
        var url = document.URL;
        var isrc = "";
                 if (url.indexOf("FINANCE") != -1||url.indexOf("BREAKINGNEWS6") != -1) {
        //if (url.indexOf("STOCK") != -1 ) {
                jQuery("iframe").each(function() {
                        isrc = jQuery(this).attr("src");
                        if (isrc.indexOf("_728X90") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("_240X400") != -1 || isrc.indexOf("-240X570") != -1) {
                                //jQuery(this).css("height","400px");
                        }
                        if (isrc.indexOf("_240X700") != -1 || isrc.indexOf("-240X700") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("_240X520") != -1 ) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("250x250") != -1 ) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("468x60") != -1 ) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("amex2014") != -1 ) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("L-iframe") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("AD/cigna") != -1) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("cigna") != -1) {
                                jQuery(this).css("height","0px");
                        }
                });
                jQuery("#sidemenu_ad").css("display","none");
                jQuery("#etuFrame").css("display","none");
                jQuery("#adsense").css("display","none");
		jQuery("#subcatend").css("display","none");
        }
</script>


<script>
        var url = document.URL;
        var isrc = "";
       		 if (url.indexOf("WORLD") != -1 || url.indexOf("BREAKINGNEWS5") != -1 || url.indexOf("MAINLAND") != -1 || url.indexOf("BREAKINGNEWS4") != -1 ) {
        //if (url.indexOf("STOCK") != -1 ) {
                jQuery("iframe").each(function() {
                        isrc = jQuery(this).attr("src");
                        if (isrc.indexOf("_728X90") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("_240X400") != -1 || isrc.indexOf("-240X570") != -1) {
                                jQuery(this).css("height","570px");
                        }
                        if (isrc.indexOf("_240X700") != -1 || isrc.indexOf("-240X700") != -1) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("_240X520") != -1 ) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("250x250") != -1 ) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("468x60") != -1 ) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("amex2014") != -1 ) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("L-iframe") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("AD/cigna") != -1) {
                                jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("cigna") != -1) {
                                jQuery(this).css("height","0px");
                        }
                });
                jQuery("#sidemenu_ad").css("display","none");
                jQuery("#etuFrame").css("display","none");
                jQuery("#adsense").css("display","none");
                jQuery("#subcatend").css("display","none");
        }
</script>

<script>
        var url = document.URL;
        var isrc = "";
                 if (url.indexOf("NATIONAL") != -1 || url.indexOf("ENTERTAINMENT") != -1 || url.indexOf("BREAKINGNEWSl") != -1 ) {
        //if (url.indexOf("STOCK") != -1 ) {
                jQuery("iframe").each(function() {
                        isrc = jQuery(this).attr("src");
                        if (isrc.indexOf("_728X90") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("_240X400") != -1 || isrc.indexOf("-240X570") != -1) {
                                //jQuery(this).css("height","570px");
                        }
                        if (isrc.indexOf("_240X700") != -1 || isrc.indexOf("-240X700") != -1) {
                                jQuery(this).css("height","455px");
                        }
                        if (isrc.indexOf("_240X520") != -1 ) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("250x250") != -1 ) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("468x60") != -1 ) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("amex2014") != -1 ) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("L-iframe") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("AD/cigna") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("cigna") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                        if (isrc.indexOf("udnshopping_promote") != -1) {
                                //jQuery(this).css("height","0px");
                        }
                });
                //jQuery("#sidemenu_ad").css("display","none");
                //jQuery("#etuFrame").css("display","none");
                //jQuery("#adsense").css("display","none");
                //jQuery("#subcatend").css("display","none");
        }
</script>


<script>
jQuery(document).ready(function() {
        jQuery("#slidetabs_1 > dl > dd").bind("click",function() {
		var ind =  parseInt(jQuery("#slidetabs_1 > dl > dd").index(this))+ parseInt(1);
		var tab = "Tab"+ind;
                var hr = jQuery(this).html();
                var hrA = hr.split("\"");
                var hr2 = hrA[3];
                var hrA2 = hr2.split("?");
                var hr3 = hrA2[0];
                _track(hr3);
                _track(tab);
        });
});

function _track(url){
        _gaq.push(['_setAccount', 'UA-19660006-3']);
        _gaq.push(['_setDomainName', '.udn.com']);
        _gaq.push(['_trackPageview', url]);
}
</script>

<script>
function getByteLength(str){
  var iCount = 0;
  for( k=0; k<str.length; k++ ) {
    c = str.charCodeAt(k);
    if ( c>=32 && c<=126 ) iCount++;
    else iCount += 2;
  }
  return iCount;
}

function getSubString(str,maxwclength) {
  var iCount = 0;

  for( k=0; k<str.length; k++ ) {
    c = str.charCodeAt(k);
    if ( c>=32 && c<=126 ) iCount++;
    else iCount += 2;

    if (iCount > maxwclength) {
      str = str.substr(0,k) + "...";
      break;
    }
  }

  return str;
}
</script>

<script type="text/javascript">
//這裡使用 rId function 是否存在來判斷是否是在內文頁中。所以沒有include gsg 會不作。

if (typeof($jq) != "undefined" && typeof window.rId == 'function' ) {
          //內文頁
	$jq('table.border tr td table tr:nth-child(10) td').each(function() {
		$jq(this).attr("align","left"); 
	});

/* 2012 01 02 fix story 內含table 會不完整問題!!
$jq('table.border tr td table tr:nth-child(6)').each(function() {
     $jq(this).hide();
     $jq(this).attr("id","embs2") ;
});
*/
	$jq('table.border tr td table tr:nth-child(5) td:nth-child(1) table ').each(function() {
		if ( ! $jq.browser.webkit ) $jq(this).hide();
		$jq(this).attr("id","embs2") ;
	});
/*if ( ! $jq.browser.webkit ){ //fix author vanished on chrome!!
$jq('table.border tr td table tr:nth-child(9) td').each(function() {
     $jq(this).attr("id","linedot") ;
     $jq(this).empty() ;
});
}
*/

if ( $jq.browser.webkit){ //for google chrome
/*
$jq('table.border tr td table tr td table tr td table tr:conddtains("story_author")').each(function() {
     $jq(this).attr("id","embauth") ;
});
*/
//table.border tr td table tr td table tr 
	var is=0;
	$jq('table.border tr td table tr td table tr td').each(function() {
		if ( $jq(this).html().match("linedot")  ) {
			is++;
			if (is==2) $jq(this).hide();
		}
	});

	$jq('table.border tr td table tr td table tr td table tr').each(function() {
		if ( $jq(this).html().match("letter.jsp") && $jq(this).html().match("push.gif") ) {
			$jq(this).hide();
		}
	});

} //end of webkit
  
var cc='--';
var str="";
var selects=$jq("#jumpMenu option");
  //append 20130412 
for (var i=1;i<(selects.length-1)/2;i++){
	var snode=selects[i].lastChild;
	descstr=snode.nodeValue;
	$jq("#jumpMenu option[value='"+selects[i].value+"']").remove();
	if ( descstr.indexOf("@--")<0 && descstr.indexOf("/") < 0 && descstr.indexOf(":") < 0 ){
		var art_id=selects[i].value;
               // selects[i].value=art_id=art_id.substring(0, art_id.indexOf(".shtml"))+"/"+encodeURIComponent(descstr)+"-"+;
           if ( location.href.indexOf("www.udn.com") > 0 ){
              //don't change seo
           }else {

		selects[i].value=encodeURIComponent(descstr)+"-"+art_id;
           }
	}
	$jq("#jumpMenu").append("<option value='"+selects[i].value+"'>"+descstr+"</option>");
}

var rlimit=20;
var displaynum=10;
//var bgimg='/2010/images/list_bg.gif';
var bgimg='http://s.udn.com.tw/img/more_bg.gif';
if ( location.href.indexOf("ENTERTAINMENT") > 0 ){
	bgimg='http://s.udn.com.tw/img/more_bg_star.gif';
}
if ( location.href.indexOf("FUND") > 0 || location.href.indexOf("FINANCE") > 0 || location.href.indexOf("STOCK") > 0 ){
	bgimg='http://s.udn.com.tw/img/more_bg_money.gif';
}
var maxwclength = 36;         
var rlimit=20;
aa='<div id="news_area" style="border-bottom: 1px dotted #CCCCCC;background: url('+bgimg+') repeat-x scroll left top transparent;height:266px;width:548px;padding-bottom:6px">';
aa=aa+'<div id="related_news"  style="border-right: 1px solid #CCCCCC;    display: block;float:left;height:264px;position: relative;width: 269px;">';

aa=aa+'<div class="news_head" style="float:left;color: #FFFFFF;display:block;font-size: 15px;font-weight: bold;height:28px;margin-bottom:5px;padding:3px 0 0 10px;width: 261px;" ><div class="more"  style="float:right;padding-right: 10px;padding-top: 0px;" ><a href="index.shtml"><img src="http://s.udn.com.tw/2010MAIN/img/more.gif" height="21" width="39"  border="0"></a></div>相 關 新 聞</div>';
aa=aa+'<dl>';
var jj=0;
for (var i=1;i<rlimit;i++){
	if (selects[i] != undefined){
		var textnode=selects[i].lastChild;
		descstr=textnode.nodeValue;
		if ( (i>1) && (selects[i].value == selects[0].value ||  i > rlimit ) )  break;

		if (getByteLength(descstr) > maxwclength ){
			descstr = getSubString(descstr,maxwclength);
		}

		var regex = /》/;
		if ( descstr.match (regex)) {}
		else{
			aa=aa+'<dd >‧<a href="'+selects[i].value+'" class="story_category" style="font-size:13px;line-height:23px" >'+descstr+'</a></dd>';
			jj++;
			if ( jj >= displaynum ) break; 
		}
	}
}

aa=aa+'</dl>';
aa=aa+'</div>';


aa=aa+'<div id="hot_news" style="display: block;float:left; position:relative;width: 273px; height:260px" >';

if (  $jq.browser.msie && $jq.browser.version == '6.0' )  {   
	aa=aa+'<div class="news_head" style="float:left;color: #FFFFFF;display:block;font-size: 15px;font-weight: bold;height:28px;margin-bottom:5px;padding:3px 0 0 10px;width: 261px;" ><div class="more" style="float:right;padding-right: 10px;padding-top: 0px;"><a href="http://dignews.udn.com/forum/udnnews/news_hot.jsp?type=daily&from=story" target="_blank"><img src="http://s.udn.com.tw/2010MAIN/img/more.gif" height="21" width="39" border="0"></a></div>發 燒 新 聞</div>';
}
else{
	aa=aa+'<div class="news_head" style="float:left;color: #FFFFFF;display:block;font-size: 15px;font-weight: bold;height:28px;margin-bottom:5px;padding:3px 0 0 10px;width: 269px;" ><div class="more" style="float:right;padding-right: 10px;padding-top: 0px;"><a href="http://dignews.udn.com/forum/udnnews/news_hot.jsp?type=daily&from=story" target="_blank"><img src="http://s.udn.com.tw/2010MAIN/img/more.gif" height="21" width="39" border="0"></a></div>發 燒 新 聞</div>';
}
aa=aa+'<dl>';
aa=aa+'</dl>';	
aa=aa+'</div>';
aa=aa+'</div>';
if ( $jq("#etuFrame").length > 0 ) $jq(aa).insertBefore( $jq("#etuFrame"));
else if ( $jq("#adsense").length > 0 ) $jq(aa).insertBefore( $jq("#adsense"));
else {}
//if ( $jq("#adsense") != undefined ) $jq(aa).insertBefore( $jq("#adsense"));
$jq("#other table").css('display','none');
if (  $jq.browser.msie && $jq.browser.version == '6.0' )  {   
	$jq.ajax({
		url: "/2010MAIN/inc/focus_hot7.html",
		success: function(data){
			$jq('#hot_news').toggle();
			$jq('#hot_news').append(data);
			$jq('#hot_news').toggle();
		}
	});
}
else{
	$jq.ajax({
		url: "/2010MAIN/inc/focus_hot7.html",
		success: function(data){
			$jq('#hot_news').toggle();
			$jq('#hot_news').append(data);
			$jq('#hot_news').toggle();
		}
	});
}
	//}
}
        //else{
         
        //}
        //$jq('#gotop').click();
</script>
<!-- ae  -->



<!-- college2011 -->
<script>
/*
var objtext;

if ( objtext=document.getElementById('search_kw') ){
	objtext.value="落點分析";
	objtext.style.color = "#999999";
	objtext.onclick = function(){
		KwCheckClear();
	};
}

function KwCheckClear(){
	if ( objtext=document.getElementById('search_kw') ){
		if( objtext.value == "落點分析" ){
			objtext.value="";
		}
		objtext.style.color = "#000000";
	}else{
		return;
	}
}*/
</script> 
<!-- college2011 -->
<!--
<script language="JavaScript" src="/Project/sess.js" type="text/javascript"></script>
-->

<script>
  if ( window.location.href.indexOf("/NEWS/main") == -1 ) {
    document.write("<script src='https://apis.google.com/js/plusone.js'><\/script>");
  }
</script>
<!--<script type="text/javascript" src="https://apis.google.com/js/plusone.js">{lang: 'zh-TW'}</script>-->


<!--nielsen tag-->
<!-- START Nielsen Online SiteCensus V6.0 -->

<!-- END Nielsen Online SiteCensus V6.0 -->


<!--nielsen tag-->

<script type="text/javascript" src="swfobject.js"></script> 
    <script type="text/javascript">
<!--	
function showAD() {
		var flashvars = {};
		var params = {};
		var attributes = {};
		params.wmode="transparent";
		attributes.id = "swf_Container";
        swfobject.embedSWF("pop.swf", "swf_Container", "1000", "700", "9.0.0", "swf/expressinstall.swf", flashvars, params, attributes);
		setTimeout('swfClose("outer")',15000);
		setTimeout('swfClose("swf_Container")',15000);
}		
//-->		
    </script>


<script language="javascript">
 
	<!--
 
	function GetCkValue( name ) {
 
	var dc=document.cookie;
 
	var prefix=name+"=";
 
	var begin=dc.indexOf("; "+prefix);
 
	if(begin==-1) begin=dc.indexOf(prefix);
 
	else begin+=2;
 
	if(begin==-1) return "";
 
	var end=document.cookie.indexOf(";",begin);
 
	if(end==-1) end=dc.length;
 
	return dc.substring(begin+prefix.length,end);
 
	}
 
	function setCookie(name,value,expiresOrNot,path,domain,secure,flag){
 
	var url=document.URL;
 
	var howLong=new Date();
 
	howLong.setFullYear(howLong.getFullYear()+1);
 
	var curCookie=name+"="+escape(value) +
 
	((expiresOrNot)?";expires="+howLong.toGMTString():"") +
 
	((path)?";path="+path:"") +
 
	((domain)?";domain="+domain:"") +
 
	((secure)?";secure":"");
 
	document.cookie=curCookie;
 
	if(flag!="null")history.go(0);
 
	}
 
	//if(GetCkValue("coverad")!="Yes"){
 
	 //setCookie("coverad","Yes",false,"/","","","null");
 
	// showAD();
 
	  //}
 
	 // -->
	</script>

</body>
</html>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AngleSharp
{
    /// <summary>
    /// Represents the list of all Html entities.
    /// </summary>
    [DebuggerStepThrough]
    static class Entities
    {
        #region ctor

        static Dictionary<Char, Dictionary<String, String>> _entities;

        static Entities()
        {
            _entities = new Dictionary<Char, Dictionary<String, String>>();

            _entities.Add('a', GetSymbolLittleA());
            _entities.Add('A', GetSymbolBigA());

            _entities.Add('b', GetSymbolLittleB());
            _entities.Add('B', GetSymbolBigB());

            _entities.Add('c', GetSymbolLittleC());
            _entities.Add('C', GetSymbolBigC());

            _entities.Add('d', GetSymbolLittleD());
            _entities.Add('D', GetSymbolBigD());

            _entities.Add('e', GetSymbolLittleE());
            _entities.Add('E', GetSymbolBigE());

            _entities.Add('f', GetSymbolLittleF());
            _entities.Add('F', GetSymbolBigF());

            _entities.Add('g', GetSymbolLittleG());
            _entities.Add('G', GetSymbolBigG());

            _entities.Add('h', GetSymbolLittleH());
            _entities.Add('H', GetSymbolBigH());

            _entities.Add('i', GetSymbolLittleI());
            _entities.Add('I', GetSymbolBigI());

            _entities.Add('j', GetSymbolLittleJ());
            _entities.Add('J', GetSymbolBigJ());

            _entities.Add('k', GetSymbolLittleK());
            _entities.Add('K', GetSymbolBigK());

            _entities.Add('l', GetSymbolLittleL());
            _entities.Add('L', GetSymbolBigL());

            _entities.Add('m', GetSymbolLittleM());
            _entities.Add('M', GetSymbolBigM());

            _entities.Add('n', GetSymbolLittleN());
            _entities.Add('N', GetSymbolBigN());

            _entities.Add('o', GetSymbolLittleO());
            _entities.Add('O', GetSymbolBigO());

            _entities.Add('p', GetSymbolLittleP());
            _entities.Add('P', GetSymbolBigP());

            _entities.Add('q', GetSymbolLittleQ());
            _entities.Add('Q', GetSymbolBigQ());

            _entities.Add('r', GetSymbolLittleR());
            _entities.Add('R', GetSymbolBigR());

            _entities.Add('s', GetSymbolLittleS());
            _entities.Add('S', GetSymbolBigS());

            _entities.Add('t', GetSymbolLittleT());
            _entities.Add('T', GetSymbolBigT());

            _entities.Add('u', GetSymbolLittleU());
            _entities.Add('U', GetSymbolBigU());

            _entities.Add('v', GetSymbolLittleV());
            _entities.Add('V', GetSymbolBigV());

            _entities.Add('w', GetSymbolLittleW());
            _entities.Add('W', GetSymbolBigW());

            _entities.Add('x', GetSymbolLittleX());
            _entities.Add('X', GetSymbolBigX());

            _entities.Add('y', GetSymbolLittleY());
            _entities.Add('Y', GetSymbolBigY());

            _entities.Add('z', GetSymbolLittleZ());
            _entities.Add('Z', GetSymbolBigZ());
        }

        #endregion

        #region Symbol Methods

        static Dictionary<String, String> GetSymbolLittleA()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("aacute", Convert(0x00E1));
            symbols.Add("abreve", Convert(0x0103));
            symbols.Add("ac", Convert(0x223E));
            symbols.Add("acd", Convert(0x223F));
            symbols.Add("acE", Convert(0x223E, 0x0333));
            symbols.Add("acirc", Convert(0x00E2));
            symbols.Add("acute", Convert(0x00B4));
            symbols.Add("acy", Convert(0x0430));
            symbols.Add("aelig", Convert(0x00E6));
            symbols.Add("af", Convert(0x2061));
            symbols.Add("afr", Convert(0x1D51E));
            symbols.Add("agrave", Convert(0x00E0));
            symbols.Add("alefsym", Convert(0x2135));
            symbols.Add("aleph", Convert(0x2135));
            symbols.Add("alpha", Convert(0x03B1));
            symbols.Add("amacr", Convert(0x0101));
            symbols.Add("amalg", Convert(0x2A3F));
            symbols.Add("amp", Convert(0x0026));
            symbols.Add("and", Convert(0x2227));
            symbols.Add("andand", Convert(0x2A55));
            symbols.Add("andd", Convert(0x2A5C));
            symbols.Add("andslope", Convert(0x2A58));
            symbols.Add("andv", Convert(0x2A5A));
            symbols.Add("ang", Convert(0x2220));
            symbols.Add("ange", Convert(0x29A4));
            symbols.Add("angle", Convert(0x2220));
            symbols.Add("angmsd", Convert(0x2221));
            symbols.Add("angmsdaa", Convert(0x29A8));
            symbols.Add("angmsdab", Convert(0x29A9));
            symbols.Add("angmsdac", Convert(0x29AA));
            symbols.Add("angmsdad", Convert(0x29AB));
            symbols.Add("angmsdae", Convert(0x29AC));
            symbols.Add("angmsdaf", Convert(0x29AD));
            symbols.Add("angmsdag", Convert(0x29AE));
            symbols.Add("angmsdah", Convert(0x29AF));
            symbols.Add("angrt", Convert(0x221F));
            symbols.Add("angrtvb", Convert(0x22BE));
            symbols.Add("angrtvbd", Convert(0x299D));
            symbols.Add("angsph", Convert(0x2222));
            symbols.Add("angst", Convert(0x00C5));
            symbols.Add("angzarr", Convert(0x237C));
            symbols.Add("aogon", Convert(0x0105));
            symbols.Add("aopf", Convert(0x1D552));
            symbols.Add("ap", Convert(0x2248));
            symbols.Add("apacir", Convert(0x2A6F));
            symbols.Add("apE", Convert(0x2A70));
            symbols.Add("ape", Convert(0x224A));
            symbols.Add("apid", Convert(0x224B));
            symbols.Add("apos", Convert(0x0027));
            symbols.Add("approx", Convert(0x2248));
            symbols.Add("approxeq", Convert(0x224A));
            symbols.Add("aring", Convert(0x00E5));
            symbols.Add("ascr", Convert(0x1D4B6));
            symbols.Add("ast", Convert(0x002A));
            symbols.Add("asymp", Convert(0x2248));
            symbols.Add("asympeq", Convert(0x224D));
            symbols.Add("atilde", Convert(0x00E3));
            symbols.Add("auml", Convert(0x00E4));
            symbols.Add("awconint", Convert(0x2233));
            symbols.Add("awint", Convert(0x2A11));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigA()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Aogon", Convert(0x0104));
            symbols.Add("Aopf", Convert(0x1D538));
            symbols.Add("ApplyFunction", Convert(0x2061));
            symbols.Add("Aring", Convert(0x00C5));
            symbols.Add("Ascr", Convert(0x1D49C));
            symbols.Add("Assign", Convert(0x2254));
            symbols.Add("Atilde", Convert(0x00C3));
            symbols.Add("Auml", Convert(0x00C4));
            symbols.Add("Aacute", Convert(0x00C1));
            symbols.Add("Abreve", Convert(0x0102));
            symbols.Add("Acirc", Convert(0x00C2));
            symbols.Add("Acy", Convert(0x0410));
            symbols.Add("AElig", Convert(0x00C6));
            symbols.Add("Afr", Convert(0x1D504));
            symbols.Add("Agrave", Convert(0x00C0));
            symbols.Add("Alpha", Convert(0x0391));
            symbols.Add("Amacr", Convert(0x0100));
            symbols.Add("AMP", Convert(0x0026));
            symbols.Add("And", Convert(0x2A53));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleB()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("backcong", Convert(0x224C));
            symbols.Add("backepsilon", Convert(0x03F6));
            symbols.Add("backprime", Convert(0x2035));
            symbols.Add("backsim", Convert(0x223D));
            symbols.Add("backsimeq", Convert(0x22CD));
            symbols.Add("barvee", Convert(0x22BD));
            symbols.Add("barwed", Convert(0x2305));
            symbols.Add("barwedge", Convert(0x2305));
            symbols.Add("bbrk", Convert(0x23B5));
            symbols.Add("bbrktbrk", Convert(0x23B6));
            symbols.Add("bcong", Convert(0x224C));
            symbols.Add("bcy", Convert(0x0431));
            symbols.Add("bdquo", Convert(0x201E));
            symbols.Add("becaus", Convert(0x2235));
            symbols.Add("because", Convert(0x2235));
            symbols.Add("bemptyv", Convert(0x29B0));
            symbols.Add("bepsi", Convert(0x03F6));
            symbols.Add("bernou", Convert(0x212C));
            symbols.Add("beta", Convert(0x03B2));
            symbols.Add("beth", Convert(0x2136));
            symbols.Add("between", Convert(0x226C));
            symbols.Add("bfr", Convert(0x1D51F));
            symbols.Add("bigcap", Convert(0x22C2));
            symbols.Add("bigcirc", Convert(0x25EF));
            symbols.Add("bigcup", Convert(0x22C3));
            symbols.Add("bigodot", Convert(0x2A00));
            symbols.Add("bigoplus", Convert(0x2A01));
            symbols.Add("bigotimes", Convert(0x2A02));
            symbols.Add("bigsqcup", Convert(0x2A06));
            symbols.Add("bigstar", Convert(0x2605));
            symbols.Add("bigtriangledown", Convert(0x25BD));
            symbols.Add("bigtriangleup", Convert(0x25B3));
            symbols.Add("biguplus", Convert(0x2A04));
            symbols.Add("bigvee", Convert(0x22C1));
            symbols.Add("bigwedge", Convert(0x22C0));
            symbols.Add("bkarow", Convert(0x290D));
            symbols.Add("blacklozenge", Convert(0x29EB));
            symbols.Add("blacksquare", Convert(0x25AA));
            symbols.Add("blacktriangle", Convert(0x25B4));
            symbols.Add("blacktriangledown", Convert(0x25BE));
            symbols.Add("blacktriangleleft", Convert(0x25C2));
            symbols.Add("blacktriangleright", Convert(0x25B8));
            symbols.Add("blank", Convert(0x2423));
            symbols.Add("blk12", Convert(0x2592));
            symbols.Add("blk14", Convert(0x2591));
            symbols.Add("blk34", Convert(0x2593));
            symbols.Add("block", Convert(0x2588));
            symbols.Add("bne", Convert(0x003D, 0x20E5));
            symbols.Add("bnequiv", Convert(0x2261, 0x20E5));
            symbols.Add("bNot", Convert(0x2AED));
            symbols.Add("bnot", Convert(0x2310));
            symbols.Add("bopf", Convert(0x1D553));
            symbols.Add("bot", Convert(0x22A5));
            symbols.Add("bottom", Convert(0x22A5));
            symbols.Add("bowtie", Convert(0x22C8));
            symbols.Add("boxbox", Convert(0x29C9));
            symbols.Add("boxDL", Convert(0x2557));
            symbols.Add("boxDl", Convert(0x2556));
            symbols.Add("boxdL", Convert(0x2555));
            symbols.Add("boxdl", Convert(0x2510));
            symbols.Add("boxDR", Convert(0x2554));
            symbols.Add("boxDr", Convert(0x2553));
            symbols.Add("boxdR", Convert(0x2552));
            symbols.Add("boxdr", Convert(0x250C));
            symbols.Add("boxH", Convert(0x2550));
            symbols.Add("boxh", Convert(0x2500));
            symbols.Add("boxHD", Convert(0x2566));
            symbols.Add("boxHd", Convert(0x2564));
            symbols.Add("boxhD", Convert(0x2565));
            symbols.Add("boxhd", Convert(0x252C));
            symbols.Add("boxHU", Convert(0x2569));
            symbols.Add("boxHu", Convert(0x2567));
            symbols.Add("boxhU", Convert(0x2568));
            symbols.Add("boxhu", Convert(0x2534));
            symbols.Add("boxminus", Convert(0x229F));
            symbols.Add("boxplus", Convert(0x229E));
            symbols.Add("boxtimes", Convert(0x22A0));
            symbols.Add("boxUL", Convert(0x255D));
            symbols.Add("boxUl", Convert(0x255C));
            symbols.Add("boxuL", Convert(0x255B));
            symbols.Add("boxul", Convert(0x2518));
            symbols.Add("boxUR", Convert(0x255A));
            symbols.Add("boxUr", Convert(0x2559));
            symbols.Add("boxuR", Convert(0x2558));
            symbols.Add("boxur", Convert(0x2514));
            symbols.Add("boxV", Convert(0x2551));
            symbols.Add("boxv", Convert(0x2502));
            symbols.Add("boxVH", Convert(0x256C));
            symbols.Add("boxVh", Convert(0x256B));
            symbols.Add("boxvH", Convert(0x256A));
            symbols.Add("boxvh", Convert(0x253C));
            symbols.Add("boxVL", Convert(0x2563));
            symbols.Add("boxVl", Convert(0x2562));
            symbols.Add("boxvL", Convert(0x2561));
            symbols.Add("boxvl", Convert(0x2524));
            symbols.Add("boxVR", Convert(0x2560));
            symbols.Add("boxVr", Convert(0x255F));
            symbols.Add("boxvR", Convert(0x255E));
            symbols.Add("boxvr", Convert(0x251C));
            symbols.Add("bprime", Convert(0x2035));
            symbols.Add("breve", Convert(0x02D8));
            symbols.Add("brvbar", Convert(0x00A6));
            symbols.Add("bscr", Convert(0x1D4B7));
            symbols.Add("bsemi", Convert(0x204F));
            symbols.Add("bsim", Convert(0x223D));
            symbols.Add("bsime", Convert(0x22CD));
            symbols.Add("bsol", Convert(0x005C));
            symbols.Add("bsolb", Convert(0x29C5));
            symbols.Add("bsolhsub", Convert(0x27C8));
            symbols.Add("bull", Convert(0x2022));
            symbols.Add("bullet", Convert(0x2022));
            symbols.Add("bump", Convert(0x224E));
            symbols.Add("bumpE", Convert(0x2AAE));
            symbols.Add("bumpe", Convert(0x224F));
            symbols.Add("bumpeq", Convert(0x224F));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigB()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Backslash", Convert(0x2216));
            symbols.Add("Barv", Convert(0x2AE7));
            symbols.Add("Barwed", Convert(0x2306));
            symbols.Add("Bcy", Convert(0x0411));
            symbols.Add("Because", Convert(0x2235));
            symbols.Add("Bernoullis", Convert(0x212C));
            symbols.Add("Beta", Convert(0x0392));
            symbols.Add("Bfr", Convert(0x1D505));
            symbols.Add("Bopf", Convert(0x1D539));
            symbols.Add("Breve", Convert(0x02D8));
            symbols.Add("Bscr", Convert(0x212C));
            symbols.Add("Bumpeq", Convert(0x224E));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleC()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("cacute", Convert(0x0107));
            symbols.Add("cap", Convert(0x2229));
            symbols.Add("capand", Convert(0x2A44));
            symbols.Add("capbrcup", Convert(0x2A49));
            symbols.Add("capcap", Convert(0x2A4B));
            symbols.Add("capcup", Convert(0x2A47));
            symbols.Add("capdot", Convert(0x2A40));
            symbols.Add("caps", Convert(0x2229, 0xFE00));
            symbols.Add("caret", Convert(0x2041));
            symbols.Add("caron", Convert(0x02C7));
            symbols.Add("ccaps", Convert(0x2A4D));
            symbols.Add("ccaron", Convert(0x010D));
            symbols.Add("ccedil", Convert(0x00E7));
            symbols.Add("ccirc", Convert(0x0109));
            symbols.Add("ccups", Convert(0x2A4C));
            symbols.Add("ccupssm", Convert(0x2A50));
            symbols.Add("cdot", Convert(0x010B));
            symbols.Add("cedil", Convert(0x00B8));
            symbols.Add("cemptyv", Convert(0x29B2));
            symbols.Add("cent", Convert(0x00A2));
            symbols.Add("centerdot", Convert(0x00B7));
            symbols.Add("cfr", Convert(0x1D520));
            symbols.Add("chcy", Convert(0x0447));
            symbols.Add("check", Convert(0x2713));
            symbols.Add("checkmark", Convert(0x2713));
            symbols.Add("chi", Convert(0x03C7));
            symbols.Add("cir", Convert(0x25CB));
            symbols.Add("circ", Convert(0x02C6));
            symbols.Add("circeq", Convert(0x2257));
            symbols.Add("circlearrowleft", Convert(0x21BA));
            symbols.Add("circlearrowright", Convert(0x21BB));
            symbols.Add("circledast", Convert(0x229B));
            symbols.Add("circledcirc", Convert(0x229A));
            symbols.Add("circleddash", Convert(0x229D));
            symbols.Add("circledR", Convert(0x00AE));
            symbols.Add("circledS", Convert(0x24C8));
            symbols.Add("cirE", Convert(0x29C3));
            symbols.Add("cire", Convert(0x2257));
            symbols.Add("cirfnint", Convert(0x2A10));
            symbols.Add("cirmid", Convert(0x2AEF));
            symbols.Add("cirscir", Convert(0x29C2));
            symbols.Add("clubs", Convert(0x2663));
            symbols.Add("clubsuit", Convert(0x2663));
            symbols.Add("colon", Convert(0x003A));
            symbols.Add("colone", Convert(0x2254));
            symbols.Add("coloneq", Convert(0x2254));
            symbols.Add("comma", Convert(0x002C));
            symbols.Add("commat", Convert(0x0040));
            symbols.Add("comp", Convert(0x2201));
            symbols.Add("compfn", Convert(0x2218));
            symbols.Add("complement", Convert(0x2201));
            symbols.Add("complexes", Convert(0x2102));
            symbols.Add("cong", Convert(0x2245));
            symbols.Add("congdot", Convert(0x2A6D));
            symbols.Add("conint", Convert(0x222E));
            symbols.Add("copf", Convert(0x1D554));
            symbols.Add("coprod", Convert(0x2210));
            symbols.Add("copy", Convert(0x00A9));
            symbols.Add("copysr", Convert(0x2117));
            symbols.Add("crarr", Convert(0x21B5));
            symbols.Add("cross", Convert(0x2717));
            symbols.Add("cscr", Convert(0x1D4B8));
            symbols.Add("csub", Convert(0x2ACF));
            symbols.Add("csube", Convert(0x2AD1));
            symbols.Add("csup", Convert(0x2AD0));
            symbols.Add("csupe", Convert(0x2AD2));
            symbols.Add("ctdot", Convert(0x22EF));
            symbols.Add("cudarrl", Convert(0x2938));
            symbols.Add("cudarrr", Convert(0x2935));
            symbols.Add("cuepr", Convert(0x22DE));
            symbols.Add("cuesc", Convert(0x22DF));
            symbols.Add("cularr", Convert(0x21B6));
            symbols.Add("cularrp", Convert(0x293D));
            symbols.Add("cup", Convert(0x222A));
            symbols.Add("cupbrcap", Convert(0x2A48));
            symbols.Add("cupcap", Convert(0x2A46));
            symbols.Add("cupcup", Convert(0x2A4A));
            symbols.Add("cupdot", Convert(0x228D));
            symbols.Add("cupor", Convert(0x2A45));
            symbols.Add("cups", Convert(0x222A, 0xFE00));
            symbols.Add("curarr", Convert(0x21B7));
            symbols.Add("curarrm", Convert(0x293C));
            symbols.Add("curlyeqprec", Convert(0x22DE));
            symbols.Add("curlyeqsucc", Convert(0x22DF));
            symbols.Add("curlyvee", Convert(0x22CE));
            symbols.Add("curlywedge", Convert(0x22CF));
            symbols.Add("curren", Convert(0x00A4));
            symbols.Add("curvearrowleft", Convert(0x21B6));
            symbols.Add("curvearrowright", Convert(0x21B7));
            symbols.Add("cuvee", Convert(0x22CE));
            symbols.Add("cuwed", Convert(0x22CF));
            symbols.Add("cwconint", Convert(0x2232));
            symbols.Add("cwint", Convert(0x2231));
            symbols.Add("cylcty", Convert(0x232D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigC()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Cacute", Convert(0x0106));
            symbols.Add("Cap", Convert(0x22D2));
            symbols.Add("CapitalDifferentialD", Convert(0x2145));
            symbols.Add("Cayleys", Convert(0x212D));
            symbols.Add("Ccaron", Convert(0x010C));
            symbols.Add("Ccedil", Convert(0x00C7));
            symbols.Add("Ccirc", Convert(0x0108));
            symbols.Add("Cconint", Convert(0x2230));
            symbols.Add("Cdot", Convert(0x010A));
            symbols.Add("Cedilla", Convert(0x00B8));
            symbols.Add("CenterDot", Convert(0x00B7));
            symbols.Add("Cfr", Convert(0x212D));
            symbols.Add("CHcy", Convert(0x0427));
            symbols.Add("Chi", Convert(0x03A7));
            symbols.Add("CircleDot", Convert(0x2299));
            symbols.Add("CircleMinus", Convert(0x2296));
            symbols.Add("CirclePlus", Convert(0x2295));
            symbols.Add("CircleTimes", Convert(0x2297));
            symbols.Add("ClockwiseContourIntegral", Convert(0x2232));
            symbols.Add("CloseCurlyDoubleQuote", Convert(0x201D));
            symbols.Add("CloseCurlyQuote", Convert(0x2019));
            symbols.Add("Colon", Convert(0x2237));
            symbols.Add("Colone", Convert(0x2A74));
            symbols.Add("Congruent", Convert(0x2261));
            symbols.Add("Conint", Convert(0x222F));
            symbols.Add("ContourIntegral", Convert(0x222E));
            symbols.Add("Copf", Convert(0x2102));
            symbols.Add("Coproduct", Convert(0x2210));
            symbols.Add("COPY", Convert(0x00A9));
            symbols.Add("CounterClockwiseContourIntegral", Convert(0x2233));
            symbols.Add("Cross", Convert(0x2A2F));
            symbols.Add("Cscr", Convert(0x1D49E));
            symbols.Add("Cup", Convert(0x22D3));
            symbols.Add("CupCap", Convert(0x224D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleD()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("dagger", Convert(0x2020));
            symbols.Add("daleth", Convert(0x2138));
            symbols.Add("dArr", Convert(0x21D3));
            symbols.Add("darr", Convert(0x2193));
            symbols.Add("dash", Convert(0x2010));
            symbols.Add("dashv", Convert(0x22A3));
            symbols.Add("dbkarow", Convert(0x290F));
            symbols.Add("dblac", Convert(0x02DD));
            symbols.Add("dcaron", Convert(0x010F));
            symbols.Add("dcy", Convert(0x0434));
            symbols.Add("dd", Convert(0x2146));
            symbols.Add("ddagger", Convert(0x2021));
            symbols.Add("ddarr", Convert(0x21CA));
            symbols.Add("ddotseq", Convert(0x2A77));
            symbols.Add("deg", Convert(0x00B0));
            symbols.Add("delta", Convert(0x03B4));
            symbols.Add("demptyv", Convert(0x29B1));
            symbols.Add("dfisht", Convert(0x297F));
            symbols.Add("dfr", Convert(0x1D521));
            symbols.Add("dHar", Convert(0x2965));
            symbols.Add("dharl", Convert(0x21C3));
            symbols.Add("dharr", Convert(0x21C2));
            symbols.Add("diam", Convert(0x22C4));
            symbols.Add("diamond", Convert(0x22C4));
            symbols.Add("diamondsuit", Convert(0x2666));
            symbols.Add("diams", Convert(0x2666));
            symbols.Add("die", Convert(0x00A8));
            symbols.Add("digamma", Convert(0x03DD));
            symbols.Add("disin", Convert(0x22F2));
            symbols.Add("div", Convert(0x00F7));
            symbols.Add("divide", Convert(0x00F7));
            symbols.Add("divideontimes", Convert(0x22C7));
            symbols.Add("divonx", Convert(0x22C7));
            symbols.Add("djcy", Convert(0x0452));
            symbols.Add("dlcorn", Convert(0x231E));
            symbols.Add("dlcrop", Convert(0x230D));
            symbols.Add("dollar", Convert(0x0024));
            symbols.Add("dopf", Convert(0x1D555));
            symbols.Add("dot", Convert(0x02D9));
            symbols.Add("doteq", Convert(0x2250));
            symbols.Add("doteqdot", Convert(0x2251));
            symbols.Add("dotminus", Convert(0x2238));
            symbols.Add("dotplus", Convert(0x2214));
            symbols.Add("dotsquare", Convert(0x22A1));
            symbols.Add("doublebarwedge", Convert(0x2306));
            symbols.Add("downarrow", Convert(0x2193));
            symbols.Add("downdownarrows", Convert(0x21CA));
            symbols.Add("downharpoonleft", Convert(0x21C3));
            symbols.Add("downharpoonright", Convert(0x21C2));
            symbols.Add("drbkarow", Convert(0x2910));
            symbols.Add("drcorn", Convert(0x231F));
            symbols.Add("drcrop", Convert(0x230C));
            symbols.Add("dscr", Convert(0x1D4B9));
            symbols.Add("dscy", Convert(0x0455));
            symbols.Add("dsol", Convert(0x29F6));
            symbols.Add("dstrok", Convert(0x0111));
            symbols.Add("dtdot", Convert(0x22F1));
            symbols.Add("dtri", Convert(0x25BF));
            symbols.Add("dtrif", Convert(0x25BE));
            symbols.Add("duarr", Convert(0x21F5));
            symbols.Add("duhar", Convert(0x296F));
            symbols.Add("dwangle", Convert(0x29A6));
            symbols.Add("dzcy", Convert(0x045F));
            symbols.Add("dzigrarr", Convert(0x27FF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigD()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Dagger", Convert(0x2021));
            symbols.Add("Darr", Convert(0x21A1));
            symbols.Add("Dashv", Convert(0x2AE4));
            symbols.Add("Dcaron", Convert(0x010E));
            symbols.Add("Dcy", Convert(0x0414));
            symbols.Add("DD", Convert(0x2145));
            symbols.Add("DDotrahd", Convert(0x2911));
            symbols.Add("Del", Convert(0x2207));
            symbols.Add("Delta", Convert(0x0394));
            symbols.Add("Dfr", Convert(0x1D507));
            symbols.Add("DiacriticalAcute", Convert(0x00B4));
            symbols.Add("DiacriticalDot", Convert(0x02D9));
            symbols.Add("DiacriticalDoubleAcute", Convert(0x02DD));
            symbols.Add("DiacriticalGrave", Convert(0x0060));
            symbols.Add("DiacriticalTilde", Convert(0x02DC));
            symbols.Add("Diamond", Convert(0x22C4));
            symbols.Add("DifferentialD", Convert(0x2146));
            symbols.Add("DJcy", Convert(0x0402));
            symbols.Add("Dopf", Convert(0x1D53B));
            symbols.Add("Dot", Convert(0x00A8));
            symbols.Add("DotDot", Convert(0x20DC));
            symbols.Add("DotEqual", Convert(0x2250));
            symbols.Add("DoubleContourIntegral", Convert(0x222F));
            symbols.Add("DoubleDot", Convert(0x00A8));
            symbols.Add("DoubleDownArrow", Convert(0x21D3));
            symbols.Add("DoubleLeftArrow", Convert(0x21D0));
            symbols.Add("DoubleLeftRightArrow", Convert(0x21D4));
            symbols.Add("DoubleLeftTee", Convert(0x2AE4));
            symbols.Add("DoubleLongLeftArrow", Convert(0x27F8));
            symbols.Add("DoubleLongLeftRightArrow", Convert(0x27FA));
            symbols.Add("DoubleLongRightArrow", Convert(0x27F9));
            symbols.Add("DoubleRightArrow", Convert(0x21D2));
            symbols.Add("DoubleRightTee", Convert(0x22A8));
            symbols.Add("DoubleUpArrow", Convert(0x21D1));
            symbols.Add("DoubleUpDownArrow", Convert(0x21D5));
            symbols.Add("DoubleVerticalBar", Convert(0x2225));
            symbols.Add("DownArrow", Convert(0x2193));
            symbols.Add("Downarrow", Convert(0x21D3));
            symbols.Add("DownArrowBar", Convert(0x2913));
            symbols.Add("DownArrowUpArrow", Convert(0x21F5));
            symbols.Add("DownBreve", Convert(0x0311));
            symbols.Add("DownLeftRightVector", Convert(0x2950));
            symbols.Add("DownLeftTeeVector", Convert(0x295E));
            symbols.Add("DownLeftVector", Convert(0x21BD));
            symbols.Add("DownLeftVectorBar", Convert(0x2956));
            symbols.Add("DownRightTeeVector", Convert(0x295F));
            symbols.Add("DownRightVector", Convert(0x21C1));
            symbols.Add("DownRightVectorBar", Convert(0x2957));
            symbols.Add("DownTee", Convert(0x22A4));
            symbols.Add("DownTeeArrow", Convert(0x21A7));
            symbols.Add("Dscr", Convert(0x1D49F));
            symbols.Add("DScy", Convert(0x0405));
            symbols.Add("Dstrok", Convert(0x0110));
            symbols.Add("DZcy", Convert(0x040F));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleE()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("eacute", Convert(0x00E9));
            symbols.Add("easter", Convert(0x2A6E));
            symbols.Add("ecaron", Convert(0x011B));
            symbols.Add("ecir", Convert(0x2256));
            symbols.Add("ecirc", Convert(0x00EA));
            symbols.Add("ecolon", Convert(0x2255));
            symbols.Add("ecy", Convert(0x044D));
            symbols.Add("eDDot", Convert(0x2A77));
            symbols.Add("eDot", Convert(0x2251));
            symbols.Add("edot", Convert(0x0117));
            symbols.Add("ee", Convert(0x2147));
            symbols.Add("efDot", Convert(0x2252));
            symbols.Add("efr", Convert(0x1D522));
            symbols.Add("eg", Convert(0x2A9A));
            symbols.Add("egrave", Convert(0x00E8));
            symbols.Add("egs", Convert(0x2A96));
            symbols.Add("egsdot", Convert(0x2A98));
            symbols.Add("el", Convert(0x2A99));
            symbols.Add("elinters", Convert(0x23E7));
            symbols.Add("ell", Convert(0x2113));
            symbols.Add("els", Convert(0x2A95));
            symbols.Add("elsdot", Convert(0x2A97));
            symbols.Add("emacr", Convert(0x0113));
            symbols.Add("empty", Convert(0x2205));
            symbols.Add("emptyset", Convert(0x2205));
            symbols.Add("emptyv", Convert(0x2205));
            symbols.Add("emsp", Convert(0x2003));
            symbols.Add("emsp13", Convert(0x2004));
            symbols.Add("emsp14", Convert(0x2005));
            symbols.Add("eng", Convert(0x014B));
            symbols.Add("ensp", Convert(0x2002));
            symbols.Add("eogon", Convert(0x0119));
            symbols.Add("eopf", Convert(0x1D556));
            symbols.Add("epar", Convert(0x22D5));
            symbols.Add("eparsl", Convert(0x29E3));
            symbols.Add("eplus", Convert(0x2A71));
            symbols.Add("epsi", Convert(0x03B5));
            symbols.Add("epsilon", Convert(0x03B5));
            symbols.Add("epsiv", Convert(0x03F5));
            symbols.Add("eqcirc", Convert(0x2256));
            symbols.Add("eqcolon", Convert(0x2255));
            symbols.Add("eqsim", Convert(0x2242));
            symbols.Add("eqslantgtr", Convert(0x2A96));
            symbols.Add("eqslantless", Convert(0x2A95));
            symbols.Add("equals", Convert(0x003D));
            symbols.Add("equest", Convert(0x225F));
            symbols.Add("equiv", Convert(0x2261));
            symbols.Add("equivDD", Convert(0x2A78));
            symbols.Add("eqvparsl", Convert(0x29E5));
            symbols.Add("erarr", Convert(0x2971));
            symbols.Add("erDot", Convert(0x2253));
            symbols.Add("escr", Convert(0x212F));
            symbols.Add("esdot", Convert(0x2250));
            symbols.Add("esim", Convert(0x2242));
            symbols.Add("eta", Convert(0x03B7));
            symbols.Add("eth", Convert(0x00F0));
            symbols.Add("euml", Convert(0x00EB));
            symbols.Add("euro", Convert(0x20AC));
            symbols.Add("excl", Convert(0x0021));
            symbols.Add("exist", Convert(0x2203));
            symbols.Add("expectation", Convert(0x2130));
            symbols.Add("exponentiale", Convert(0x2147));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigE()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Eacute", Convert(0x00C9));
            symbols.Add("Ecaron", Convert(0x011A));
            symbols.Add("Ecirc", Convert(0x00CA));
            symbols.Add("Ecy", Convert(0x042D));
            symbols.Add("Edot", Convert(0x0116));
            symbols.Add("Efr", Convert(0x1D508));
            symbols.Add("Egrave", Convert(0x00C8));
            symbols.Add("Element", Convert(0x2208));
            symbols.Add("Emacr", Convert(0x0112));
            symbols.Add("EmptySmallSquare", Convert(0x25FB));
            symbols.Add("EmptyVerySmallSquare", Convert(0x25AB));
            symbols.Add("ENG", Convert(0x014A));
            symbols.Add("Eogon", Convert(0x0118));
            symbols.Add("Eopf", Convert(0x1D53C));
            symbols.Add("Epsilon", Convert(0x0395));
            symbols.Add("Equal", Convert(0x2A75));
            symbols.Add("EqualTilde", Convert(0x2242));
            symbols.Add("Equilibrium", Convert(0x21CC));
            symbols.Add("Escr", Convert(0x2130));
            symbols.Add("Esim", Convert(0x2A73));
            symbols.Add("Eta", Convert(0x0397));
            symbols.Add("ETH", Convert(0x00D0));
            symbols.Add("Euml", Convert(0x00CB));
            symbols.Add("Exists", Convert(0x2203));
            symbols.Add("ExponentialE", Convert(0x2147));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleF()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("fallingdotseq", Convert(0x2252));
            symbols.Add("fcy", Convert(0x0444));
            symbols.Add("female", Convert(0x2640));
            symbols.Add("ffilig", Convert(0xFB03));
            symbols.Add("fflig", Convert(0xFB00));
            symbols.Add("ffllig", Convert(0xFB04));
            symbols.Add("ffr", Convert(0x1D523));
            symbols.Add("filig", Convert(0xFB01));
            symbols.Add("fjlig", Convert(0x0066, 0x006A));
            symbols.Add("flat", Convert(0x266D));
            symbols.Add("fllig", Convert(0xFB02));
            symbols.Add("fltns", Convert(0x25B1));
            symbols.Add("fnof", Convert(0x0192));
            symbols.Add("fopf", Convert(0x1D557));
            symbols.Add("forall", Convert(0x2200));
            symbols.Add("fork", Convert(0x22D4));
            symbols.Add("forkv", Convert(0x2AD9));
            symbols.Add("fpartint", Convert(0x2A0D));
            symbols.Add("frac12", Convert(0x00BD));
            symbols.Add("frac13", Convert(0x2153));
            symbols.Add("frac14", Convert(0x00BC));
            symbols.Add("frac15", Convert(0x2155));
            symbols.Add("frac16", Convert(0x2159));
            symbols.Add("frac18", Convert(0x215B));
            symbols.Add("frac23", Convert(0x2154));
            symbols.Add("frac25", Convert(0x2156));
            symbols.Add("frac34", Convert(0x00BE));
            symbols.Add("frac35", Convert(0x2157));
            symbols.Add("frac38", Convert(0x215C));
            symbols.Add("frac45", Convert(0x2158));
            symbols.Add("frac56", Convert(0x215A));
            symbols.Add("frac58", Convert(0x215D));
            symbols.Add("frac78", Convert(0x215E));
            symbols.Add("frasl", Convert(0x2044));
            symbols.Add("frown", Convert(0x2322));
            symbols.Add("fscr", Convert(0x1D4BB));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigF()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Fcy", Convert(0x0424));
            symbols.Add("Ffr", Convert(0x1D509));
            symbols.Add("FilledSmallSquare", Convert(0x25FC));
            symbols.Add("FilledVerySmallSquare", Convert(0x25AA));
            symbols.Add("Fopf", Convert(0x1D53D));
            symbols.Add("ForAll", Convert(0x2200));
            symbols.Add("Fouriertrf", Convert(0x2131));
            symbols.Add("Fscr", Convert(0x2131));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleG()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("gacute", Convert(0x01F5));
            symbols.Add("gamma", Convert(0x03B3));
            symbols.Add("gammad", Convert(0x03DD));
            symbols.Add("gap", Convert(0x2A86));
            symbols.Add("gbreve", Convert(0x011F));
            symbols.Add("gcirc", Convert(0x011D));
            symbols.Add("gcy", Convert(0x0433));
            symbols.Add("gdot", Convert(0x0121));
            symbols.Add("gE", Convert(0x2267));
            symbols.Add("ge", Convert(0x2265));
            symbols.Add("gEl", Convert(0x2A8C));
            symbols.Add("gel", Convert(0x22DB));
            symbols.Add("geq", Convert(0x2265));
            symbols.Add("geqq", Convert(0x2267));
            symbols.Add("geqslant", Convert(0x2A7E));
            symbols.Add("ges", Convert(0x2A7E));
            symbols.Add("gescc", Convert(0x2AA9));
            symbols.Add("gesdot", Convert(0x2A80));
            symbols.Add("gesdoto", Convert(0x2A82));
            symbols.Add("gesdotol", Convert(0x2A84));
            symbols.Add("gesl", Convert(0x22DB, 0xFE00));
            symbols.Add("gesles", Convert(0x2A94));
            symbols.Add("gfr", Convert(0x1D524));
            symbols.Add("gg", Convert(0x226B));
            symbols.Add("ggg", Convert(0x22D9));
            symbols.Add("gimel", Convert(0x2137));
            symbols.Add("gjcy", Convert(0x0453));
            symbols.Add("gl", Convert(0x2277));
            symbols.Add("gla", Convert(0x2AA5));
            symbols.Add("glE", Convert(0x2A92));
            symbols.Add("glj", Convert(0x2AA4));
            symbols.Add("gnap", Convert(0x2A8A));
            symbols.Add("gnapprox", Convert(0x2A8A));
            symbols.Add("gnE", Convert(0x2269));
            symbols.Add("gne", Convert(0x2A88));
            symbols.Add("gneq", Convert(0x2A88));
            symbols.Add("gneqq", Convert(0x2269));
            symbols.Add("gnsim", Convert(0x22E7));
            symbols.Add("gopf", Convert(0x1D558));
            symbols.Add("grave", Convert(0x0060));
            symbols.Add("gscr", Convert(0x210A));
            symbols.Add("gsim", Convert(0x2273));
            symbols.Add("gsime", Convert(0x2A8E));
            symbols.Add("gsiml", Convert(0x2A90));
            symbols.Add("gt", Convert(0x003E));
            symbols.Add("gtcc", Convert(0x2AA7));
            symbols.Add("gtcir", Convert(0x2A7A));
            symbols.Add("gtdot", Convert(0x22D7));
            symbols.Add("gtlPar", Convert(0x2995));
            symbols.Add("gtquest", Convert(0x2A7C));
            symbols.Add("gtrapprox", Convert(0x2A86));
            symbols.Add("gtrarr", Convert(0x2978));
            symbols.Add("gtrdot", Convert(0x22D7));
            symbols.Add("gtreqless", Convert(0x22DB));
            symbols.Add("gtreqqless", Convert(0x2A8C));
            symbols.Add("gtrless", Convert(0x2277));
            symbols.Add("gtrsim", Convert(0x2273));
            symbols.Add("gvertneqq", Convert(0x2269, 0xFE00));
            symbols.Add("gvnE", Convert(0x2269, 0xFE00));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigG()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Gamma", Convert(0x0393));
            symbols.Add("Gammad", Convert(0x03DC));
            symbols.Add("Gbreve", Convert(0x011E));
            symbols.Add("Gcedil", Convert(0x0122));
            symbols.Add("Gcirc", Convert(0x011C));
            symbols.Add("Gcy", Convert(0x0413));
            symbols.Add("Gdot", Convert(0x0120));
            symbols.Add("Gfr", Convert(0x1D50A));
            symbols.Add("Gg", Convert(0x22D9));
            symbols.Add("GJcy", Convert(0x0403));
            symbols.Add("Gopf", Convert(0x1D53E));
            symbols.Add("GreaterEqual", Convert(0x2265));
            symbols.Add("GreaterEqualLess", Convert(0x22DB));
            symbols.Add("GreaterFullEqual", Convert(0x2267));
            symbols.Add("GreaterGreater", Convert(0x2AA2));
            symbols.Add("GreaterLess", Convert(0x2277));
            symbols.Add("GreaterSlantEqual", Convert(0x2A7E));
            symbols.Add("GreaterTilde", Convert(0x2273));
            symbols.Add("Gscr", Convert(0x1D4A2));
            symbols.Add("GT", Convert(0x003E));
            symbols.Add("Gt", Convert(0x226B));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleH()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("hairsp", Convert(0x200A));
            symbols.Add("half", Convert(0x00BD));
            symbols.Add("hamilt", Convert(0x210B));
            symbols.Add("hardcy", Convert(0x044A));
            symbols.Add("hArr", Convert(0x21D4));
            symbols.Add("harr", Convert(0x2194));
            symbols.Add("harrcir", Convert(0x2948));
            symbols.Add("harrw", Convert(0x21AD));
            symbols.Add("hbar", Convert(0x210F));
            symbols.Add("hcirc", Convert(0x0125));
            symbols.Add("hearts", Convert(0x2665));
            symbols.Add("heartsuit", Convert(0x2665));
            symbols.Add("hellip", Convert(0x2026));
            symbols.Add("hercon", Convert(0x22B9));
            symbols.Add("hfr", Convert(0x1D525));
            symbols.Add("hksearow", Convert(0x2925));
            symbols.Add("hkswarow", Convert(0x2926));
            symbols.Add("hoarr", Convert(0x21FF));
            symbols.Add("homtht", Convert(0x223B));
            symbols.Add("hookleftarrow", Convert(0x21A9));
            symbols.Add("hookrightarrow", Convert(0x21AA));
            symbols.Add("hopf", Convert(0x1D559));
            symbols.Add("horbar", Convert(0x2015));
            symbols.Add("hscr", Convert(0x1D4BD));
            symbols.Add("hslash", Convert(0x210F));
            symbols.Add("hstrok", Convert(0x0127));
            symbols.Add("hybull", Convert(0x2043));
            symbols.Add("hyphen", Convert(0x2010));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigH()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Hacek", Convert(0x02C7));
            symbols.Add("HARDcy", Convert(0x042A));
            symbols.Add("Hat", Convert(0x005E));
            symbols.Add("Hcirc", Convert(0x0124));
            symbols.Add("Hfr", Convert(0x210C));
            symbols.Add("HilbertSpace", Convert(0x210B));
            symbols.Add("Hopf", Convert(0x210D));
            symbols.Add("HorizontalLine", Convert(0x2500));
            symbols.Add("Hscr", Convert(0x210B));
            symbols.Add("Hstrok", Convert(0x0126));
            symbols.Add("HumpDownHump", Convert(0x224E));
            symbols.Add("HumpEqual", Convert(0x224F));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleI()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("iacute", Convert(0x00ED));
            symbols.Add("ic", Convert(0x2063));
            symbols.Add("icirc", Convert(0x00EE));
            symbols.Add("icy", Convert(0x0438));
            symbols.Add("iecy", Convert(0x0435));
            symbols.Add("iexcl", Convert(0x00A1));
            symbols.Add("iff", Convert(0x21D4));
            symbols.Add("ifr", Convert(0x1D526));
            symbols.Add("igrave", Convert(0x00EC));
            symbols.Add("ii", Convert(0x2148));
            symbols.Add("iiiint", Convert(0x2A0C));
            symbols.Add("iiint", Convert(0x222D));
            symbols.Add("iinfin", Convert(0x29DC));
            symbols.Add("iiota", Convert(0x2129));
            symbols.Add("ijlig", Convert(0x0133));
            symbols.Add("imacr", Convert(0x012B));
            symbols.Add("image", Convert(0x2111));
            symbols.Add("imagline", Convert(0x2110));
            symbols.Add("imagpart", Convert(0x2111));
            symbols.Add("imath", Convert(0x0131));
            symbols.Add("imof", Convert(0x22B7));
            symbols.Add("imped", Convert(0x01B5));
            symbols.Add("in", Convert(0x2208));
            symbols.Add("incare", Convert(0x2105));
            symbols.Add("infin", Convert(0x221E));
            symbols.Add("infintie", Convert(0x29DD));
            symbols.Add("inodot", Convert(0x0131));
            symbols.Add("int", Convert(0x222B));
            symbols.Add("intcal", Convert(0x22BA));
            symbols.Add("integers", Convert(0x2124));
            symbols.Add("intercal", Convert(0x22BA));
            symbols.Add("intlarhk", Convert(0x2A17));
            symbols.Add("intprod", Convert(0x2A3C));
            symbols.Add("iocy", Convert(0x0451));
            symbols.Add("iogon", Convert(0x012F));
            symbols.Add("iopf", Convert(0x1D55A));
            symbols.Add("iota", Convert(0x03B9));
            symbols.Add("iprod", Convert(0x2A3C));
            symbols.Add("iquest", Convert(0x00BF));
            symbols.Add("iscr", Convert(0x1D4BE));
            symbols.Add("isin", Convert(0x2208));
            symbols.Add("isindot", Convert(0x22F5));
            symbols.Add("isinE", Convert(0x22F9));
            symbols.Add("isins", Convert(0x22F4));
            symbols.Add("isinsv", Convert(0x22F3));
            symbols.Add("isinv", Convert(0x2208));
            symbols.Add("it", Convert(0x2062));
            symbols.Add("itilde", Convert(0x0129));
            symbols.Add("iukcy", Convert(0x0456));
            symbols.Add("iuml", Convert(0x00EF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigI()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Iacute", Convert(0x00CD));
            symbols.Add("Icirc", Convert(0x00CE));
            symbols.Add("Icy", Convert(0x0418));
            symbols.Add("Idot", Convert(0x0130));
            symbols.Add("IEcy", Convert(0x0415));
            symbols.Add("Ifr", Convert(0x2111));
            symbols.Add("Igrave", Convert(0x00CC));
            symbols.Add("IJlig", Convert(0x0132));
            symbols.Add("Im", Convert(0x2111));
            symbols.Add("Imacr", Convert(0x012A));
            symbols.Add("ImaginaryI", Convert(0x2148));
            symbols.Add("Implies", Convert(0x21D2));
            symbols.Add("Int", Convert(0x222C));
            symbols.Add("Integral", Convert(0x222B));
            symbols.Add("Intersection", Convert(0x22C2));
            symbols.Add("InvisibleComma", Convert(0x2063));
            symbols.Add("InvisibleTimes", Convert(0x2062));
            symbols.Add("IOcy", Convert(0x0401));
            symbols.Add("Iogon", Convert(0x012E));
            symbols.Add("Iopf", Convert(0x1D540));
            symbols.Add("Iota", Convert(0x0399));
            symbols.Add("Iscr", Convert(0x2110));
            symbols.Add("Itilde", Convert(0x0128));
            symbols.Add("Iukcy", Convert(0x0406));
            symbols.Add("Iuml", Convert(0x00CF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleJ()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("jcirc", Convert(0x0135));
            symbols.Add("jcy", Convert(0x0439));
            symbols.Add("jfr", Convert(0x1D527));
            symbols.Add("jmath", Convert(0x0237));
            symbols.Add("jopf", Convert(0x1D55B));
            symbols.Add("jscr", Convert(0x1D4BF));
            symbols.Add("jsercy", Convert(0x0458));
            symbols.Add("jukcy", Convert(0x0454));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigJ()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Jcirc", Convert(0x0134));
            symbols.Add("Jcy", Convert(0x0419));
            symbols.Add("Jfr", Convert(0x1D50D));
            symbols.Add("Jopf", Convert(0x1D541));
            symbols.Add("Jscr", Convert(0x1D4A5));
            symbols.Add("Jsercy", Convert(0x0408));
            symbols.Add("Jukcy", Convert(0x0404));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleK()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("kappa", Convert(0x03BA));
            symbols.Add("kappav", Convert(0x03F0));
            symbols.Add("kcedil", Convert(0x0137));
            symbols.Add("kcy", Convert(0x043A));
            symbols.Add("kfr", Convert(0x1D528));
            symbols.Add("kgreen", Convert(0x0138));
            symbols.Add("khcy", Convert(0x0445));
            symbols.Add("kjcy", Convert(0x045C));
            symbols.Add("kopf", Convert(0x1D55C));
            symbols.Add("kscr", Convert(0x1D4C0));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigK()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Kappa", Convert(0x039A));
            symbols.Add("Kcedil", Convert(0x0136));
            symbols.Add("Kcy", Convert(0x041A));
            symbols.Add("Kfr", Convert(0x1D50E));
            symbols.Add("KHcy", Convert(0x0425));
            symbols.Add("KJcy", Convert(0x040C));
            symbols.Add("Kopf", Convert(0x1D542));
            symbols.Add("Kscr", Convert(0x1D4A6));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleL()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("lAarr", Convert(0x21DA));
            symbols.Add("lacute", Convert(0x013A));
            symbols.Add("laemptyv", Convert(0x29B4));
            symbols.Add("lagran", Convert(0x2112));
            symbols.Add("lambda", Convert(0x03BB));
            symbols.Add("lang", Convert(0x27E8));
            symbols.Add("langd", Convert(0x2991));
            symbols.Add("langle", Convert(0x27E8));
            symbols.Add("lap", Convert(0x2A85));
            symbols.Add("laquo", Convert(0x00AB));
            symbols.Add("lArr", Convert(0x21D0));
            symbols.Add("larr", Convert(0x2190));
            symbols.Add("larrb", Convert(0x21E4));
            symbols.Add("larrbfs", Convert(0x291F));
            symbols.Add("larrfs", Convert(0x291D));
            symbols.Add("larrhk", Convert(0x21A9));
            symbols.Add("larrlp", Convert(0x21AB));
            symbols.Add("larrpl", Convert(0x2939));
            symbols.Add("larrsim", Convert(0x2973));
            symbols.Add("larrtl", Convert(0x21A2));
            symbols.Add("lat", Convert(0x2AAB));
            symbols.Add("lAtail", Convert(0x291B));
            symbols.Add("latail", Convert(0x2919));
            symbols.Add("late", Convert(0x2AAD));
            symbols.Add("lates", Convert(0x2AAD, 0xFE00));
            symbols.Add("lBarr", Convert(0x290E));
            symbols.Add("lbarr", Convert(0x290C));
            symbols.Add("lbbrk", Convert(0x2772));
            symbols.Add("lbrace", Convert(0x007B));
            symbols.Add("lbrack", Convert(0x005B));
            symbols.Add("lbrke", Convert(0x298B));
            symbols.Add("lbrksld", Convert(0x298F));
            symbols.Add("lbrkslu", Convert(0x298D));
            symbols.Add("lcaron", Convert(0x013E));
            symbols.Add("lcedil", Convert(0x013C));
            symbols.Add("lceil", Convert(0x2308));
            symbols.Add("lcub", Convert(0x007B));
            symbols.Add("lcy", Convert(0x043B));
            symbols.Add("ldca", Convert(0x2936));
            symbols.Add("ldquo", Convert(0x201C));
            symbols.Add("ldquor", Convert(0x201E));
            symbols.Add("ldrdhar", Convert(0x2967));
            symbols.Add("ldrushar", Convert(0x294B));
            symbols.Add("ldsh", Convert(0x21B2));
            symbols.Add("lE", Convert(0x2266));
            symbols.Add("le", Convert(0x2264));
            symbols.Add("leftarrow", Convert(0x2190));
            symbols.Add("leftarrowtail", Convert(0x21A2));
            symbols.Add("leftharpoondown", Convert(0x21BD));
            symbols.Add("leftharpoonup", Convert(0x21BC));
            symbols.Add("leftleftarrows", Convert(0x21C7));
            symbols.Add("leftrightarrow", Convert(0x2194));
            symbols.Add("leftrightarrows", Convert(0x21C6));
            symbols.Add("leftrightharpoons", Convert(0x21CB));
            symbols.Add("leftrightsquigarrow", Convert(0x21AD));
            symbols.Add("leftthreetimes", Convert(0x22CB));
            symbols.Add("lEg", Convert(0x2A8B));
            symbols.Add("leg", Convert(0x22DA));
            symbols.Add("leq", Convert(0x2264));
            symbols.Add("leqq", Convert(0x2266));
            symbols.Add("leqslant", Convert(0x2A7D));
            symbols.Add("les", Convert(0x2A7D));
            symbols.Add("lescc", Convert(0x2AA8));
            symbols.Add("lesdot", Convert(0x2A7F));
            symbols.Add("lesdoto", Convert(0x2A81));
            symbols.Add("lesdotor", Convert(0x2A83));
            symbols.Add("lesg", Convert(0x22DA, 0xFE00));
            symbols.Add("lesges", Convert(0x2A93));
            symbols.Add("lessapprox", Convert(0x2A85));
            symbols.Add("lessdot", Convert(0x22D6));
            symbols.Add("lesseqgtr", Convert(0x22DA));
            symbols.Add("lesseqqgtr", Convert(0x2A8B));
            symbols.Add("lessgtr", Convert(0x2276));
            symbols.Add("lesssim", Convert(0x2272));
            symbols.Add("lfisht", Convert(0x297C));
            symbols.Add("lfloor", Convert(0x230A));
            symbols.Add("lfr", Convert(0x1D529));
            symbols.Add("lg", Convert(0x2276));
            symbols.Add("lgE", Convert(0x2A91));
            symbols.Add("lHar", Convert(0x2962));
            symbols.Add("lhard", Convert(0x21BD));
            symbols.Add("lharu", Convert(0x21BC));
            symbols.Add("lharul", Convert(0x296A));
            symbols.Add("lhblk", Convert(0x2584));
            symbols.Add("ljcy", Convert(0x0459));
            symbols.Add("ll", Convert(0x226A));
            symbols.Add("llarr", Convert(0x21C7));
            symbols.Add("llcorner", Convert(0x231E));
            symbols.Add("llhard", Convert(0x296B));
            symbols.Add("lltri", Convert(0x25FA));
            symbols.Add("lmidot", Convert(0x0140));
            symbols.Add("lmoust", Convert(0x23B0));
            symbols.Add("lmoustache", Convert(0x23B0));
            symbols.Add("lnap", Convert(0x2A89));
            symbols.Add("lnapprox", Convert(0x2A89));
            symbols.Add("lnE", Convert(0x2268));
            symbols.Add("lne", Convert(0x2A87));
            symbols.Add("lneq", Convert(0x2A87));
            symbols.Add("lneqq", Convert(0x2268));
            symbols.Add("lnsim", Convert(0x22E6));
            symbols.Add("loang", Convert(0x27EC));
            symbols.Add("loarr", Convert(0x21FD));
            symbols.Add("lobrk", Convert(0x27E6));
            symbols.Add("longleftarrow", Convert(0x27F5));
            symbols.Add("longleftrightarrow", Convert(0x27F7));
            symbols.Add("longmapsto", Convert(0x27FC));
            symbols.Add("longrightarrow", Convert(0x27F6));
            symbols.Add("looparrowleft", Convert(0x21AB));
            symbols.Add("looparrowright", Convert(0x21AC));
            symbols.Add("lopar", Convert(0x2985));
            symbols.Add("lopf", Convert(0x1D55D));
            symbols.Add("loplus", Convert(0x2A2D));
            symbols.Add("lotimes", Convert(0x2A34));
            symbols.Add("lowast", Convert(0x2217));
            symbols.Add("lowbar", Convert(0x005F));
            symbols.Add("loz", Convert(0x25CA));
            symbols.Add("lozenge", Convert(0x25CA));
            symbols.Add("lozf", Convert(0x29EB));
            symbols.Add("lpar", Convert(0x0028));
            symbols.Add("lparlt", Convert(0x2993));
            symbols.Add("lrarr", Convert(0x21C6));
            symbols.Add("lrcorner", Convert(0x231F));
            symbols.Add("lrhar", Convert(0x21CB));
            symbols.Add("lrhard", Convert(0x296D));
            symbols.Add("lrm", Convert(0x200E));
            symbols.Add("lrtri", Convert(0x22BF));
            symbols.Add("lsaquo", Convert(0x2039));
            symbols.Add("lscr", Convert(0x1D4C1));
            symbols.Add("lsh", Convert(0x21B0));
            symbols.Add("lsim", Convert(0x2272));
            symbols.Add("lsime", Convert(0x2A8D));
            symbols.Add("lsimg", Convert(0x2A8F));
            symbols.Add("lsqb", Convert(0x005B));
            symbols.Add("lsquo", Convert(0x2018));
            symbols.Add("lsquor", Convert(0x201A));
            symbols.Add("lstrok", Convert(0x0142));
            symbols.Add("lt", Convert(0x003C));
            symbols.Add("ltcc", Convert(0x2AA6));
            symbols.Add("ltcir", Convert(0x2A79));
            symbols.Add("ltdot", Convert(0x22D6));
            symbols.Add("lthree", Convert(0x22CB));
            symbols.Add("ltimes", Convert(0x22C9));
            symbols.Add("ltlarr", Convert(0x2976));
            symbols.Add("ltquest", Convert(0x2A7B));
            symbols.Add("ltri", Convert(0x25C3));
            symbols.Add("ltrie", Convert(0x22B4));
            symbols.Add("ltrif", Convert(0x25C2));
            symbols.Add("ltrPar", Convert(0x2996));
            symbols.Add("lurdshar", Convert(0x294A));
            symbols.Add("luruhar", Convert(0x2966));
            symbols.Add("lvertneqq", Convert(0x2268, 0xFE00));
            symbols.Add("lvnE", Convert(0x2268, 0xFE00));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigL()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Lacute", Convert(0x0139));
            symbols.Add("Lambda", Convert(0x039B));
            symbols.Add("Lang", Convert(0x27EA));
            symbols.Add("Laplacetrf", Convert(0x2112));
            symbols.Add("Larr", Convert(0x219E));
            symbols.Add("Lcaron", Convert(0x013D));
            symbols.Add("Lcedil", Convert(0x013B));
            symbols.Add("Lcy", Convert(0x041B));
            symbols.Add("LeftAngleBracket", Convert(0x27E8));
            symbols.Add("LeftArrow", Convert(0x2190));
            symbols.Add("Leftarrow", Convert(0x21D0));
            symbols.Add("LeftArrowBar", Convert(0x21E4));
            symbols.Add("LeftArrowRightArrow", Convert(0x21C6));
            symbols.Add("LeftCeiling", Convert(0x2308));
            symbols.Add("LeftDoubleBracket", Convert(0x27E6));
            symbols.Add("LeftDownTeeVector", Convert(0x2961));
            symbols.Add("LeftDownVector", Convert(0x21C3));
            symbols.Add("LeftDownVectorBar", Convert(0x2959));
            symbols.Add("LeftFloor", Convert(0x230A));
            symbols.Add("LeftRightArrow", Convert(0x2194));
            symbols.Add("Leftrightarrow", Convert(0x21D4));
            symbols.Add("LeftRightVector", Convert(0x294E));
            symbols.Add("LeftTee", Convert(0x22A3));
            symbols.Add("LeftTeeArrow", Convert(0x21A4));
            symbols.Add("LeftTeeVector", Convert(0x295A));
            symbols.Add("LeftTriangle", Convert(0x22B2));
            symbols.Add("LeftTriangleBar", Convert(0x29CF));
            symbols.Add("LeftTriangleEqual", Convert(0x22B4));
            symbols.Add("LeftUpDownVector", Convert(0x2951));
            symbols.Add("LeftUpTeeVector", Convert(0x2960));
            symbols.Add("LeftUpVector", Convert(0x21BF));
            symbols.Add("LeftUpVectorBar", Convert(0x2958));
            symbols.Add("LeftVector", Convert(0x21BC));
            symbols.Add("LeftVectorBar", Convert(0x2952));
            symbols.Add("LessEqualGreater", Convert(0x22DA));
            symbols.Add("LessFullEqual", Convert(0x2266));
            symbols.Add("LessGreater", Convert(0x2276));
            symbols.Add("LessLess", Convert(0x2AA1));
            symbols.Add("LessSlantEqual", Convert(0x2A7D));
            symbols.Add("LessTilde", Convert(0x2272));
            symbols.Add("Lfr", Convert(0x1D50F));
            symbols.Add("LJcy", Convert(0x0409));
            symbols.Add("Ll", Convert(0x22D8));
            symbols.Add("Lleftarrow", Convert(0x21DA));
            symbols.Add("Lmidot", Convert(0x013F));
            symbols.Add("LongLeftArrow", Convert(0x27F5));
            symbols.Add("Longleftarrow", Convert(0x27F8));
            symbols.Add("LongLeftRightArrow", Convert(0x27F7));
            symbols.Add("Longleftrightarrow", Convert(0x27FA));
            symbols.Add("LongRightArrow", Convert(0x27F6));
            symbols.Add("Longrightarrow", Convert(0x27F9));
            symbols.Add("Lopf", Convert(0x1D543));
            symbols.Add("LowerLeftArrow", Convert(0x2199));
            symbols.Add("LowerRightArrow", Convert(0x2198));
            symbols.Add("Lscr", Convert(0x2112));
            symbols.Add("Lsh", Convert(0x21B0));
            symbols.Add("Lstrok", Convert(0x0141));
            symbols.Add("LT", Convert(0x003C));
            symbols.Add("Lt", Convert(0x226A));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleM()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("macr", Convert(0x00AF));
            symbols.Add("male", Convert(0x2642));
            symbols.Add("malt", Convert(0x2720));
            symbols.Add("maltese", Convert(0x2720));
            symbols.Add("map", Convert(0x21A6));
            symbols.Add("mapsto", Convert(0x21A6));
            symbols.Add("mapstodown", Convert(0x21A7));
            symbols.Add("mapstoleft", Convert(0x21A4));
            symbols.Add("mapstoup", Convert(0x21A5));
            symbols.Add("marker", Convert(0x25AE));
            symbols.Add("mcomma", Convert(0x2A29));
            symbols.Add("mcy", Convert(0x043C));
            symbols.Add("mdash", Convert(0x2014));
            symbols.Add("mDDot", Convert(0x223A));
            symbols.Add("measuredangle", Convert(0x2221));
            symbols.Add("mfr", Convert(0x1D52A));
            symbols.Add("mho", Convert(0x2127));
            symbols.Add("micro", Convert(0x00B5));
            symbols.Add("mid", Convert(0x2223));
            symbols.Add("midast", Convert(0x002A));
            symbols.Add("midcir", Convert(0x2AF0));
            symbols.Add("middot", Convert(0x00B7));
            symbols.Add("minus", Convert(0x2212));
            symbols.Add("minusb", Convert(0x229F));
            symbols.Add("minusd", Convert(0x2238));
            symbols.Add("minusdu", Convert(0x2A2A));
            symbols.Add("mlcp", Convert(0x2ADB));
            symbols.Add("mldr", Convert(0x2026));
            symbols.Add("mnplus", Convert(0x2213));
            symbols.Add("models", Convert(0x22A7));
            symbols.Add("mopf", Convert(0x1D55E));
            symbols.Add("mp", Convert(0x2213));
            symbols.Add("mscr", Convert(0x1D4C2));
            symbols.Add("mstpos", Convert(0x223E));
            symbols.Add("mu", Convert(0x03BC));
            symbols.Add("multimap", Convert(0x22B8));
            symbols.Add("mumap", Convert(0x22B8));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigM()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Map", Convert(0x2905));
            symbols.Add("Mcy", Convert(0x041C));
            symbols.Add("MediumSpace", Convert(0x205F));
            symbols.Add("Mellintrf", Convert(0x2133));
            symbols.Add("Mfr", Convert(0x1D510));
            symbols.Add("MinusPlus", Convert(0x2213));
            symbols.Add("Mopf", Convert(0x1D544));
            symbols.Add("Mscr", Convert(0x2133));
            symbols.Add("Mu", Convert(0x039C));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleN()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("nabla", Convert(0x2207));
            symbols.Add("nacute", Convert(0x0144));
            symbols.Add("nang", Convert(0x2220, 0x20D2));
            symbols.Add("nap", Convert(0x2249));
            symbols.Add("napE", Convert(0x2A70, 0x0338));
            symbols.Add("napid", Convert(0x224B, 0x0338));
            symbols.Add("napos", Convert(0x0149));
            symbols.Add("napprox", Convert(0x2249));
            symbols.Add("natur", Convert(0x266E));
            symbols.Add("natural", Convert(0x266E));
            symbols.Add("naturals", Convert(0x2115));
            symbols.Add("nbsp", Convert(0x00A0));
            symbols.Add("nbump", Convert(0x224E, 0x0338));
            symbols.Add("nbumpe", Convert(0x224F, 0x0338));
            symbols.Add("ncap", Convert(0x2A43));
            symbols.Add("ncaron", Convert(0x0148));
            symbols.Add("ncedil", Convert(0x0146));
            symbols.Add("ncong", Convert(0x2247));
            symbols.Add("ncongdot", Convert(0x2A6D, 0x0338));
            symbols.Add("ncup", Convert(0x2A42));
            symbols.Add("ncy", Convert(0x043D));
            symbols.Add("ndash", Convert(0x2013));
            symbols.Add("ne", Convert(0x2260));
            symbols.Add("nearhk", Convert(0x2924));
            symbols.Add("neArr", Convert(0x21D7));
            symbols.Add("nearr", Convert(0x2197));
            symbols.Add("nearrow", Convert(0x2197));
            symbols.Add("nedot", Convert(0x2250, 0x0338));
            symbols.Add("nequiv", Convert(0x2262));
            symbols.Add("nesear", Convert(0x2928));
            symbols.Add("nesim", Convert(0x2242, 0x0338));
            symbols.Add("nexist", Convert(0x2204));
            symbols.Add("nexists", Convert(0x2204));
            symbols.Add("nfr", Convert(0x1D52B));
            symbols.Add("ngE", Convert(0x2267, 0x0338));
            symbols.Add("nge", Convert(0x2271));
            symbols.Add("ngeq", Convert(0x2271));
            symbols.Add("ngeqq", Convert(0x2267, 0x0338));
            symbols.Add("ngeqslant", Convert(0x2A7E, 0x0338));
            symbols.Add("nges", Convert(0x2A7E, 0x0338));
            symbols.Add("nGg", Convert(0x22D9, 0x0338));
            symbols.Add("ngsim", Convert(0x2275));
            symbols.Add("nGt", Convert(0x226B, 0x20D2));
            symbols.Add("ngt", Convert(0x226F));
            symbols.Add("ngtr", Convert(0x226F));
            symbols.Add("nGtv", Convert(0x226B, 0x0338));
            symbols.Add("nhArr", Convert(0x21CE));
            symbols.Add("nharr", Convert(0x21AE));
            symbols.Add("nhpar", Convert(0x2AF2));
            symbols.Add("ni", Convert(0x220B));
            symbols.Add("nis", Convert(0x22FC));
            symbols.Add("nisd", Convert(0x22FA));
            symbols.Add("niv", Convert(0x220B));
            symbols.Add("njcy", Convert(0x045A));
            symbols.Add("nlArr", Convert(0x21CD));
            symbols.Add("nlarr", Convert(0x219A));
            symbols.Add("nldr", Convert(0x2025));
            symbols.Add("nlE", Convert(0x2266, 0x0338));
            symbols.Add("nle", Convert(0x2270));
            symbols.Add("nLeftarrow", Convert(0x21CD));
            symbols.Add("nleftarrow", Convert(0x219A));
            symbols.Add("nLeftrightarrow", Convert(0x21CE));
            symbols.Add("nleftrightarrow", Convert(0x21AE));
            symbols.Add("nleq", Convert(0x2270));
            symbols.Add("nleqq", Convert(0x2266, 0x0338));
            symbols.Add("nleqslant", Convert(0x2A7D, 0x0338));
            symbols.Add("nles", Convert(0x2A7D, 0x0338));
            symbols.Add("nless", Convert(0x226E));
            symbols.Add("nLl", Convert(0x22D8, 0x0338));
            symbols.Add("nlsim", Convert(0x2274));
            symbols.Add("nLt", Convert(0x226A, 0x20D2));
            symbols.Add("nlt", Convert(0x226E));
            symbols.Add("nltri", Convert(0x22EA));
            symbols.Add("nltrie", Convert(0x22EC));
            symbols.Add("nLtv", Convert(0x226A, 0x0338));
            symbols.Add("nmid", Convert(0x2224));
            symbols.Add("nopf", Convert(0x1D55F));
            symbols.Add("not", Convert(0x00AC));
            symbols.Add("notin", Convert(0x2209));
            symbols.Add("notindot", Convert(0x22F5, 0x0338));
            symbols.Add("notinE", Convert(0x22F9, 0x0338));
            symbols.Add("notinva", Convert(0x2209));
            symbols.Add("notinvb", Convert(0x22F7));
            symbols.Add("notinvc", Convert(0x22F6));
            symbols.Add("notni", Convert(0x220C));
            symbols.Add("notniva", Convert(0x220C));
            symbols.Add("notnivb", Convert(0x22FE));
            symbols.Add("notnivc", Convert(0x22FD));
            symbols.Add("npar", Convert(0x2226));
            symbols.Add("nparallel", Convert(0x2226));
            symbols.Add("nparsl", Convert(0x2AFD, 0x20E5));
            symbols.Add("npart", Convert(0x2202, 0x0338));
            symbols.Add("npolint", Convert(0x2A14));
            symbols.Add("npr", Convert(0x2280));
            symbols.Add("nprcue", Convert(0x22E0));
            symbols.Add("npre", Convert(0x2AAF, 0x0338));
            symbols.Add("nprec", Convert(0x2280));
            symbols.Add("npreceq", Convert(0x2AAF, 0x0338));
            symbols.Add("nrArr", Convert(0x21CF));
            symbols.Add("nrarr", Convert(0x219B));
            symbols.Add("nrarrc", Convert(0x2933, 0x0338));
            symbols.Add("nrarrw", Convert(0x219D, 0x0338));
            symbols.Add("nRightarrow", Convert(0x21CF));
            symbols.Add("nrightarrow", Convert(0x219B));
            symbols.Add("nrtri", Convert(0x22EB));
            symbols.Add("nrtrie", Convert(0x22ED));
            symbols.Add("nsc", Convert(0x2281));
            symbols.Add("nsccue", Convert(0x22E1));
            symbols.Add("nsce", Convert(0x2AB0, 0x0338));
            symbols.Add("nscr", Convert(0x1D4C3));
            symbols.Add("nshortmid", Convert(0x2224));
            symbols.Add("nshortparallel", Convert(0x2226));
            symbols.Add("nsim", Convert(0x2241));
            symbols.Add("nsime", Convert(0x2244));
            symbols.Add("nsimeq", Convert(0x2244));
            symbols.Add("nsmid", Convert(0x2224));
            symbols.Add("nspar", Convert(0x2226));
            symbols.Add("nsqsube", Convert(0x22E2));
            symbols.Add("nsqsupe", Convert(0x22E3));
            symbols.Add("nsub", Convert(0x2284));
            symbols.Add("nsubE", Convert(0x2AC5, 0x0338));
            symbols.Add("nsube", Convert(0x2288));
            symbols.Add("nsubset", Convert(0x2282, 0x20D2));
            symbols.Add("nsubseteq", Convert(0x2288));
            symbols.Add("nsubseteqq", Convert(0x2AC5, 0x0338));
            symbols.Add("nsucc", Convert(0x2281));
            symbols.Add("nsucceq", Convert(0x2AB0, 0x0338));
            symbols.Add("nsup", Convert(0x2285));
            symbols.Add("nsupE", Convert(0x2AC6, 0x0338));
            symbols.Add("nsupe", Convert(0x2289));
            symbols.Add("nsupset", Convert(0x2283, 0x20D2));
            symbols.Add("nsupseteq", Convert(0x2289));
            symbols.Add("nsupseteqq", Convert(0x2AC6, 0x0338));
            symbols.Add("ntgl", Convert(0x2279));
            symbols.Add("ntilde", Convert(0x00F1));
            symbols.Add("ntlg", Convert(0x2278));
            symbols.Add("ntriangleleft", Convert(0x22EA));
            symbols.Add("ntrianglelefteq", Convert(0x22EC));
            symbols.Add("ntriangleright", Convert(0x22EB));
            symbols.Add("ntrianglerighteq", Convert(0x22ED));
            symbols.Add("nu", Convert(0x03BD));
            symbols.Add("num", Convert(0x0023));
            symbols.Add("numero", Convert(0x2116));
            symbols.Add("numsp", Convert(0x2007));
            symbols.Add("nvap", Convert(0x224D, 0x20D2));
            symbols.Add("nVDash", Convert(0x22AF));
            symbols.Add("nVdash", Convert(0x22AE));
            symbols.Add("nvDash", Convert(0x22AD));
            symbols.Add("nvdash", Convert(0x22AC));
            symbols.Add("nvge", Convert(0x2265, 0x20D2));
            symbols.Add("nvgt", Convert(0x003E, 0x20D2));
            symbols.Add("nvHarr", Convert(0x2904));
            symbols.Add("nvinfin", Convert(0x29DE));
            symbols.Add("nvlArr", Convert(0x2902));
            symbols.Add("nvle", Convert(0x2264, 0x20D2));
            symbols.Add("nvlt", Convert(0x003C, 0x20D2));
            symbols.Add("nvltrie", Convert(0x22B4, 0x20D2));
            symbols.Add("nvrArr", Convert(0x2903));
            symbols.Add("nvrtrie", Convert(0x22B5, 0x20D2));
            symbols.Add("nvsim", Convert(0x223C, 0x20D2));
            symbols.Add("nwarhk", Convert(0x2923));
            symbols.Add("nwArr", Convert(0x21D6));
            symbols.Add("nwarr", Convert(0x2196));
            symbols.Add("nwarrow", Convert(0x2196));
            symbols.Add("nwnear", Convert(0x2927));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigN()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Nacute", Convert(0x0143));
            symbols.Add("Ncaron", Convert(0x0147));
            symbols.Add("Ncedil", Convert(0x0145));
            symbols.Add("NegativeMediumSpace", Convert(0x200B));
            symbols.Add("NegativeThickSpace", Convert(0x200B));
            symbols.Add("NegativeThinSpace", Convert(0x200B));
            symbols.Add("NegativeVeryThinSpace", Convert(0x200B));
            symbols.Add("NestedGreaterGreater", Convert(0x226B));
            symbols.Add("NestedLessLess", Convert(0x226A));
            symbols.Add("Ncy", Convert(0x041D));
            symbols.Add("Nfr", Convert(0x1D511));
            symbols.Add("NoBreak", Convert(0x2060));
            symbols.Add("NonBreakingSpace", Convert(0x00A0));
            symbols.Add("Nopf", Convert(0x2115));
            symbols.Add("NewLine", Convert(0x000A));
            symbols.Add("NJcy", Convert(0x040A));
            symbols.Add("Not", Convert(0x2AEC));
            symbols.Add("NotCongruent", Convert(0x2262));
            symbols.Add("NotCupCap", Convert(0x226D));
            symbols.Add("NotDoubleVerticalBar", Convert(0x2226));
            symbols.Add("NotElement", Convert(0x2209));
            symbols.Add("NotEqual", Convert(0x2260));
            symbols.Add("NotEqualTilde", Convert(0x2242, 0x0338));
            symbols.Add("NotExists", Convert(0x2204));
            symbols.Add("NotGreater", Convert(0x226F));
            symbols.Add("NotGreaterEqual", Convert(0x2271));
            symbols.Add("NotGreaterFullEqual", Convert(0x2267, 0x0338));
            symbols.Add("NotGreaterGreater", Convert(0x226B, 0x0338));
            symbols.Add("NotGreaterLess", Convert(0x2279));
            symbols.Add("NotGreaterSlantEqual", Convert(0x2A7E, 0x0338));
            symbols.Add("NotGreaterTilde", Convert(0x2275));
            symbols.Add("NotHumpDownHump", Convert(0x224E, 0x0338));
            symbols.Add("NotHumpEqual", Convert(0x224F, 0x0338));
            symbols.Add("NotLeftTriangle", Convert(0x22EA));
            symbols.Add("NotLeftTriangleBar", Convert(0x29CF, 0x0338));
            symbols.Add("NotLeftTriangleEqual", Convert(0x22EC));
            symbols.Add("NotLess", Convert(0x226E));
            symbols.Add("NotLessEqual", Convert(0x2270));
            symbols.Add("NotLessGreater", Convert(0x2278));
            symbols.Add("NotLessLess", Convert(0x226A, 0x0338));
            symbols.Add("NotLessSlantEqual", Convert(0x2A7D, 0x0338));
            symbols.Add("NotLessTilde", Convert(0x2274));
            symbols.Add("NotNestedGreaterGreater", Convert(0x2AA2, 0x0338));
            symbols.Add("NotNestedLessLess", Convert(0x2AA1, 0x0338));
            symbols.Add("NotPrecedes", Convert(0x2280));
            symbols.Add("NotPrecedesEqual", Convert(0x2AAF, 0x0338));
            symbols.Add("NotPrecedesSlantEqual", Convert(0x22E0));
            symbols.Add("NotReverseElement", Convert(0x220C));
            symbols.Add("NotRightTriangle", Convert(0x22EB));
            symbols.Add("NotRightTriangleBar", Convert(0x29D0, 0x0338));
            symbols.Add("NotRightTriangleEqual", Convert(0x22ED));
            symbols.Add("NotSquareSubset", Convert(0x228F, 0x0338));
            symbols.Add("NotSquareSubsetEqual", Convert(0x22E2));
            symbols.Add("NotSquareSuperset", Convert(0x2290, 0x0338));
            symbols.Add("NotSquareSupersetEqual", Convert(0x22E3));
            symbols.Add("NotSubset", Convert(0x2282, 0x20D2));
            symbols.Add("NotSubsetEqual", Convert(0x2288));
            symbols.Add("NotSucceeds", Convert(0x2281));
            symbols.Add("NotSucceedsEqual", Convert(0x2AB0, 0x0338));
            symbols.Add("NotSucceedsSlantEqual", Convert(0x22E1));
            symbols.Add("NotSucceedsTilde", Convert(0x227F, 0x0338));
            symbols.Add("NotSuperset", Convert(0x2283, 0x20D2));
            symbols.Add("NotSupersetEqual", Convert(0x2289));
            symbols.Add("NotTilde", Convert(0x2241));
            symbols.Add("NotTildeEqual", Convert(0x2244));
            symbols.Add("NotTildeFullEqual", Convert(0x2247));
            symbols.Add("NotTildeTilde", Convert(0x2249));
            symbols.Add("NotVerticalBar", Convert(0x2224));
            symbols.Add("Nscr", Convert(0x1D4A9));
            symbols.Add("Ntilde", Convert(0x00D1));
            symbols.Add("Nu", Convert(0x039D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleO()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("oacute", Convert(0x00F3));
            symbols.Add("oast", Convert(0x229B));
            symbols.Add("ocir", Convert(0x229A));
            symbols.Add("ocirc", Convert(0x00F4));
            symbols.Add("ocy", Convert(0x043E));
            symbols.Add("odash", Convert(0x229D));
            symbols.Add("odblac", Convert(0x0151));
            symbols.Add("odiv", Convert(0x2A38));
            symbols.Add("odot", Convert(0x2299));
            symbols.Add("odsold", Convert(0x29BC));
            symbols.Add("oelig", Convert(0x0153));
            symbols.Add("ofcir", Convert(0x29BF));
            symbols.Add("ofr", Convert(0x1D52C));
            symbols.Add("ogon", Convert(0x02DB));
            symbols.Add("ograve", Convert(0x00F2));
            symbols.Add("ogt", Convert(0x29C1));
            symbols.Add("ohbar", Convert(0x29B5));
            symbols.Add("ohm", Convert(0x03A9));
            symbols.Add("oint", Convert(0x222E));
            symbols.Add("olarr", Convert(0x21BA));
            symbols.Add("olcir", Convert(0x29BE));
            symbols.Add("olcross", Convert(0x29BB));
            symbols.Add("oline", Convert(0x203E));
            symbols.Add("olt", Convert(0x29C0));
            symbols.Add("omacr", Convert(0x014D));
            symbols.Add("omega", Convert(0x03C9));
            symbols.Add("omicron", Convert(0x03BF));
            symbols.Add("omid", Convert(0x29B6));
            symbols.Add("ominus", Convert(0x2296));
            symbols.Add("oopf", Convert(0x1D560));
            symbols.Add("opar", Convert(0x29B7));
            symbols.Add("operp", Convert(0x29B9));
            symbols.Add("oplus", Convert(0x2295));
            symbols.Add("or", Convert(0x2228));
            symbols.Add("orarr", Convert(0x21BB));
            symbols.Add("ord", Convert(0x2A5D));
            symbols.Add("order", Convert(0x2134));
            symbols.Add("orderof", Convert(0x2134));
            symbols.Add("ordf", Convert(0x00AA));
            symbols.Add("ordm", Convert(0x00BA));
            symbols.Add("origof", Convert(0x22B6));
            symbols.Add("oror", Convert(0x2A56));
            symbols.Add("orslope", Convert(0x2A57));
            symbols.Add("orv", Convert(0x2A5B));
            symbols.Add("oS", Convert(0x24C8));
            symbols.Add("oscr", Convert(0x2134));
            symbols.Add("oslash", Convert(0x00F8));
            symbols.Add("osol", Convert(0x2298));
            symbols.Add("otilde", Convert(0x00F5));
            symbols.Add("otimes", Convert(0x2297));
            symbols.Add("otimesas", Convert(0x2A36));
            symbols.Add("ouml", Convert(0x00F6));
            symbols.Add("ovbar", Convert(0x233D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigO()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Oacute", Convert(0x00D3));
            symbols.Add("Ocirc", Convert(0x00D4));
            symbols.Add("Ocy", Convert(0x041E));
            symbols.Add("Odblac", Convert(0x0150));
            symbols.Add("OElig", Convert(0x0152));
            symbols.Add("Ofr", Convert(0x1D512));
            symbols.Add("Ograve", Convert(0x00D2));
            symbols.Add("Omacr", Convert(0x014C));
            symbols.Add("Omega", Convert(0x03A9));
            symbols.Add("Omicron", Convert(0x039F));
            symbols.Add("Oopf", Convert(0x1D546));
            symbols.Add("OpenCurlyDoubleQuote", Convert(0x201C));
            symbols.Add("OpenCurlyQuote", Convert(0x2018));
            symbols.Add("Or", Convert(0x2A54));
            symbols.Add("Oscr", Convert(0x1D4AA));
            symbols.Add("Oslash", Convert(0x00D8));
            symbols.Add("Otilde", Convert(0x00D5));
            symbols.Add("Otimes", Convert(0x2A37));
            symbols.Add("Ouml", Convert(0x00D6));
            symbols.Add("OverBar", Convert(0x203E));
            symbols.Add("OverBrace", Convert(0x23DE));
            symbols.Add("OverBracket", Convert(0x23B4));
            symbols.Add("OverParenthesis", Convert(0x23DC));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleP()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("pfr", Convert(0x1D52D));
            symbols.Add("par", Convert(0x2225));
            symbols.Add("para", Convert(0x00B6));
            symbols.Add("parallel", Convert(0x2225));
            symbols.Add("parsim", Convert(0x2AF3));
            symbols.Add("parsl", Convert(0x2AFD));
            symbols.Add("part", Convert(0x2202));
            symbols.Add("pcy", Convert(0x043F));
            symbols.Add("percnt", Convert(0x0025));
            symbols.Add("period", Convert(0x002E));
            symbols.Add("permil", Convert(0x2030));
            symbols.Add("perp", Convert(0x22A5));
            symbols.Add("pertenk", Convert(0x2031));
            symbols.Add("phi", Convert(0x03C6));
            symbols.Add("phiv", Convert(0x03D5));
            symbols.Add("phmmat", Convert(0x2133));
            symbols.Add("phone", Convert(0x260E));
            symbols.Add("pi", Convert(0x03C0));
            symbols.Add("pitchfork", Convert(0x22D4));
            symbols.Add("piv", Convert(0x03D6));
            symbols.Add("planck", Convert(0x210F));
            symbols.Add("planckh", Convert(0x210E));
            symbols.Add("plankv", Convert(0x210F));
            symbols.Add("plus", Convert(0x002B));
            symbols.Add("plusacir", Convert(0x2A23));
            symbols.Add("plusb", Convert(0x229E));
            symbols.Add("pluscir", Convert(0x2A22));
            symbols.Add("plusdo", Convert(0x2214));
            symbols.Add("plusdu", Convert(0x2A25));
            symbols.Add("pluse", Convert(0x2A72));
            symbols.Add("plusmn", Convert(0x00B1));
            symbols.Add("plussim", Convert(0x2A26));
            symbols.Add("plustwo", Convert(0x2A27));
            symbols.Add("pm", Convert(0x00B1));
            symbols.Add("pointint", Convert(0x2A15));
            symbols.Add("popf", Convert(0x1D561));
            symbols.Add("pound", Convert(0x00A3));
            symbols.Add("pr", Convert(0x227A));
            symbols.Add("prap", Convert(0x2AB7));
            symbols.Add("prcue", Convert(0x227C));
            symbols.Add("prE", Convert(0x2AB3));
            symbols.Add("pre", Convert(0x2AAF));
            symbols.Add("prec", Convert(0x227A));
            symbols.Add("precapprox", Convert(0x2AB7));
            symbols.Add("preccurlyeq", Convert(0x227C));
            symbols.Add("preceq", Convert(0x2AAF));
            symbols.Add("precnapprox", Convert(0x2AB9));
            symbols.Add("precneqq", Convert(0x2AB5));
            symbols.Add("precnsim", Convert(0x22E8));
            symbols.Add("precsim", Convert(0x227E));
            symbols.Add("prime", Convert(0x2032));
            symbols.Add("primes", Convert(0x2119));
            symbols.Add("prnap", Convert(0x2AB9));
            symbols.Add("prnE", Convert(0x2AB5));
            symbols.Add("prnsim", Convert(0x22E8));
            symbols.Add("prod", Convert(0x220F));
            symbols.Add("profalar", Convert(0x232E));
            symbols.Add("profline", Convert(0x2312));
            symbols.Add("profsurf", Convert(0x2313));
            symbols.Add("prop", Convert(0x221D));
            symbols.Add("propto", Convert(0x221D));
            symbols.Add("prsim", Convert(0x227E));
            symbols.Add("prurel", Convert(0x22B0));
            symbols.Add("pscr", Convert(0x1D4C5));
            symbols.Add("psi", Convert(0x03C8));
            symbols.Add("puncsp", Convert(0x2008));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigP()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("PartialD", Convert(0x2202));
            symbols.Add("Pcy", Convert(0x041F));
            symbols.Add("Pfr", Convert(0x1D513));
            symbols.Add("Phi", Convert(0x03A6));
            symbols.Add("Pi", Convert(0x03A0));
            symbols.Add("PlusMinus", Convert(0x00B1));
            symbols.Add("Poincareplane", Convert(0x210C));
            symbols.Add("Popf", Convert(0x2119));
            symbols.Add("Pr", Convert(0x2ABB));
            symbols.Add("Precedes", Convert(0x227A));
            symbols.Add("PrecedesEqual", Convert(0x2AAF));
            symbols.Add("PrecedesSlantEqual", Convert(0x227C));
            symbols.Add("PrecedesTilde", Convert(0x227E));
            symbols.Add("Prime", Convert(0x2033));
            symbols.Add("Product", Convert(0x220F));
            symbols.Add("Proportion", Convert(0x2237));
            symbols.Add("Proportional", Convert(0x221D));
            symbols.Add("Pscr", Convert(0x1D4AB));
            symbols.Add("Psi", Convert(0x03A8));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleQ()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("qfr", Convert(0x1D52E));
            symbols.Add("qint", Convert(0x2A0C));
            symbols.Add("qopf", Convert(0x1D562));
            symbols.Add("qprime", Convert(0x2057));
            symbols.Add("qscr", Convert(0x1D4C6));
            symbols.Add("quaternions", Convert(0x210D));
            symbols.Add("quatint", Convert(0x2A16));
            symbols.Add("quest", Convert(0x003F));
            symbols.Add("questeq", Convert(0x225F));
            symbols.Add("quot", Convert(0x0022));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigQ()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Qfr", Convert(0x1D514));
            symbols.Add("Qopf", Convert(0x211A));
            symbols.Add("Qscr", Convert(0x1D4AC));
            symbols.Add("QUOT", Convert(0x0022));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleR()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("rAarr", Convert(0x21DB));
            symbols.Add("race", Convert(0x223D, 0x0331));
            symbols.Add("racute", Convert(0x0155));
            symbols.Add("radic", Convert(0x221A));
            symbols.Add("raemptyv", Convert(0x29B3));
            symbols.Add("rang", Convert(0x27E9));
            symbols.Add("rangd", Convert(0x2992));
            symbols.Add("range", Convert(0x29A5));
            symbols.Add("rangle", Convert(0x27E9));
            symbols.Add("raquo", Convert(0x00BB));
            symbols.Add("rArr", Convert(0x21D2));
            symbols.Add("rarr", Convert(0x2192));
            symbols.Add("rarrap", Convert(0x2975));
            symbols.Add("rarrb", Convert(0x21E5));
            symbols.Add("rarrbfs", Convert(0x2920));
            symbols.Add("rarrc", Convert(0x2933));
            symbols.Add("rarrfs", Convert(0x291E));
            symbols.Add("rarrhk", Convert(0x21AA));
            symbols.Add("rarrlp", Convert(0x21AC));
            symbols.Add("rarrpl", Convert(0x2945));
            symbols.Add("rarrsim", Convert(0x2974));
            symbols.Add("rarrtl", Convert(0x21A3));
            symbols.Add("rarrw", Convert(0x219D));
            symbols.Add("rAtail", Convert(0x291C));
            symbols.Add("ratail", Convert(0x291A));
            symbols.Add("ratio", Convert(0x2236));
            symbols.Add("rationals", Convert(0x211A));
            symbols.Add("rBarr", Convert(0x290F));
            symbols.Add("rbarr", Convert(0x290D));
            symbols.Add("rbbrk", Convert(0x2773));
            symbols.Add("rbrace", Convert(0x007D));
            symbols.Add("rbrack", Convert(0x005D));
            symbols.Add("rbrke", Convert(0x298C));
            symbols.Add("rbrksld", Convert(0x298E));
            symbols.Add("rbrkslu", Convert(0x2990));
            symbols.Add("rcaron", Convert(0x0159));
            symbols.Add("rcedil", Convert(0x0157));
            symbols.Add("rceil", Convert(0x2309));
            symbols.Add("rcub", Convert(0x007D));
            symbols.Add("rcy", Convert(0x0440));
            symbols.Add("rdca", Convert(0x2937));
            symbols.Add("rdldhar", Convert(0x2969));
            symbols.Add("rdquo", Convert(0x201D));
            symbols.Add("rdquor", Convert(0x201D));
            symbols.Add("rdsh", Convert(0x21B3));
            symbols.Add("real", Convert(0x211C));
            symbols.Add("realine", Convert(0x211B));
            symbols.Add("realpart", Convert(0x211C));
            symbols.Add("reals", Convert(0x211D));
            symbols.Add("rect", Convert(0x25AD));
            symbols.Add("reg", Convert(0x00AE));
            symbols.Add("rfisht", Convert(0x297D));
            symbols.Add("rfloor", Convert(0x230B));
            symbols.Add("rfr", Convert(0x1D52F));
            symbols.Add("rHar", Convert(0x2964));
            symbols.Add("rhard", Convert(0x21C1));
            symbols.Add("rharu", Convert(0x21C0));
            symbols.Add("rharul", Convert(0x296C));
            symbols.Add("rho", Convert(0x03C1));
            symbols.Add("rhov", Convert(0x03F1));
            symbols.Add("rightarrow", Convert(0x2192));
            symbols.Add("rightarrowtail", Convert(0x21A3));
            symbols.Add("rightharpoondown", Convert(0x21C1));
            symbols.Add("rightharpoonup", Convert(0x21C0));
            symbols.Add("rightleftarrows", Convert(0x21C4));
            symbols.Add("rightleftharpoons", Convert(0x21CC));
            symbols.Add("rightrightarrows", Convert(0x21C9));
            symbols.Add("rightsquigarrow", Convert(0x219D));
            symbols.Add("rightthreetimes", Convert(0x22CC));
            symbols.Add("ring", Convert(0x02DA));
            symbols.Add("risingdotseq", Convert(0x2253));
            symbols.Add("rlarr", Convert(0x21C4));
            symbols.Add("rlhar", Convert(0x21CC));
            symbols.Add("rlm", Convert(0x200F));
            symbols.Add("rmoust", Convert(0x23B1));
            symbols.Add("rmoustache", Convert(0x23B1));
            symbols.Add("rnmid", Convert(0x2AEE));
            symbols.Add("roang", Convert(0x27ED));
            symbols.Add("roarr", Convert(0x21FE));
            symbols.Add("robrk", Convert(0x27E7));
            symbols.Add("ropar", Convert(0x2986));
            symbols.Add("ropf", Convert(0x1D563));
            symbols.Add("roplus", Convert(0x2A2E));
            symbols.Add("rotimes", Convert(0x2A35));
            symbols.Add("rpar", Convert(0x0029));
            symbols.Add("rpargt", Convert(0x2994));
            symbols.Add("rppolint", Convert(0x2A12));
            symbols.Add("rrarr", Convert(0x21C9));
            symbols.Add("rsaquo", Convert(0x203A));
            symbols.Add("rscr", Convert(0x1D4C7));
            symbols.Add("rsh", Convert(0x21B1));
            symbols.Add("rsqb", Convert(0x005D));
            symbols.Add("rsquo", Convert(0x2019));
            symbols.Add("rsquor", Convert(0x2019));
            symbols.Add("rthree", Convert(0x22CC));
            symbols.Add("rtimes", Convert(0x22CA));
            symbols.Add("rtri", Convert(0x25B9));
            symbols.Add("rtrie", Convert(0x22B5));
            symbols.Add("rtrif", Convert(0x25B8));
            symbols.Add("rtriltri", Convert(0x29CE));
            symbols.Add("ruluhar", Convert(0x2968));
            symbols.Add("rx", Convert(0x211E));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigR()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Racute", Convert(0x0154));
            symbols.Add("Rang", Convert(0x27EB));
            symbols.Add("Rarr", Convert(0x21A0));
            symbols.Add("Rarrtl", Convert(0x2916));
            symbols.Add("RBarr", Convert(0x2910));
            symbols.Add("Rcaron", Convert(0x0158));
            symbols.Add("Rcedil", Convert(0x0156));
            symbols.Add("Rcy", Convert(0x0420));
            symbols.Add("Re", Convert(0x211C));
            symbols.Add("REG", Convert(0x00AE));
            symbols.Add("ReverseElement", Convert(0x220B));
            symbols.Add("ReverseEquilibrium", Convert(0x21CB));
            symbols.Add("ReverseUpEquilibrium", Convert(0x296F));
            symbols.Add("Rfr", Convert(0x211C));
            symbols.Add("Rho", Convert(0x03A1));
            symbols.Add("RightAngleBracket", Convert(0x27E9));
            symbols.Add("RightArrow", Convert(0x2192));
            symbols.Add("Rightarrow", Convert(0x21D2));
            symbols.Add("RightArrowBar", Convert(0x21E5));
            symbols.Add("RightArrowLeftArrow", Convert(0x21C4));
            symbols.Add("RightCeiling", Convert(0x2309));
            symbols.Add("RightDoubleBracket", Convert(0x27E7));
            symbols.Add("RightDownTeeVector", Convert(0x295D));
            symbols.Add("RightDownVector", Convert(0x21C2));
            symbols.Add("RightDownVectorBar", Convert(0x2955));
            symbols.Add("RightFloor", Convert(0x230B));
            symbols.Add("RightTee", Convert(0x22A2));
            symbols.Add("RightTeeArrow", Convert(0x21A6));
            symbols.Add("RightTeeVector", Convert(0x295B));
            symbols.Add("RightTriangle", Convert(0x22B3));
            symbols.Add("RightTriangleBar", Convert(0x29D0));
            symbols.Add("RightTriangleEqual", Convert(0x22B5));
            symbols.Add("RightUpDownVector", Convert(0x294F));
            symbols.Add("RightUpTeeVector", Convert(0x295C));
            symbols.Add("RightUpVector", Convert(0x21BE));
            symbols.Add("RightUpVectorBar", Convert(0x2954));
            symbols.Add("RightVector", Convert(0x21C0));
            symbols.Add("RightVectorBar", Convert(0x2953));
            symbols.Add("Ropf", Convert(0x211D));
            symbols.Add("RoundImplies", Convert(0x2970));
            symbols.Add("Rrightarrow", Convert(0x21DB));
            symbols.Add("Rscr", Convert(0x211B));
            symbols.Add("Rsh", Convert(0x21B1));
            symbols.Add("RuleDelayed", Convert(0x29F4));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleS()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("sacute", Convert(0x015B));
            symbols.Add("sbquo", Convert(0x201A));
            symbols.Add("sc", Convert(0x227B));
            symbols.Add("scap", Convert(0x2AB8));
            symbols.Add("scaron", Convert(0x0161));
            symbols.Add("sccue", Convert(0x227D));
            symbols.Add("scE", Convert(0x2AB4));
            symbols.Add("sce", Convert(0x2AB0));
            symbols.Add("scedil", Convert(0x015F));
            symbols.Add("scirc", Convert(0x015D));
            symbols.Add("scnap", Convert(0x2ABA));
            symbols.Add("scnE", Convert(0x2AB6));
            symbols.Add("scnsim", Convert(0x22E9));
            symbols.Add("scpolint", Convert(0x2A13));
            symbols.Add("scsim", Convert(0x227F));
            symbols.Add("scy", Convert(0x0441));
            symbols.Add("sdot", Convert(0x22C5));
            symbols.Add("sdotb", Convert(0x22A1));
            symbols.Add("sdote", Convert(0x2A66));
            symbols.Add("searhk", Convert(0x2925));
            symbols.Add("seArr", Convert(0x21D8));
            symbols.Add("searr", Convert(0x2198));
            symbols.Add("searrow", Convert(0x2198));
            symbols.Add("sect", Convert(0x00A7));
            symbols.Add("semi", Convert(0x003B));
            symbols.Add("seswar", Convert(0x2929));
            symbols.Add("setminus", Convert(0x2216));
            symbols.Add("setmn", Convert(0x2216));
            symbols.Add("sext", Convert(0x2736));
            symbols.Add("sfr", Convert(0x1D530));
            symbols.Add("sfrown", Convert(0x2322));
            symbols.Add("sharp", Convert(0x266F));
            symbols.Add("shchcy", Convert(0x0449));
            symbols.Add("shcy", Convert(0x0448));
            symbols.Add("shortmid", Convert(0x2223));
            symbols.Add("shortparallel", Convert(0x2225));
            symbols.Add("shy", Convert(0x00AD));
            symbols.Add("sigma", Convert(0x03C3));
            symbols.Add("sigmaf", Convert(0x03C2));
            symbols.Add("sigmav", Convert(0x03C2));
            symbols.Add("sim", Convert(0x223C));
            symbols.Add("simdot", Convert(0x2A6A));
            symbols.Add("sime", Convert(0x2243));
            symbols.Add("simeq", Convert(0x2243));
            symbols.Add("simg", Convert(0x2A9E));
            symbols.Add("simgE", Convert(0x2AA0));
            symbols.Add("siml", Convert(0x2A9D));
            symbols.Add("simlE", Convert(0x2A9F));
            symbols.Add("simne", Convert(0x2246));
            symbols.Add("simplus", Convert(0x2A24));
            symbols.Add("simrarr", Convert(0x2972));
            symbols.Add("slarr", Convert(0x2190));
            symbols.Add("smallsetminus", Convert(0x2216));
            symbols.Add("smashp", Convert(0x2A33));
            symbols.Add("smeparsl", Convert(0x29E4));
            symbols.Add("smid", Convert(0x2223));
            symbols.Add("smile", Convert(0x2323));
            symbols.Add("smt", Convert(0x2AAA));
            symbols.Add("smte", Convert(0x2AAC));
            symbols.Add("smtes", Convert(0x2AAC, 0xFE00));
            symbols.Add("softcy", Convert(0x044C));
            symbols.Add("sol", Convert(0x002F));
            symbols.Add("solb", Convert(0x29C4));
            symbols.Add("solbar", Convert(0x233F));
            symbols.Add("sopf", Convert(0x1D564));
            symbols.Add("spades", Convert(0x2660));
            symbols.Add("spadesuit", Convert(0x2660));
            symbols.Add("spar", Convert(0x2225));
            symbols.Add("sqcap", Convert(0x2293));
            symbols.Add("sqcaps", Convert(0x2293, 0xFE00));
            symbols.Add("sqcup", Convert(0x2294));
            symbols.Add("sqcups", Convert(0x2294, 0xFE00));
            symbols.Add("sqsub", Convert(0x228F));
            symbols.Add("sqsube", Convert(0x2291));
            symbols.Add("sqsubset", Convert(0x228F));
            symbols.Add("sqsubseteq", Convert(0x2291));
            symbols.Add("sqsup", Convert(0x2290));
            symbols.Add("sqsupe", Convert(0x2292));
            symbols.Add("sqsupset", Convert(0x2290));
            symbols.Add("sqsupseteq", Convert(0x2292));
            symbols.Add("squ", Convert(0x25A1));
            symbols.Add("square", Convert(0x25A1));
            symbols.Add("squarf", Convert(0x25AA));
            symbols.Add("squf", Convert(0x25AA));
            symbols.Add("srarr", Convert(0x2192));
            symbols.Add("sscr", Convert(0x1D4C8));
            symbols.Add("ssetmn", Convert(0x2216));
            symbols.Add("ssmile", Convert(0x2323));
            symbols.Add("sstarf", Convert(0x22C6));
            symbols.Add("star", Convert(0x2606));
            symbols.Add("starf", Convert(0x2605));
            symbols.Add("straightepsilon", Convert(0x03F5));
            symbols.Add("straightphi", Convert(0x03D5));
            symbols.Add("strns", Convert(0x00AF));
            symbols.Add("sub", Convert(0x2282));
            symbols.Add("subdot", Convert(0x2ABD));
            symbols.Add("subE", Convert(0x2AC5));
            symbols.Add("sube", Convert(0x2286));
            symbols.Add("subedot", Convert(0x2AC3));
            symbols.Add("submult", Convert(0x2AC1));
            symbols.Add("subnE", Convert(0x2ACB));
            symbols.Add("subne", Convert(0x228A));
            symbols.Add("subplus", Convert(0x2ABF));
            symbols.Add("subrarr", Convert(0x2979));
            symbols.Add("subset", Convert(0x2282));
            symbols.Add("subseteq", Convert(0x2286));
            symbols.Add("subseteqq", Convert(0x2AC5));
            symbols.Add("subsetneq", Convert(0x228A));
            symbols.Add("subsetneqq", Convert(0x2ACB));
            symbols.Add("subsim", Convert(0x2AC7));
            symbols.Add("subsub", Convert(0x2AD5));
            symbols.Add("subsup", Convert(0x2AD3));
            symbols.Add("succ", Convert(0x227B));
            symbols.Add("succapprox", Convert(0x2AB8));
            symbols.Add("succcurlyeq", Convert(0x227D));
            symbols.Add("succeq", Convert(0x2AB0));
            symbols.Add("succnapprox", Convert(0x2ABA));
            symbols.Add("succneqq", Convert(0x2AB6));
            symbols.Add("succnsim", Convert(0x22E9));
            symbols.Add("succsim", Convert(0x227F));
            symbols.Add("sum", Convert(0x2211));
            symbols.Add("sung", Convert(0x266A));
            symbols.Add("sup", Convert(0x2283));
            symbols.Add("sup1", Convert(0x00B9));
            symbols.Add("sup2", Convert(0x00B2));
            symbols.Add("sup3", Convert(0x00B3));
            symbols.Add("supdot", Convert(0x2ABE));
            symbols.Add("supdsub", Convert(0x2AD8));
            symbols.Add("supE", Convert(0x2AC6));
            symbols.Add("supe", Convert(0x2287));
            symbols.Add("supedot", Convert(0x2AC4));
            symbols.Add("suphsol", Convert(0x27C9));
            symbols.Add("suphsub", Convert(0x2AD7));
            symbols.Add("suplarr", Convert(0x297B));
            symbols.Add("supmult", Convert(0x2AC2));
            symbols.Add("supnE", Convert(0x2ACC));
            symbols.Add("supne", Convert(0x228B));
            symbols.Add("supplus", Convert(0x2AC0));
            symbols.Add("supset", Convert(0x2283));
            symbols.Add("supseteq", Convert(0x2287));
            symbols.Add("supseteqq", Convert(0x2AC6));
            symbols.Add("supsetneq", Convert(0x228B));
            symbols.Add("supsetneqq", Convert(0x2ACC));
            symbols.Add("supsim", Convert(0x2AC8));
            symbols.Add("supsub", Convert(0x2AD4));
            symbols.Add("supsup", Convert(0x2AD6));
            symbols.Add("swarhk", Convert(0x2926));
            symbols.Add("swArr", Convert(0x21D9));
            symbols.Add("swarr", Convert(0x2199));
            symbols.Add("swarrow", Convert(0x2199));
            symbols.Add("swnwar", Convert(0x292A));
            symbols.Add("szlig", Convert(0x00DF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigS()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Sacute", Convert(0x015A));
            symbols.Add("Sc", Convert(0x2ABC));
            symbols.Add("Scaron", Convert(0x0160));
            symbols.Add("Scedil", Convert(0x015E));
            symbols.Add("Scirc", Convert(0x015C));
            symbols.Add("Scy", Convert(0x0421));
            symbols.Add("Sfr", Convert(0x1D516));
            symbols.Add("SHCHcy", Convert(0x0429));
            symbols.Add("SHcy", Convert(0x0428));
            symbols.Add("ShortDownArrow", Convert(0x2193));
            symbols.Add("ShortLeftArrow", Convert(0x2190));
            symbols.Add("ShortRightArrow", Convert(0x2192));
            symbols.Add("ShortUpArrow", Convert(0x2191));
            symbols.Add("Sigma", Convert(0x03A3));
            symbols.Add("SmallCircle", Convert(0x2218));
            symbols.Add("SOFTcy", Convert(0x042C));
            symbols.Add("Sopf", Convert(0x1D54A));
            symbols.Add("Sqrt", Convert(0x221A));
            symbols.Add("Square", Convert(0x25A1));
            symbols.Add("SquareIntersection", Convert(0x2293));
            symbols.Add("SquareSubset", Convert(0x228F));
            symbols.Add("SquareSubsetEqual", Convert(0x2291));
            symbols.Add("SquareSuperset", Convert(0x2290));
            symbols.Add("SquareSupersetEqual", Convert(0x2292));
            symbols.Add("SquareUnion", Convert(0x2294));
            symbols.Add("Sscr", Convert(0x1D4AE));
            symbols.Add("Star", Convert(0x22C6));
            symbols.Add("Sub", Convert(0x22D0));
            symbols.Add("Subset", Convert(0x22D0));
            symbols.Add("SubsetEqual", Convert(0x2286));
            symbols.Add("Succeeds", Convert(0x227B));
            symbols.Add("SucceedsEqual", Convert(0x2AB0));
            symbols.Add("SucceedsSlantEqual", Convert(0x227D));
            symbols.Add("SucceedsTilde", Convert(0x227F));
            symbols.Add("SuchThat", Convert(0x220B));
            symbols.Add("Sum", Convert(0x2211));
            symbols.Add("Sup", Convert(0x22D1));
            symbols.Add("Superset", Convert(0x2283));
            symbols.Add("SupersetEqual", Convert(0x2287));
            symbols.Add("Supset", Convert(0x22D1));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleT()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("target", Convert(0x2316));
            symbols.Add("tau", Convert(0x03C4));
            symbols.Add("tbrk", Convert(0x23B4));
            symbols.Add("tcaron", Convert(0x0165));
            symbols.Add("tcedil", Convert(0x0163));
            symbols.Add("tcy", Convert(0x0442));
            symbols.Add("tdot", Convert(0x20DB));
            symbols.Add("telrec", Convert(0x2315));
            symbols.Add("tfr", Convert(0x1D531));
            symbols.Add("there4", Convert(0x2234));
            symbols.Add("therefore", Convert(0x2234));
            symbols.Add("theta", Convert(0x03B8));
            symbols.Add("thetasym", Convert(0x03D1));
            symbols.Add("thetav", Convert(0x03D1));
            symbols.Add("thickapprox", Convert(0x2248));
            symbols.Add("thicksim", Convert(0x223C));
            symbols.Add("thinsp", Convert(0x2009));
            symbols.Add("thkap", Convert(0x2248));
            symbols.Add("thksim", Convert(0x223C));
            symbols.Add("thorn", Convert(0x00FE));
            symbols.Add("tilde", Convert(0x02DC));
            symbols.Add("times", Convert(0x00D7));
            symbols.Add("timesb", Convert(0x22A0));
            symbols.Add("timesbar", Convert(0x2A31));
            symbols.Add("timesd", Convert(0x2A30));
            symbols.Add("tint", Convert(0x222D));
            symbols.Add("toea", Convert(0x2928));
            symbols.Add("top", Convert(0x22A4));
            symbols.Add("topbot", Convert(0x2336));
            symbols.Add("topcir", Convert(0x2AF1));
            symbols.Add("topf", Convert(0x1D565));
            symbols.Add("topfork", Convert(0x2ADA));
            symbols.Add("tosa", Convert(0x2929));
            symbols.Add("tprime", Convert(0x2034));
            symbols.Add("trade", Convert(0x2122));
            symbols.Add("triangle", Convert(0x25B5));
            symbols.Add("triangledown", Convert(0x25BF));
            symbols.Add("triangleleft", Convert(0x25C3));
            symbols.Add("trianglelefteq", Convert(0x22B4));
            symbols.Add("triangleq", Convert(0x225C));
            symbols.Add("triangleright", Convert(0x25B9));
            symbols.Add("trianglerighteq", Convert(0x22B5));
            symbols.Add("tridot", Convert(0x25EC));
            symbols.Add("trie", Convert(0x225C));
            symbols.Add("triminus", Convert(0x2A3A));
            symbols.Add("triplus", Convert(0x2A39));
            symbols.Add("trisb", Convert(0x29CD));
            symbols.Add("tritime", Convert(0x2A3B));
            symbols.Add("trpezium", Convert(0x23E2));
            symbols.Add("tscr", Convert(0x1D4C9));
            symbols.Add("tscy", Convert(0x0446));
            symbols.Add("tshcy", Convert(0x045B));
            symbols.Add("tstrok", Convert(0x0167));
            symbols.Add("twixt", Convert(0x226C));
            symbols.Add("twoheadleftarrow", Convert(0x219E));
            symbols.Add("twoheadrightarrow", Convert(0x21A0));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigT()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Tab", Convert(0x0009));
            symbols.Add("Tau", Convert(0x03A4));
            symbols.Add("Tcaron", Convert(0x0164));
            symbols.Add("Tcedil", Convert(0x0162));
            symbols.Add("Tcy", Convert(0x0422));
            symbols.Add("Tfr", Convert(0x1D517));
            symbols.Add("Therefore", Convert(0x2234));
            symbols.Add("Theta", Convert(0x0398));
            symbols.Add("ThickSpace", Convert(0x205F, 0x200A));
            symbols.Add("ThinSpace", Convert(0x2009));
            symbols.Add("THORN", Convert(0x00DE));
            symbols.Add("Tilde", Convert(0x223C));
            symbols.Add("TildeEqual", Convert(0x2243));
            symbols.Add("TildeFullEqual", Convert(0x2245));
            symbols.Add("TildeTilde", Convert(0x2248));
            symbols.Add("Topf", Convert(0x1D54B));
            symbols.Add("TRADE", Convert(0x2122));
            symbols.Add("TripleDot", Convert(0x20DB));
            symbols.Add("Tscr", Convert(0x1D4AF));
            symbols.Add("TScy", Convert(0x0426));
            symbols.Add("TSHcy", Convert(0x040B));
            symbols.Add("Tstrok", Convert(0x0166));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleU()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("uacute", Convert(0x00FA));
            symbols.Add("uArr", Convert(0x21D1));
            symbols.Add("uarr", Convert(0x2191));
            symbols.Add("ubrcy", Convert(0x045E));
            symbols.Add("ubreve", Convert(0x016D));
            symbols.Add("ucirc", Convert(0x00FB));
            symbols.Add("ucy", Convert(0x0443));
            symbols.Add("udarr", Convert(0x21C5));
            symbols.Add("udblac", Convert(0x0171));
            symbols.Add("udhar", Convert(0x296E));
            symbols.Add("ufisht", Convert(0x297E));
            symbols.Add("ufr", Convert(0x1D532));
            symbols.Add("ugrave", Convert(0x00F9));
            symbols.Add("uHar", Convert(0x2963));
            symbols.Add("uharl", Convert(0x21BF));
            symbols.Add("uharr", Convert(0x21BE));
            symbols.Add("uhblk", Convert(0x2580));
            symbols.Add("ulcorn", Convert(0x231C));
            symbols.Add("ulcorner", Convert(0x231C));
            symbols.Add("ulcrop", Convert(0x230F));
            symbols.Add("ultri", Convert(0x25F8));
            symbols.Add("umacr", Convert(0x016B));
            symbols.Add("uml", Convert(0x00A8));
            symbols.Add("uogon", Convert(0x0173));
            symbols.Add("uopf", Convert(0x1D566));
            symbols.Add("uparrow", Convert(0x2191));
            symbols.Add("updownarrow", Convert(0x2195));
            symbols.Add("upharpoonleft", Convert(0x21BF));
            symbols.Add("upharpoonright", Convert(0x21BE));
            symbols.Add("uplus", Convert(0x228E));
            symbols.Add("upsi", Convert(0x03C5));
            symbols.Add("upsih", Convert(0x03D2));
            symbols.Add("upsilon", Convert(0x03C5));
            symbols.Add("upuparrows", Convert(0x21C8));
            symbols.Add("urcorn", Convert(0x231D));
            symbols.Add("urcorner", Convert(0x231D));
            symbols.Add("urcrop", Convert(0x230E));
            symbols.Add("uring", Convert(0x016F));
            symbols.Add("urtri", Convert(0x25F9));
            symbols.Add("uscr", Convert(0x1D4CA));
            symbols.Add("utdot", Convert(0x22F0));
            symbols.Add("utilde", Convert(0x0169));
            symbols.Add("utri", Convert(0x25B5));
            symbols.Add("utrif", Convert(0x25B4));
            symbols.Add("uuarr", Convert(0x21C8));
            symbols.Add("uuml", Convert(0x00FC));
            symbols.Add("uwangle", Convert(0x29A7));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigU()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Uacute", Convert(0x00DA));
            symbols.Add("Uarr", Convert(0x219F));
            symbols.Add("Uarrocir", Convert(0x2949));
            symbols.Add("Ubrcy", Convert(0x040E));
            symbols.Add("Ubreve", Convert(0x016C));
            symbols.Add("Ucirc", Convert(0x00DB));
            symbols.Add("Ucy", Convert(0x0423));
            symbols.Add("Udblac", Convert(0x0170));
            symbols.Add("Ufr", Convert(0x1D518));
            symbols.Add("Ugrave", Convert(0x00D9));
            symbols.Add("Umacr", Convert(0x016A));
            symbols.Add("UnderBar", Convert(0x005F));
            symbols.Add("UnderBrace", Convert(0x23DF));
            symbols.Add("UnderBracket", Convert(0x23B5));
            symbols.Add("UnderParenthesis", Convert(0x23DD));
            symbols.Add("Union", Convert(0x22C3));
            symbols.Add("UnionPlus", Convert(0x228E));
            symbols.Add("Uogon", Convert(0x0172));
            symbols.Add("Uopf", Convert(0x1D54C));
            symbols.Add("UpArrow", Convert(0x2191));
            symbols.Add("Uparrow", Convert(0x21D1));
            symbols.Add("UpArrowBar", Convert(0x2912));
            symbols.Add("UpArrowDownArrow", Convert(0x21C5));
            symbols.Add("UpDownArrow", Convert(0x2195));
            symbols.Add("Updownarrow", Convert(0x21D5));
            symbols.Add("UpEquilibrium", Convert(0x296E));
            symbols.Add("UpperLeftArrow", Convert(0x2196));
            symbols.Add("UpperRightArrow", Convert(0x2197));
            symbols.Add("Upsi", Convert(0x03D2));
            symbols.Add("Upsilon", Convert(0x03A5));
            symbols.Add("UpTee", Convert(0x22A5));
            symbols.Add("UpTeeArrow", Convert(0x21A5));
            symbols.Add("Uring", Convert(0x016E));
            symbols.Add("Uscr", Convert(0x1D4B0));
            symbols.Add("Utilde", Convert(0x0168));
            symbols.Add("Uuml", Convert(0x00DC));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleV()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("vangrt", Convert(0x299C));
            symbols.Add("varepsilon", Convert(0x03F5));
            symbols.Add("varkappa", Convert(0x03F0));
            symbols.Add("varnothing", Convert(0x2205));
            symbols.Add("varphi", Convert(0x03D5));
            symbols.Add("varpi", Convert(0x03D6));
            symbols.Add("varpropto", Convert(0x221D));
            symbols.Add("vArr", Convert(0x21D5));
            symbols.Add("varr", Convert(0x2195));
            symbols.Add("varrho", Convert(0x03F1));
            symbols.Add("varsigma", Convert(0x03C2));
            symbols.Add("varsubsetneq", Convert(0x228A, 0xFE00));
            symbols.Add("varsubsetneqq", Convert(0x2ACB, 0xFE00));
            symbols.Add("varsupsetneq", Convert(0x228B, 0xFE00));
            symbols.Add("varsupsetneqq", Convert(0x2ACC, 0xFE00));
            symbols.Add("vartheta", Convert(0x03D1));
            symbols.Add("vartriangleleft", Convert(0x22B2));
            symbols.Add("vartriangleright", Convert(0x22B3));
            symbols.Add("vBar", Convert(0x2AE8));
            symbols.Add("vBarv", Convert(0x2AE9));
            symbols.Add("vcy", Convert(0x0432));
            symbols.Add("vDash", Convert(0x22A8));
            symbols.Add("vdash", Convert(0x22A2));
            symbols.Add("vee", Convert(0x2228));
            symbols.Add("veebar", Convert(0x22BB));
            symbols.Add("veeeq", Convert(0x225A));
            symbols.Add("vellip", Convert(0x22EE));
            symbols.Add("verbar", Convert(0x007C));
            symbols.Add("vert", Convert(0x007C));
            symbols.Add("vfr", Convert(0x1D533));
            symbols.Add("vltri", Convert(0x22B2));
            symbols.Add("vnsub", Convert(0x2282, 0x20D2));
            symbols.Add("vnsup", Convert(0x2283, 0x20D2));
            symbols.Add("vopf", Convert(0x1D567));
            symbols.Add("vprop", Convert(0x221D));
            symbols.Add("vrtri", Convert(0x22B3));
            symbols.Add("vscr", Convert(0x1D4CB));
            symbols.Add("vsubnE", Convert(0x2ACB, 0xFE00));
            symbols.Add("vsubne", Convert(0x228A, 0xFE00));
            symbols.Add("vsupnE", Convert(0x2ACC, 0xFE00));
            symbols.Add("vsupne", Convert(0x228B, 0xFE00));
            symbols.Add("vzigzag", Convert(0x299A));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigV()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Vbar", Convert(0x2AEB));
            symbols.Add("Vcy", Convert(0x0412));
            symbols.Add("VDash", Convert(0x22AB));
            symbols.Add("Vdash", Convert(0x22A9));
            symbols.Add("Vdashl", Convert(0x2AE6));
            symbols.Add("Vee", Convert(0x22C1));
            symbols.Add("Verbar", Convert(0x2016));
            symbols.Add("Vert", Convert(0x2016));
            symbols.Add("VerticalBar", Convert(0x2223));
            symbols.Add("VerticalLine", Convert(0x007C));
            symbols.Add("VerticalSeparator", Convert(0x2758));
            symbols.Add("VerticalTilde", Convert(0x2240));
            symbols.Add("VeryThinSpace", Convert(0x200A));
            symbols.Add("Vfr", Convert(0x1D519));
            symbols.Add("Vopf", Convert(0x1D54D));
            symbols.Add("Vscr", Convert(0x1D4B1));
            symbols.Add("Vvdash", Convert(0x22AA));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleW()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("wcirc", Convert(0x0175));
            symbols.Add("wedbar", Convert(0x2A5F));
            symbols.Add("wedge", Convert(0x2227));
            symbols.Add("wedgeq", Convert(0x2259));
            symbols.Add("weierp", Convert(0x2118));
            symbols.Add("wfr", Convert(0x1D534));
            symbols.Add("wopf", Convert(0x1D568));
            symbols.Add("wp", Convert(0x2118));
            symbols.Add("wr", Convert(0x2240));
            symbols.Add("wreath", Convert(0x2240));
            symbols.Add("wscr", Convert(0x1D4CC));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigW()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Wcirc", Convert(0x0174));
            symbols.Add("Wedge", Convert(0x22C0));
            symbols.Add("Wfr", Convert(0x1D51A));
            symbols.Add("Wopf", Convert(0x1D54E));
            symbols.Add("Wscr", Convert(0x1D4B2));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleX()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("xcap", Convert(0x22C2));
            symbols.Add("xcirc", Convert(0x25EF));
            symbols.Add("xcup", Convert(0x22C3));
            symbols.Add("xdtri", Convert(0x25BD));
            symbols.Add("xfr", Convert(0x1D535));
            symbols.Add("xhArr", Convert(0x27FA));
            symbols.Add("xharr", Convert(0x27F7));
            symbols.Add("xi", Convert(0x03BE));
            symbols.Add("xlArr", Convert(0x27F8));
            symbols.Add("xlarr", Convert(0x27F5));
            symbols.Add("xmap", Convert(0x27FC));
            symbols.Add("xnis", Convert(0x22FB));
            symbols.Add("xodot", Convert(0x2A00));
            symbols.Add("xopf", Convert(0x1D569));
            symbols.Add("xoplus", Convert(0x2A01));
            symbols.Add("xotime", Convert(0x2A02));
            symbols.Add("xrArr", Convert(0x27F9));
            symbols.Add("xrarr", Convert(0x27F6));
            symbols.Add("xscr", Convert(0x1D4CD));
            symbols.Add("xsqcup", Convert(0x2A06));
            symbols.Add("xuplus", Convert(0x2A04));
            symbols.Add("xutri", Convert(0x25B3));
            symbols.Add("xvee", Convert(0x22C1));
            symbols.Add("xwedge", Convert(0x22C0));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigX()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Xfr", Convert(0x1D51B));
            symbols.Add("Xi", Convert(0x039E));
            symbols.Add("Xopf", Convert(0x1D54F));
            symbols.Add("Xscr", Convert(0x1D4B3));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleY()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("yacute", Convert(0x00FD));
            symbols.Add("yacy", Convert(0x044F));
            symbols.Add("ycirc", Convert(0x0177));
            symbols.Add("ycy", Convert(0x044B));
            symbols.Add("yen", Convert(0x00A5));
            symbols.Add("yfr", Convert(0x1D536));
            symbols.Add("yicy", Convert(0x0457));
            symbols.Add("yopf", Convert(0x1D56A));
            symbols.Add("yscr", Convert(0x1D4CE));
            symbols.Add("yucy", Convert(0x044E));
            symbols.Add("yuml", Convert(0x00FF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigY()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Yacute", Convert(0x00DD));
            symbols.Add("YAcy", Convert(0x042F));
            symbols.Add("Ycirc", Convert(0x0176));
            symbols.Add("Ycy", Convert(0x042B));
            symbols.Add("Yfr", Convert(0x1D51C));
            symbols.Add("YIcy", Convert(0x0407));
            symbols.Add("Yopf", Convert(0x1D550));
            symbols.Add("Yscr", Convert(0x1D4B4));
            symbols.Add("YUcy", Convert(0x042E));
            symbols.Add("Yuml", Convert(0x0178));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleZ()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("zacute", Convert(0x017A));
            symbols.Add("zcaron", Convert(0x017E));
            symbols.Add("zcy", Convert(0x0437));
            symbols.Add("zdot", Convert(0x017C));
            symbols.Add("zeetrf", Convert(0x2128));
            symbols.Add("zeta", Convert(0x03B6));
            symbols.Add("zfr", Convert(0x1D537));
            symbols.Add("zhcy", Convert(0x0436));
            symbols.Add("zigrarr", Convert(0x21DD));
            symbols.Add("zopf", Convert(0x1D56B));
            symbols.Add("zscr", Convert(0x1D4CF));
            symbols.Add("zwj", Convert(0x200D));
            symbols.Add("zwnj", Convert(0x200C));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigZ()
        {
            var symbols = new Dictionary<String, String>();
            symbols.Add("Zacute", Convert(0x0179));
            symbols.Add("Zcaron", Convert(0x017D));
            symbols.Add("Zcy", Convert(0x0417));
            symbols.Add("Zdot", Convert(0x017B));
            symbols.Add("ZeroWidthSpace", Convert(0x200B));
            symbols.Add("Zeta", Convert(0x0396));
            symbols.Add("Zfr", Convert(0x2128));
            symbols.Add("ZHcy", Convert(0x0416));
            symbols.Add("Zopf", Convert(0x2124));
            symbols.Add("Zscr", Convert(0x1D4B5));
            return symbols;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an symbol specified by its entity name.
        /// </summary>
        /// <param name="name">The name of the entity, specified by &amp;NAME; in the Html source code.</param>
        /// <returns>The string with the symbol or null.</returns>
        public static String GetSymbol(String name)
        {
            if (!String.IsNullOrEmpty(name) && _entities.ContainsKey(name[0]))
            {
                var symbols = _entities[name[0]];

                if (symbols.ContainsKey(name))
                    return symbols[name];
            }

            return null;
        }

        /// <summary>
        /// Gets the entity name specified by its symbol (may be ambiguous).
        /// </summary>
        /// <param name="symbol">The value of the entity.</param>
        /// <returns>The string with the entity name or null.</returns>
        public static String GetEntity(String symbol)
        {
            if (!String.IsNullOrEmpty(symbol))
            {
                foreach (var symbols in _entities)
                {
                    foreach (var pair in symbols.Value)
                    {
                        if (pair.Value == symbol)
                            return pair.Key;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Converts a given number into its unicode character.
        /// </summary>
        /// <param name="code">The code to convert.</param>
        /// <returns>The array containing the character.</returns>
        public static String Convert(Int32 code)
        {
            return Char.ConvertFromUtf32(code);
        }

        /// <summary>
        /// Converts a set of two numbers into their unicode characters.
        /// </summary>
        /// <param name="leadingCode">The first (leading) character code.</param>
        /// <param name="trailingCode">The second (trailing) character code.</param>
        /// <returns>The array containing the two characters.</returns>
        public static String Convert(Int32 leadingCode, Int32 trailingCode)
        {
            return Char.ConvertFromUtf32(leadingCode) + Char.ConvertFromUtf32(trailingCode);
        }

        /// <summary>
        /// Determines if the code is an invalid number.
        /// </summary>
        /// <param name="code">The code to examine.</param>
        /// <returns>True if it is an invalid number, false otherwise.</returns>
        public static Boolean IsInvalidNumber(Int32 code)
        {
            /*
             * Otherwise, if the number is in the range 0xD800 to 0xDFFF or is
             * greater than 0x10FFFF, then this is a parse error. Return a U+FFFD
             * REPLACEMENT CHARACTER.
             */

            return (code >= 0xD800 && code <= 0xDFFF) || (code > 0x10FFFF);
        }

        /// <summary>
        /// Determines if the given code is actually in the table of common redirections.
        /// </summary>
        /// <param name="code">The code to examine.</param>
        /// <returns>True if the code is in the table, otherwise false.</returns>
        public static Boolean IsInCharacterTable(Int32 code)
        {
            /* 
             * If that number is one of the numbers in the first column of the
             * following table, then this is a parse error. Find the row with that
             * number in the first column, and return a character token for the
             * Unicode character given in the second column of that row.
             */

            return code == 0x00 || code == 0x0D || code == 0x80 || code == 0x81 ||
                   code == 0x82 || code == 0x83 || code == 0x84 || code == 0x85 ||
                   code == 0x86 || code == 0x87 || code == 0x88 || code == 0x89 ||
                   code == 0x8A || code == 0x8B || code == 0x8C || code == 0x8D ||
                   code == 0x8E || code == 0x8F || code == 0x90 || code == 0x91 ||
                   code == 0x92 || code == 0x93 || code == 0x94 || code == 0x95 ||
                   code == 0x96 || code == 0x97 || code == 0x98 || code == 0x99 ||
                   code == 0x9A || code == 0x9B || code == 0x9C || code == 0x9D ||
                   code == 0x9E || code == 0x9F;
        }

        /// <summary>
        /// Gets the symbol mapped by the table of common redirections.
        /// </summary>
        /// <param name="code">The original code.</param>
        /// <returns>The character wrapped in a string.</returns>
        public static String GetSymbolFromTable(Int32 code)
        {
            switch (code)
            { 
                case 0x00:
                    return Convert(0xfffd);
                case 0x0D:
                    return Convert(0xd);
                case 0x80:
                    return Convert(0x20ac);
                case 0x81:
                    return Convert(0x81);;
                case 0x82:
                    return Convert(0x201a);
                case 0x83: 	
                    return Convert(0x192);
                case 0x84:  
                    return Convert(0x201e);
                case 0x85:
                    return Convert(0x2026);
                case 0x86: 	
                    return Convert(0x2020);
                case 0x87: 
                    return Convert(0x2021);
                case 0x88:
                    return Convert(0x02C6);
                case 0x89:
                    return Convert(0x2030);
                case 0x8A:
                    return Convert(0x0160);
                case 0x8B:
                    return Convert(0x2039);
                case 0x8C:
                    return Convert(0x0152);
                case 0x8D:
                    return Convert(0x008D);
                case 0x8E:
                    return Convert(0x017D);
                case 0x8F: 	
                    return Convert(0x008F);
                case 0x90:
                    return Convert(0x0090);
                case 0x91: 	
                    return Convert(0x2018);
                case 0x92: 	
                    return Convert(0x2019);
                case 0x93: 	
                    return Convert(0x201C);
                case 0x94: 	
                    return Convert(0x201D);
                case 0x95: 	
                    return Convert(0x2022);
                case 0x96: 	
                    return Convert(0x2013);
                case 0x97: 	
                    return Convert(0x2014);
                case 0x98: 	
                    return Convert(0x02DC);
                case 0x99: 	
                    return Convert(0x2122);
                case 0x9A: 	
                    return Convert(0x0161);
                case 0x9B: 	
                    return Convert(0x203A);
                case 0x9C: 	
                    return Convert(0x0153);
                case 0x9D:
                    return Convert(0x009D);
                case 0x9E: 	
                    return Convert(0x017E);
                case 0x9F: 
                    return Convert(0x0178);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Determines if the code is within an invalid range.
        /// </summary>
        /// <param name="code">The code to examine.</param>
        /// <returns>True if it is within an invalid range, false otherwise.</returns>
        public static Boolean IsInInvalidRange(Int32 code)
        {
            /*
             * Otherwise, return a character token for the Unicode character whose
             * code point is that number.  Additionally, if the number is in the
             * range 0x0001 to 0x0008, 0x000E to 0x001F, 0x007F to 0x009F,
             * 0xFDD0 to 0xFDEF, or is one of 0x000B, 0xFFFE, 0xFFFF, 0x1FFFE,
             * 0x1FFFF, 0x2FFFE, 0x2FFFF, 0x3FFFE, 0x3FFFF, 0x4FFFE, 0x4FFFF,
             * 0x5FFFE, 0x5FFFF, 0x6FFFE, 0x6FFFF, 0x7FFFE, 0x7FFFF, 0x8FFFE,
             * 0x8FFFF, 0x9FFFE, 0x9FFFF, 0xAFFFE, 0xAFFFF, 0xBFFFE, 0xBFFFF,
             * 0xCFFFE, 0xCFFFF, 0xDFFFE, 0xDFFFF, 0xEFFFE, 0xEFFFF, 0xFFFFE,
             * 0xFFFFF, 0x10FFFE, or 0x10FFFF, then this is a parse error.
             */

            return (code >= 0x0001 && code <= 0x0008) ||
                    (code >= 0x000E && code <= 0x001F) ||
                    (code >= 0x007F && code <= 0x009F) ||
                    (code >= 0xFDD0 && code <= 0xFDEF) ||
                    (code == 0x000B || code == 0xFFFE) ||
                    (code == 0xFFFF || code == 0x1FFFE) ||
                    (code == 0x2FFFE || code == 0x1FFFF) ||
                    (code == 0x2FFFF || code == 0x3FFFE) ||
                    (code == 0x3FFFF || code == 0x4FFFE) ||
                    (code == 0x4FFFF || code == 0x5FFFE) ||
                    (code == 0x5FFFF || code == 0x6FFFE) ||
                    (code == 0x6FFFF || code == 0x7FFFE) ||
                    (code == 0x7FFFF || code == 0x8FFFE) ||
                    (code == 0x8FFFE || code == 0x9FFFF) ||
                    (code == 0x9FFFF || code == 0xAFFFE) ||
                    (code == 0xAFFFF || code == 0xBFFFE) ||
                    (code == 0xBFFFF || code == 0xCFFFE) ||
                    (code == 0xCFFFF || code == 0xDFFFE) ||
                    (code == 0xDFFFF || code == 0xEFFFE) ||
                    (code == 0xEFFFF || code == 0xFFFFE) ||
                    (code == 0xFFFFF || code == 0x10FFFE) ||
                    (code == 0x10FFFF);
        }

        #endregion
    }
}

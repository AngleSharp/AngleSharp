namespace AngleSharp.Html
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Extensions;

    /// <summary>
    /// Represents the list of all Html entities.
    /// </summary>
    [DebuggerStepThrough]
    static class Entities
    {
        #region Fields

        /// <summary>
        /// Strong entities always end with a semicolon.
        /// </summary>
        static Dictionary<Char, Dictionary<String, String>> _strongEntities;
        /// <summary>
        /// Weak entities do not end with a semicolon (there are much less weak entities).
        /// </summary>
        static Dictionary<String, String> _weakEntities;

        #endregion

        #region ctor

        static Entities()
        {
            _strongEntities = new Dictionary<Char, Dictionary<String, String>>();
            _weakEntities = new Dictionary<String, String>();

            _strongEntities.Add('a', GetSymbolLittleA());
            _strongEntities.Add('A', GetSymbolBigA());

            _strongEntities.Add('b', GetSymbolLittleB());
            _strongEntities.Add('B', GetSymbolBigB());

            _strongEntities.Add('c', GetSymbolLittleC());
            _strongEntities.Add('C', GetSymbolBigC());

            _strongEntities.Add('d', GetSymbolLittleD());
            _strongEntities.Add('D', GetSymbolBigD());

            _strongEntities.Add('e', GetSymbolLittleE());
            _strongEntities.Add('E', GetSymbolBigE());

            _strongEntities.Add('f', GetSymbolLittleF());
            _strongEntities.Add('F', GetSymbolBigF());

            _strongEntities.Add('g', GetSymbolLittleG());
            _strongEntities.Add('G', GetSymbolBigG());

            _strongEntities.Add('h', GetSymbolLittleH());
            _strongEntities.Add('H', GetSymbolBigH());

            _strongEntities.Add('i', GetSymbolLittleI());
            _strongEntities.Add('I', GetSymbolBigI());

            _strongEntities.Add('j', GetSymbolLittleJ());
            _strongEntities.Add('J', GetSymbolBigJ());

            _strongEntities.Add('k', GetSymbolLittleK());
            _strongEntities.Add('K', GetSymbolBigK());

            _strongEntities.Add('l', GetSymbolLittleL());
            _strongEntities.Add('L', GetSymbolBigL());

            _strongEntities.Add('m', GetSymbolLittleM());
            _strongEntities.Add('M', GetSymbolBigM());

            _strongEntities.Add('n', GetSymbolLittleN());
            _strongEntities.Add('N', GetSymbolBigN());

            _strongEntities.Add('o', GetSymbolLittleO());
            _strongEntities.Add('O', GetSymbolBigO());

            _strongEntities.Add('p', GetSymbolLittleP());
            _strongEntities.Add('P', GetSymbolBigP());

            _strongEntities.Add('q', GetSymbolLittleQ());
            _strongEntities.Add('Q', GetSymbolBigQ());

            _strongEntities.Add('r', GetSymbolLittleR());
            _strongEntities.Add('R', GetSymbolBigR());

            _strongEntities.Add('s', GetSymbolLittleS());
            _strongEntities.Add('S', GetSymbolBigS());

            _strongEntities.Add('t', GetSymbolLittleT());
            _strongEntities.Add('T', GetSymbolBigT());

            _strongEntities.Add('u', GetSymbolLittleU());
            _strongEntities.Add('U', GetSymbolBigU());

            _strongEntities.Add('v', GetSymbolLittleV());
            _strongEntities.Add('V', GetSymbolBigV());

            _strongEntities.Add('w', GetSymbolLittleW());
            _strongEntities.Add('W', GetSymbolBigW());

            _strongEntities.Add('x', GetSymbolLittleX());
            _strongEntities.Add('X', GetSymbolBigX());

            _strongEntities.Add('y', GetSymbolLittleY());
            _strongEntities.Add('Y', GetSymbolBigY());

            _strongEntities.Add('z', GetSymbolLittleZ());
            _strongEntities.Add('Z', GetSymbolBigZ());
        }

        #endregion

        #region Symbol Methods

        static Dictionary<String, String> GetSymbolLittleA()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "aacute", Convert(0x00E1));
            Add(symbols, "abreve", Convert(0x0103));
            Add(symbols, "ac", Convert(0x223E));
            Add(symbols, "acd", Convert(0x223F));
            Add(symbols, "acE", Convert(0x223E, 0x0333));
            AddWeak(symbols, "acirc", Convert(0x00E2));
            AddWeak(symbols, "acute", Convert(0x00B4));
            Add(symbols, "acy", Convert(0x0430));
            AddWeak(symbols, "aelig", Convert(0x00E6));
            Add(symbols, "af", Convert(0x2061));
            Add(symbols, "afr", Convert(0x1D51E));
            AddWeak(symbols, "agrave", Convert(0x00E0));
            Add(symbols, "alefsym", Convert(0x2135));
            Add(symbols, "aleph", Convert(0x2135));
            Add(symbols, "alpha", Convert(0x03B1));
            Add(symbols, "amacr", Convert(0x0101));
            Add(symbols, "amalg", Convert(0x2A3F));
            AddWeak(symbols, "amp", Convert(0x0026));
            Add(symbols, "and", Convert(0x2227));
            Add(symbols, "andand", Convert(0x2A55));
            Add(symbols, "andd", Convert(0x2A5C));
            Add(symbols, "andslope", Convert(0x2A58));
            Add(symbols, "andv", Convert(0x2A5A));
            Add(symbols, "ang", Convert(0x2220));
            Add(symbols, "ange", Convert(0x29A4));
            Add(symbols, "angle", Convert(0x2220));
            Add(symbols, "angmsd", Convert(0x2221));
            Add(symbols, "angmsdaa", Convert(0x29A8));
            Add(symbols, "angmsdab", Convert(0x29A9));
            Add(symbols, "angmsdac", Convert(0x29AA));
            Add(symbols, "angmsdad", Convert(0x29AB));
            Add(symbols, "angmsdae", Convert(0x29AC));
            Add(symbols, "angmsdaf", Convert(0x29AD));
            Add(symbols, "angmsdag", Convert(0x29AE));
            Add(symbols, "angmsdah", Convert(0x29AF));
            Add(symbols, "angrt", Convert(0x221F));
            Add(symbols, "angrtvb", Convert(0x22BE));
            Add(symbols, "angrtvbd", Convert(0x299D));
            Add(symbols, "angsph", Convert(0x2222));
            Add(symbols, "angst", Convert(0x00C5));
            Add(symbols, "angzarr", Convert(0x237C));
            Add(symbols, "aogon", Convert(0x0105));
            Add(symbols, "aopf", Convert(0x1D552));
            Add(symbols, "ap", Convert(0x2248));
            Add(symbols, "apacir", Convert(0x2A6F));
            Add(symbols, "apE", Convert(0x2A70));
            Add(symbols, "ape", Convert(0x224A));
            Add(symbols, "apid", Convert(0x224B));
            Add(symbols, "apos", Convert(0x0027));
            Add(symbols, "approx", Convert(0x2248));
            Add(symbols, "approxeq", Convert(0x224A));
            AddWeak(symbols, "aring", Convert(0x00E5));
            Add(symbols, "ascr", Convert(0x1D4B6));
            Add(symbols, "ast", Convert(0x002A));
            Add(symbols, "asymp", Convert(0x2248));
            Add(symbols, "asympeq", Convert(0x224D));
            AddWeak(symbols, "atilde", Convert(0x00E3));
            AddWeak(symbols, "auml", Convert(0x00E4));
            Add(symbols, "awconint", Convert(0x2233));
            Add(symbols, "awint", Convert(0x2A11));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigA()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Aogon", Convert(0x0104));
            Add(symbols, "Aopf", Convert(0x1D538));
            Add(symbols, "ApplyFunction", Convert(0x2061));
            AddWeak(symbols, "Aring", Convert(0x00C5));
            Add(symbols, "Ascr", Convert(0x1D49C));
            Add(symbols, "Assign", Convert(0x2254));
            AddWeak(symbols, "Atilde", Convert(0x00C3));
            AddWeak(symbols, "Auml", Convert(0x00C4));
            AddWeak(symbols, "Aacute", Convert(0x00C1));
            Add(symbols, "Abreve", Convert(0x0102));
            AddWeak(symbols, "Acirc", Convert(0x00C2));
            Add(symbols, "Acy", Convert(0x0410));
            AddWeak(symbols, "AElig", Convert(0x00C6));
            Add(symbols, "Afr", Convert(0x1D504));
            AddWeak(symbols, "Agrave", Convert(0x00C0));
            Add(symbols, "Alpha", Convert(0x0391));
            Add(symbols, "Amacr", Convert(0x0100));
            AddWeak(symbols, "AMP", Convert(0x0026));
            Add(symbols, "And", Convert(0x2A53));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleB()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "backcong", Convert(0x224C));
            Add(symbols, "backepsilon", Convert(0x03F6));
            Add(symbols, "backprime", Convert(0x2035));
            Add(symbols, "backsim", Convert(0x223D));
            Add(symbols, "backsimeq", Convert(0x22CD));
            Add(symbols, "barvee", Convert(0x22BD));
            Add(symbols, "barwed", Convert(0x2305));
            Add(symbols, "barwedge", Convert(0x2305));
            Add(symbols, "bbrk", Convert(0x23B5));
            Add(symbols, "bbrktbrk", Convert(0x23B6));
            Add(symbols, "bcong", Convert(0x224C));
            Add(symbols, "bcy", Convert(0x0431));
            Add(symbols, "bdquo", Convert(0x201E));
            Add(symbols, "becaus", Convert(0x2235));
            Add(symbols, "because", Convert(0x2235));
            Add(symbols, "bemptyv", Convert(0x29B0));
            Add(symbols, "bepsi", Convert(0x03F6));
            Add(symbols, "bernou", Convert(0x212C));
            Add(symbols, "beta", Convert(0x03B2));
            Add(symbols, "beth", Convert(0x2136));
            Add(symbols, "between", Convert(0x226C));
            Add(symbols, "bfr", Convert(0x1D51F));
            Add(symbols, "bigcap", Convert(0x22C2));
            Add(symbols, "bigcirc", Convert(0x25EF));
            Add(symbols, "bigcup", Convert(0x22C3));
            Add(symbols, "bigodot", Convert(0x2A00));
            Add(symbols, "bigoplus", Convert(0x2A01));
            Add(symbols, "bigotimes", Convert(0x2A02));
            Add(symbols, "bigsqcup", Convert(0x2A06));
            Add(symbols, "bigstar", Convert(0x2605));
            Add(symbols, "bigtriangledown", Convert(0x25BD));
            Add(symbols, "bigtriangleup", Convert(0x25B3));
            Add(symbols, "biguplus", Convert(0x2A04));
            Add(symbols, "bigvee", Convert(0x22C1));
            Add(symbols, "bigwedge", Convert(0x22C0));
            Add(symbols, "bkarow", Convert(0x290D));
            Add(symbols, "blacklozenge", Convert(0x29EB));
            Add(symbols, "blacksquare", Convert(0x25AA));
            Add(symbols, "blacktriangle", Convert(0x25B4));
            Add(symbols, "blacktriangledown", Convert(0x25BE));
            Add(symbols, "blacktriangleleft", Convert(0x25C2));
            Add(symbols, "blacktriangleright", Convert(0x25B8));
            Add(symbols, "blank", Convert(0x2423));
            Add(symbols, "blk12", Convert(0x2592));
            Add(symbols, "blk14", Convert(0x2591));
            Add(symbols, "blk34", Convert(0x2593));
            Add(symbols, "block", Convert(0x2588));
            Add(symbols, "bne", Convert(0x003D, 0x20E5));
            Add(symbols, "bnequiv", Convert(0x2261, 0x20E5));
            Add(symbols, "bNot", Convert(0x2AED));
            Add(symbols, "bnot", Convert(0x2310));
            Add(symbols, "bopf", Convert(0x1D553));
            Add(symbols, "bot", Convert(0x22A5));
            Add(symbols, "bottom", Convert(0x22A5));
            Add(symbols, "bowtie", Convert(0x22C8));
            Add(symbols, "boxbox", Convert(0x29C9));
            Add(symbols, "boxDL", Convert(0x2557));
            Add(symbols, "boxDl", Convert(0x2556));
            Add(symbols, "boxdL", Convert(0x2555));
            Add(symbols, "boxdl", Convert(0x2510));
            Add(symbols, "boxDR", Convert(0x2554));
            Add(symbols, "boxDr", Convert(0x2553));
            Add(symbols, "boxdR", Convert(0x2552));
            Add(symbols, "boxdr", Convert(0x250C));
            Add(symbols, "boxH", Convert(0x2550));
            Add(symbols, "boxh", Convert(0x2500));
            Add(symbols, "boxHD", Convert(0x2566));
            Add(symbols, "boxHd", Convert(0x2564));
            Add(symbols, "boxhD", Convert(0x2565));
            Add(symbols, "boxhd", Convert(0x252C));
            Add(symbols, "boxHU", Convert(0x2569));
            Add(symbols, "boxHu", Convert(0x2567));
            Add(symbols, "boxhU", Convert(0x2568));
            Add(symbols, "boxhu", Convert(0x2534));
            Add(symbols, "boxminus", Convert(0x229F));
            Add(symbols, "boxplus", Convert(0x229E));
            Add(symbols, "boxtimes", Convert(0x22A0));
            Add(symbols, "boxUL", Convert(0x255D));
            Add(symbols, "boxUl", Convert(0x255C));
            Add(symbols, "boxuL", Convert(0x255B));
            Add(symbols, "boxul", Convert(0x2518));
            Add(symbols, "boxUR", Convert(0x255A));
            Add(symbols, "boxUr", Convert(0x2559));
            Add(symbols, "boxuR", Convert(0x2558));
            Add(symbols, "boxur", Convert(0x2514));
            Add(symbols, "boxV", Convert(0x2551));
            Add(symbols, "boxv", Convert(0x2502));
            Add(symbols, "boxVH", Convert(0x256C));
            Add(symbols, "boxVh", Convert(0x256B));
            Add(symbols, "boxvH", Convert(0x256A));
            Add(symbols, "boxvh", Convert(0x253C));
            Add(symbols, "boxVL", Convert(0x2563));
            Add(symbols, "boxVl", Convert(0x2562));
            Add(symbols, "boxvL", Convert(0x2561));
            Add(symbols, "boxvl", Convert(0x2524));
            Add(symbols, "boxVR", Convert(0x2560));
            Add(symbols, "boxVr", Convert(0x255F));
            Add(symbols, "boxvR", Convert(0x255E));
            Add(symbols, "boxvr", Convert(0x251C));
            Add(symbols, "bprime", Convert(0x2035));
            Add(symbols, "breve", Convert(0x02D8));
            AddWeak(symbols, "brvbar", Convert(0x00A6));
            Add(symbols, "bscr", Convert(0x1D4B7));
            Add(symbols, "bsemi", Convert(0x204F));
            Add(symbols, "bsim", Convert(0x223D));
            Add(symbols, "bsime", Convert(0x22CD));
            Add(symbols, "bsol", Convert(0x005C));
            Add(symbols, "bsolb", Convert(0x29C5));
            Add(symbols, "bsolhsub", Convert(0x27C8));
            Add(symbols, "bull", Convert(0x2022));
            Add(symbols, "bullet", Convert(0x2022));
            Add(symbols, "bump", Convert(0x224E));
            Add(symbols, "bumpE", Convert(0x2AAE));
            Add(symbols, "bumpe", Convert(0x224F));
            Add(symbols, "bumpeq", Convert(0x224F));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigB()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Backslash", Convert(0x2216));
            Add(symbols, "Barv", Convert(0x2AE7));
            Add(symbols, "Barwed", Convert(0x2306));
            Add(symbols, "Bcy", Convert(0x0411));
            Add(symbols, "Because", Convert(0x2235));
            Add(symbols, "Bernoullis", Convert(0x212C));
            Add(symbols, "Beta", Convert(0x0392));
            Add(symbols, "Bfr", Convert(0x1D505));
            Add(symbols, "Bopf", Convert(0x1D539));
            Add(symbols, "Breve", Convert(0x02D8));
            Add(symbols, "Bscr", Convert(0x212C));
            Add(symbols, "Bumpeq", Convert(0x224E));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleC()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "cacute", Convert(0x0107));
            Add(symbols, "cap", Convert(0x2229));
            Add(symbols, "capand", Convert(0x2A44));
            Add(symbols, "capbrcup", Convert(0x2A49));
            Add(symbols, "capcap", Convert(0x2A4B));
            Add(symbols, "capcup", Convert(0x2A47));
            Add(symbols, "capdot", Convert(0x2A40));
            Add(symbols, "caps", Convert(0x2229, 0xFE00));
            Add(symbols, "caret", Convert(0x2041));
            Add(symbols, "caron", Convert(0x02C7));
            Add(symbols, "ccaps", Convert(0x2A4D));
            Add(symbols, "ccaron", Convert(0x010D));
            AddWeak(symbols, "ccedil", Convert(0x00E7));
            Add(symbols, "ccirc", Convert(0x0109));
            Add(symbols, "ccups", Convert(0x2A4C));
            Add(symbols, "ccupssm", Convert(0x2A50));
            Add(symbols, "cdot", Convert(0x010B));
            AddWeak(symbols, "cedil", Convert(0x00B8));
            Add(symbols, "cemptyv", Convert(0x29B2));
            AddWeak(symbols, "cent", Convert(0x00A2));
            Add(symbols, "centerdot", Convert(0x00B7));
            Add(symbols, "cfr", Convert(0x1D520));
            Add(symbols, "chcy", Convert(0x0447));
            Add(symbols, "check", Convert(0x2713));
            Add(symbols, "checkmark", Convert(0x2713));
            Add(symbols, "chi", Convert(0x03C7));
            Add(symbols, "cir", Convert(0x25CB));
            Add(symbols, "circ", Convert(0x02C6));
            Add(symbols, "circeq", Convert(0x2257));
            Add(symbols, "circlearrowleft", Convert(0x21BA));
            Add(symbols, "circlearrowright", Convert(0x21BB));
            Add(symbols, "circledast", Convert(0x229B));
            Add(symbols, "circledcirc", Convert(0x229A));
            Add(symbols, "circleddash", Convert(0x229D));
            Add(symbols, "circledR", Convert(0x00AE));
            Add(symbols, "circledS", Convert(0x24C8));
            Add(symbols, "cirE", Convert(0x29C3));
            Add(symbols, "cire", Convert(0x2257));
            Add(symbols, "cirfnint", Convert(0x2A10));
            Add(symbols, "cirmid", Convert(0x2AEF));
            Add(symbols, "cirscir", Convert(0x29C2));
            Add(symbols, "clubs", Convert(0x2663));
            Add(symbols, "clubsuit", Convert(0x2663));
            Add(symbols, "colon", Convert(0x003A));
            Add(symbols, "colone", Convert(0x2254));
            Add(symbols, "coloneq", Convert(0x2254));
            Add(symbols, "comma", Convert(0x002C));
            Add(symbols, "commat", Convert(0x0040));
            Add(symbols, "comp", Convert(0x2201));
            Add(symbols, "compfn", Convert(0x2218));
            Add(symbols, "complement", Convert(0x2201));
            Add(symbols, "complexes", Convert(0x2102));
            Add(symbols, "cong", Convert(0x2245));
            Add(symbols, "congdot", Convert(0x2A6D));
            Add(symbols, "conint", Convert(0x222E));
            Add(symbols, "copf", Convert(0x1D554));
            Add(symbols, "coprod", Convert(0x2210));
            AddWeak(symbols, "copy", Convert(0x00A9));
            Add(symbols, "copysr", Convert(0x2117));
            Add(symbols, "crarr", Convert(0x21B5));
            Add(symbols, "cross", Convert(0x2717));
            Add(symbols, "cscr", Convert(0x1D4B8));
            Add(symbols, "csub", Convert(0x2ACF));
            Add(symbols, "csube", Convert(0x2AD1));
            Add(symbols, "csup", Convert(0x2AD0));
            Add(symbols, "csupe", Convert(0x2AD2));
            Add(symbols, "ctdot", Convert(0x22EF));
            Add(symbols, "cudarrl", Convert(0x2938));
            Add(symbols, "cudarrr", Convert(0x2935));
            Add(symbols, "cuepr", Convert(0x22DE));
            Add(symbols, "cuesc", Convert(0x22DF));
            Add(symbols, "cularr", Convert(0x21B6));
            Add(symbols, "cularrp", Convert(0x293D));
            Add(symbols, "cup", Convert(0x222A));
            Add(symbols, "cupbrcap", Convert(0x2A48));
            Add(symbols, "cupcap", Convert(0x2A46));
            Add(symbols, "cupcup", Convert(0x2A4A));
            Add(symbols, "cupdot", Convert(0x228D));
            Add(symbols, "cupor", Convert(0x2A45));
            Add(symbols, "cups", Convert(0x222A, 0xFE00));
            Add(symbols, "curarr", Convert(0x21B7));
            Add(symbols, "curarrm", Convert(0x293C));
            Add(symbols, "curlyeqprec", Convert(0x22DE));
            Add(symbols, "curlyeqsucc", Convert(0x22DF));
            Add(symbols, "curlyvee", Convert(0x22CE));
            Add(symbols, "curlywedge", Convert(0x22CF));
            AddWeak(symbols, "curren", Convert(0x00A4));
            Add(symbols, "curvearrowleft", Convert(0x21B6));
            Add(symbols, "curvearrowright", Convert(0x21B7));
            Add(symbols, "cuvee", Convert(0x22CE));
            Add(symbols, "cuwed", Convert(0x22CF));
            Add(symbols, "cwconint", Convert(0x2232));
            Add(symbols, "cwint", Convert(0x2231));
            Add(symbols, "cylcty", Convert(0x232D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigC()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Cacute", Convert(0x0106));
            Add(symbols, "Cap", Convert(0x22D2));
            Add(symbols, "CapitalDifferentialD", Convert(0x2145));
            Add(symbols, "Cayleys", Convert(0x212D));
            Add(symbols, "Ccaron", Convert(0x010C));
            AddWeak(symbols, "Ccedil", Convert(0x00C7));
            Add(symbols, "Ccirc", Convert(0x0108));
            Add(symbols, "Cconint", Convert(0x2230));
            Add(symbols, "Cdot", Convert(0x010A));
            Add(symbols, "Cedilla", Convert(0x00B8));
            Add(symbols, "CenterDot", Convert(0x00B7));
            Add(symbols, "Cfr", Convert(0x212D));
            Add(symbols, "CHcy", Convert(0x0427));
            Add(symbols, "Chi", Convert(0x03A7));
            Add(symbols, "CircleDot", Convert(0x2299));
            Add(symbols, "CircleMinus", Convert(0x2296));
            Add(symbols, "CirclePlus", Convert(0x2295));
            Add(symbols, "CircleTimes", Convert(0x2297));
            Add(symbols, "ClockwiseContourIntegral", Convert(0x2232));
            Add(symbols, "CloseCurlyDoubleQuote", Convert(0x201D));
            Add(symbols, "CloseCurlyQuote", Convert(0x2019));
            Add(symbols, "Colon", Convert(0x2237));
            Add(symbols, "Colone", Convert(0x2A74));
            Add(symbols, "Congruent", Convert(0x2261));
            Add(symbols, "Conint", Convert(0x222F));
            Add(symbols, "ContourIntegral", Convert(0x222E));
            Add(symbols, "Copf", Convert(0x2102));
            Add(symbols, "Coproduct", Convert(0x2210));
            AddWeak(symbols, "COPY", Convert(0x00A9));
            Add(symbols, "CounterClockwiseContourIntegral", Convert(0x2233));
            Add(symbols, "Cross", Convert(0x2A2F));
            Add(symbols, "Cscr", Convert(0x1D49E));
            Add(symbols, "Cup", Convert(0x22D3));
            Add(symbols, "CupCap", Convert(0x224D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleD()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "dagger", Convert(0x2020));
            Add(symbols, "daleth", Convert(0x2138));
            Add(symbols, "dArr", Convert(0x21D3));
            Add(symbols, "darr", Convert(0x2193));
            Add(symbols, "dash", Convert(0x2010));
            Add(symbols, "dashv", Convert(0x22A3));
            Add(symbols, "dbkarow", Convert(0x290F));
            Add(symbols, "dblac", Convert(0x02DD));
            Add(symbols, "dcaron", Convert(0x010F));
            Add(symbols, "dcy", Convert(0x0434));
            Add(symbols, "dd", Convert(0x2146));
            Add(symbols, "ddagger", Convert(0x2021));
            Add(symbols, "ddarr", Convert(0x21CA));
            Add(symbols, "ddotseq", Convert(0x2A77));
            AddWeak(symbols, "deg", Convert(0x00B0));
            Add(symbols, "delta", Convert(0x03B4));
            Add(symbols, "demptyv", Convert(0x29B1));
            Add(symbols, "dfisht", Convert(0x297F));
            Add(symbols, "dfr", Convert(0x1D521));
            Add(symbols, "dHar", Convert(0x2965));
            Add(symbols, "dharl", Convert(0x21C3));
            Add(symbols, "dharr", Convert(0x21C2));
            Add(symbols, "diam", Convert(0x22C4));
            Add(symbols, "diamond", Convert(0x22C4));
            Add(symbols, "diamondsuit", Convert(0x2666));
            Add(symbols, "diams", Convert(0x2666));
            Add(symbols, "die", Convert(0x00A8));
            Add(symbols, "digamma", Convert(0x03DD));
            Add(symbols, "disin", Convert(0x22F2));
            Add(symbols, "div", Convert(0x00F7));
            AddWeak(symbols, "divide", Convert(0x00F7));
            Add(symbols, "divideontimes", Convert(0x22C7));
            Add(symbols, "divonx", Convert(0x22C7));
            Add(symbols, "djcy", Convert(0x0452));
            Add(symbols, "dlcorn", Convert(0x231E));
            Add(symbols, "dlcrop", Convert(0x230D));
            Add(symbols, "dollar", Convert(0x0024));
            Add(symbols, "dopf", Convert(0x1D555));
            Add(symbols, "dot", Convert(0x02D9));
            Add(symbols, "doteq", Convert(0x2250));
            Add(symbols, "doteqdot", Convert(0x2251));
            Add(symbols, "dotminus", Convert(0x2238));
            Add(symbols, "dotplus", Convert(0x2214));
            Add(symbols, "dotsquare", Convert(0x22A1));
            Add(symbols, "doublebarwedge", Convert(0x2306));
            Add(symbols, "downarrow", Convert(0x2193));
            Add(symbols, "downdownarrows", Convert(0x21CA));
            Add(symbols, "downharpoonleft", Convert(0x21C3));
            Add(symbols, "downharpoonright", Convert(0x21C2));
            Add(symbols, "drbkarow", Convert(0x2910));
            Add(symbols, "drcorn", Convert(0x231F));
            Add(symbols, "drcrop", Convert(0x230C));
            Add(symbols, "dscr", Convert(0x1D4B9));
            Add(symbols, "dscy", Convert(0x0455));
            Add(symbols, "dsol", Convert(0x29F6));
            Add(symbols, "dstrok", Convert(0x0111));
            Add(symbols, "dtdot", Convert(0x22F1));
            Add(symbols, "dtri", Convert(0x25BF));
            Add(symbols, "dtrif", Convert(0x25BE));
            Add(symbols, "duarr", Convert(0x21F5));
            Add(symbols, "duhar", Convert(0x296F));
            Add(symbols, "dwangle", Convert(0x29A6));
            Add(symbols, "dzcy", Convert(0x045F));
            Add(symbols, "dzigrarr", Convert(0x27FF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigD()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Dagger", Convert(0x2021));
            Add(symbols, "Darr", Convert(0x21A1));
            Add(symbols, "Dashv", Convert(0x2AE4));
            Add(symbols, "Dcaron", Convert(0x010E));
            Add(symbols, "Dcy", Convert(0x0414));
            Add(symbols, "DD", Convert(0x2145));
            Add(symbols, "DDotrahd", Convert(0x2911));
            Add(symbols, "Del", Convert(0x2207));
            Add(symbols, "Delta", Convert(0x0394));
            Add(symbols, "Dfr", Convert(0x1D507));
            Add(symbols, "DiacriticalAcute", Convert(0x00B4));
            Add(symbols, "DiacriticalDot", Convert(0x02D9));
            Add(symbols, "DiacriticalDoubleAcute", Convert(0x02DD));
            Add(symbols, "DiacriticalGrave", Convert(0x0060));
            Add(symbols, "DiacriticalTilde", Convert(0x02DC));
            Add(symbols, "Diamond", Convert(0x22C4));
            Add(symbols, "DifferentialD", Convert(0x2146));
            Add(symbols, "DJcy", Convert(0x0402));
            Add(symbols, "Dopf", Convert(0x1D53B));
            Add(symbols, "Dot", Convert(0x00A8));
            Add(symbols, "DotDot", Convert(0x20DC));
            Add(symbols, "DotEqual", Convert(0x2250));
            Add(symbols, "DoubleContourIntegral", Convert(0x222F));
            Add(symbols, "DoubleDot", Convert(0x00A8));
            Add(symbols, "DoubleDownArrow", Convert(0x21D3));
            Add(symbols, "DoubleLeftArrow", Convert(0x21D0));
            Add(symbols, "DoubleLeftRightArrow", Convert(0x21D4));
            Add(symbols, "DoubleLeftTee", Convert(0x2AE4));
            Add(symbols, "DoubleLongLeftArrow", Convert(0x27F8));
            Add(symbols, "DoubleLongLeftRightArrow", Convert(0x27FA));
            Add(symbols, "DoubleLongRightArrow", Convert(0x27F9));
            Add(symbols, "DoubleRightArrow", Convert(0x21D2));
            Add(symbols, "DoubleRightTee", Convert(0x22A8));
            Add(symbols, "DoubleUpArrow", Convert(0x21D1));
            Add(symbols, "DoubleUpDownArrow", Convert(0x21D5));
            Add(symbols, "DoubleVerticalBar", Convert(0x2225));
            Add(symbols, "DownArrow", Convert(0x2193));
            Add(symbols, "Downarrow", Convert(0x21D3));
            Add(symbols, "DownArrowBar", Convert(0x2913));
            Add(symbols, "DownArrowUpArrow", Convert(0x21F5));
            Add(symbols, "DownBreve", Convert(0x0311));
            Add(symbols, "DownLeftRightVector", Convert(0x2950));
            Add(symbols, "DownLeftTeeVector", Convert(0x295E));
            Add(symbols, "DownLeftVector", Convert(0x21BD));
            Add(symbols, "DownLeftVectorBar", Convert(0x2956));
            Add(symbols, "DownRightTeeVector", Convert(0x295F));
            Add(symbols, "DownRightVector", Convert(0x21C1));
            Add(symbols, "DownRightVectorBar", Convert(0x2957));
            Add(symbols, "DownTee", Convert(0x22A4));
            Add(symbols, "DownTeeArrow", Convert(0x21A7));
            Add(symbols, "Dscr", Convert(0x1D49F));
            Add(symbols, "DScy", Convert(0x0405));
            Add(symbols, "Dstrok", Convert(0x0110));
            Add(symbols, "DZcy", Convert(0x040F));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleE()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "eacute", Convert(0x00E9));
            Add(symbols, "easter", Convert(0x2A6E));
            Add(symbols, "ecaron", Convert(0x011B));
            Add(symbols, "ecir", Convert(0x2256));
            AddWeak(symbols, "ecirc", Convert(0x00EA));
            Add(symbols, "ecolon", Convert(0x2255));
            Add(symbols, "ecy", Convert(0x044D));
            Add(symbols, "eDDot", Convert(0x2A77));
            Add(symbols, "eDot", Convert(0x2251));
            Add(symbols, "edot", Convert(0x0117));
            Add(symbols, "ee", Convert(0x2147));
            Add(symbols, "efDot", Convert(0x2252));
            Add(symbols, "efr", Convert(0x1D522));
            Add(symbols, "eg", Convert(0x2A9A));
            AddWeak(symbols, "egrave", Convert(0x00E8));
            Add(symbols, "egs", Convert(0x2A96));
            Add(symbols, "egsdot", Convert(0x2A98));
            Add(symbols, "el", Convert(0x2A99));
            Add(symbols, "elinters", Convert(0x23E7));
            Add(symbols, "ell", Convert(0x2113));
            Add(symbols, "els", Convert(0x2A95));
            Add(symbols, "elsdot", Convert(0x2A97));
            Add(symbols, "emacr", Convert(0x0113));
            Add(symbols, "empty", Convert(0x2205));
            Add(symbols, "emptyset", Convert(0x2205));
            Add(symbols, "emptyv", Convert(0x2205));
            Add(symbols, "emsp", Convert(0x2003));
            Add(symbols, "emsp13", Convert(0x2004));
            Add(symbols, "emsp14", Convert(0x2005));
            Add(symbols, "eng", Convert(0x014B));
            Add(symbols, "ensp", Convert(0x2002));
            Add(symbols, "eogon", Convert(0x0119));
            Add(symbols, "eopf", Convert(0x1D556));
            Add(symbols, "epar", Convert(0x22D5));
            Add(symbols, "eparsl", Convert(0x29E3));
            Add(symbols, "eplus", Convert(0x2A71));
            Add(symbols, "epsi", Convert(0x03B5));
            Add(symbols, "epsilon", Convert(0x03B5));
            Add(symbols, "epsiv", Convert(0x03F5));
            Add(symbols, "eqcirc", Convert(0x2256));
            Add(symbols, "eqcolon", Convert(0x2255));
            Add(symbols, "eqsim", Convert(0x2242));
            Add(symbols, "eqslantgtr", Convert(0x2A96));
            Add(symbols, "eqslantless", Convert(0x2A95));
            Add(symbols, "equals", Convert(0x003D));
            Add(symbols, "equest", Convert(0x225F));
            Add(symbols, "equiv", Convert(0x2261));
            Add(symbols, "equivDD", Convert(0x2A78));
            Add(symbols, "eqvparsl", Convert(0x29E5));
            Add(symbols, "erarr", Convert(0x2971));
            Add(symbols, "erDot", Convert(0x2253));
            Add(symbols, "escr", Convert(0x212F));
            Add(symbols, "esdot", Convert(0x2250));
            Add(symbols, "esim", Convert(0x2242));
            Add(symbols, "eta", Convert(0x03B7));
            AddWeak(symbols, "eth", Convert(0x00F0));
            AddWeak(symbols, "euml", Convert(0x00EB));
            Add(symbols, "euro", Convert(0x20AC));
            Add(symbols, "excl", Convert(0x0021));
            Add(symbols, "exist", Convert(0x2203));
            Add(symbols, "expectation", Convert(0x2130));
            Add(symbols, "exponentiale", Convert(0x2147));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigE()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "Eacute", Convert(0x00C9));
            Add(symbols, "Ecaron", Convert(0x011A));
            AddWeak(symbols, "Ecirc", Convert(0x00CA));
            Add(symbols, "Ecy", Convert(0x042D));
            Add(symbols, "Edot", Convert(0x0116));
            Add(symbols, "Efr", Convert(0x1D508));
            AddWeak(symbols, "Egrave", Convert(0x00C8));
            Add(symbols, "Element", Convert(0x2208));
            Add(symbols, "Emacr", Convert(0x0112));
            Add(symbols, "EmptySmallSquare", Convert(0x25FB));
            Add(symbols, "EmptyVerySmallSquare", Convert(0x25AB));
            Add(symbols, "ENG", Convert(0x014A));
            Add(symbols, "Eogon", Convert(0x0118));
            Add(symbols, "Eopf", Convert(0x1D53C));
            Add(symbols, "Epsilon", Convert(0x0395));
            Add(symbols, "Equal", Convert(0x2A75));
            Add(symbols, "EqualTilde", Convert(0x2242));
            Add(symbols, "Equilibrium", Convert(0x21CC));
            Add(symbols, "Escr", Convert(0x2130));
            Add(symbols, "Esim", Convert(0x2A73));
            Add(symbols, "Eta", Convert(0x0397));
            AddWeak(symbols, "ETH", Convert(0x00D0));
            AddWeak(symbols, "Euml", Convert(0x00CB));
            Add(symbols, "Exists", Convert(0x2203));
            Add(symbols, "ExponentialE", Convert(0x2147));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleF()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "fallingdotseq", Convert(0x2252));
            Add(symbols, "fcy", Convert(0x0444));
            Add(symbols, "female", Convert(0x2640));
            Add(symbols, "ffilig", Convert(0xFB03));
            Add(symbols, "fflig", Convert(0xFB00));
            Add(symbols, "ffllig", Convert(0xFB04));
            Add(symbols, "ffr", Convert(0x1D523));
            Add(symbols, "filig", Convert(0xFB01));
            Add(symbols, "fjlig", Convert(0x0066, 0x006A));
            Add(symbols, "flat", Convert(0x266D));
            Add(symbols, "fllig", Convert(0xFB02));
            Add(symbols, "fltns", Convert(0x25B1));
            Add(symbols, "fnof", Convert(0x0192));
            Add(symbols, "fopf", Convert(0x1D557));
            Add(symbols, "forall", Convert(0x2200));
            Add(symbols, "fork", Convert(0x22D4));
            Add(symbols, "forkv", Convert(0x2AD9));
            Add(symbols, "fpartint", Convert(0x2A0D));
            AddWeak(symbols, "frac12", Convert(0x00BD));
            Add(symbols, "frac13", Convert(0x2153));
            AddWeak(symbols, "frac14", Convert(0x00BC));
            Add(symbols, "frac15", Convert(0x2155));
            Add(symbols, "frac16", Convert(0x2159));
            Add(symbols, "frac18", Convert(0x215B));
            Add(symbols, "frac23", Convert(0x2154));
            Add(symbols, "frac25", Convert(0x2156));
            AddWeak(symbols, "frac34", Convert(0x00BE));
            Add(symbols, "frac35", Convert(0x2157));
            Add(symbols, "frac38", Convert(0x215C));
            Add(symbols, "frac45", Convert(0x2158));
            Add(symbols, "frac56", Convert(0x215A));
            Add(symbols, "frac58", Convert(0x215D));
            Add(symbols, "frac78", Convert(0x215E));
            Add(symbols, "frasl", Convert(0x2044));
            Add(symbols, "frown", Convert(0x2322));
            Add(symbols, "fscr", Convert(0x1D4BB));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigF()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Fcy", Convert(0x0424));
            Add(symbols, "Ffr", Convert(0x1D509));
            Add(symbols, "FilledSmallSquare", Convert(0x25FC));
            Add(symbols, "FilledVerySmallSquare", Convert(0x25AA));
            Add(symbols, "Fopf", Convert(0x1D53D));
            Add(symbols, "ForAll", Convert(0x2200));
            Add(symbols, "Fouriertrf", Convert(0x2131));
            Add(symbols, "Fscr", Convert(0x2131));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleG()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "gacute", Convert(0x01F5));
            Add(symbols, "gamma", Convert(0x03B3));
            Add(symbols, "gammad", Convert(0x03DD));
            Add(symbols, "gap", Convert(0x2A86));
            Add(symbols, "gbreve", Convert(0x011F));
            Add(symbols, "gcirc", Convert(0x011D));
            Add(symbols, "gcy", Convert(0x0433));
            Add(symbols, "gdot", Convert(0x0121));
            Add(symbols, "gE", Convert(0x2267));
            Add(symbols, "ge", Convert(0x2265));
            Add(symbols, "gEl", Convert(0x2A8C));
            Add(symbols, "gel", Convert(0x22DB));
            Add(symbols, "geq", Convert(0x2265));
            Add(symbols, "geqq", Convert(0x2267));
            Add(symbols, "geqslant", Convert(0x2A7E));
            Add(symbols, "ges", Convert(0x2A7E));
            Add(symbols, "gescc", Convert(0x2AA9));
            Add(symbols, "gesdot", Convert(0x2A80));
            Add(symbols, "gesdoto", Convert(0x2A82));
            Add(symbols, "gesdotol", Convert(0x2A84));
            Add(symbols, "gesl", Convert(0x22DB, 0xFE00));
            Add(symbols, "gesles", Convert(0x2A94));
            Add(symbols, "gfr", Convert(0x1D524));
            Add(symbols, "gg", Convert(0x226B));
            Add(symbols, "ggg", Convert(0x22D9));
            Add(symbols, "gimel", Convert(0x2137));
            Add(symbols, "gjcy", Convert(0x0453));
            Add(symbols, "gl", Convert(0x2277));
            Add(symbols, "gla", Convert(0x2AA5));
            Add(symbols, "glE", Convert(0x2A92));
            Add(symbols, "glj", Convert(0x2AA4));
            Add(symbols, "gnap", Convert(0x2A8A));
            Add(symbols, "gnapprox", Convert(0x2A8A));
            Add(symbols, "gnE", Convert(0x2269));
            Add(symbols, "gne", Convert(0x2A88));
            Add(symbols, "gneq", Convert(0x2A88));
            Add(symbols, "gneqq", Convert(0x2269));
            Add(symbols, "gnsim", Convert(0x22E7));
            Add(symbols, "gopf", Convert(0x1D558));
            Add(symbols, "grave", Convert(0x0060));
            Add(symbols, "gscr", Convert(0x210A));
            Add(symbols, "gsim", Convert(0x2273));
            Add(symbols, "gsime", Convert(0x2A8E));
            Add(symbols, "gsiml", Convert(0x2A90));
            AddWeak(symbols, "gt", Convert(0x003E));
            Add(symbols, "gtcc", Convert(0x2AA7));
            Add(symbols, "gtcir", Convert(0x2A7A));
            Add(symbols, "gtdot", Convert(0x22D7));
            Add(symbols, "gtlPar", Convert(0x2995));
            Add(symbols, "gtquest", Convert(0x2A7C));
            Add(symbols, "gtrapprox", Convert(0x2A86));
            Add(symbols, "gtrarr", Convert(0x2978));
            Add(symbols, "gtrdot", Convert(0x22D7));
            Add(symbols, "gtreqless", Convert(0x22DB));
            Add(symbols, "gtreqqless", Convert(0x2A8C));
            Add(symbols, "gtrless", Convert(0x2277));
            Add(symbols, "gtrsim", Convert(0x2273));
            Add(symbols, "gvertneqq", Convert(0x2269, 0xFE00));
            Add(symbols, "gvnE", Convert(0x2269, 0xFE00));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigG()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Gamma", Convert(0x0393));
            Add(symbols, "Gammad", Convert(0x03DC));
            Add(symbols, "Gbreve", Convert(0x011E));
            Add(symbols, "Gcedil", Convert(0x0122));
            Add(symbols, "Gcirc", Convert(0x011C));
            Add(symbols, "Gcy", Convert(0x0413));
            Add(symbols, "Gdot", Convert(0x0120));
            Add(symbols, "Gfr", Convert(0x1D50A));
            Add(symbols, "Gg", Convert(0x22D9));
            Add(symbols, "GJcy", Convert(0x0403));
            Add(symbols, "Gopf", Convert(0x1D53E));
            Add(symbols, "GreaterEqual", Convert(0x2265));
            Add(symbols, "GreaterEqualLess", Convert(0x22DB));
            Add(symbols, "GreaterFullEqual", Convert(0x2267));
            Add(symbols, "GreaterGreater", Convert(0x2AA2));
            Add(symbols, "GreaterLess", Convert(0x2277));
            Add(symbols, "GreaterSlantEqual", Convert(0x2A7E));
            Add(symbols, "GreaterTilde", Convert(0x2273));
            Add(symbols, "Gscr", Convert(0x1D4A2));
            AddWeak(symbols, "GT", Convert(0x003E));
            Add(symbols, "Gt", Convert(0x226B));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleH()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "hairsp", Convert(0x200A));
            Add(symbols, "half", Convert(0x00BD));
            Add(symbols, "hamilt", Convert(0x210B));
            Add(symbols, "hardcy", Convert(0x044A));
            Add(symbols, "hArr", Convert(0x21D4));
            Add(symbols, "harr", Convert(0x2194));
            Add(symbols, "harrcir", Convert(0x2948));
            Add(symbols, "harrw", Convert(0x21AD));
            Add(symbols, "hbar", Convert(0x210F));
            Add(symbols, "hcirc", Convert(0x0125));
            Add(symbols, "hearts", Convert(0x2665));
            Add(symbols, "heartsuit", Convert(0x2665));
            Add(symbols, "hellip", Convert(0x2026));
            Add(symbols, "hercon", Convert(0x22B9));
            Add(symbols, "hfr", Convert(0x1D525));
            Add(symbols, "hksearow", Convert(0x2925));
            Add(symbols, "hkswarow", Convert(0x2926));
            Add(symbols, "hoarr", Convert(0x21FF));
            Add(symbols, "homtht", Convert(0x223B));
            Add(symbols, "hookleftarrow", Convert(0x21A9));
            Add(symbols, "hookrightarrow", Convert(0x21AA));
            Add(symbols, "hopf", Convert(0x1D559));
            Add(symbols, "horbar", Convert(0x2015));
            Add(symbols, "hscr", Convert(0x1D4BD));
            Add(symbols, "hslash", Convert(0x210F));
            Add(symbols, "hstrok", Convert(0x0127));
            Add(symbols, "hybull", Convert(0x2043));
            Add(symbols, "hyphen", Convert(0x2010));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigH()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Hacek", Convert(0x02C7));
            Add(symbols, "HARDcy", Convert(0x042A));
            Add(symbols, "Hat", Convert(0x005E));
            Add(symbols, "Hcirc", Convert(0x0124));
            Add(symbols, "Hfr", Convert(0x210C));
            Add(symbols, "HilbertSpace", Convert(0x210B));
            Add(symbols, "Hopf", Convert(0x210D));
            Add(symbols, "HorizontalLine", Convert(0x2500));
            Add(symbols, "Hscr", Convert(0x210B));
            Add(symbols, "Hstrok", Convert(0x0126));
            Add(symbols, "HumpDownHump", Convert(0x224E));
            Add(symbols, "HumpEqual", Convert(0x224F));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleI()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "iacute", Convert(0x00ED));
            Add(symbols, "ic", Convert(0x2063));
            AddWeak(symbols, "icirc", Convert(0x00EE));
            Add(symbols, "icy", Convert(0x0438));
            Add(symbols, "iecy", Convert(0x0435));
            AddWeak(symbols, "iexcl", Convert(0x00A1));
            Add(symbols, "iff", Convert(0x21D4));
            Add(symbols, "ifr", Convert(0x1D526));
            AddWeak(symbols, "igrave", Convert(0x00EC));
            Add(symbols, "ii", Convert(0x2148));
            Add(symbols, "iiiint", Convert(0x2A0C));
            Add(symbols, "iiint", Convert(0x222D));
            Add(symbols, "iinfin", Convert(0x29DC));
            Add(symbols, "iiota", Convert(0x2129));
            Add(symbols, "ijlig", Convert(0x0133));
            Add(symbols, "imacr", Convert(0x012B));
            Add(symbols, "image", Convert(0x2111));
            Add(symbols, "imagline", Convert(0x2110));
            Add(symbols, "imagpart", Convert(0x2111));
            Add(symbols, "imath", Convert(0x0131));
            Add(symbols, "imof", Convert(0x22B7));
            Add(symbols, "imped", Convert(0x01B5));
            Add(symbols, "in", Convert(0x2208));
            Add(symbols, "incare", Convert(0x2105));
            Add(symbols, "infin", Convert(0x221E));
            Add(symbols, "infintie", Convert(0x29DD));
            Add(symbols, "inodot", Convert(0x0131));
            Add(symbols, "int", Convert(0x222B));
            Add(symbols, "intcal", Convert(0x22BA));
            Add(symbols, "integers", Convert(0x2124));
            Add(symbols, "intercal", Convert(0x22BA));
            Add(symbols, "intlarhk", Convert(0x2A17));
            Add(symbols, "intprod", Convert(0x2A3C));
            Add(symbols, "iocy", Convert(0x0451));
            Add(symbols, "iogon", Convert(0x012F));
            Add(symbols, "iopf", Convert(0x1D55A));
            Add(symbols, "iota", Convert(0x03B9));
            Add(symbols, "iprod", Convert(0x2A3C));
            AddWeak(symbols, "iquest", Convert(0x00BF));
            Add(symbols, "iscr", Convert(0x1D4BE));
            Add(symbols, "isin", Convert(0x2208));
            Add(symbols, "isindot", Convert(0x22F5));
            Add(symbols, "isinE", Convert(0x22F9));
            Add(symbols, "isins", Convert(0x22F4));
            Add(symbols, "isinsv", Convert(0x22F3));
            Add(symbols, "isinv", Convert(0x2208));
            Add(symbols, "it", Convert(0x2062));
            Add(symbols, "itilde", Convert(0x0129));
            Add(symbols, "iukcy", Convert(0x0456));
            AddWeak(symbols, "iuml", Convert(0x00EF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigI()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "Iacute", Convert(0x00CD));
            AddWeak(symbols, "Icirc", Convert(0x00CE));
            Add(symbols, "Icy", Convert(0x0418));
            Add(symbols, "Idot", Convert(0x0130));
            Add(symbols, "IEcy", Convert(0x0415));
            Add(symbols, "Ifr", Convert(0x2111));
            AddWeak(symbols, "Igrave", Convert(0x00CC));
            Add(symbols, "IJlig", Convert(0x0132));
            Add(symbols, "Im", Convert(0x2111));
            Add(symbols, "Imacr", Convert(0x012A));
            Add(symbols, "ImaginaryI", Convert(0x2148));
            Add(symbols, "Implies", Convert(0x21D2));
            Add(symbols, "Int", Convert(0x222C));
            Add(symbols, "Integral", Convert(0x222B));
            Add(symbols, "Intersection", Convert(0x22C2));
            Add(symbols, "InvisibleComma", Convert(0x2063));
            Add(symbols, "InvisibleTimes", Convert(0x2062));
            Add(symbols, "IOcy", Convert(0x0401));
            Add(symbols, "Iogon", Convert(0x012E));
            Add(symbols, "Iopf", Convert(0x1D540));
            Add(symbols, "Iota", Convert(0x0399));
            Add(symbols, "Iscr", Convert(0x2110));
            Add(symbols, "Itilde", Convert(0x0128));
            Add(symbols, "Iukcy", Convert(0x0406));
            AddWeak(symbols, "Iuml", Convert(0x00CF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleJ()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "jcirc", Convert(0x0135));
            Add(symbols, "jcy", Convert(0x0439));
            Add(symbols, "jfr", Convert(0x1D527));
            Add(symbols, "jmath", Convert(0x0237));
            Add(symbols, "jopf", Convert(0x1D55B));
            Add(symbols, "jscr", Convert(0x1D4BF));
            Add(symbols, "jsercy", Convert(0x0458));
            Add(symbols, "jukcy", Convert(0x0454));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigJ()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Jcirc", Convert(0x0134));
            Add(symbols, "Jcy", Convert(0x0419));
            Add(symbols, "Jfr", Convert(0x1D50D));
            Add(symbols, "Jopf", Convert(0x1D541));
            Add(symbols, "Jscr", Convert(0x1D4A5));
            Add(symbols, "Jsercy", Convert(0x0408));
            Add(symbols, "Jukcy", Convert(0x0404));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleK()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "kappa", Convert(0x03BA));
            Add(symbols, "kappav", Convert(0x03F0));
            Add(symbols, "kcedil", Convert(0x0137));
            Add(symbols, "kcy", Convert(0x043A));
            Add(symbols, "kfr", Convert(0x1D528));
            Add(symbols, "kgreen", Convert(0x0138));
            Add(symbols, "khcy", Convert(0x0445));
            Add(symbols, "kjcy", Convert(0x045C));
            Add(symbols, "kopf", Convert(0x1D55C));
            Add(symbols, "kscr", Convert(0x1D4C0));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigK()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Kappa", Convert(0x039A));
            Add(symbols, "Kcedil", Convert(0x0136));
            Add(symbols, "Kcy", Convert(0x041A));
            Add(symbols, "Kfr", Convert(0x1D50E));
            Add(symbols, "KHcy", Convert(0x0425));
            Add(symbols, "KJcy", Convert(0x040C));
            Add(symbols, "Kopf", Convert(0x1D542));
            Add(symbols, "Kscr", Convert(0x1D4A6));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleL()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "lAarr", Convert(0x21DA));
            Add(symbols, "lacute", Convert(0x013A));
            Add(symbols, "laemptyv", Convert(0x29B4));
            Add(symbols, "lagran", Convert(0x2112));
            Add(symbols, "lambda", Convert(0x03BB));
            Add(symbols, "lang", Convert(0x27E8));
            Add(symbols, "langd", Convert(0x2991));
            Add(symbols, "langle", Convert(0x27E8));
            Add(symbols, "lap", Convert(0x2A85));
            AddWeak(symbols, "laquo", Convert(0x00AB));
            Add(symbols, "lArr", Convert(0x21D0));
            Add(symbols, "larr", Convert(0x2190));
            Add(symbols, "larrb", Convert(0x21E4));
            Add(symbols, "larrbfs", Convert(0x291F));
            Add(symbols, "larrfs", Convert(0x291D));
            Add(symbols, "larrhk", Convert(0x21A9));
            Add(symbols, "larrlp", Convert(0x21AB));
            Add(symbols, "larrpl", Convert(0x2939));
            Add(symbols, "larrsim", Convert(0x2973));
            Add(symbols, "larrtl", Convert(0x21A2));
            Add(symbols, "lat", Convert(0x2AAB));
            Add(symbols, "lAtail", Convert(0x291B));
            Add(symbols, "latail", Convert(0x2919));
            Add(symbols, "late", Convert(0x2AAD));
            Add(symbols, "lates", Convert(0x2AAD, 0xFE00));
            Add(symbols, "lBarr", Convert(0x290E));
            Add(symbols, "lbarr", Convert(0x290C));
            Add(symbols, "lbbrk", Convert(0x2772));
            Add(symbols, "lbrace", Convert(0x007B));
            Add(symbols, "lbrack", Convert(0x005B));
            Add(symbols, "lbrke", Convert(0x298B));
            Add(symbols, "lbrksld", Convert(0x298F));
            Add(symbols, "lbrkslu", Convert(0x298D));
            Add(symbols, "lcaron", Convert(0x013E));
            Add(symbols, "lcedil", Convert(0x013C));
            Add(symbols, "lceil", Convert(0x2308));
            Add(symbols, "lcub", Convert(0x007B));
            Add(symbols, "lcy", Convert(0x043B));
            Add(symbols, "ldca", Convert(0x2936));
            Add(symbols, "ldquo", Convert(0x201C));
            Add(symbols, "ldquor", Convert(0x201E));
            Add(symbols, "ldrdhar", Convert(0x2967));
            Add(symbols, "ldrushar", Convert(0x294B));
            Add(symbols, "ldsh", Convert(0x21B2));
            Add(symbols, "lE", Convert(0x2266));
            Add(symbols, "le", Convert(0x2264));
            Add(symbols, "leftarrow", Convert(0x2190));
            Add(symbols, "leftarrowtail", Convert(0x21A2));
            Add(symbols, "leftharpoondown", Convert(0x21BD));
            Add(symbols, "leftharpoonup", Convert(0x21BC));
            Add(symbols, "leftleftarrows", Convert(0x21C7));
            Add(symbols, "leftrightarrow", Convert(0x2194));
            Add(symbols, "leftrightarrows", Convert(0x21C6));
            Add(symbols, "leftrightharpoons", Convert(0x21CB));
            Add(symbols, "leftrightsquigarrow", Convert(0x21AD));
            Add(symbols, "leftthreetimes", Convert(0x22CB));
            Add(symbols, "lEg", Convert(0x2A8B));
            Add(symbols, "leg", Convert(0x22DA));
            Add(symbols, "leq", Convert(0x2264));
            Add(symbols, "leqq", Convert(0x2266));
            Add(symbols, "leqslant", Convert(0x2A7D));
            Add(symbols, "les", Convert(0x2A7D));
            Add(symbols, "lescc", Convert(0x2AA8));
            Add(symbols, "lesdot", Convert(0x2A7F));
            Add(symbols, "lesdoto", Convert(0x2A81));
            Add(symbols, "lesdotor", Convert(0x2A83));
            Add(symbols, "lesg", Convert(0x22DA, 0xFE00));
            Add(symbols, "lesges", Convert(0x2A93));
            Add(symbols, "lessapprox", Convert(0x2A85));
            Add(symbols, "lessdot", Convert(0x22D6));
            Add(symbols, "lesseqgtr", Convert(0x22DA));
            Add(symbols, "lesseqqgtr", Convert(0x2A8B));
            Add(symbols, "lessgtr", Convert(0x2276));
            Add(symbols, "lesssim", Convert(0x2272));
            Add(symbols, "lfisht", Convert(0x297C));
            Add(symbols, "lfloor", Convert(0x230A));
            Add(symbols, "lfr", Convert(0x1D529));
            Add(symbols, "lg", Convert(0x2276));
            Add(symbols, "lgE", Convert(0x2A91));
            Add(symbols, "lHar", Convert(0x2962));
            Add(symbols, "lhard", Convert(0x21BD));
            Add(symbols, "lharu", Convert(0x21BC));
            Add(symbols, "lharul", Convert(0x296A));
            Add(symbols, "lhblk", Convert(0x2584));
            Add(symbols, "ljcy", Convert(0x0459));
            Add(symbols, "ll", Convert(0x226A));
            Add(symbols, "llarr", Convert(0x21C7));
            Add(symbols, "llcorner", Convert(0x231E));
            Add(symbols, "llhard", Convert(0x296B));
            Add(symbols, "lltri", Convert(0x25FA));
            Add(symbols, "lmidot", Convert(0x0140));
            Add(symbols, "lmoust", Convert(0x23B0));
            Add(symbols, "lmoustache", Convert(0x23B0));
            Add(symbols, "lnap", Convert(0x2A89));
            Add(symbols, "lnapprox", Convert(0x2A89));
            Add(symbols, "lnE", Convert(0x2268));
            Add(symbols, "lne", Convert(0x2A87));
            Add(symbols, "lneq", Convert(0x2A87));
            Add(symbols, "lneqq", Convert(0x2268));
            Add(symbols, "lnsim", Convert(0x22E6));
            Add(symbols, "loang", Convert(0x27EC));
            Add(symbols, "loarr", Convert(0x21FD));
            Add(symbols, "lobrk", Convert(0x27E6));
            Add(symbols, "longleftarrow", Convert(0x27F5));
            Add(symbols, "longleftrightarrow", Convert(0x27F7));
            Add(symbols, "longmapsto", Convert(0x27FC));
            Add(symbols, "longrightarrow", Convert(0x27F6));
            Add(symbols, "looparrowleft", Convert(0x21AB));
            Add(symbols, "looparrowright", Convert(0x21AC));
            Add(symbols, "lopar", Convert(0x2985));
            Add(symbols, "lopf", Convert(0x1D55D));
            Add(symbols, "loplus", Convert(0x2A2D));
            Add(symbols, "lotimes", Convert(0x2A34));
            Add(symbols, "lowast", Convert(0x2217));
            Add(symbols, "lowbar", Convert(0x005F));
            Add(symbols, "loz", Convert(0x25CA));
            Add(symbols, "lozenge", Convert(0x25CA));
            Add(symbols, "lozf", Convert(0x29EB));
            Add(symbols, "lpar", Convert(0x0028));
            Add(symbols, "lparlt", Convert(0x2993));
            Add(symbols, "lrarr", Convert(0x21C6));
            Add(symbols, "lrcorner", Convert(0x231F));
            Add(symbols, "lrhar", Convert(0x21CB));
            Add(symbols, "lrhard", Convert(0x296D));
            Add(symbols, "lrm", Convert(0x200E));
            Add(symbols, "lrtri", Convert(0x22BF));
            Add(symbols, "lsaquo", Convert(0x2039));
            Add(symbols, "lscr", Convert(0x1D4C1));
            Add(symbols, "lsh", Convert(0x21B0));
            Add(symbols, "lsim", Convert(0x2272));
            Add(symbols, "lsime", Convert(0x2A8D));
            Add(symbols, "lsimg", Convert(0x2A8F));
            Add(symbols, "lsqb", Convert(0x005B));
            Add(symbols, "lsquo", Convert(0x2018));
            Add(symbols, "lsquor", Convert(0x201A));
            Add(symbols, "lstrok", Convert(0x0142));
            AddWeak(symbols, "lt", Convert(0x003C));
            Add(symbols, "ltcc", Convert(0x2AA6));
            Add(symbols, "ltcir", Convert(0x2A79));
            Add(symbols, "ltdot", Convert(0x22D6));
            Add(symbols, "lthree", Convert(0x22CB));
            Add(symbols, "ltimes", Convert(0x22C9));
            Add(symbols, "ltlarr", Convert(0x2976));
            Add(symbols, "ltquest", Convert(0x2A7B));
            Add(symbols, "ltri", Convert(0x25C3));
            Add(symbols, "ltrie", Convert(0x22B4));
            Add(symbols, "ltrif", Convert(0x25C2));
            Add(symbols, "ltrPar", Convert(0x2996));
            Add(symbols, "lurdshar", Convert(0x294A));
            Add(symbols, "luruhar", Convert(0x2966));
            Add(symbols, "lvertneqq", Convert(0x2268, 0xFE00));
            Add(symbols, "lvnE", Convert(0x2268, 0xFE00));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigL()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Lacute", Convert(0x0139));
            Add(symbols, "Lambda", Convert(0x039B));
            Add(symbols, "Lang", Convert(0x27EA));
            Add(symbols, "Laplacetrf", Convert(0x2112));
            Add(symbols, "Larr", Convert(0x219E));
            Add(symbols, "Lcaron", Convert(0x013D));
            Add(symbols, "Lcedil", Convert(0x013B));
            Add(symbols, "Lcy", Convert(0x041B));
            Add(symbols, "LeftAngleBracket", Convert(0x27E8));
            Add(symbols, "LeftArrow", Convert(0x2190));
            Add(symbols, "Leftarrow", Convert(0x21D0));
            Add(symbols, "LeftArrowBar", Convert(0x21E4));
            Add(symbols, "LeftArrowRightArrow", Convert(0x21C6));
            Add(symbols, "LeftCeiling", Convert(0x2308));
            Add(symbols, "LeftDoubleBracket", Convert(0x27E6));
            Add(symbols, "LeftDownTeeVector", Convert(0x2961));
            Add(symbols, "LeftDownVector", Convert(0x21C3));
            Add(symbols, "LeftDownVectorBar", Convert(0x2959));
            Add(symbols, "LeftFloor", Convert(0x230A));
            Add(symbols, "LeftRightArrow", Convert(0x2194));
            Add(symbols, "Leftrightarrow", Convert(0x21D4));
            Add(symbols, "LeftRightVector", Convert(0x294E));
            Add(symbols, "LeftTee", Convert(0x22A3));
            Add(symbols, "LeftTeeArrow", Convert(0x21A4));
            Add(symbols, "LeftTeeVector", Convert(0x295A));
            Add(symbols, "LeftTriangle", Convert(0x22B2));
            Add(symbols, "LeftTriangleBar", Convert(0x29CF));
            Add(symbols, "LeftTriangleEqual", Convert(0x22B4));
            Add(symbols, "LeftUpDownVector", Convert(0x2951));
            Add(symbols, "LeftUpTeeVector", Convert(0x2960));
            Add(symbols, "LeftUpVector", Convert(0x21BF));
            Add(symbols, "LeftUpVectorBar", Convert(0x2958));
            Add(symbols, "LeftVector", Convert(0x21BC));
            Add(symbols, "LeftVectorBar", Convert(0x2952));
            Add(symbols, "LessEqualGreater", Convert(0x22DA));
            Add(symbols, "LessFullEqual", Convert(0x2266));
            Add(symbols, "LessGreater", Convert(0x2276));
            Add(symbols, "LessLess", Convert(0x2AA1));
            Add(symbols, "LessSlantEqual", Convert(0x2A7D));
            Add(symbols, "LessTilde", Convert(0x2272));
            Add(symbols, "Lfr", Convert(0x1D50F));
            Add(symbols, "LJcy", Convert(0x0409));
            Add(symbols, "Ll", Convert(0x22D8));
            Add(symbols, "Lleftarrow", Convert(0x21DA));
            Add(symbols, "Lmidot", Convert(0x013F));
            Add(symbols, "LongLeftArrow", Convert(0x27F5));
            Add(symbols, "Longleftarrow", Convert(0x27F8));
            Add(symbols, "LongLeftRightArrow", Convert(0x27F7));
            Add(symbols, "Longleftrightarrow", Convert(0x27FA));
            Add(symbols, "LongRightArrow", Convert(0x27F6));
            Add(symbols, "Longrightarrow", Convert(0x27F9));
            Add(symbols, "Lopf", Convert(0x1D543));
            Add(symbols, "LowerLeftArrow", Convert(0x2199));
            Add(symbols, "LowerRightArrow", Convert(0x2198));
            Add(symbols, "Lscr", Convert(0x2112));
            Add(symbols, "Lsh", Convert(0x21B0));
            Add(symbols, "Lstrok", Convert(0x0141));
            AddWeak(symbols, "LT", Convert(0x003C));
            Add(symbols, "Lt", Convert(0x226A));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleM()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "macr", Convert(0x00AF));
            Add(symbols, "male", Convert(0x2642));
            Add(symbols, "malt", Convert(0x2720));
            Add(symbols, "maltese", Convert(0x2720));
            Add(symbols, "map", Convert(0x21A6));
            Add(symbols, "mapsto", Convert(0x21A6));
            Add(symbols, "mapstodown", Convert(0x21A7));
            Add(symbols, "mapstoleft", Convert(0x21A4));
            Add(symbols, "mapstoup", Convert(0x21A5));
            Add(symbols, "marker", Convert(0x25AE));
            Add(symbols, "mcomma", Convert(0x2A29));
            Add(symbols, "mcy", Convert(0x043C));
            Add(symbols, "mdash", Convert(0x2014));
            Add(symbols, "mDDot", Convert(0x223A));
            Add(symbols, "measuredangle", Convert(0x2221));
            Add(symbols, "mfr", Convert(0x1D52A));
            Add(symbols, "mho", Convert(0x2127));
            AddWeak(symbols, "micro", Convert(0x00B5));
            Add(symbols, "mid", Convert(0x2223));
            Add(symbols, "midast", Convert(0x002A));
            Add(symbols, "midcir", Convert(0x2AF0));
            AddWeak(symbols, "middot", Convert(0x00B7));
            Add(symbols, "minus", Convert(0x2212));
            Add(symbols, "minusb", Convert(0x229F));
            Add(symbols, "minusd", Convert(0x2238));
            Add(symbols, "minusdu", Convert(0x2A2A));
            Add(symbols, "mlcp", Convert(0x2ADB));
            Add(symbols, "mldr", Convert(0x2026));
            Add(symbols, "mnplus", Convert(0x2213));
            Add(symbols, "models", Convert(0x22A7));
            Add(symbols, "mopf", Convert(0x1D55E));
            Add(symbols, "mp", Convert(0x2213));
            Add(symbols, "mscr", Convert(0x1D4C2));
            Add(symbols, "mstpos", Convert(0x223E));
            Add(symbols, "mu", Convert(0x03BC));
            Add(symbols, "multimap", Convert(0x22B8));
            Add(symbols, "mumap", Convert(0x22B8));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigM()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Map", Convert(0x2905));
            Add(symbols, "Mcy", Convert(0x041C));
            Add(symbols, "MediumSpace", Convert(0x205F));
            Add(symbols, "Mellintrf", Convert(0x2133));
            Add(symbols, "Mfr", Convert(0x1D510));
            Add(symbols, "MinusPlus", Convert(0x2213));
            Add(symbols, "Mopf", Convert(0x1D544));
            Add(symbols, "Mscr", Convert(0x2133));
            Add(symbols, "Mu", Convert(0x039C));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleN()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "nabla", Convert(0x2207));
            Add(symbols, "nacute", Convert(0x0144));
            Add(symbols, "nang", Convert(0x2220, 0x20D2));
            Add(symbols, "nap", Convert(0x2249));
            Add(symbols, "napE", Convert(0x2A70, 0x0338));
            Add(symbols, "napid", Convert(0x224B, 0x0338));
            Add(symbols, "napos", Convert(0x0149));
            Add(symbols, "napprox", Convert(0x2249));
            Add(symbols, "natur", Convert(0x266E));
            Add(symbols, "natural", Convert(0x266E));
            Add(symbols, "naturals", Convert(0x2115));
            AddWeak(symbols, "nbsp", Convert(0x00A0));
            Add(symbols, "nbump", Convert(0x224E, 0x0338));
            Add(symbols, "nbumpe", Convert(0x224F, 0x0338));
            Add(symbols, "ncap", Convert(0x2A43));
            Add(symbols, "ncaron", Convert(0x0148));
            Add(symbols, "ncedil", Convert(0x0146));
            Add(symbols, "ncong", Convert(0x2247));
            Add(symbols, "ncongdot", Convert(0x2A6D, 0x0338));
            Add(symbols, "ncup", Convert(0x2A42));
            Add(symbols, "ncy", Convert(0x043D));
            Add(symbols, "ndash", Convert(0x2013));
            Add(symbols, "ne", Convert(0x2260));
            Add(symbols, "nearhk", Convert(0x2924));
            Add(symbols, "neArr", Convert(0x21D7));
            Add(symbols, "nearr", Convert(0x2197));
            Add(symbols, "nearrow", Convert(0x2197));
            Add(symbols, "nedot", Convert(0x2250, 0x0338));
            Add(symbols, "nequiv", Convert(0x2262));
            Add(symbols, "nesear", Convert(0x2928));
            Add(symbols, "nesim", Convert(0x2242, 0x0338));
            Add(symbols, "nexist", Convert(0x2204));
            Add(symbols, "nexists", Convert(0x2204));
            Add(symbols, "nfr", Convert(0x1D52B));
            Add(symbols, "ngE", Convert(0x2267, 0x0338));
            Add(symbols, "nge", Convert(0x2271));
            Add(symbols, "ngeq", Convert(0x2271));
            Add(symbols, "ngeqq", Convert(0x2267, 0x0338));
            Add(symbols, "ngeqslant", Convert(0x2A7E, 0x0338));
            Add(symbols, "nges", Convert(0x2A7E, 0x0338));
            Add(symbols, "nGg", Convert(0x22D9, 0x0338));
            Add(symbols, "ngsim", Convert(0x2275));
            Add(symbols, "nGt", Convert(0x226B, 0x20D2));
            Add(symbols, "ngt", Convert(0x226F));
            Add(symbols, "ngtr", Convert(0x226F));
            Add(symbols, "nGtv", Convert(0x226B, 0x0338));
            Add(symbols, "nhArr", Convert(0x21CE));
            Add(symbols, "nharr", Convert(0x21AE));
            Add(symbols, "nhpar", Convert(0x2AF2));
            Add(symbols, "ni", Convert(0x220B));
            Add(symbols, "nis", Convert(0x22FC));
            Add(symbols, "nisd", Convert(0x22FA));
            Add(symbols, "niv", Convert(0x220B));
            Add(symbols, "njcy", Convert(0x045A));
            Add(symbols, "nlArr", Convert(0x21CD));
            Add(symbols, "nlarr", Convert(0x219A));
            Add(symbols, "nldr", Convert(0x2025));
            Add(symbols, "nlE", Convert(0x2266, 0x0338));
            Add(symbols, "nle", Convert(0x2270));
            Add(symbols, "nLeftarrow", Convert(0x21CD));
            Add(symbols, "nleftarrow", Convert(0x219A));
            Add(symbols, "nLeftrightarrow", Convert(0x21CE));
            Add(symbols, "nleftrightarrow", Convert(0x21AE));
            Add(symbols, "nleq", Convert(0x2270));
            Add(symbols, "nleqq", Convert(0x2266, 0x0338));
            Add(symbols, "nleqslant", Convert(0x2A7D, 0x0338));
            Add(symbols, "nles", Convert(0x2A7D, 0x0338));
            Add(symbols, "nless", Convert(0x226E));
            Add(symbols, "nLl", Convert(0x22D8, 0x0338));
            Add(symbols, "nlsim", Convert(0x2274));
            Add(symbols, "nLt", Convert(0x226A, 0x20D2));
            Add(symbols, "nlt", Convert(0x226E));
            Add(symbols, "nltri", Convert(0x22EA));
            Add(symbols, "nltrie", Convert(0x22EC));
            Add(symbols, "nLtv", Convert(0x226A, 0x0338));
            Add(symbols, "nmid", Convert(0x2224));
            Add(symbols, "nopf", Convert(0x1D55F));
            AddWeak(symbols, "not", Convert(0x00AC));
            Add(symbols, "notin", Convert(0x2209));
            Add(symbols, "notindot", Convert(0x22F5, 0x0338));
            Add(symbols, "notinE", Convert(0x22F9, 0x0338));
            Add(symbols, "notinva", Convert(0x2209));
            Add(symbols, "notinvb", Convert(0x22F7));
            Add(symbols, "notinvc", Convert(0x22F6));
            Add(symbols, "notni", Convert(0x220C));
            Add(symbols, "notniva", Convert(0x220C));
            Add(symbols, "notnivb", Convert(0x22FE));
            Add(symbols, "notnivc", Convert(0x22FD));
            Add(symbols, "npar", Convert(0x2226));
            Add(symbols, "nparallel", Convert(0x2226));
            Add(symbols, "nparsl", Convert(0x2AFD, 0x20E5));
            Add(symbols, "npart", Convert(0x2202, 0x0338));
            Add(symbols, "npolint", Convert(0x2A14));
            Add(symbols, "npr", Convert(0x2280));
            Add(symbols, "nprcue", Convert(0x22E0));
            Add(symbols, "npre", Convert(0x2AAF, 0x0338));
            Add(symbols, "nprec", Convert(0x2280));
            Add(symbols, "npreceq", Convert(0x2AAF, 0x0338));
            Add(symbols, "nrArr", Convert(0x21CF));
            Add(symbols, "nrarr", Convert(0x219B));
            Add(symbols, "nrarrc", Convert(0x2933, 0x0338));
            Add(symbols, "nrarrw", Convert(0x219D, 0x0338));
            Add(symbols, "nRightarrow", Convert(0x21CF));
            Add(symbols, "nrightarrow", Convert(0x219B));
            Add(symbols, "nrtri", Convert(0x22EB));
            Add(symbols, "nrtrie", Convert(0x22ED));
            Add(symbols, "nsc", Convert(0x2281));
            Add(symbols, "nsccue", Convert(0x22E1));
            Add(symbols, "nsce", Convert(0x2AB0, 0x0338));
            Add(symbols, "nscr", Convert(0x1D4C3));
            Add(symbols, "nshortmid", Convert(0x2224));
            Add(symbols, "nshortparallel", Convert(0x2226));
            Add(symbols, "nsim", Convert(0x2241));
            Add(symbols, "nsime", Convert(0x2244));
            Add(symbols, "nsimeq", Convert(0x2244));
            Add(symbols, "nsmid", Convert(0x2224));
            Add(symbols, "nspar", Convert(0x2226));
            Add(symbols, "nsqsube", Convert(0x22E2));
            Add(symbols, "nsqsupe", Convert(0x22E3));
            Add(symbols, "nsub", Convert(0x2284));
            Add(symbols, "nsubE", Convert(0x2AC5, 0x0338));
            Add(symbols, "nsube", Convert(0x2288));
            Add(symbols, "nsubset", Convert(0x2282, 0x20D2));
            Add(symbols, "nsubseteq", Convert(0x2288));
            Add(symbols, "nsubseteqq", Convert(0x2AC5, 0x0338));
            Add(symbols, "nsucc", Convert(0x2281));
            Add(symbols, "nsucceq", Convert(0x2AB0, 0x0338));
            Add(symbols, "nsup", Convert(0x2285));
            Add(symbols, "nsupE", Convert(0x2AC6, 0x0338));
            Add(symbols, "nsupe", Convert(0x2289));
            Add(symbols, "nsupset", Convert(0x2283, 0x20D2));
            Add(symbols, "nsupseteq", Convert(0x2289));
            Add(symbols, "nsupseteqq", Convert(0x2AC6, 0x0338));
            Add(symbols, "ntgl", Convert(0x2279));
            AddWeak(symbols, "ntilde", Convert(0x00F1));
            Add(symbols, "ntlg", Convert(0x2278));
            Add(symbols, "ntriangleleft", Convert(0x22EA));
            Add(symbols, "ntrianglelefteq", Convert(0x22EC));
            Add(symbols, "ntriangleright", Convert(0x22EB));
            Add(symbols, "ntrianglerighteq", Convert(0x22ED));
            Add(symbols, "nu", Convert(0x03BD));
            Add(symbols, "num", Convert(0x0023));
            Add(symbols, "numero", Convert(0x2116));
            Add(symbols, "numsp", Convert(0x2007));
            Add(symbols, "nvap", Convert(0x224D, 0x20D2));
            Add(symbols, "nVDash", Convert(0x22AF));
            Add(symbols, "nVdash", Convert(0x22AE));
            Add(symbols, "nvDash", Convert(0x22AD));
            Add(symbols, "nvdash", Convert(0x22AC));
            Add(symbols, "nvge", Convert(0x2265, 0x20D2));
            Add(symbols, "nvgt", Convert(0x003E, 0x20D2));
            Add(symbols, "nvHarr", Convert(0x2904));
            Add(symbols, "nvinfin", Convert(0x29DE));
            Add(symbols, "nvlArr", Convert(0x2902));
            Add(symbols, "nvle", Convert(0x2264, 0x20D2));
            Add(symbols, "nvlt", Convert(0x003C, 0x20D2));
            Add(symbols, "nvltrie", Convert(0x22B4, 0x20D2));
            Add(symbols, "nvrArr", Convert(0x2903));
            Add(symbols, "nvrtrie", Convert(0x22B5, 0x20D2));
            Add(symbols, "nvsim", Convert(0x223C, 0x20D2));
            Add(symbols, "nwarhk", Convert(0x2923));
            Add(symbols, "nwArr", Convert(0x21D6));
            Add(symbols, "nwarr", Convert(0x2196));
            Add(symbols, "nwarrow", Convert(0x2196));
            Add(symbols, "nwnear", Convert(0x2927));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigN()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Nacute", Convert(0x0143));
            Add(symbols, "Ncaron", Convert(0x0147));
            Add(symbols, "Ncedil", Convert(0x0145));
            Add(symbols, "NegativeMediumSpace", Convert(0x200B));
            Add(symbols, "NegativeThickSpace", Convert(0x200B));
            Add(symbols, "NegativeThinSpace", Convert(0x200B));
            Add(symbols, "NegativeVeryThinSpace", Convert(0x200B));
            Add(symbols, "NestedGreaterGreater", Convert(0x226B));
            Add(symbols, "NestedLessLess", Convert(0x226A));
            Add(symbols, "Ncy", Convert(0x041D));
            Add(symbols, "Nfr", Convert(0x1D511));
            Add(symbols, "NoBreak", Convert(0x2060));
            Add(symbols, "NonBreakingSpace", Convert(0x00A0));
            Add(symbols, "Nopf", Convert(0x2115));
            Add(symbols, "NewLine", Convert(0x000A));
            Add(symbols, "NJcy", Convert(0x040A));
            Add(symbols, "Not", Convert(0x2AEC));
            Add(symbols, "NotCongruent", Convert(0x2262));
            Add(symbols, "NotCupCap", Convert(0x226D));
            Add(symbols, "NotDoubleVerticalBar", Convert(0x2226));
            Add(symbols, "NotElement", Convert(0x2209));
            Add(symbols, "NotEqual", Convert(0x2260));
            Add(symbols, "NotEqualTilde", Convert(0x2242, 0x0338));
            Add(symbols, "NotExists", Convert(0x2204));
            Add(symbols, "NotGreater", Convert(0x226F));
            Add(symbols, "NotGreaterEqual", Convert(0x2271));
            Add(symbols, "NotGreaterFullEqual", Convert(0x2267, 0x0338));
            Add(symbols, "NotGreaterGreater", Convert(0x226B, 0x0338));
            Add(symbols, "NotGreaterLess", Convert(0x2279));
            Add(symbols, "NotGreaterSlantEqual", Convert(0x2A7E, 0x0338));
            Add(symbols, "NotGreaterTilde", Convert(0x2275));
            Add(symbols, "NotHumpDownHump", Convert(0x224E, 0x0338));
            Add(symbols, "NotHumpEqual", Convert(0x224F, 0x0338));
            Add(symbols, "NotLeftTriangle", Convert(0x22EA));
            Add(symbols, "NotLeftTriangleBar", Convert(0x29CF, 0x0338));
            Add(symbols, "NotLeftTriangleEqual", Convert(0x22EC));
            Add(symbols, "NotLess", Convert(0x226E));
            Add(symbols, "NotLessEqual", Convert(0x2270));
            Add(symbols, "NotLessGreater", Convert(0x2278));
            Add(symbols, "NotLessLess", Convert(0x226A, 0x0338));
            Add(symbols, "NotLessSlantEqual", Convert(0x2A7D, 0x0338));
            Add(symbols, "NotLessTilde", Convert(0x2274));
            Add(symbols, "NotNestedGreaterGreater", Convert(0x2AA2, 0x0338));
            Add(symbols, "NotNestedLessLess", Convert(0x2AA1, 0x0338));
            Add(symbols, "NotPrecedes", Convert(0x2280));
            Add(symbols, "NotPrecedesEqual", Convert(0x2AAF, 0x0338));
            Add(symbols, "NotPrecedesSlantEqual", Convert(0x22E0));
            Add(symbols, "NotReverseElement", Convert(0x220C));
            Add(symbols, "NotRightTriangle", Convert(0x22EB));
            Add(symbols, "NotRightTriangleBar", Convert(0x29D0, 0x0338));
            Add(symbols, "NotRightTriangleEqual", Convert(0x22ED));
            Add(symbols, "NotSquareSubset", Convert(0x228F, 0x0338));
            Add(symbols, "NotSquareSubsetEqual", Convert(0x22E2));
            Add(symbols, "NotSquareSuperset", Convert(0x2290, 0x0338));
            Add(symbols, "NotSquareSupersetEqual", Convert(0x22E3));
            Add(symbols, "NotSubset", Convert(0x2282, 0x20D2));
            Add(symbols, "NotSubsetEqual", Convert(0x2288));
            Add(symbols, "NotSucceeds", Convert(0x2281));
            Add(symbols, "NotSucceedsEqual", Convert(0x2AB0, 0x0338));
            Add(symbols, "NotSucceedsSlantEqual", Convert(0x22E1));
            Add(symbols, "NotSucceedsTilde", Convert(0x227F, 0x0338));
            Add(symbols, "NotSuperset", Convert(0x2283, 0x20D2));
            Add(symbols, "NotSupersetEqual", Convert(0x2289));
            Add(symbols, "NotTilde", Convert(0x2241));
            Add(symbols, "NotTildeEqual", Convert(0x2244));
            Add(symbols, "NotTildeFullEqual", Convert(0x2247));
            Add(symbols, "NotTildeTilde", Convert(0x2249));
            Add(symbols, "NotVerticalBar", Convert(0x2224));
            Add(symbols, "Nscr", Convert(0x1D4A9));
            AddWeak(symbols, "Ntilde", Convert(0x00D1));
            Add(symbols, "Nu", Convert(0x039D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleO()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "oacute", Convert(0x00F3));
            Add(symbols, "oast", Convert(0x229B));
            Add(symbols, "ocir", Convert(0x229A));
            AddWeak(symbols, "ocirc", Convert(0x00F4));
            Add(symbols, "ocy", Convert(0x043E));
            Add(symbols, "odash", Convert(0x229D));
            Add(symbols, "odblac", Convert(0x0151));
            Add(symbols, "odiv", Convert(0x2A38));
            Add(symbols, "odot", Convert(0x2299));
            Add(symbols, "odsold", Convert(0x29BC));
            Add(symbols, "oelig", Convert(0x0153));
            Add(symbols, "ofcir", Convert(0x29BF));
            Add(symbols, "ofr", Convert(0x1D52C));
            Add(symbols, "ogon", Convert(0x02DB));
            AddWeak(symbols, "ograve", Convert(0x00F2));
            Add(symbols, "ogt", Convert(0x29C1));
            Add(symbols, "ohbar", Convert(0x29B5));
            Add(symbols, "ohm", Convert(0x03A9));
            Add(symbols, "oint", Convert(0x222E));
            Add(symbols, "olarr", Convert(0x21BA));
            Add(symbols, "olcir", Convert(0x29BE));
            Add(symbols, "olcross", Convert(0x29BB));
            Add(symbols, "oline", Convert(0x203E));
            Add(symbols, "olt", Convert(0x29C0));
            Add(symbols, "omacr", Convert(0x014D));
            Add(symbols, "omega", Convert(0x03C9));
            Add(symbols, "omicron", Convert(0x03BF));
            Add(symbols, "omid", Convert(0x29B6));
            Add(symbols, "ominus", Convert(0x2296));
            Add(symbols, "oopf", Convert(0x1D560));
            Add(symbols, "opar", Convert(0x29B7));
            Add(symbols, "operp", Convert(0x29B9));
            Add(symbols, "oplus", Convert(0x2295));
            Add(symbols, "or", Convert(0x2228));
            Add(symbols, "orarr", Convert(0x21BB));
            Add(symbols, "ord", Convert(0x2A5D));
            Add(symbols, "order", Convert(0x2134));
            Add(symbols, "orderof", Convert(0x2134));
            AddWeak(symbols, "ordf", Convert(0x00AA));
            AddWeak(symbols, "ordm", Convert(0x00BA));
            Add(symbols, "origof", Convert(0x22B6));
            Add(symbols, "oror", Convert(0x2A56));
            Add(symbols, "orslope", Convert(0x2A57));
            Add(symbols, "orv", Convert(0x2A5B));
            Add(symbols, "oS", Convert(0x24C8));
            Add(symbols, "oscr", Convert(0x2134));
            AddWeak(symbols, "oslash", Convert(0x00F8));
            Add(symbols, "osol", Convert(0x2298));
            AddWeak(symbols, "otilde", Convert(0x00F5));
            Add(symbols, "otimes", Convert(0x2297));
            Add(symbols, "otimesas", Convert(0x2A36));
            AddWeak(symbols, "ouml", Convert(0x00F6));
            Add(symbols, "ovbar", Convert(0x233D));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigO()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "Oacute", Convert(0x00D3));
            AddWeak(symbols, "Ocirc", Convert(0x00D4));
            Add(symbols, "Ocy", Convert(0x041E));
            Add(symbols, "Odblac", Convert(0x0150));
            Add(symbols, "OElig", Convert(0x0152));
            Add(symbols, "Ofr", Convert(0x1D512));
            AddWeak(symbols, "Ograve", Convert(0x00D2));
            Add(symbols, "Omacr", Convert(0x014C));
            Add(symbols, "Omega", Convert(0x03A9));
            Add(symbols, "Omicron", Convert(0x039F));
            Add(symbols, "Oopf", Convert(0x1D546));
            Add(symbols, "OpenCurlyDoubleQuote", Convert(0x201C));
            Add(symbols, "OpenCurlyQuote", Convert(0x2018));
            Add(symbols, "Or", Convert(0x2A54));
            Add(symbols, "Oscr", Convert(0x1D4AA));
            AddWeak(symbols, "Oslash", Convert(0x00D8));
            AddWeak(symbols, "Otilde", Convert(0x00D5));
            Add(symbols, "Otimes", Convert(0x2A37));
            AddWeak(symbols, "Ouml", Convert(0x00D6));
            Add(symbols, "OverBar", Convert(0x203E));
            Add(symbols, "OverBrace", Convert(0x23DE));
            Add(symbols, "OverBracket", Convert(0x23B4));
            Add(symbols, "OverParenthesis", Convert(0x23DC));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleP()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "pfr", Convert(0x1D52D));
            Add(symbols, "par", Convert(0x2225));
            AddWeak(symbols, "para", Convert(0x00B6));
            Add(symbols, "parallel", Convert(0x2225));
            Add(symbols, "parsim", Convert(0x2AF3));
            Add(symbols, "parsl", Convert(0x2AFD));
            Add(symbols, "part", Convert(0x2202));
            Add(symbols, "pcy", Convert(0x043F));
            Add(symbols, "percnt", Convert(0x0025));
            Add(symbols, "period", Convert(0x002E));
            Add(symbols, "permil", Convert(0x2030));
            Add(symbols, "perp", Convert(0x22A5));
            Add(symbols, "pertenk", Convert(0x2031));
            Add(symbols, "phi", Convert(0x03C6));
            Add(symbols, "phiv", Convert(0x03D5));
            Add(symbols, "phmmat", Convert(0x2133));
            Add(symbols, "phone", Convert(0x260E));
            Add(symbols, "pi", Convert(0x03C0));
            Add(symbols, "pitchfork", Convert(0x22D4));
            Add(symbols, "piv", Convert(0x03D6));
            Add(symbols, "planck", Convert(0x210F));
            Add(symbols, "planckh", Convert(0x210E));
            Add(symbols, "plankv", Convert(0x210F));
            Add(symbols, "plus", Convert(0x002B));
            Add(symbols, "plusacir", Convert(0x2A23));
            Add(symbols, "plusb", Convert(0x229E));
            Add(symbols, "pluscir", Convert(0x2A22));
            Add(symbols, "plusdo", Convert(0x2214));
            Add(symbols, "plusdu", Convert(0x2A25));
            Add(symbols, "pluse", Convert(0x2A72));
            AddWeak(symbols, "plusmn", Convert(0x00B1));
            Add(symbols, "plussim", Convert(0x2A26));
            Add(symbols, "plustwo", Convert(0x2A27));
            Add(symbols, "pm", Convert(0x00B1));
            Add(symbols, "pointint", Convert(0x2A15));
            Add(symbols, "popf", Convert(0x1D561));
            AddWeak(symbols, "pound", Convert(0x00A3));
            Add(symbols, "pr", Convert(0x227A));
            Add(symbols, "prap", Convert(0x2AB7));
            Add(symbols, "prcue", Convert(0x227C));
            Add(symbols, "prE", Convert(0x2AB3));
            Add(symbols, "pre", Convert(0x2AAF));
            Add(symbols, "prec", Convert(0x227A));
            Add(symbols, "precapprox", Convert(0x2AB7));
            Add(symbols, "preccurlyeq", Convert(0x227C));
            Add(symbols, "preceq", Convert(0x2AAF));
            Add(symbols, "precnapprox", Convert(0x2AB9));
            Add(symbols, "precneqq", Convert(0x2AB5));
            Add(symbols, "precnsim", Convert(0x22E8));
            Add(symbols, "precsim", Convert(0x227E));
            Add(symbols, "prime", Convert(0x2032));
            Add(symbols, "primes", Convert(0x2119));
            Add(symbols, "prnap", Convert(0x2AB9));
            Add(symbols, "prnE", Convert(0x2AB5));
            Add(symbols, "prnsim", Convert(0x22E8));
            Add(symbols, "prod", Convert(0x220F));
            Add(symbols, "profalar", Convert(0x232E));
            Add(symbols, "profline", Convert(0x2312));
            Add(symbols, "profsurf", Convert(0x2313));
            Add(symbols, "prop", Convert(0x221D));
            Add(symbols, "propto", Convert(0x221D));
            Add(symbols, "prsim", Convert(0x227E));
            Add(symbols, "prurel", Convert(0x22B0));
            Add(symbols, "pscr", Convert(0x1D4C5));
            Add(symbols, "psi", Convert(0x03C8));
            Add(symbols, "puncsp", Convert(0x2008));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigP()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "PartialD", Convert(0x2202));
            Add(symbols, "Pcy", Convert(0x041F));
            Add(symbols, "Pfr", Convert(0x1D513));
            Add(symbols, "Phi", Convert(0x03A6));
            Add(symbols, "Pi", Convert(0x03A0));
            Add(symbols, "PlusMinus", Convert(0x00B1));
            Add(symbols, "Poincareplane", Convert(0x210C));
            Add(symbols, "Popf", Convert(0x2119));
            Add(symbols, "Pr", Convert(0x2ABB));
            Add(symbols, "Precedes", Convert(0x227A));
            Add(symbols, "PrecedesEqual", Convert(0x2AAF));
            Add(symbols, "PrecedesSlantEqual", Convert(0x227C));
            Add(symbols, "PrecedesTilde", Convert(0x227E));
            Add(symbols, "Prime", Convert(0x2033));
            Add(symbols, "Product", Convert(0x220F));
            Add(symbols, "Proportion", Convert(0x2237));
            Add(symbols, "Proportional", Convert(0x221D));
            Add(symbols, "Pscr", Convert(0x1D4AB));
            Add(symbols, "Psi", Convert(0x03A8));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleQ()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "qfr", Convert(0x1D52E));
            Add(symbols, "qint", Convert(0x2A0C));
            Add(symbols, "qopf", Convert(0x1D562));
            Add(symbols, "qprime", Convert(0x2057));
            Add(symbols, "qscr", Convert(0x1D4C6));
            Add(symbols, "quaternions", Convert(0x210D));
            Add(symbols, "quatint", Convert(0x2A16));
            Add(symbols, "quest", Convert(0x003F));
            Add(symbols, "questeq", Convert(0x225F));
            AddWeak(symbols, "quot", Convert(0x0022));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigQ()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Qfr", Convert(0x1D514));
            Add(symbols, "Qopf", Convert(0x211A));
            Add(symbols, "Qscr", Convert(0x1D4AC));
            AddWeak(symbols, "QUOT", Convert(0x0022));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleR()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "rAarr", Convert(0x21DB));
            Add(symbols, "race", Convert(0x223D, 0x0331));
            Add(symbols, "racute", Convert(0x0155));
            Add(symbols, "radic", Convert(0x221A));
            Add(symbols, "raemptyv", Convert(0x29B3));
            Add(symbols, "rang", Convert(0x27E9));
            Add(symbols, "rangd", Convert(0x2992));
            Add(symbols, "range", Convert(0x29A5));
            Add(symbols, "rangle", Convert(0x27E9));
            AddWeak(symbols, "raquo", Convert(0x00BB));
            Add(symbols, "rArr", Convert(0x21D2));
            Add(symbols, "rarr", Convert(0x2192));
            Add(symbols, "rarrap", Convert(0x2975));
            Add(symbols, "rarrb", Convert(0x21E5));
            Add(symbols, "rarrbfs", Convert(0x2920));
            Add(symbols, "rarrc", Convert(0x2933));
            Add(symbols, "rarrfs", Convert(0x291E));
            Add(symbols, "rarrhk", Convert(0x21AA));
            Add(symbols, "rarrlp", Convert(0x21AC));
            Add(symbols, "rarrpl", Convert(0x2945));
            Add(symbols, "rarrsim", Convert(0x2974));
            Add(symbols, "rarrtl", Convert(0x21A3));
            Add(symbols, "rarrw", Convert(0x219D));
            Add(symbols, "rAtail", Convert(0x291C));
            Add(symbols, "ratail", Convert(0x291A));
            Add(symbols, "ratio", Convert(0x2236));
            Add(symbols, "rationals", Convert(0x211A));
            Add(symbols, "rBarr", Convert(0x290F));
            Add(symbols, "rbarr", Convert(0x290D));
            Add(symbols, "rbbrk", Convert(0x2773));
            Add(symbols, "rbrace", Convert(0x007D));
            Add(symbols, "rbrack", Convert(0x005D));
            Add(symbols, "rbrke", Convert(0x298C));
            Add(symbols, "rbrksld", Convert(0x298E));
            Add(symbols, "rbrkslu", Convert(0x2990));
            Add(symbols, "rcaron", Convert(0x0159));
            Add(symbols, "rcedil", Convert(0x0157));
            Add(symbols, "rceil", Convert(0x2309));
            Add(symbols, "rcub", Convert(0x007D));
            Add(symbols, "rcy", Convert(0x0440));
            Add(symbols, "rdca", Convert(0x2937));
            Add(symbols, "rdldhar", Convert(0x2969));
            Add(symbols, "rdquo", Convert(0x201D));
            Add(symbols, "rdquor", Convert(0x201D));
            Add(symbols, "rdsh", Convert(0x21B3));
            Add(symbols, "real", Convert(0x211C));
            Add(symbols, "realine", Convert(0x211B));
            Add(symbols, "realpart", Convert(0x211C));
            Add(symbols, "reals", Convert(0x211D));
            Add(symbols, "rect", Convert(0x25AD));
            AddWeak(symbols, "reg", Convert(0x00AE));
            Add(symbols, "rfisht", Convert(0x297D));
            Add(symbols, "rfloor", Convert(0x230B));
            Add(symbols, "rfr", Convert(0x1D52F));
            Add(symbols, "rHar", Convert(0x2964));
            Add(symbols, "rhard", Convert(0x21C1));
            Add(symbols, "rharu", Convert(0x21C0));
            Add(symbols, "rharul", Convert(0x296C));
            Add(symbols, "rho", Convert(0x03C1));
            Add(symbols, "rhov", Convert(0x03F1));
            Add(symbols, "rightarrow", Convert(0x2192));
            Add(symbols, "rightarrowtail", Convert(0x21A3));
            Add(symbols, "rightharpoondown", Convert(0x21C1));
            Add(symbols, "rightharpoonup", Convert(0x21C0));
            Add(symbols, "rightleftarrows", Convert(0x21C4));
            Add(symbols, "rightleftharpoons", Convert(0x21CC));
            Add(symbols, "rightrightarrows", Convert(0x21C9));
            Add(symbols, "rightsquigarrow", Convert(0x219D));
            Add(symbols, "rightthreetimes", Convert(0x22CC));
            Add(symbols, "ring", Convert(0x02DA));
            Add(symbols, "risingdotseq", Convert(0x2253));
            Add(symbols, "rlarr", Convert(0x21C4));
            Add(symbols, "rlhar", Convert(0x21CC));
            Add(symbols, "rlm", Convert(0x200F));
            Add(symbols, "rmoust", Convert(0x23B1));
            Add(symbols, "rmoustache", Convert(0x23B1));
            Add(symbols, "rnmid", Convert(0x2AEE));
            Add(symbols, "roang", Convert(0x27ED));
            Add(symbols, "roarr", Convert(0x21FE));
            Add(symbols, "robrk", Convert(0x27E7));
            Add(symbols, "ropar", Convert(0x2986));
            Add(symbols, "ropf", Convert(0x1D563));
            Add(symbols, "roplus", Convert(0x2A2E));
            Add(symbols, "rotimes", Convert(0x2A35));
            Add(symbols, "rpar", Convert(0x0029));
            Add(symbols, "rpargt", Convert(0x2994));
            Add(symbols, "rppolint", Convert(0x2A12));
            Add(symbols, "rrarr", Convert(0x21C9));
            Add(symbols, "rsaquo", Convert(0x203A));
            Add(symbols, "rscr", Convert(0x1D4C7));
            Add(symbols, "rsh", Convert(0x21B1));
            Add(symbols, "rsqb", Convert(0x005D));
            Add(symbols, "rsquo", Convert(0x2019));
            Add(symbols, "rsquor", Convert(0x2019));
            Add(symbols, "rthree", Convert(0x22CC));
            Add(symbols, "rtimes", Convert(0x22CA));
            Add(symbols, "rtri", Convert(0x25B9));
            Add(symbols, "rtrie", Convert(0x22B5));
            Add(symbols, "rtrif", Convert(0x25B8));
            Add(symbols, "rtriltri", Convert(0x29CE));
            Add(symbols, "ruluhar", Convert(0x2968));
            Add(symbols, "rx", Convert(0x211E));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigR()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Racute", Convert(0x0154));
            Add(symbols, "Rang", Convert(0x27EB));
            Add(symbols, "Rarr", Convert(0x21A0));
            Add(symbols, "Rarrtl", Convert(0x2916));
            Add(symbols, "RBarr", Convert(0x2910));
            Add(symbols, "Rcaron", Convert(0x0158));
            Add(symbols, "Rcedil", Convert(0x0156));
            Add(symbols, "Rcy", Convert(0x0420));
            Add(symbols, "Re", Convert(0x211C));
            AddWeak(symbols, "REG", Convert(0x00AE));
            Add(symbols, "ReverseElement", Convert(0x220B));
            Add(symbols, "ReverseEquilibrium", Convert(0x21CB));
            Add(symbols, "ReverseUpEquilibrium", Convert(0x296F));
            Add(symbols, "Rfr", Convert(0x211C));
            Add(symbols, "Rho", Convert(0x03A1));
            Add(symbols, "RightAngleBracket", Convert(0x27E9));
            Add(symbols, "RightArrow", Convert(0x2192));
            Add(symbols, "Rightarrow", Convert(0x21D2));
            Add(symbols, "RightArrowBar", Convert(0x21E5));
            Add(symbols, "RightArrowLeftArrow", Convert(0x21C4));
            Add(symbols, "RightCeiling", Convert(0x2309));
            Add(symbols, "RightDoubleBracket", Convert(0x27E7));
            Add(symbols, "RightDownTeeVector", Convert(0x295D));
            Add(symbols, "RightDownVector", Convert(0x21C2));
            Add(symbols, "RightDownVectorBar", Convert(0x2955));
            Add(symbols, "RightFloor", Convert(0x230B));
            Add(symbols, "RightTee", Convert(0x22A2));
            Add(symbols, "RightTeeArrow", Convert(0x21A6));
            Add(symbols, "RightTeeVector", Convert(0x295B));
            Add(symbols, "RightTriangle", Convert(0x22B3));
            Add(symbols, "RightTriangleBar", Convert(0x29D0));
            Add(symbols, "RightTriangleEqual", Convert(0x22B5));
            Add(symbols, "RightUpDownVector", Convert(0x294F));
            Add(symbols, "RightUpTeeVector", Convert(0x295C));
            Add(symbols, "RightUpVector", Convert(0x21BE));
            Add(symbols, "RightUpVectorBar", Convert(0x2954));
            Add(symbols, "RightVector", Convert(0x21C0));
            Add(symbols, "RightVectorBar", Convert(0x2953));
            Add(symbols, "Ropf", Convert(0x211D));
            Add(symbols, "RoundImplies", Convert(0x2970));
            Add(symbols, "Rrightarrow", Convert(0x21DB));
            Add(symbols, "Rscr", Convert(0x211B));
            Add(symbols, "Rsh", Convert(0x21B1));
            Add(symbols, "RuleDelayed", Convert(0x29F4));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleS()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "sacute", Convert(0x015B));
            Add(symbols, "sbquo", Convert(0x201A));
            Add(symbols, "sc", Convert(0x227B));
            Add(symbols, "scap", Convert(0x2AB8));
            Add(symbols, "scaron", Convert(0x0161));
            Add(symbols, "sccue", Convert(0x227D));
            Add(symbols, "scE", Convert(0x2AB4));
            Add(symbols, "sce", Convert(0x2AB0));
            Add(symbols, "scedil", Convert(0x015F));
            Add(symbols, "scirc", Convert(0x015D));
            Add(symbols, "scnap", Convert(0x2ABA));
            Add(symbols, "scnE", Convert(0x2AB6));
            Add(symbols, "scnsim", Convert(0x22E9));
            Add(symbols, "scpolint", Convert(0x2A13));
            Add(symbols, "scsim", Convert(0x227F));
            Add(symbols, "scy", Convert(0x0441));
            Add(symbols, "sdot", Convert(0x22C5));
            Add(symbols, "sdotb", Convert(0x22A1));
            Add(symbols, "sdote", Convert(0x2A66));
            Add(symbols, "searhk", Convert(0x2925));
            Add(symbols, "seArr", Convert(0x21D8));
            Add(symbols, "searr", Convert(0x2198));
            Add(symbols, "searrow", Convert(0x2198));
            AddWeak(symbols, "sect", Convert(0x00A7));
            Add(symbols, "semi", Convert(0x003B));
            Add(symbols, "seswar", Convert(0x2929));
            Add(symbols, "setminus", Convert(0x2216));
            Add(symbols, "setmn", Convert(0x2216));
            Add(symbols, "sext", Convert(0x2736));
            Add(symbols, "sfr", Convert(0x1D530));
            Add(symbols, "sfrown", Convert(0x2322));
            Add(symbols, "sharp", Convert(0x266F));
            Add(symbols, "shchcy", Convert(0x0449));
            Add(symbols, "shcy", Convert(0x0448));
            Add(symbols, "shortmid", Convert(0x2223));
            Add(symbols, "shortparallel", Convert(0x2225));
            AddWeak(symbols, "shy", Convert(0x00AD));
            Add(symbols, "sigma", Convert(0x03C3));
            Add(symbols, "sigmaf", Convert(0x03C2));
            Add(symbols, "sigmav", Convert(0x03C2));
            Add(symbols, "sim", Convert(0x223C));
            Add(symbols, "simdot", Convert(0x2A6A));
            Add(symbols, "sime", Convert(0x2243));
            Add(symbols, "simeq", Convert(0x2243));
            Add(symbols, "simg", Convert(0x2A9E));
            Add(symbols, "simgE", Convert(0x2AA0));
            Add(symbols, "siml", Convert(0x2A9D));
            Add(symbols, "simlE", Convert(0x2A9F));
            Add(symbols, "simne", Convert(0x2246));
            Add(symbols, "simplus", Convert(0x2A24));
            Add(symbols, "simrarr", Convert(0x2972));
            Add(symbols, "slarr", Convert(0x2190));
            Add(symbols, "smallsetminus", Convert(0x2216));
            Add(symbols, "smashp", Convert(0x2A33));
            Add(symbols, "smeparsl", Convert(0x29E4));
            Add(symbols, "smid", Convert(0x2223));
            Add(symbols, "smile", Convert(0x2323));
            Add(symbols, "smt", Convert(0x2AAA));
            Add(symbols, "smte", Convert(0x2AAC));
            Add(symbols, "smtes", Convert(0x2AAC, 0xFE00));
            Add(symbols, "softcy", Convert(0x044C));
            Add(symbols, "sol", Convert(0x002F));
            Add(symbols, "solb", Convert(0x29C4));
            Add(symbols, "solbar", Convert(0x233F));
            Add(symbols, "sopf", Convert(0x1D564));
            Add(symbols, "spades", Convert(0x2660));
            Add(symbols, "spadesuit", Convert(0x2660));
            Add(symbols, "spar", Convert(0x2225));
            Add(symbols, "sqcap", Convert(0x2293));
            Add(symbols, "sqcaps", Convert(0x2293, 0xFE00));
            Add(symbols, "sqcup", Convert(0x2294));
            Add(symbols, "sqcups", Convert(0x2294, 0xFE00));
            Add(symbols, "sqsub", Convert(0x228F));
            Add(symbols, "sqsube", Convert(0x2291));
            Add(symbols, "sqsubset", Convert(0x228F));
            Add(symbols, "sqsubseteq", Convert(0x2291));
            Add(symbols, "sqsup", Convert(0x2290));
            Add(symbols, "sqsupe", Convert(0x2292));
            Add(symbols, "sqsupset", Convert(0x2290));
            Add(symbols, "sqsupseteq", Convert(0x2292));
            Add(symbols, "squ", Convert(0x25A1));
            Add(symbols, "square", Convert(0x25A1));
            Add(symbols, "squarf", Convert(0x25AA));
            Add(symbols, "squf", Convert(0x25AA));
            Add(symbols, "srarr", Convert(0x2192));
            Add(symbols, "sscr", Convert(0x1D4C8));
            Add(symbols, "ssetmn", Convert(0x2216));
            Add(symbols, "ssmile", Convert(0x2323));
            Add(symbols, "sstarf", Convert(0x22C6));
            Add(symbols, "star", Convert(0x2606));
            Add(symbols, "starf", Convert(0x2605));
            Add(symbols, "straightepsilon", Convert(0x03F5));
            Add(symbols, "straightphi", Convert(0x03D5));
            Add(symbols, "strns", Convert(0x00AF));
            Add(symbols, "sub", Convert(0x2282));
            Add(symbols, "subdot", Convert(0x2ABD));
            Add(symbols, "subE", Convert(0x2AC5));
            Add(symbols, "sube", Convert(0x2286));
            Add(symbols, "subedot", Convert(0x2AC3));
            Add(symbols, "submult", Convert(0x2AC1));
            Add(symbols, "subnE", Convert(0x2ACB));
            Add(symbols, "subne", Convert(0x228A));
            Add(symbols, "subplus", Convert(0x2ABF));
            Add(symbols, "subrarr", Convert(0x2979));
            Add(symbols, "subset", Convert(0x2282));
            Add(symbols, "subseteq", Convert(0x2286));
            Add(symbols, "subseteqq", Convert(0x2AC5));
            Add(symbols, "subsetneq", Convert(0x228A));
            Add(symbols, "subsetneqq", Convert(0x2ACB));
            Add(symbols, "subsim", Convert(0x2AC7));
            Add(symbols, "subsub", Convert(0x2AD5));
            Add(symbols, "subsup", Convert(0x2AD3));
            Add(symbols, "succ", Convert(0x227B));
            Add(symbols, "succapprox", Convert(0x2AB8));
            Add(symbols, "succcurlyeq", Convert(0x227D));
            Add(symbols, "succeq", Convert(0x2AB0));
            Add(symbols, "succnapprox", Convert(0x2ABA));
            Add(symbols, "succneqq", Convert(0x2AB6));
            Add(symbols, "succnsim", Convert(0x22E9));
            Add(symbols, "succsim", Convert(0x227F));
            Add(symbols, "sum", Convert(0x2211));
            Add(symbols, "sung", Convert(0x266A));
            Add(symbols, "sup", Convert(0x2283));
            AddWeak(symbols, "sup1", Convert(0x00B9));
            AddWeak(symbols, "sup2", Convert(0x00B2));
            AddWeak(symbols, "sup3", Convert(0x00B3));
            Add(symbols, "supdot", Convert(0x2ABE));
            Add(symbols, "supdsub", Convert(0x2AD8));
            Add(symbols, "supE", Convert(0x2AC6));
            Add(symbols, "supe", Convert(0x2287));
            Add(symbols, "supedot", Convert(0x2AC4));
            Add(symbols, "suphsol", Convert(0x27C9));
            Add(symbols, "suphsub", Convert(0x2AD7));
            Add(symbols, "suplarr", Convert(0x297B));
            Add(symbols, "supmult", Convert(0x2AC2));
            Add(symbols, "supnE", Convert(0x2ACC));
            Add(symbols, "supne", Convert(0x228B));
            Add(symbols, "supplus", Convert(0x2AC0));
            Add(symbols, "supset", Convert(0x2283));
            Add(symbols, "supseteq", Convert(0x2287));
            Add(symbols, "supseteqq", Convert(0x2AC6));
            Add(symbols, "supsetneq", Convert(0x228B));
            Add(symbols, "supsetneqq", Convert(0x2ACC));
            Add(symbols, "supsim", Convert(0x2AC8));
            Add(symbols, "supsub", Convert(0x2AD4));
            Add(symbols, "supsup", Convert(0x2AD6));
            Add(symbols, "swarhk", Convert(0x2926));
            Add(symbols, "swArr", Convert(0x21D9));
            Add(symbols, "swarr", Convert(0x2199));
            Add(symbols, "swarrow", Convert(0x2199));
            Add(symbols, "swnwar", Convert(0x292A));
            AddWeak(symbols, "szlig", Convert(0x00DF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigS()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Sacute", Convert(0x015A));
            Add(symbols, "Sc", Convert(0x2ABC));
            Add(symbols, "Scaron", Convert(0x0160));
            Add(symbols, "Scedil", Convert(0x015E));
            Add(symbols, "Scirc", Convert(0x015C));
            Add(symbols, "Scy", Convert(0x0421));
            Add(symbols, "Sfr", Convert(0x1D516));
            Add(symbols, "SHCHcy", Convert(0x0429));
            Add(symbols, "SHcy", Convert(0x0428));
            Add(symbols, "ShortDownArrow", Convert(0x2193));
            Add(symbols, "ShortLeftArrow", Convert(0x2190));
            Add(symbols, "ShortRightArrow", Convert(0x2192));
            Add(symbols, "ShortUpArrow", Convert(0x2191));
            Add(symbols, "Sigma", Convert(0x03A3));
            Add(symbols, "SmallCircle", Convert(0x2218));
            Add(symbols, "SOFTcy", Convert(0x042C));
            Add(symbols, "Sopf", Convert(0x1D54A));
            Add(symbols, "Sqrt", Convert(0x221A));
            Add(symbols, "Square", Convert(0x25A1));
            Add(symbols, "SquareIntersection", Convert(0x2293));
            Add(symbols, "SquareSubset", Convert(0x228F));
            Add(symbols, "SquareSubsetEqual", Convert(0x2291));
            Add(symbols, "SquareSuperset", Convert(0x2290));
            Add(symbols, "SquareSupersetEqual", Convert(0x2292));
            Add(symbols, "SquareUnion", Convert(0x2294));
            Add(symbols, "Sscr", Convert(0x1D4AE));
            Add(symbols, "Star", Convert(0x22C6));
            Add(symbols, "Sub", Convert(0x22D0));
            Add(symbols, "Subset", Convert(0x22D0));
            Add(symbols, "SubsetEqual", Convert(0x2286));
            Add(symbols, "Succeeds", Convert(0x227B));
            Add(symbols, "SucceedsEqual", Convert(0x2AB0));
            Add(symbols, "SucceedsSlantEqual", Convert(0x227D));
            Add(symbols, "SucceedsTilde", Convert(0x227F));
            Add(symbols, "SuchThat", Convert(0x220B));
            Add(symbols, "Sum", Convert(0x2211));
            Add(symbols, "Sup", Convert(0x22D1));
            Add(symbols, "Superset", Convert(0x2283));
            Add(symbols, "SupersetEqual", Convert(0x2287));
            Add(symbols, "Supset", Convert(0x22D1));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleT()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "target", Convert(0x2316));
            Add(symbols, "tau", Convert(0x03C4));
            Add(symbols, "tbrk", Convert(0x23B4));
            Add(symbols, "tcaron", Convert(0x0165));
            Add(symbols, "tcedil", Convert(0x0163));
            Add(symbols, "tcy", Convert(0x0442));
            Add(symbols, "tdot", Convert(0x20DB));
            Add(symbols, "telrec", Convert(0x2315));
            Add(symbols, "tfr", Convert(0x1D531));
            Add(symbols, "there4", Convert(0x2234));
            Add(symbols, "therefore", Convert(0x2234));
            Add(symbols, "theta", Convert(0x03B8));
            Add(symbols, "thetasym", Convert(0x03D1));
            Add(symbols, "thetav", Convert(0x03D1));
            Add(symbols, "thickapprox", Convert(0x2248));
            Add(symbols, "thicksim", Convert(0x223C));
            Add(symbols, "thinsp", Convert(0x2009));
            Add(symbols, "thkap", Convert(0x2248));
            Add(symbols, "thksim", Convert(0x223C));
            AddWeak(symbols, "thorn", Convert(0x00FE));
            Add(symbols, "tilde", Convert(0x02DC));
            AddWeak(symbols, "times", Convert(0x00D7));
            Add(symbols, "timesb", Convert(0x22A0));
            Add(symbols, "timesbar", Convert(0x2A31));
            Add(symbols, "timesd", Convert(0x2A30));
            Add(symbols, "tint", Convert(0x222D));
            Add(symbols, "toea", Convert(0x2928));
            Add(symbols, "top", Convert(0x22A4));
            Add(symbols, "topbot", Convert(0x2336));
            Add(symbols, "topcir", Convert(0x2AF1));
            Add(symbols, "topf", Convert(0x1D565));
            Add(symbols, "topfork", Convert(0x2ADA));
            Add(symbols, "tosa", Convert(0x2929));
            Add(symbols, "tprime", Convert(0x2034));
            Add(symbols, "trade", Convert(0x2122));
            Add(symbols, "triangle", Convert(0x25B5));
            Add(symbols, "triangledown", Convert(0x25BF));
            Add(symbols, "triangleleft", Convert(0x25C3));
            Add(symbols, "trianglelefteq", Convert(0x22B4));
            Add(symbols, "triangleq", Convert(0x225C));
            Add(symbols, "triangleright", Convert(0x25B9));
            Add(symbols, "trianglerighteq", Convert(0x22B5));
            Add(symbols, "tridot", Convert(0x25EC));
            Add(symbols, "trie", Convert(0x225C));
            Add(symbols, "triminus", Convert(0x2A3A));
            Add(symbols, "triplus", Convert(0x2A39));
            Add(symbols, "trisb", Convert(0x29CD));
            Add(symbols, "tritime", Convert(0x2A3B));
            Add(symbols, "trpezium", Convert(0x23E2));
            Add(symbols, "tscr", Convert(0x1D4C9));
            Add(symbols, "tscy", Convert(0x0446));
            Add(symbols, "tshcy", Convert(0x045B));
            Add(symbols, "tstrok", Convert(0x0167));
            Add(symbols, "twixt", Convert(0x226C));
            Add(symbols, "twoheadleftarrow", Convert(0x219E));
            Add(symbols, "twoheadrightarrow", Convert(0x21A0));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigT()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Tab", Convert(0x0009));
            Add(symbols, "Tau", Convert(0x03A4));
            Add(symbols, "Tcaron", Convert(0x0164));
            Add(symbols, "Tcedil", Convert(0x0162));
            Add(symbols, "Tcy", Convert(0x0422));
            Add(symbols, "Tfr", Convert(0x1D517));
            Add(symbols, "Therefore", Convert(0x2234));
            Add(symbols, "Theta", Convert(0x0398));
            Add(symbols, "ThickSpace", Convert(0x205F, 0x200A));
            Add(symbols, "ThinSpace", Convert(0x2009));
            AddWeak(symbols, "THORN", Convert(0x00DE));
            Add(symbols, "Tilde", Convert(0x223C));
            Add(symbols, "TildeEqual", Convert(0x2243));
            Add(symbols, "TildeFullEqual", Convert(0x2245));
            Add(symbols, "TildeTilde", Convert(0x2248));
            Add(symbols, "Topf", Convert(0x1D54B));
            Add(symbols, "TRADE", Convert(0x2122));
            Add(symbols, "TripleDot", Convert(0x20DB));
            Add(symbols, "Tscr", Convert(0x1D4AF));
            Add(symbols, "TScy", Convert(0x0426));
            Add(symbols, "TSHcy", Convert(0x040B));
            Add(symbols, "Tstrok", Convert(0x0166));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleU()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "uacute", Convert(0x00FA));
            Add(symbols, "uArr", Convert(0x21D1));
            Add(symbols, "uarr", Convert(0x2191));
            Add(symbols, "ubrcy", Convert(0x045E));
            Add(symbols, "ubreve", Convert(0x016D));
            AddWeak(symbols, "ucirc", Convert(0x00FB));
            Add(symbols, "ucy", Convert(0x0443));
            Add(symbols, "udarr", Convert(0x21C5));
            Add(symbols, "udblac", Convert(0x0171));
            Add(symbols, "udhar", Convert(0x296E));
            Add(symbols, "ufisht", Convert(0x297E));
            Add(symbols, "ufr", Convert(0x1D532));
            AddWeak(symbols, "ugrave", Convert(0x00F9));
            Add(symbols, "uHar", Convert(0x2963));
            Add(symbols, "uharl", Convert(0x21BF));
            Add(symbols, "uharr", Convert(0x21BE));
            Add(symbols, "uhblk", Convert(0x2580));
            Add(symbols, "ulcorn", Convert(0x231C));
            Add(symbols, "ulcorner", Convert(0x231C));
            Add(symbols, "ulcrop", Convert(0x230F));
            Add(symbols, "ultri", Convert(0x25F8));
            Add(symbols, "umacr", Convert(0x016B));
            AddWeak(symbols, "uml", Convert(0x00A8));
            Add(symbols, "uogon", Convert(0x0173));
            Add(symbols, "uopf", Convert(0x1D566));
            Add(symbols, "uparrow", Convert(0x2191));
            Add(symbols, "updownarrow", Convert(0x2195));
            Add(symbols, "upharpoonleft", Convert(0x21BF));
            Add(symbols, "upharpoonright", Convert(0x21BE));
            Add(symbols, "uplus", Convert(0x228E));
            Add(symbols, "upsi", Convert(0x03C5));
            Add(symbols, "upsih", Convert(0x03D2));
            Add(symbols, "upsilon", Convert(0x03C5));
            Add(symbols, "upuparrows", Convert(0x21C8));
            Add(symbols, "urcorn", Convert(0x231D));
            Add(symbols, "urcorner", Convert(0x231D));
            Add(symbols, "urcrop", Convert(0x230E));
            Add(symbols, "uring", Convert(0x016F));
            Add(symbols, "urtri", Convert(0x25F9));
            Add(symbols, "uscr", Convert(0x1D4CA));
            Add(symbols, "utdot", Convert(0x22F0));
            Add(symbols, "utilde", Convert(0x0169));
            Add(symbols, "utri", Convert(0x25B5));
            Add(symbols, "utrif", Convert(0x25B4));
            Add(symbols, "uuarr", Convert(0x21C8));
            AddWeak(symbols, "uuml", Convert(0x00FC));
            Add(symbols, "uwangle", Convert(0x29A7));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigU()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "Uacute", Convert(0x00DA));
            Add(symbols, "Uarr", Convert(0x219F));
            Add(symbols, "Uarrocir", Convert(0x2949));
            Add(symbols, "Ubrcy", Convert(0x040E));
            Add(symbols, "Ubreve", Convert(0x016C));
            AddWeak(symbols, "Ucirc", Convert(0x00DB));
            Add(symbols, "Ucy", Convert(0x0423));
            Add(symbols, "Udblac", Convert(0x0170));
            Add(symbols, "Ufr", Convert(0x1D518));
            AddWeak(symbols, "Ugrave", Convert(0x00D9));
            Add(symbols, "Umacr", Convert(0x016A));
            Add(symbols, "UnderBar", Convert(0x005F));
            Add(symbols, "UnderBrace", Convert(0x23DF));
            Add(symbols, "UnderBracket", Convert(0x23B5));
            Add(symbols, "UnderParenthesis", Convert(0x23DD));
            Add(symbols, "Union", Convert(0x22C3));
            Add(symbols, "UnionPlus", Convert(0x228E));
            Add(symbols, "Uogon", Convert(0x0172));
            Add(symbols, "Uopf", Convert(0x1D54C));
            Add(symbols, "UpArrow", Convert(0x2191));
            Add(symbols, "Uparrow", Convert(0x21D1));
            Add(symbols, "UpArrowBar", Convert(0x2912));
            Add(symbols, "UpArrowDownArrow", Convert(0x21C5));
            Add(symbols, "UpDownArrow", Convert(0x2195));
            Add(symbols, "Updownarrow", Convert(0x21D5));
            Add(symbols, "UpEquilibrium", Convert(0x296E));
            Add(symbols, "UpperLeftArrow", Convert(0x2196));
            Add(symbols, "UpperRightArrow", Convert(0x2197));
            Add(symbols, "Upsi", Convert(0x03D2));
            Add(symbols, "Upsilon", Convert(0x03A5));
            Add(symbols, "UpTee", Convert(0x22A5));
            Add(symbols, "UpTeeArrow", Convert(0x21A5));
            Add(symbols, "Uring", Convert(0x016E));
            Add(symbols, "Uscr", Convert(0x1D4B0));
            Add(symbols, "Utilde", Convert(0x0168));
            AddWeak(symbols, "Uuml", Convert(0x00DC));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleV()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "vangrt", Convert(0x299C));
            Add(symbols, "varepsilon", Convert(0x03F5));
            Add(symbols, "varkappa", Convert(0x03F0));
            Add(symbols, "varnothing", Convert(0x2205));
            Add(symbols, "varphi", Convert(0x03D5));
            Add(symbols, "varpi", Convert(0x03D6));
            Add(symbols, "varpropto", Convert(0x221D));
            Add(symbols, "vArr", Convert(0x21D5));
            Add(symbols, "varr", Convert(0x2195));
            Add(symbols, "varrho", Convert(0x03F1));
            Add(symbols, "varsigma", Convert(0x03C2));
            Add(symbols, "varsubsetneq", Convert(0x228A, 0xFE00));
            Add(symbols, "varsubsetneqq", Convert(0x2ACB, 0xFE00));
            Add(symbols, "varsupsetneq", Convert(0x228B, 0xFE00));
            Add(symbols, "varsupsetneqq", Convert(0x2ACC, 0xFE00));
            Add(symbols, "vartheta", Convert(0x03D1));
            Add(symbols, "vartriangleleft", Convert(0x22B2));
            Add(symbols, "vartriangleright", Convert(0x22B3));
            Add(symbols, "vBar", Convert(0x2AE8));
            Add(symbols, "vBarv", Convert(0x2AE9));
            Add(symbols, "vcy", Convert(0x0432));
            Add(symbols, "vDash", Convert(0x22A8));
            Add(symbols, "vdash", Convert(0x22A2));
            Add(symbols, "vee", Convert(0x2228));
            Add(symbols, "veebar", Convert(0x22BB));
            Add(symbols, "veeeq", Convert(0x225A));
            Add(symbols, "vellip", Convert(0x22EE));
            Add(symbols, "verbar", Convert(0x007C));
            Add(symbols, "vert", Convert(0x007C));
            Add(symbols, "vfr", Convert(0x1D533));
            Add(symbols, "vltri", Convert(0x22B2));
            Add(symbols, "vnsub", Convert(0x2282, 0x20D2));
            Add(symbols, "vnsup", Convert(0x2283, 0x20D2));
            Add(symbols, "vopf", Convert(0x1D567));
            Add(symbols, "vprop", Convert(0x221D));
            Add(symbols, "vrtri", Convert(0x22B3));
            Add(symbols, "vscr", Convert(0x1D4CB));
            Add(symbols, "vsubnE", Convert(0x2ACB, 0xFE00));
            Add(symbols, "vsubne", Convert(0x228A, 0xFE00));
            Add(symbols, "vsupnE", Convert(0x2ACC, 0xFE00));
            Add(symbols, "vsupne", Convert(0x228B, 0xFE00));
            Add(symbols, "vzigzag", Convert(0x299A));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigV()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Vbar", Convert(0x2AEB));
            Add(symbols, "Vcy", Convert(0x0412));
            Add(symbols, "VDash", Convert(0x22AB));
            Add(symbols, "Vdash", Convert(0x22A9));
            Add(symbols, "Vdashl", Convert(0x2AE6));
            Add(symbols, "Vee", Convert(0x22C1));
            Add(symbols, "Verbar", Convert(0x2016));
            Add(symbols, "Vert", Convert(0x2016));
            Add(symbols, "VerticalBar", Convert(0x2223));
            Add(symbols, "VerticalLine", Convert(0x007C));
            Add(symbols, "VerticalSeparator", Convert(0x2758));
            Add(symbols, "VerticalTilde", Convert(0x2240));
            Add(symbols, "VeryThinSpace", Convert(0x200A));
            Add(symbols, "Vfr", Convert(0x1D519));
            Add(symbols, "Vopf", Convert(0x1D54D));
            Add(symbols, "Vscr", Convert(0x1D4B1));
            Add(symbols, "Vvdash", Convert(0x22AA));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleW()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "wcirc", Convert(0x0175));
            Add(symbols, "wedbar", Convert(0x2A5F));
            Add(symbols, "wedge", Convert(0x2227));
            Add(symbols, "wedgeq", Convert(0x2259));
            Add(symbols, "weierp", Convert(0x2118));
            Add(symbols, "wfr", Convert(0x1D534));
            Add(symbols, "wopf", Convert(0x1D568));
            Add(symbols, "wp", Convert(0x2118));
            Add(symbols, "wr", Convert(0x2240));
            Add(symbols, "wreath", Convert(0x2240));
            Add(symbols, "wscr", Convert(0x1D4CC));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigW()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Wcirc", Convert(0x0174));
            Add(symbols, "Wedge", Convert(0x22C0));
            Add(symbols, "Wfr", Convert(0x1D51A));
            Add(symbols, "Wopf", Convert(0x1D54E));
            Add(symbols, "Wscr", Convert(0x1D4B2));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleX()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "xcap", Convert(0x22C2));
            Add(symbols, "xcirc", Convert(0x25EF));
            Add(symbols, "xcup", Convert(0x22C3));
            Add(symbols, "xdtri", Convert(0x25BD));
            Add(symbols, "xfr", Convert(0x1D535));
            Add(symbols, "xhArr", Convert(0x27FA));
            Add(symbols, "xharr", Convert(0x27F7));
            Add(symbols, "xi", Convert(0x03BE));
            Add(symbols, "xlArr", Convert(0x27F8));
            Add(symbols, "xlarr", Convert(0x27F5));
            Add(symbols, "xmap", Convert(0x27FC));
            Add(symbols, "xnis", Convert(0x22FB));
            Add(symbols, "xodot", Convert(0x2A00));
            Add(symbols, "xopf", Convert(0x1D569));
            Add(symbols, "xoplus", Convert(0x2A01));
            Add(symbols, "xotime", Convert(0x2A02));
            Add(symbols, "xrArr", Convert(0x27F9));
            Add(symbols, "xrarr", Convert(0x27F6));
            Add(symbols, "xscr", Convert(0x1D4CD));
            Add(symbols, "xsqcup", Convert(0x2A06));
            Add(symbols, "xuplus", Convert(0x2A04));
            Add(symbols, "xutri", Convert(0x25B3));
            Add(symbols, "xvee", Convert(0x22C1));
            Add(symbols, "xwedge", Convert(0x22C0));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigX()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Xfr", Convert(0x1D51B));
            Add(symbols, "Xi", Convert(0x039E));
            Add(symbols, "Xopf", Convert(0x1D54F));
            Add(symbols, "Xscr", Convert(0x1D4B3));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleY()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "yacute", Convert(0x00FD));
            Add(symbols, "yacy", Convert(0x044F));
            Add(symbols, "ycirc", Convert(0x0177));
            Add(symbols, "ycy", Convert(0x044B));
            AddWeak(symbols, "yen", Convert(0x00A5));
            Add(symbols, "yfr", Convert(0x1D536));
            Add(symbols, "yicy", Convert(0x0457));
            Add(symbols, "yopf", Convert(0x1D56A));
            Add(symbols, "yscr", Convert(0x1D4CE));
            Add(symbols, "yucy", Convert(0x044E));
            AddWeak(symbols, "yuml", Convert(0x00FF));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigY()
        {
            var symbols = new Dictionary<String, String>();
            AddWeak(symbols, "Yacute", Convert(0x00DD));
            Add(symbols, "YAcy", Convert(0x042F));
            Add(symbols, "Ycirc", Convert(0x0176));
            Add(symbols, "Ycy", Convert(0x042B));
            Add(symbols, "Yfr", Convert(0x1D51C));
            Add(symbols, "YIcy", Convert(0x0407));
            Add(symbols, "Yopf", Convert(0x1D550));
            Add(symbols, "Yscr", Convert(0x1D4B4));
            Add(symbols, "YUcy", Convert(0x042E));
            Add(symbols, "Yuml", Convert(0x0178));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolLittleZ()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "zacute", Convert(0x017A));
            Add(symbols, "zcaron", Convert(0x017E));
            Add(symbols, "zcy", Convert(0x0437));
            Add(symbols, "zdot", Convert(0x017C));
            Add(symbols, "zeetrf", Convert(0x2128));
            Add(symbols, "zeta", Convert(0x03B6));
            Add(symbols, "zfr", Convert(0x1D537));
            Add(symbols, "zhcy", Convert(0x0436));
            Add(symbols, "zigrarr", Convert(0x21DD));
            Add(symbols, "zopf", Convert(0x1D56B));
            Add(symbols, "zscr", Convert(0x1D4CF));
            Add(symbols, "zwj", Convert(0x200D));
            Add(symbols, "zwnj", Convert(0x200C));
            return symbols;
        }

        static Dictionary<String, String> GetSymbolBigZ()
        {
            var symbols = new Dictionary<String, String>();
            Add(symbols, "Zacute", Convert(0x0179));
            Add(symbols, "Zcaron", Convert(0x017D));
            Add(symbols, "Zcy", Convert(0x0417));
            Add(symbols, "Zdot", Convert(0x017B));
            Add(symbols, "ZeroWidthSpace", Convert(0x200B));
            Add(symbols, "Zeta", Convert(0x0396));
            Add(symbols, "Zfr", Convert(0x2128));
            Add(symbols, "ZHcy", Convert(0x0416));
            Add(symbols, "Zopf", Convert(0x2124));
            Add(symbols, "Zscr", Convert(0x1D4B5));
            return symbols;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an symbol (that ended with a semicolon) specified by its entity name.
        /// </summary>
        /// <param name="name">The name of the entity, specified by &amp;NAME; in the Html source code.</param>
        /// <returns>The string with the symbol or null.</returns>
        public static String GetSymbol(String name)
        {
            var symbol = default(String);
            var symbols = default(Dictionary<String, String>);

            if (!String.IsNullOrEmpty(name) && _strongEntities.TryGetValue(name[0], out symbols))
                symbols.TryGetValue(name, out symbol);

            return symbol;
        }

        /// <summary>
        /// Gets an symbol (that did not end with a semicolon) specified by its entity name.
        /// </summary>
        /// <param name="name">The name of the entity, specified by &amp;NAME in the Html source code.</param>
        /// <returns>The string with the symbol or null.</returns>
        public static String GetSymbolWithoutSemicolon(String name)
        {
            var symbol = default(String);

            if (!String.IsNullOrEmpty(name))
                _weakEntities.TryGetValue(name, out symbol);

            return symbol;
        }

        /// <summary>
        /// Converts a given number into its unicode character.
        /// </summary>
        /// <param name="code">The code to convert.</param>
        /// <returns>The array containing the character.</returns>
        public static String Convert(Int32 code)
        {
            return code.ConvertFromUtf32();
        }

        /// <summary>
        /// Converts a set of two numbers into their unicode characters.
        /// </summary>
        /// <param name="leadingCode">The first (leading) character code.</param>
        /// <param name="trailingCode">The second (trailing) character code.</param>
        /// <returns>The array containing the two characters.</returns>
        public static String Convert(Int32 leadingCode, Int32 trailingCode)
        {
            return leadingCode.ConvertFromUtf32() + trailingCode.ConvertFromUtf32();
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

        #region Helper

        static void Add(Dictionary<String, String> symbols, String key, String value)
        {
            symbols.Add(key, value);
        }

        static void AddWeak(Dictionary<String, String> symbols, String key, String value)
        {
            Add(symbols, key, value);
            _weakEntities.Add(key, value);
        }

        #endregion
    }
}

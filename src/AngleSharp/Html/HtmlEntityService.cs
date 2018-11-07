namespace AngleSharp.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the list of all Html entities.
    /// </summary>
    public sealed class HtmlEntityService : IEntityProvider
    {
        #region Fields

        readonly Dictionary<Char, Dictionary<String, String>> _entities;

        #endregion

        #region Instance

        /// <summary>
        /// Gets the instance to resolve entities.
        /// </summary>
        public static readonly IEntityProvider Resolver = new HtmlEntityService();

        #endregion

        #region ctor

        private HtmlEntityService()
        {
            _entities = new Dictionary<Char, Dictionary<String, String>>
                            {
                                { 'a', this.GetSymbolLittleA() },
                                { 'A', this.GetSymbolBigA() },
                                { 'b', this.GetSymbolLittleB() },
                                { 'B', this.GetSymbolBigB() },
                                { 'c', this.GetSymbolLittleC() },
                                { 'C', this.GetSymbolBigC() },
                                { 'd', this.GetSymbolLittleD() },
                                { 'D', this.GetSymbolBigD() },
                                { 'e', this.GetSymbolLittleE() },
                                { 'E', this.GetSymbolBigE() },
                                { 'f', this.GetSymbolLittleF() },
                                { 'F', this.GetSymbolBigF() },
                                { 'g', this.GetSymbolLittleG() },
                                { 'G', this.GetSymbolBigG() },
                                { 'h', this.GetSymbolLittleH() },
                                { 'H', this.GetSymbolBigH() },
                                { 'i', this.GetSymbolLittleI() },
                                { 'I', this.GetSymbolBigI() },
                                { 'j', this.GetSymbolLittleJ() },
                                { 'J', this.GetSymbolBigJ() },
                                { 'k', this.GetSymbolLittleK() },
                                { 'K', this.GetSymbolBigK() },
                                { 'l', this.GetSymbolLittleL() },
                                { 'L', this.GetSymbolBigL() },
                                { 'm', this.GetSymbolLittleM() },
                                { 'M', this.GetSymbolBigM() },
                                { 'n', this.GetSymbolLittleN() },
                                { 'N', this.GetSymbolBigN() },
                                { 'o', this.GetSymbolLittleO() },
                                { 'O', this.GetSymbolBigO() },
                                { 'p', this.GetSymbolLittleP() },
                                { 'P', this.GetSymbolBigP() },
                                { 'q', this.GetSymbolLittleQ() },
                                { 'Q', this.GetSymbolBigQ() },
                                { 'r', this.GetSymbolLittleR() },
                                { 'R', this.GetSymbolBigR() },
                                { 's', this.GetSymbolLittleS() },
                                { 'S', this.GetSymbolBigS() },
                                { 't', this.GetSymbolLittleT() },
                                { 'T', this.GetSymbolBigT() },
                                { 'u', this.GetSymbolLittleU() },
                                { 'U', this.GetSymbolBigU() },
                                { 'v', this.GetSymbolLittleV() },
                                { 'V', this.GetSymbolBigV() },
                                { 'w', this.GetSymbolLittleW() },
                                { 'W', this.GetSymbolBigW() },
                                { 'x', this.GetSymbolLittleX() },
                                { 'X', this.GetSymbolBigX() },
                                { 'y', this.GetSymbolLittleY() },
                                { 'Y', this.GetSymbolBigY() },
                                { 'z', this.GetSymbolLittleZ() },
                                { 'Z', this.GetSymbolBigZ() }
                            };


























        }

        #endregion

        #region Symbol Methods

        Dictionary<String, String> GetSymbolLittleA()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "aacute;", Convert(0x00E1));
            AddSingle(symbols, "abreve;", Convert(0x0103));
            AddSingle(symbols, "ac;", Convert(0x223E));
            AddSingle(symbols, "acd;", Convert(0x223F));
            AddSingle(symbols, "acE;", Convert(0x223E, 0x0333));
            AddBoth(symbols, "acirc;", Convert(0x00E2));
            AddBoth(symbols, "acute;", Convert(0x00B4));
            AddSingle(symbols, "acy;", Convert(0x0430));
            AddBoth(symbols, "aelig;", Convert(0x00E6));
            AddSingle(symbols, "af;", Convert(0x2061));
            AddSingle(symbols, "afr;", Convert(0x1D51E));
            AddBoth(symbols, "agrave;", Convert(0x00E0));
            AddSingle(symbols, "alefsym;", Convert(0x2135));
            AddSingle(symbols, "aleph;", Convert(0x2135));
            AddSingle(symbols, "alpha;", Convert(0x03B1));
            AddSingle(symbols, "amacr;", Convert(0x0101));
            AddSingle(symbols, "amalg;", Convert(0x2A3F));
            AddBoth(symbols, "amp;", Convert(0x0026));
            AddSingle(symbols, "and;", Convert(0x2227));
            AddSingle(symbols, "andand;", Convert(0x2A55));
            AddSingle(symbols, "andd;", Convert(0x2A5C));
            AddSingle(symbols, "andslope;", Convert(0x2A58));
            AddSingle(symbols, "andv;", Convert(0x2A5A));
            AddSingle(symbols, "ang;", Convert(0x2220));
            AddSingle(symbols, "ange;", Convert(0x29A4));
            AddSingle(symbols, "angle;", Convert(0x2220));
            AddSingle(symbols, "angmsd;", Convert(0x2221));
            AddSingle(symbols, "angmsdaa;", Convert(0x29A8));
            AddSingle(symbols, "angmsdab;", Convert(0x29A9));
            AddSingle(symbols, "angmsdac;", Convert(0x29AA));
            AddSingle(symbols, "angmsdad;", Convert(0x29AB));
            AddSingle(symbols, "angmsdae;", Convert(0x29AC));
            AddSingle(symbols, "angmsdaf;", Convert(0x29AD));
            AddSingle(symbols, "angmsdag;", Convert(0x29AE));
            AddSingle(symbols, "angmsdah;", Convert(0x29AF));
            AddSingle(symbols, "angrt;", Convert(0x221F));
            AddSingle(symbols, "angrtvb;", Convert(0x22BE));
            AddSingle(symbols, "angrtvbd;", Convert(0x299D));
            AddSingle(symbols, "angsph;", Convert(0x2222));
            AddSingle(symbols, "angst;", Convert(0x00C5));
            AddSingle(symbols, "angzarr;", Convert(0x237C));
            AddSingle(symbols, "aogon;", Convert(0x0105));
            AddSingle(symbols, "aopf;", Convert(0x1D552));
            AddSingle(symbols, "ap;", Convert(0x2248));
            AddSingle(symbols, "apacir;", Convert(0x2A6F));
            AddSingle(symbols, "apE;", Convert(0x2A70));
            AddSingle(symbols, "ape;", Convert(0x224A));
            AddSingle(symbols, "apid;", Convert(0x224B));
            AddSingle(symbols, "apos;", Convert(0x0027));
            AddSingle(symbols, "approx;", Convert(0x2248));
            AddSingle(symbols, "approxeq;", Convert(0x224A));
            AddBoth(symbols, "aring;", Convert(0x00E5));
            AddSingle(symbols, "ascr;", Convert(0x1D4B6));
            AddSingle(symbols, "ast;", Convert(0x002A));
            AddSingle(symbols, "asymp;", Convert(0x2248));
            AddSingle(symbols, "asympeq;", Convert(0x224D));
            AddBoth(symbols, "atilde;", Convert(0x00E3));
            AddBoth(symbols, "auml;", Convert(0x00E4));
            AddSingle(symbols, "awconint;", Convert(0x2233));
            AddSingle(symbols, "awint;", Convert(0x2A11));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigA()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Aogon;", Convert(0x0104));
            AddSingle(symbols, "Aopf;", Convert(0x1D538));
            AddSingle(symbols, "ApplyFunction;", Convert(0x2061));
            AddBoth(symbols, "Aring;", Convert(0x00C5));
            AddSingle(symbols, "Ascr;", Convert(0x1D49C));
            AddSingle(symbols, "Assign;", Convert(0x2254));
            AddBoth(symbols, "Atilde;", Convert(0x00C3));
            AddBoth(symbols, "Auml;", Convert(0x00C4));
            AddBoth(symbols, "Aacute;", Convert(0x00C1));
            AddSingle(symbols, "Abreve;", Convert(0x0102));
            AddBoth(symbols, "Acirc;", Convert(0x00C2));
            AddSingle(symbols, "Acy;", Convert(0x0410));
            AddBoth(symbols, "AElig;", Convert(0x00C6));
            AddSingle(symbols, "Afr;", Convert(0x1D504));
            AddBoth(symbols, "Agrave;", Convert(0x00C0));
            AddSingle(symbols, "Alpha;", Convert(0x0391));
            AddSingle(symbols, "Amacr;", Convert(0x0100));
            AddBoth(symbols, "AMP;", Convert(0x0026));
            AddSingle(symbols, "And;", Convert(0x2A53));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleB()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "backcong;", Convert(0x224C));
            AddSingle(symbols, "backepsilon;", Convert(0x03F6));
            AddSingle(symbols, "backprime;", Convert(0x2035));
            AddSingle(symbols, "backsim;", Convert(0x223D));
            AddSingle(symbols, "backsimeq;", Convert(0x22CD));
            AddSingle(symbols, "barvee;", Convert(0x22BD));
            AddSingle(symbols, "barwed;", Convert(0x2305));
            AddSingle(symbols, "barwedge;", Convert(0x2305));
            AddSingle(symbols, "bbrk;", Convert(0x23B5));
            AddSingle(symbols, "bbrktbrk;", Convert(0x23B6));
            AddSingle(symbols, "bcong;", Convert(0x224C));
            AddSingle(symbols, "bcy;", Convert(0x0431));
            AddSingle(symbols, "bdquo;", Convert(0x201E));
            AddSingle(symbols, "becaus;", Convert(0x2235));
            AddSingle(symbols, "because;", Convert(0x2235));
            AddSingle(symbols, "bemptyv;", Convert(0x29B0));
            AddSingle(symbols, "bepsi;", Convert(0x03F6));
            AddSingle(symbols, "bernou;", Convert(0x212C));
            AddSingle(symbols, "beta;", Convert(0x03B2));
            AddSingle(symbols, "beth;", Convert(0x2136));
            AddSingle(symbols, "between;", Convert(0x226C));
            AddSingle(symbols, "bfr;", Convert(0x1D51F));
            AddSingle(symbols, "bigcap;", Convert(0x22C2));
            AddSingle(symbols, "bigcirc;", Convert(0x25EF));
            AddSingle(symbols, "bigcup;", Convert(0x22C3));
            AddSingle(symbols, "bigodot;", Convert(0x2A00));
            AddSingle(symbols, "bigoplus;", Convert(0x2A01));
            AddSingle(symbols, "bigotimes;", Convert(0x2A02));
            AddSingle(symbols, "bigsqcup;", Convert(0x2A06));
            AddSingle(symbols, "bigstar;", Convert(0x2605));
            AddSingle(symbols, "bigtriangledown;", Convert(0x25BD));
            AddSingle(symbols, "bigtriangleup;", Convert(0x25B3));
            AddSingle(symbols, "biguplus;", Convert(0x2A04));
            AddSingle(symbols, "bigvee;", Convert(0x22C1));
            AddSingle(symbols, "bigwedge;", Convert(0x22C0));
            AddSingle(symbols, "bkarow;", Convert(0x290D));
            AddSingle(symbols, "blacklozenge;", Convert(0x29EB));
            AddSingle(symbols, "blacksquare;", Convert(0x25AA));
            AddSingle(symbols, "blacktriangle;", Convert(0x25B4));
            AddSingle(symbols, "blacktriangledown;", Convert(0x25BE));
            AddSingle(symbols, "blacktriangleleft;", Convert(0x25C2));
            AddSingle(symbols, "blacktriangleright;", Convert(0x25B8));
            AddSingle(symbols, "blank;", Convert(0x2423));
            AddSingle(symbols, "blk12;", Convert(0x2592));
            AddSingle(symbols, "blk14;", Convert(0x2591));
            AddSingle(symbols, "blk34;", Convert(0x2593));
            AddSingle(symbols, "block;", Convert(0x2588));
            AddSingle(symbols, "bne;", Convert(0x003D, 0x20E5));
            AddSingle(symbols, "bnequiv;", Convert(0x2261, 0x20E5));
            AddSingle(symbols, "bNot;", Convert(0x2AED));
            AddSingle(symbols, "bnot;", Convert(0x2310));
            AddSingle(symbols, "bopf;", Convert(0x1D553));
            AddSingle(symbols, "bot;", Convert(0x22A5));
            AddSingle(symbols, "bottom;", Convert(0x22A5));
            AddSingle(symbols, "bowtie;", Convert(0x22C8));
            AddSingle(symbols, "boxbox;", Convert(0x29C9));
            AddSingle(symbols, "boxDL;", Convert(0x2557));
            AddSingle(symbols, "boxDl;", Convert(0x2556));
            AddSingle(symbols, "boxdL;", Convert(0x2555));
            AddSingle(symbols, "boxdl;", Convert(0x2510));
            AddSingle(symbols, "boxDR;", Convert(0x2554));
            AddSingle(symbols, "boxDr;", Convert(0x2553));
            AddSingle(symbols, "boxdR;", Convert(0x2552));
            AddSingle(symbols, "boxdr;", Convert(0x250C));
            AddSingle(symbols, "boxH;", Convert(0x2550));
            AddSingle(symbols, "boxh;", Convert(0x2500));
            AddSingle(symbols, "boxHD;", Convert(0x2566));
            AddSingle(symbols, "boxHd;", Convert(0x2564));
            AddSingle(symbols, "boxhD;", Convert(0x2565));
            AddSingle(symbols, "boxhd;", Convert(0x252C));
            AddSingle(symbols, "boxHU;", Convert(0x2569));
            AddSingle(symbols, "boxHu;", Convert(0x2567));
            AddSingle(symbols, "boxhU;", Convert(0x2568));
            AddSingle(symbols, "boxhu;", Convert(0x2534));
            AddSingle(symbols, "boxminus;", Convert(0x229F));
            AddSingle(symbols, "boxplus;", Convert(0x229E));
            AddSingle(symbols, "boxtimes;", Convert(0x22A0));
            AddSingle(symbols, "boxUL;", Convert(0x255D));
            AddSingle(symbols, "boxUl;", Convert(0x255C));
            AddSingle(symbols, "boxuL;", Convert(0x255B));
            AddSingle(symbols, "boxul;", Convert(0x2518));
            AddSingle(symbols, "boxUR;", Convert(0x255A));
            AddSingle(symbols, "boxUr;", Convert(0x2559));
            AddSingle(symbols, "boxuR;", Convert(0x2558));
            AddSingle(symbols, "boxur;", Convert(0x2514));
            AddSingle(symbols, "boxV;", Convert(0x2551));
            AddSingle(symbols, "boxv;", Convert(0x2502));
            AddSingle(symbols, "boxVH;", Convert(0x256C));
            AddSingle(symbols, "boxVh;", Convert(0x256B));
            AddSingle(symbols, "boxvH;", Convert(0x256A));
            AddSingle(symbols, "boxvh;", Convert(0x253C));
            AddSingle(symbols, "boxVL;", Convert(0x2563));
            AddSingle(symbols, "boxVl;", Convert(0x2562));
            AddSingle(symbols, "boxvL;", Convert(0x2561));
            AddSingle(symbols, "boxvl;", Convert(0x2524));
            AddSingle(symbols, "boxVR;", Convert(0x2560));
            AddSingle(symbols, "boxVr;", Convert(0x255F));
            AddSingle(symbols, "boxvR;", Convert(0x255E));
            AddSingle(symbols, "boxvr;", Convert(0x251C));
            AddSingle(symbols, "bprime;", Convert(0x2035));
            AddSingle(symbols, "breve;", Convert(0x02D8));
            AddBoth(symbols, "brvbar;", Convert(0x00A6));
            AddSingle(symbols, "bscr;", Convert(0x1D4B7));
            AddSingle(symbols, "bsemi;", Convert(0x204F));
            AddSingle(symbols, "bsim;", Convert(0x223D));
            AddSingle(symbols, "bsime;", Convert(0x22CD));
            AddSingle(symbols, "bsol;", Convert(0x005C));
            AddSingle(symbols, "bsolb;", Convert(0x29C5));
            AddSingle(symbols, "bsolhsub;", Convert(0x27C8));
            AddSingle(symbols, "bull;", Convert(0x2022));
            AddSingle(symbols, "bullet;", Convert(0x2022));
            AddSingle(symbols, "bump;", Convert(0x224E));
            AddSingle(symbols, "bumpE;", Convert(0x2AAE));
            AddSingle(symbols, "bumpe;", Convert(0x224F));
            AddSingle(symbols, "bumpeq;", Convert(0x224F));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigB()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Backslash;", Convert(0x2216));
            AddSingle(symbols, "Barv;", Convert(0x2AE7));
            AddSingle(symbols, "Barwed;", Convert(0x2306));
            AddSingle(symbols, "Bcy;", Convert(0x0411));
            AddSingle(symbols, "Because;", Convert(0x2235));
            AddSingle(symbols, "Bernoullis;", Convert(0x212C));
            AddSingle(symbols, "Beta;", Convert(0x0392));
            AddSingle(symbols, "Bfr;", Convert(0x1D505));
            AddSingle(symbols, "Bopf;", Convert(0x1D539));
            AddSingle(symbols, "Breve;", Convert(0x02D8));
            AddSingle(symbols, "Bscr;", Convert(0x212C));
            AddSingle(symbols, "Bumpeq;", Convert(0x224E));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleC()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "cacute;", Convert(0x0107));
            AddSingle(symbols, "cap;", Convert(0x2229));
            AddSingle(symbols, "capand;", Convert(0x2A44));
            AddSingle(symbols, "capbrcup;", Convert(0x2A49));
            AddSingle(symbols, "capcap;", Convert(0x2A4B));
            AddSingle(symbols, "capcup;", Convert(0x2A47));
            AddSingle(symbols, "capdot;", Convert(0x2A40));
            AddSingle(symbols, "caps;", Convert(0x2229, 0xFE00));
            AddSingle(symbols, "caret;", Convert(0x2041));
            AddSingle(symbols, "caron;", Convert(0x02C7));
            AddSingle(symbols, "ccaps;", Convert(0x2A4D));
            AddSingle(symbols, "ccaron;", Convert(0x010D));
            AddBoth(symbols, "ccedil;", Convert(0x00E7));
            AddSingle(symbols, "ccirc;", Convert(0x0109));
            AddSingle(symbols, "ccups;", Convert(0x2A4C));
            AddSingle(symbols, "ccupssm;", Convert(0x2A50));
            AddSingle(symbols, "cdot;", Convert(0x010B));
            AddBoth(symbols, "cedil;", Convert(0x00B8));
            AddSingle(symbols, "cemptyv;", Convert(0x29B2));
            AddBoth(symbols, "cent;", Convert(0x00A2));
            AddSingle(symbols, "centerdot;", Convert(0x00B7));
            AddSingle(symbols, "cfr;", Convert(0x1D520));
            AddSingle(symbols, "chcy;", Convert(0x0447));
            AddSingle(symbols, "check;", Convert(0x2713));
            AddSingle(symbols, "checkmark;", Convert(0x2713));
            AddSingle(symbols, "chi;", Convert(0x03C7));
            AddSingle(symbols, "cir;", Convert(0x25CB));
            AddSingle(symbols, "circ;", Convert(0x02C6));
            AddSingle(symbols, "circeq;", Convert(0x2257));
            AddSingle(symbols, "circlearrowleft;", Convert(0x21BA));
            AddSingle(symbols, "circlearrowright;", Convert(0x21BB));
            AddSingle(symbols, "circledast;", Convert(0x229B));
            AddSingle(symbols, "circledcirc;", Convert(0x229A));
            AddSingle(symbols, "circleddash;", Convert(0x229D));
            AddSingle(symbols, "circledR;", Convert(0x00AE));
            AddSingle(symbols, "circledS;", Convert(0x24C8));
            AddSingle(symbols, "cirE;", Convert(0x29C3));
            AddSingle(symbols, "cire;", Convert(0x2257));
            AddSingle(symbols, "cirfnint;", Convert(0x2A10));
            AddSingle(symbols, "cirmid;", Convert(0x2AEF));
            AddSingle(symbols, "cirscir;", Convert(0x29C2));
            AddSingle(symbols, "clubs;", Convert(0x2663));
            AddSingle(symbols, "clubsuit;", Convert(0x2663));
            AddSingle(symbols, "colon;", Convert(0x003A));
            AddSingle(symbols, "colone;", Convert(0x2254));
            AddSingle(symbols, "coloneq;", Convert(0x2254));
            AddSingle(symbols, "comma;", Convert(0x002C));
            AddSingle(symbols, "commat;", Convert(0x0040));
            AddSingle(symbols, "comp;", Convert(0x2201));
            AddSingle(symbols, "compfn;", Convert(0x2218));
            AddSingle(symbols, "complement;", Convert(0x2201));
            AddSingle(symbols, "complexes;", Convert(0x2102));
            AddSingle(symbols, "cong;", Convert(0x2245));
            AddSingle(symbols, "congdot;", Convert(0x2A6D));
            AddSingle(symbols, "conint;", Convert(0x222E));
            AddSingle(symbols, "copf;", Convert(0x1D554));
            AddSingle(symbols, "coprod;", Convert(0x2210));
            AddBoth(symbols, "copy;", Convert(0x00A9));
            AddSingle(symbols, "copysr;", Convert(0x2117));
            AddSingle(symbols, "crarr;", Convert(0x21B5));
            AddSingle(symbols, "cross;", Convert(0x2717));
            AddSingle(symbols, "cscr;", Convert(0x1D4B8));
            AddSingle(symbols, "csub;", Convert(0x2ACF));
            AddSingle(symbols, "csube;", Convert(0x2AD1));
            AddSingle(symbols, "csup;", Convert(0x2AD0));
            AddSingle(symbols, "csupe;", Convert(0x2AD2));
            AddSingle(symbols, "ctdot;", Convert(0x22EF));
            AddSingle(symbols, "cudarrl;", Convert(0x2938));
            AddSingle(symbols, "cudarrr;", Convert(0x2935));
            AddSingle(symbols, "cuepr;", Convert(0x22DE));
            AddSingle(symbols, "cuesc;", Convert(0x22DF));
            AddSingle(symbols, "cularr;", Convert(0x21B6));
            AddSingle(symbols, "cularrp;", Convert(0x293D));
            AddSingle(symbols, "cup;", Convert(0x222A));
            AddSingle(symbols, "cupbrcap;", Convert(0x2A48));
            AddSingle(symbols, "cupcap;", Convert(0x2A46));
            AddSingle(symbols, "cupcup;", Convert(0x2A4A));
            AddSingle(symbols, "cupdot;", Convert(0x228D));
            AddSingle(symbols, "cupor;", Convert(0x2A45));
            AddSingle(symbols, "cups;", Convert(0x222A, 0xFE00));
            AddSingle(symbols, "curarr;", Convert(0x21B7));
            AddSingle(symbols, "curarrm;", Convert(0x293C));
            AddSingle(symbols, "curlyeqprec;", Convert(0x22DE));
            AddSingle(symbols, "curlyeqsucc;", Convert(0x22DF));
            AddSingle(symbols, "curlyvee;", Convert(0x22CE));
            AddSingle(symbols, "curlywedge;", Convert(0x22CF));
            AddBoth(symbols, "curren;", Convert(0x00A4));
            AddSingle(symbols, "curvearrowleft;", Convert(0x21B6));
            AddSingle(symbols, "curvearrowright;", Convert(0x21B7));
            AddSingle(symbols, "cuvee;", Convert(0x22CE));
            AddSingle(symbols, "cuwed;", Convert(0x22CF));
            AddSingle(symbols, "cwconint;", Convert(0x2232));
            AddSingle(symbols, "cwint;", Convert(0x2231));
            AddSingle(symbols, "cylcty;", Convert(0x232D));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigC()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Cacute;", Convert(0x0106));
            AddSingle(symbols, "Cap;", Convert(0x22D2));
            AddSingle(symbols, "CapitalDifferentialD;", Convert(0x2145));
            AddSingle(symbols, "Cayleys;", Convert(0x212D));
            AddSingle(symbols, "Ccaron;", Convert(0x010C));
            AddBoth(symbols, "Ccedil;", Convert(0x00C7));
            AddSingle(symbols, "Ccirc;", Convert(0x0108));
            AddSingle(symbols, "Cconint;", Convert(0x2230));
            AddSingle(symbols, "Cdot;", Convert(0x010A));
            AddSingle(symbols, "Cedilla;", Convert(0x00B8));
            AddSingle(symbols, "CenterDot;", Convert(0x00B7));
            AddSingle(symbols, "Cfr;", Convert(0x212D));
            AddSingle(symbols, "CHcy;", Convert(0x0427));
            AddSingle(symbols, "Chi;", Convert(0x03A7));
            AddSingle(symbols, "CircleDot;", Convert(0x2299));
            AddSingle(symbols, "CircleMinus;", Convert(0x2296));
            AddSingle(symbols, "CirclePlus;", Convert(0x2295));
            AddSingle(symbols, "CircleTimes;", Convert(0x2297));
            AddSingle(symbols, "ClockwiseContourIntegral;", Convert(0x2232));
            AddSingle(symbols, "CloseCurlyDoubleQuote;", Convert(0x201D));
            AddSingle(symbols, "CloseCurlyQuote;", Convert(0x2019));
            AddSingle(symbols, "Colon;", Convert(0x2237));
            AddSingle(symbols, "Colone;", Convert(0x2A74));
            AddSingle(symbols, "Congruent;", Convert(0x2261));
            AddSingle(symbols, "Conint;", Convert(0x222F));
            AddSingle(symbols, "ContourIntegral;", Convert(0x222E));
            AddSingle(symbols, "Copf;", Convert(0x2102));
            AddSingle(symbols, "Coproduct;", Convert(0x2210));
            AddBoth(symbols, "COPY;", Convert(0x00A9));
            AddSingle(symbols, "CounterClockwiseContourIntegral;", Convert(0x2233));
            AddSingle(symbols, "Cross;", Convert(0x2A2F));
            AddSingle(symbols, "Cscr;", Convert(0x1D49E));
            AddSingle(symbols, "Cup;", Convert(0x22D3));
            AddSingle(symbols, "CupCap;", Convert(0x224D));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleD()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "dagger;", Convert(0x2020));
            AddSingle(symbols, "daleth;", Convert(0x2138));
            AddSingle(symbols, "dArr;", Convert(0x21D3));
            AddSingle(symbols, "darr;", Convert(0x2193));
            AddSingle(symbols, "dash;", Convert(0x2010));
            AddSingle(symbols, "dashv;", Convert(0x22A3));
            AddSingle(symbols, "dbkarow;", Convert(0x290F));
            AddSingle(symbols, "dblac;", Convert(0x02DD));
            AddSingle(symbols, "dcaron;", Convert(0x010F));
            AddSingle(symbols, "dcy;", Convert(0x0434));
            AddSingle(symbols, "dd;", Convert(0x2146));
            AddSingle(symbols, "ddagger;", Convert(0x2021));
            AddSingle(symbols, "ddarr;", Convert(0x21CA));
            AddSingle(symbols, "ddotseq;", Convert(0x2A77));
            AddBoth(symbols, "deg;", Convert(0x00B0));
            AddSingle(symbols, "delta;", Convert(0x03B4));
            AddSingle(symbols, "demptyv;", Convert(0x29B1));
            AddSingle(symbols, "dfisht;", Convert(0x297F));
            AddSingle(symbols, "dfr;", Convert(0x1D521));
            AddSingle(symbols, "dHar;", Convert(0x2965));
            AddSingle(symbols, "dharl;", Convert(0x21C3));
            AddSingle(symbols, "dharr;", Convert(0x21C2));
            AddSingle(symbols, "diam;", Convert(0x22C4));
            AddSingle(symbols, "diamond;", Convert(0x22C4));
            AddSingle(symbols, "diamondsuit;", Convert(0x2666));
            AddSingle(symbols, "diams;", Convert(0x2666));
            AddSingle(symbols, "die;", Convert(0x00A8));
            AddSingle(symbols, "digamma;", Convert(0x03DD));
            AddSingle(symbols, "disin;", Convert(0x22F2));
            AddSingle(symbols, "div;", Convert(0x00F7));
            AddBoth(symbols, "divide;", Convert(0x00F7));
            AddSingle(symbols, "divideontimes;", Convert(0x22C7));
            AddSingle(symbols, "divonx;", Convert(0x22C7));
            AddSingle(symbols, "djcy;", Convert(0x0452));
            AddSingle(symbols, "dlcorn;", Convert(0x231E));
            AddSingle(symbols, "dlcrop;", Convert(0x230D));
            AddSingle(symbols, "dollar;", Convert(0x0024));
            AddSingle(symbols, "dopf;", Convert(0x1D555));
            AddSingle(symbols, "dot;", Convert(0x02D9));
            AddSingle(symbols, "doteq;", Convert(0x2250));
            AddSingle(symbols, "doteqdot;", Convert(0x2251));
            AddSingle(symbols, "dotminus;", Convert(0x2238));
            AddSingle(symbols, "dotplus;", Convert(0x2214));
            AddSingle(symbols, "dotsquare;", Convert(0x22A1));
            AddSingle(symbols, "doublebarwedge;", Convert(0x2306));
            AddSingle(symbols, "downarrow;", Convert(0x2193));
            AddSingle(symbols, "downdownarrows;", Convert(0x21CA));
            AddSingle(symbols, "downharpoonleft;", Convert(0x21C3));
            AddSingle(symbols, "downharpoonright;", Convert(0x21C2));
            AddSingle(symbols, "drbkarow;", Convert(0x2910));
            AddSingle(symbols, "drcorn;", Convert(0x231F));
            AddSingle(symbols, "drcrop;", Convert(0x230C));
            AddSingle(symbols, "dscr;", Convert(0x1D4B9));
            AddSingle(symbols, "dscy;", Convert(0x0455));
            AddSingle(symbols, "dsol;", Convert(0x29F6));
            AddSingle(symbols, "dstrok;", Convert(0x0111));
            AddSingle(symbols, "dtdot;", Convert(0x22F1));
            AddSingle(symbols, "dtri;", Convert(0x25BF));
            AddSingle(symbols, "dtrif;", Convert(0x25BE));
            AddSingle(symbols, "duarr;", Convert(0x21F5));
            AddSingle(symbols, "duhar;", Convert(0x296F));
            AddSingle(symbols, "dwangle;", Convert(0x29A6));
            AddSingle(symbols, "dzcy;", Convert(0x045F));
            AddSingle(symbols, "dzigrarr;", Convert(0x27FF));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigD()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Dagger;", Convert(0x2021));
            AddSingle(symbols, "Darr;", Convert(0x21A1));
            AddSingle(symbols, "Dashv;", Convert(0x2AE4));
            AddSingle(symbols, "Dcaron;", Convert(0x010E));
            AddSingle(symbols, "Dcy;", Convert(0x0414));
            AddSingle(symbols, "DD;", Convert(0x2145));
            AddSingle(symbols, "DDotrahd;", Convert(0x2911));
            AddSingle(symbols, "Del;", Convert(0x2207));
            AddSingle(symbols, "Delta;", Convert(0x0394));
            AddSingle(symbols, "Dfr;", Convert(0x1D507));
            AddSingle(symbols, "DiacriticalAcute;", Convert(0x00B4));
            AddSingle(symbols, "DiacriticalDot;", Convert(0x02D9));
            AddSingle(symbols, "DiacriticalDoubleAcute;", Convert(0x02DD));
            AddSingle(symbols, "DiacriticalGrave;", Convert(0x0060));
            AddSingle(symbols, "DiacriticalTilde;", Convert(0x02DC));
            AddSingle(symbols, "Diamond;", Convert(0x22C4));
            AddSingle(symbols, "DifferentialD;", Convert(0x2146));
            AddSingle(symbols, "DJcy;", Convert(0x0402));
            AddSingle(symbols, "Dopf;", Convert(0x1D53B));
            AddSingle(symbols, "Dot;", Convert(0x00A8));
            AddSingle(symbols, "DotDot;", Convert(0x20DC));
            AddSingle(symbols, "DotEqual;", Convert(0x2250));
            AddSingle(symbols, "DoubleContourIntegral;", Convert(0x222F));
            AddSingle(symbols, "DoubleDot;", Convert(0x00A8));
            AddSingle(symbols, "DoubleDownArrow;", Convert(0x21D3));
            AddSingle(symbols, "DoubleLeftArrow;", Convert(0x21D0));
            AddSingle(symbols, "DoubleLeftRightArrow;", Convert(0x21D4));
            AddSingle(symbols, "DoubleLeftTee;", Convert(0x2AE4));
            AddSingle(symbols, "DoubleLongLeftArrow;", Convert(0x27F8));
            AddSingle(symbols, "DoubleLongLeftRightArrow;", Convert(0x27FA));
            AddSingle(symbols, "DoubleLongRightArrow;", Convert(0x27F9));
            AddSingle(symbols, "DoubleRightArrow;", Convert(0x21D2));
            AddSingle(symbols, "DoubleRightTee;", Convert(0x22A8));
            AddSingle(symbols, "DoubleUpArrow;", Convert(0x21D1));
            AddSingle(symbols, "DoubleUpDownArrow;", Convert(0x21D5));
            AddSingle(symbols, "DoubleVerticalBar;", Convert(0x2225));
            AddSingle(symbols, "DownArrow;", Convert(0x2193));
            AddSingle(symbols, "Downarrow;", Convert(0x21D3));
            AddSingle(symbols, "DownArrowBar;", Convert(0x2913));
            AddSingle(symbols, "DownArrowUpArrow;", Convert(0x21F5));
            AddSingle(symbols, "DownBreve;", Convert(0x0311));
            AddSingle(symbols, "DownLeftRightVector;", Convert(0x2950));
            AddSingle(symbols, "DownLeftTeeVector;", Convert(0x295E));
            AddSingle(symbols, "DownLeftVector;", Convert(0x21BD));
            AddSingle(symbols, "DownLeftVectorBar;", Convert(0x2956));
            AddSingle(symbols, "DownRightTeeVector;", Convert(0x295F));
            AddSingle(symbols, "DownRightVector;", Convert(0x21C1));
            AddSingle(symbols, "DownRightVectorBar;", Convert(0x2957));
            AddSingle(symbols, "DownTee;", Convert(0x22A4));
            AddSingle(symbols, "DownTeeArrow;", Convert(0x21A7));
            AddSingle(symbols, "Dscr;", Convert(0x1D49F));
            AddSingle(symbols, "DScy;", Convert(0x0405));
            AddSingle(symbols, "Dstrok;", Convert(0x0110));
            AddSingle(symbols, "DZcy;", Convert(0x040F));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleE()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "eacute;", Convert(0x00E9));
            AddSingle(symbols, "easter;", Convert(0x2A6E));
            AddSingle(symbols, "ecaron;", Convert(0x011B));
            AddSingle(symbols, "ecir;", Convert(0x2256));
            AddBoth(symbols, "ecirc;", Convert(0x00EA));
            AddSingle(symbols, "ecolon;", Convert(0x2255));
            AddSingle(symbols, "ecy;", Convert(0x044D));
            AddSingle(symbols, "eDDot;", Convert(0x2A77));
            AddSingle(symbols, "eDot;", Convert(0x2251));
            AddSingle(symbols, "edot;", Convert(0x0117));
            AddSingle(symbols, "ee;", Convert(0x2147));
            AddSingle(symbols, "efDot;", Convert(0x2252));
            AddSingle(symbols, "efr;", Convert(0x1D522));
            AddSingle(symbols, "eg;", Convert(0x2A9A));
            AddBoth(symbols, "egrave;", Convert(0x00E8));
            AddSingle(symbols, "egs;", Convert(0x2A96));
            AddSingle(symbols, "egsdot;", Convert(0x2A98));
            AddSingle(symbols, "el;", Convert(0x2A99));
            AddSingle(symbols, "elinters;", Convert(0x23E7));
            AddSingle(symbols, "ell;", Convert(0x2113));
            AddSingle(symbols, "els;", Convert(0x2A95));
            AddSingle(symbols, "elsdot;", Convert(0x2A97));
            AddSingle(symbols, "emacr;", Convert(0x0113));
            AddSingle(symbols, "empty;", Convert(0x2205));
            AddSingle(symbols, "emptyset;", Convert(0x2205));
            AddSingle(symbols, "emptyv;", Convert(0x2205));
            AddSingle(symbols, "emsp;", Convert(0x2003));
            AddSingle(symbols, "emsp13;", Convert(0x2004));
            AddSingle(symbols, "emsp14;", Convert(0x2005));
            AddSingle(symbols, "eng;", Convert(0x014B));
            AddSingle(symbols, "ensp;", Convert(0x2002));
            AddSingle(symbols, "eogon;", Convert(0x0119));
            AddSingle(symbols, "eopf;", Convert(0x1D556));
            AddSingle(symbols, "epar;", Convert(0x22D5));
            AddSingle(symbols, "eparsl;", Convert(0x29E3));
            AddSingle(symbols, "eplus;", Convert(0x2A71));
            AddSingle(symbols, "epsi;", Convert(0x03B5));
            AddSingle(symbols, "epsilon;", Convert(0x03B5));
            AddSingle(symbols, "epsiv;", Convert(0x03F5));
            AddSingle(symbols, "eqcirc;", Convert(0x2256));
            AddSingle(symbols, "eqcolon;", Convert(0x2255));
            AddSingle(symbols, "eqsim;", Convert(0x2242));
            AddSingle(symbols, "eqslantgtr;", Convert(0x2A96));
            AddSingle(symbols, "eqslantless;", Convert(0x2A95));
            AddSingle(symbols, "equals;", Convert(0x003D));
            AddSingle(symbols, "equest;", Convert(0x225F));
            AddSingle(symbols, "equiv;", Convert(0x2261));
            AddSingle(symbols, "equivDD;", Convert(0x2A78));
            AddSingle(symbols, "eqvparsl;", Convert(0x29E5));
            AddSingle(symbols, "erarr;", Convert(0x2971));
            AddSingle(symbols, "erDot;", Convert(0x2253));
            AddSingle(symbols, "escr;", Convert(0x212F));
            AddSingle(symbols, "esdot;", Convert(0x2250));
            AddSingle(symbols, "esim;", Convert(0x2242));
            AddSingle(symbols, "eta;", Convert(0x03B7));
            AddBoth(symbols, "eth;", Convert(0x00F0));
            AddBoth(symbols, "euml;", Convert(0x00EB));
            AddSingle(symbols, "euro;", Convert(0x20AC));
            AddSingle(symbols, "excl;", Convert(0x0021));
            AddSingle(symbols, "exist;", Convert(0x2203));
            AddSingle(symbols, "expectation;", Convert(0x2130));
            AddSingle(symbols, "exponentiale;", Convert(0x2147));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigE()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "Eacute;", Convert(0x00C9));
            AddSingle(symbols, "Ecaron;", Convert(0x011A));
            AddBoth(symbols, "Ecirc;", Convert(0x00CA));
            AddSingle(symbols, "Ecy;", Convert(0x042D));
            AddSingle(symbols, "Edot;", Convert(0x0116));
            AddSingle(symbols, "Efr;", Convert(0x1D508));
            AddBoth(symbols, "Egrave;", Convert(0x00C8));
            AddSingle(symbols, "Element;", Convert(0x2208));
            AddSingle(symbols, "Emacr;", Convert(0x0112));
            AddSingle(symbols, "EmptySmallSquare;", Convert(0x25FB));
            AddSingle(symbols, "EmptyVerySmallSquare;", Convert(0x25AB));
            AddSingle(symbols, "ENG;", Convert(0x014A));
            AddSingle(symbols, "Eogon;", Convert(0x0118));
            AddSingle(symbols, "Eopf;", Convert(0x1D53C));
            AddSingle(symbols, "Epsilon;", Convert(0x0395));
            AddSingle(symbols, "Equal;", Convert(0x2A75));
            AddSingle(symbols, "EqualTilde;", Convert(0x2242));
            AddSingle(symbols, "Equilibrium;", Convert(0x21CC));
            AddSingle(symbols, "Escr;", Convert(0x2130));
            AddSingle(symbols, "Esim;", Convert(0x2A73));
            AddSingle(symbols, "Eta;", Convert(0x0397));
            AddBoth(symbols, "ETH;", Convert(0x00D0));
            AddBoth(symbols, "Euml;", Convert(0x00CB));
            AddSingle(symbols, "Exists;", Convert(0x2203));
            AddSingle(symbols, "ExponentialE;", Convert(0x2147));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleF()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "fallingdotseq;", Convert(0x2252));
            AddSingle(symbols, "fcy;", Convert(0x0444));
            AddSingle(symbols, "female;", Convert(0x2640));
            AddSingle(symbols, "ffilig;", Convert(0xFB03));
            AddSingle(symbols, "fflig;", Convert(0xFB00));
            AddSingle(symbols, "ffllig;", Convert(0xFB04));
            AddSingle(symbols, "ffr;", Convert(0x1D523));
            AddSingle(symbols, "filig;", Convert(0xFB01));
            AddSingle(symbols, "fjlig;", Convert(0x0066, 0x006A));
            AddSingle(symbols, "flat;", Convert(0x266D));
            AddSingle(symbols, "fllig;", Convert(0xFB02));
            AddSingle(symbols, "fltns;", Convert(0x25B1));
            AddSingle(symbols, "fnof;", Convert(0x0192));
            AddSingle(symbols, "fopf;", Convert(0x1D557));
            AddSingle(symbols, "forall;", Convert(0x2200));
            AddSingle(symbols, "fork;", Convert(0x22D4));
            AddSingle(symbols, "forkv;", Convert(0x2AD9));
            AddSingle(symbols, "fpartint;", Convert(0x2A0D));
            AddBoth(symbols, "frac12;", Convert(0x00BD));
            AddSingle(symbols, "frac13;", Convert(0x2153));
            AddBoth(symbols, "frac14;", Convert(0x00BC));
            AddSingle(symbols, "frac15;", Convert(0x2155));
            AddSingle(symbols, "frac16;", Convert(0x2159));
            AddSingle(symbols, "frac18;", Convert(0x215B));
            AddSingle(symbols, "frac23;", Convert(0x2154));
            AddSingle(symbols, "frac25;", Convert(0x2156));
            AddBoth(symbols, "frac34;", Convert(0x00BE));
            AddSingle(symbols, "frac35;", Convert(0x2157));
            AddSingle(symbols, "frac38;", Convert(0x215C));
            AddSingle(symbols, "frac45;", Convert(0x2158));
            AddSingle(symbols, "frac56;", Convert(0x215A));
            AddSingle(symbols, "frac58;", Convert(0x215D));
            AddSingle(symbols, "frac78;", Convert(0x215E));
            AddSingle(symbols, "frasl;", Convert(0x2044));
            AddSingle(symbols, "frown;", Convert(0x2322));
            AddSingle(symbols, "fscr;", Convert(0x1D4BB));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigF()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Fcy;", Convert(0x0424));
            AddSingle(symbols, "Ffr;", Convert(0x1D509));
            AddSingle(symbols, "FilledSmallSquare;", Convert(0x25FC));
            AddSingle(symbols, "FilledVerySmallSquare;", Convert(0x25AA));
            AddSingle(symbols, "Fopf;", Convert(0x1D53D));
            AddSingle(symbols, "ForAll;", Convert(0x2200));
            AddSingle(symbols, "Fouriertrf;", Convert(0x2131));
            AddSingle(symbols, "Fscr;", Convert(0x2131));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleG()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "gacute;", Convert(0x01F5));
            AddSingle(symbols, "gamma;", Convert(0x03B3));
            AddSingle(symbols, "gammad;", Convert(0x03DD));
            AddSingle(symbols, "gap;", Convert(0x2A86));
            AddSingle(symbols, "gbreve;", Convert(0x011F));
            AddSingle(symbols, "gcirc;", Convert(0x011D));
            AddSingle(symbols, "gcy;", Convert(0x0433));
            AddSingle(symbols, "gdot;", Convert(0x0121));
            AddSingle(symbols, "gE;", Convert(0x2267));
            AddSingle(symbols, "ge;", Convert(0x2265));
            AddSingle(symbols, "gEl;", Convert(0x2A8C));
            AddSingle(symbols, "gel;", Convert(0x22DB));
            AddSingle(symbols, "geq;", Convert(0x2265));
            AddSingle(symbols, "geqq;", Convert(0x2267));
            AddSingle(symbols, "geqslant;", Convert(0x2A7E));
            AddSingle(symbols, "ges;", Convert(0x2A7E));
            AddSingle(symbols, "gescc;", Convert(0x2AA9));
            AddSingle(symbols, "gesdot;", Convert(0x2A80));
            AddSingle(symbols, "gesdoto;", Convert(0x2A82));
            AddSingle(symbols, "gesdotol;", Convert(0x2A84));
            AddSingle(symbols, "gesl;", Convert(0x22DB, 0xFE00));
            AddSingle(symbols, "gesles;", Convert(0x2A94));
            AddSingle(symbols, "gfr;", Convert(0x1D524));
            AddSingle(symbols, "gg;", Convert(0x226B));
            AddSingle(symbols, "ggg;", Convert(0x22D9));
            AddSingle(symbols, "gimel;", Convert(0x2137));
            AddSingle(symbols, "gjcy;", Convert(0x0453));
            AddSingle(symbols, "gl;", Convert(0x2277));
            AddSingle(symbols, "gla;", Convert(0x2AA5));
            AddSingle(symbols, "glE;", Convert(0x2A92));
            AddSingle(symbols, "glj;", Convert(0x2AA4));
            AddSingle(symbols, "gnap;", Convert(0x2A8A));
            AddSingle(symbols, "gnapprox;", Convert(0x2A8A));
            AddSingle(symbols, "gnE;", Convert(0x2269));
            AddSingle(symbols, "gne;", Convert(0x2A88));
            AddSingle(symbols, "gneq;", Convert(0x2A88));
            AddSingle(symbols, "gneqq;", Convert(0x2269));
            AddSingle(symbols, "gnsim;", Convert(0x22E7));
            AddSingle(symbols, "gopf;", Convert(0x1D558));
            AddSingle(symbols, "grave;", Convert(0x0060));
            AddSingle(symbols, "gscr;", Convert(0x210A));
            AddSingle(symbols, "gsim;", Convert(0x2273));
            AddSingle(symbols, "gsime;", Convert(0x2A8E));
            AddSingle(symbols, "gsiml;", Convert(0x2A90));
            AddBoth(symbols, "gt;", Convert(0x003E));
            AddSingle(symbols, "gtcc;", Convert(0x2AA7));
            AddSingle(symbols, "gtcir;", Convert(0x2A7A));
            AddSingle(symbols, "gtdot;", Convert(0x22D7));
            AddSingle(symbols, "gtlPar;", Convert(0x2995));
            AddSingle(symbols, "gtquest;", Convert(0x2A7C));
            AddSingle(symbols, "gtrapprox;", Convert(0x2A86));
            AddSingle(symbols, "gtrarr;", Convert(0x2978));
            AddSingle(symbols, "gtrdot;", Convert(0x22D7));
            AddSingle(symbols, "gtreqless;", Convert(0x22DB));
            AddSingle(symbols, "gtreqqless;", Convert(0x2A8C));
            AddSingle(symbols, "gtrless;", Convert(0x2277));
            AddSingle(symbols, "gtrsim;", Convert(0x2273));
            AddSingle(symbols, "gvertneqq;", Convert(0x2269, 0xFE00));
            AddSingle(symbols, "gvnE;", Convert(0x2269, 0xFE00));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigG()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Gamma;", Convert(0x0393));
            AddSingle(symbols, "Gammad;", Convert(0x03DC));
            AddSingle(symbols, "Gbreve;", Convert(0x011E));
            AddSingle(symbols, "Gcedil;", Convert(0x0122));
            AddSingle(symbols, "Gcirc;", Convert(0x011C));
            AddSingle(symbols, "Gcy;", Convert(0x0413));
            AddSingle(symbols, "Gdot;", Convert(0x0120));
            AddSingle(symbols, "Gfr;", Convert(0x1D50A));
            AddSingle(symbols, "Gg;", Convert(0x22D9));
            AddSingle(symbols, "GJcy;", Convert(0x0403));
            AddSingle(symbols, "Gopf;", Convert(0x1D53E));
            AddSingle(symbols, "GreaterEqual;", Convert(0x2265));
            AddSingle(symbols, "GreaterEqualLess;", Convert(0x22DB));
            AddSingle(symbols, "GreaterFullEqual;", Convert(0x2267));
            AddSingle(symbols, "GreaterGreater;", Convert(0x2AA2));
            AddSingle(symbols, "GreaterLess;", Convert(0x2277));
            AddSingle(symbols, "GreaterSlantEqual;", Convert(0x2A7E));
            AddSingle(symbols, "GreaterTilde;", Convert(0x2273));
            AddSingle(symbols, "Gscr;", Convert(0x1D4A2));
            AddBoth(symbols, "GT;", Convert(0x003E));
            AddSingle(symbols, "Gt;", Convert(0x226B));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleH()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "hairsp;", Convert(0x200A));
            AddSingle(symbols, "half;", Convert(0x00BD));
            AddSingle(symbols, "hamilt;", Convert(0x210B));
            AddSingle(symbols, "hardcy;", Convert(0x044A));
            AddSingle(symbols, "hArr;", Convert(0x21D4));
            AddSingle(symbols, "harr;", Convert(0x2194));
            AddSingle(symbols, "harrcir;", Convert(0x2948));
            AddSingle(symbols, "harrw;", Convert(0x21AD));
            AddSingle(symbols, "hbar;", Convert(0x210F));
            AddSingle(symbols, "hcirc;", Convert(0x0125));
            AddSingle(symbols, "hearts;", Convert(0x2665));
            AddSingle(symbols, "heartsuit;", Convert(0x2665));
            AddSingle(symbols, "hellip;", Convert(0x2026));
            AddSingle(symbols, "hercon;", Convert(0x22B9));
            AddSingle(symbols, "hfr;", Convert(0x1D525));
            AddSingle(symbols, "hksearow;", Convert(0x2925));
            AddSingle(symbols, "hkswarow;", Convert(0x2926));
            AddSingle(symbols, "hoarr;", Convert(0x21FF));
            AddSingle(symbols, "homtht;", Convert(0x223B));
            AddSingle(symbols, "hookleftarrow;", Convert(0x21A9));
            AddSingle(symbols, "hookrightarrow;", Convert(0x21AA));
            AddSingle(symbols, "hopf;", Convert(0x1D559));
            AddSingle(symbols, "horbar;", Convert(0x2015));
            AddSingle(symbols, "hscr;", Convert(0x1D4BD));
            AddSingle(symbols, "hslash;", Convert(0x210F));
            AddSingle(symbols, "hstrok;", Convert(0x0127));
            AddSingle(symbols, "hybull;", Convert(0x2043));
            AddSingle(symbols, "hyphen;", Convert(0x2010));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigH()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Hacek;", Convert(0x02C7));
            AddSingle(symbols, "HARDcy;", Convert(0x042A));
            AddSingle(symbols, "Hat;", Convert(0x005E));
            AddSingle(symbols, "Hcirc;", Convert(0x0124));
            AddSingle(symbols, "Hfr;", Convert(0x210C));
            AddSingle(symbols, "HilbertSpace;", Convert(0x210B));
            AddSingle(symbols, "Hopf;", Convert(0x210D));
            AddSingle(symbols, "HorizontalLine;", Convert(0x2500));
            AddSingle(symbols, "Hscr;", Convert(0x210B));
            AddSingle(symbols, "Hstrok;", Convert(0x0126));
            AddSingle(symbols, "HumpDownHump;", Convert(0x224E));
            AddSingle(symbols, "HumpEqual;", Convert(0x224F));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleI()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "iacute;", Convert(0x00ED));
            AddSingle(symbols, "ic;", Convert(0x2063));
            AddBoth(symbols, "icirc;", Convert(0x00EE));
            AddSingle(symbols, "icy;", Convert(0x0438));
            AddSingle(symbols, "iecy;", Convert(0x0435));
            AddBoth(symbols, "iexcl;", Convert(0x00A1));
            AddSingle(symbols, "iff;", Convert(0x21D4));
            AddSingle(symbols, "ifr;", Convert(0x1D526));
            AddBoth(symbols, "igrave;", Convert(0x00EC));
            AddSingle(symbols, "ii;", Convert(0x2148));
            AddSingle(symbols, "iiiint;", Convert(0x2A0C));
            AddSingle(symbols, "iiint;", Convert(0x222D));
            AddSingle(symbols, "iinfin;", Convert(0x29DC));
            AddSingle(symbols, "iiota;", Convert(0x2129));
            AddSingle(symbols, "ijlig;", Convert(0x0133));
            AddSingle(symbols, "imacr;", Convert(0x012B));
            AddSingle(symbols, "image;", Convert(0x2111));
            AddSingle(symbols, "imagline;", Convert(0x2110));
            AddSingle(symbols, "imagpart;", Convert(0x2111));
            AddSingle(symbols, "imath;", Convert(0x0131));
            AddSingle(symbols, "imof;", Convert(0x22B7));
            AddSingle(symbols, "imped;", Convert(0x01B5));
            AddSingle(symbols, "in;", Convert(0x2208));
            AddSingle(symbols, "incare;", Convert(0x2105));
            AddSingle(symbols, "infin;", Convert(0x221E));
            AddSingle(symbols, "infintie;", Convert(0x29DD));
            AddSingle(symbols, "inodot;", Convert(0x0131));
            AddSingle(symbols, "int;", Convert(0x222B));
            AddSingle(symbols, "intcal;", Convert(0x22BA));
            AddSingle(symbols, "integers;", Convert(0x2124));
            AddSingle(symbols, "intercal;", Convert(0x22BA));
            AddSingle(symbols, "intlarhk;", Convert(0x2A17));
            AddSingle(symbols, "intprod;", Convert(0x2A3C));
            AddSingle(symbols, "iocy;", Convert(0x0451));
            AddSingle(symbols, "iogon;", Convert(0x012F));
            AddSingle(symbols, "iopf;", Convert(0x1D55A));
            AddSingle(symbols, "iota;", Convert(0x03B9));
            AddSingle(symbols, "iprod;", Convert(0x2A3C));
            AddBoth(symbols, "iquest;", Convert(0x00BF));
            AddSingle(symbols, "iscr;", Convert(0x1D4BE));
            AddSingle(symbols, "isin;", Convert(0x2208));
            AddSingle(symbols, "isindot;", Convert(0x22F5));
            AddSingle(symbols, "isinE;", Convert(0x22F9));
            AddSingle(symbols, "isins;", Convert(0x22F4));
            AddSingle(symbols, "isinsv;", Convert(0x22F3));
            AddSingle(symbols, "isinv;", Convert(0x2208));
            AddSingle(symbols, "it;", Convert(0x2062));
            AddSingle(symbols, "itilde;", Convert(0x0129));
            AddSingle(symbols, "iukcy;", Convert(0x0456));
            AddBoth(symbols, "iuml;", Convert(0x00EF));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigI()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "Iacute;", Convert(0x00CD));
            AddBoth(symbols, "Icirc;", Convert(0x00CE));
            AddSingle(symbols, "Icy;", Convert(0x0418));
            AddSingle(symbols, "Idot;", Convert(0x0130));
            AddSingle(symbols, "IEcy;", Convert(0x0415));
            AddSingle(symbols, "Ifr;", Convert(0x2111));
            AddBoth(symbols, "Igrave;", Convert(0x00CC));
            AddSingle(symbols, "IJlig;", Convert(0x0132));
            AddSingle(symbols, "Im;", Convert(0x2111));
            AddSingle(symbols, "Imacr;", Convert(0x012A));
            AddSingle(symbols, "ImaginaryI;", Convert(0x2148));
            AddSingle(symbols, "Implies;", Convert(0x21D2));
            AddSingle(symbols, "Int;", Convert(0x222C));
            AddSingle(symbols, "Integral;", Convert(0x222B));
            AddSingle(symbols, "Intersection;", Convert(0x22C2));
            AddSingle(symbols, "InvisibleComma;", Convert(0x2063));
            AddSingle(symbols, "InvisibleTimes;", Convert(0x2062));
            AddSingle(symbols, "IOcy;", Convert(0x0401));
            AddSingle(symbols, "Iogon;", Convert(0x012E));
            AddSingle(symbols, "Iopf;", Convert(0x1D540));
            AddSingle(symbols, "Iota;", Convert(0x0399));
            AddSingle(symbols, "Iscr;", Convert(0x2110));
            AddSingle(symbols, "Itilde;", Convert(0x0128));
            AddSingle(symbols, "Iukcy;", Convert(0x0406));
            AddBoth(symbols, "Iuml;", Convert(0x00CF));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleJ()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "jcirc;", Convert(0x0135));
            AddSingle(symbols, "jcy;", Convert(0x0439));
            AddSingle(symbols, "jfr;", Convert(0x1D527));
            AddSingle(symbols, "jmath;", Convert(0x0237));
            AddSingle(symbols, "jopf;", Convert(0x1D55B));
            AddSingle(symbols, "jscr;", Convert(0x1D4BF));
            AddSingle(symbols, "jsercy;", Convert(0x0458));
            AddSingle(symbols, "jukcy;", Convert(0x0454));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigJ()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Jcirc;", Convert(0x0134));
            AddSingle(symbols, "Jcy;", Convert(0x0419));
            AddSingle(symbols, "Jfr;", Convert(0x1D50D));
            AddSingle(symbols, "Jopf;", Convert(0x1D541));
            AddSingle(symbols, "Jscr;", Convert(0x1D4A5));
            AddSingle(symbols, "Jsercy;", Convert(0x0408));
            AddSingle(symbols, "Jukcy;", Convert(0x0404));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleK()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "kappa;", Convert(0x03BA));
            AddSingle(symbols, "kappav;", Convert(0x03F0));
            AddSingle(symbols, "kcedil;", Convert(0x0137));
            AddSingle(symbols, "kcy;", Convert(0x043A));
            AddSingle(symbols, "kfr;", Convert(0x1D528));
            AddSingle(symbols, "kgreen;", Convert(0x0138));
            AddSingle(symbols, "khcy;", Convert(0x0445));
            AddSingle(symbols, "kjcy;", Convert(0x045C));
            AddSingle(symbols, "kopf;", Convert(0x1D55C));
            AddSingle(symbols, "kscr;", Convert(0x1D4C0));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigK()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Kappa;", Convert(0x039A));
            AddSingle(symbols, "Kcedil;", Convert(0x0136));
            AddSingle(symbols, "Kcy;", Convert(0x041A));
            AddSingle(symbols, "Kfr;", Convert(0x1D50E));
            AddSingle(symbols, "KHcy;", Convert(0x0425));
            AddSingle(symbols, "KJcy;", Convert(0x040C));
            AddSingle(symbols, "Kopf;", Convert(0x1D542));
            AddSingle(symbols, "Kscr;", Convert(0x1D4A6));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleL()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "lAarr;", Convert(0x21DA));
            AddSingle(symbols, "lacute;", Convert(0x013A));
            AddSingle(symbols, "laemptyv;", Convert(0x29B4));
            AddSingle(symbols, "lagran;", Convert(0x2112));
            AddSingle(symbols, "lambda;", Convert(0x03BB));
            AddSingle(symbols, "lang;", Convert(0x27E8));
            AddSingle(symbols, "langd;", Convert(0x2991));
            AddSingle(symbols, "langle;", Convert(0x27E8));
            AddSingle(symbols, "lap;", Convert(0x2A85));
            AddBoth(symbols, "laquo;", Convert(0x00AB));
            AddSingle(symbols, "lArr;", Convert(0x21D0));
            AddSingle(symbols, "larr;", Convert(0x2190));
            AddSingle(symbols, "larrb;", Convert(0x21E4));
            AddSingle(symbols, "larrbfs;", Convert(0x291F));
            AddSingle(symbols, "larrfs;", Convert(0x291D));
            AddSingle(symbols, "larrhk;", Convert(0x21A9));
            AddSingle(symbols, "larrlp;", Convert(0x21AB));
            AddSingle(symbols, "larrpl;", Convert(0x2939));
            AddSingle(symbols, "larrsim;", Convert(0x2973));
            AddSingle(symbols, "larrtl;", Convert(0x21A2));
            AddSingle(symbols, "lat;", Convert(0x2AAB));
            AddSingle(symbols, "lAtail;", Convert(0x291B));
            AddSingle(symbols, "latail;", Convert(0x2919));
            AddSingle(symbols, "late;", Convert(0x2AAD));
            AddSingle(symbols, "lates;", Convert(0x2AAD, 0xFE00));
            AddSingle(symbols, "lBarr;", Convert(0x290E));
            AddSingle(symbols, "lbarr;", Convert(0x290C));
            AddSingle(symbols, "lbbrk;", Convert(0x2772));
            AddSingle(symbols, "lbrace;", Convert(0x007B));
            AddSingle(symbols, "lbrack;", Convert(0x005B));
            AddSingle(symbols, "lbrke;", Convert(0x298B));
            AddSingle(symbols, "lbrksld;", Convert(0x298F));
            AddSingle(symbols, "lbrkslu;", Convert(0x298D));
            AddSingle(symbols, "lcaron;", Convert(0x013E));
            AddSingle(symbols, "lcedil;", Convert(0x013C));
            AddSingle(symbols, "lceil;", Convert(0x2308));
            AddSingle(symbols, "lcub;", Convert(0x007B));
            AddSingle(symbols, "lcy;", Convert(0x043B));
            AddSingle(symbols, "ldca;", Convert(0x2936));
            AddSingle(symbols, "ldquo;", Convert(0x201C));
            AddSingle(symbols, "ldquor;", Convert(0x201E));
            AddSingle(symbols, "ldrdhar;", Convert(0x2967));
            AddSingle(symbols, "ldrushar;", Convert(0x294B));
            AddSingle(symbols, "ldsh;", Convert(0x21B2));
            AddSingle(symbols, "lE;", Convert(0x2266));
            AddSingle(symbols, "le;", Convert(0x2264));
            AddSingle(symbols, "leftarrow;", Convert(0x2190));
            AddSingle(symbols, "leftarrowtail;", Convert(0x21A2));
            AddSingle(symbols, "leftharpoondown;", Convert(0x21BD));
            AddSingle(symbols, "leftharpoonup;", Convert(0x21BC));
            AddSingle(symbols, "leftleftarrows;", Convert(0x21C7));
            AddSingle(symbols, "leftrightarrow;", Convert(0x2194));
            AddSingle(symbols, "leftrightarrows;", Convert(0x21C6));
            AddSingle(symbols, "leftrightharpoons;", Convert(0x21CB));
            AddSingle(symbols, "leftrightsquigarrow;", Convert(0x21AD));
            AddSingle(symbols, "leftthreetimes;", Convert(0x22CB));
            AddSingle(symbols, "lEg;", Convert(0x2A8B));
            AddSingle(symbols, "leg;", Convert(0x22DA));
            AddSingle(symbols, "leq;", Convert(0x2264));
            AddSingle(symbols, "leqq;", Convert(0x2266));
            AddSingle(symbols, "leqslant;", Convert(0x2A7D));
            AddSingle(symbols, "les;", Convert(0x2A7D));
            AddSingle(symbols, "lescc;", Convert(0x2AA8));
            AddSingle(symbols, "lesdot;", Convert(0x2A7F));
            AddSingle(symbols, "lesdoto;", Convert(0x2A81));
            AddSingle(symbols, "lesdotor;", Convert(0x2A83));
            AddSingle(symbols, "lesg;", Convert(0x22DA, 0xFE00));
            AddSingle(symbols, "lesges;", Convert(0x2A93));
            AddSingle(symbols, "lessapprox;", Convert(0x2A85));
            AddSingle(symbols, "lessdot;", Convert(0x22D6));
            AddSingle(symbols, "lesseqgtr;", Convert(0x22DA));
            AddSingle(symbols, "lesseqqgtr;", Convert(0x2A8B));
            AddSingle(symbols, "lessgtr;", Convert(0x2276));
            AddSingle(symbols, "lesssim;", Convert(0x2272));
            AddSingle(symbols, "lfisht;", Convert(0x297C));
            AddSingle(symbols, "lfloor;", Convert(0x230A));
            AddSingle(symbols, "lfr;", Convert(0x1D529));
            AddSingle(symbols, "lg;", Convert(0x2276));
            AddSingle(symbols, "lgE;", Convert(0x2A91));
            AddSingle(symbols, "lHar;", Convert(0x2962));
            AddSingle(symbols, "lhard;", Convert(0x21BD));
            AddSingle(symbols, "lharu;", Convert(0x21BC));
            AddSingle(symbols, "lharul;", Convert(0x296A));
            AddSingle(symbols, "lhblk;", Convert(0x2584));
            AddSingle(symbols, "ljcy;", Convert(0x0459));
            AddSingle(symbols, "ll;", Convert(0x226A));
            AddSingle(symbols, "llarr;", Convert(0x21C7));
            AddSingle(symbols, "llcorner;", Convert(0x231E));
            AddSingle(symbols, "llhard;", Convert(0x296B));
            AddSingle(symbols, "lltri;", Convert(0x25FA));
            AddSingle(symbols, "lmidot;", Convert(0x0140));
            AddSingle(symbols, "lmoust;", Convert(0x23B0));
            AddSingle(symbols, "lmoustache;", Convert(0x23B0));
            AddSingle(symbols, "lnap;", Convert(0x2A89));
            AddSingle(symbols, "lnapprox;", Convert(0x2A89));
            AddSingle(symbols, "lnE;", Convert(0x2268));
            AddSingle(symbols, "lne;", Convert(0x2A87));
            AddSingle(symbols, "lneq;", Convert(0x2A87));
            AddSingle(symbols, "lneqq;", Convert(0x2268));
            AddSingle(symbols, "lnsim;", Convert(0x22E6));
            AddSingle(symbols, "loang;", Convert(0x27EC));
            AddSingle(symbols, "loarr;", Convert(0x21FD));
            AddSingle(symbols, "lobrk;", Convert(0x27E6));
            AddSingle(symbols, "longleftarrow;", Convert(0x27F5));
            AddSingle(symbols, "longleftrightarrow;", Convert(0x27F7));
            AddSingle(symbols, "longmapsto;", Convert(0x27FC));
            AddSingle(symbols, "longrightarrow;", Convert(0x27F6));
            AddSingle(symbols, "looparrowleft;", Convert(0x21AB));
            AddSingle(symbols, "looparrowright;", Convert(0x21AC));
            AddSingle(symbols, "lopar;", Convert(0x2985));
            AddSingle(symbols, "lopf;", Convert(0x1D55D));
            AddSingle(symbols, "loplus;", Convert(0x2A2D));
            AddSingle(symbols, "lotimes;", Convert(0x2A34));
            AddSingle(symbols, "lowast;", Convert(0x2217));
            AddSingle(symbols, "lowbar;", Convert(0x005F));
            AddSingle(symbols, "loz;", Convert(0x25CA));
            AddSingle(symbols, "lozenge;", Convert(0x25CA));
            AddSingle(symbols, "lozf;", Convert(0x29EB));
            AddSingle(symbols, "lpar;", Convert(0x0028));
            AddSingle(symbols, "lparlt;", Convert(0x2993));
            AddSingle(symbols, "lrarr;", Convert(0x21C6));
            AddSingle(symbols, "lrcorner;", Convert(0x231F));
            AddSingle(symbols, "lrhar;", Convert(0x21CB));
            AddSingle(symbols, "lrhard;", Convert(0x296D));
            AddSingle(symbols, "lrm;", Convert(0x200E));
            AddSingle(symbols, "lrtri;", Convert(0x22BF));
            AddSingle(symbols, "lsaquo;", Convert(0x2039));
            AddSingle(symbols, "lscr;", Convert(0x1D4C1));
            AddSingle(symbols, "lsh;", Convert(0x21B0));
            AddSingle(symbols, "lsim;", Convert(0x2272));
            AddSingle(symbols, "lsime;", Convert(0x2A8D));
            AddSingle(symbols, "lsimg;", Convert(0x2A8F));
            AddSingle(symbols, "lsqb;", Convert(0x005B));
            AddSingle(symbols, "lsquo;", Convert(0x2018));
            AddSingle(symbols, "lsquor;", Convert(0x201A));
            AddSingle(symbols, "lstrok;", Convert(0x0142));
            AddBoth(symbols, "lt;", Convert(0x003C));
            AddSingle(symbols, "ltcc;", Convert(0x2AA6));
            AddSingle(symbols, "ltcir;", Convert(0x2A79));
            AddSingle(symbols, "ltdot;", Convert(0x22D6));
            AddSingle(symbols, "lthree;", Convert(0x22CB));
            AddSingle(symbols, "ltimes;", Convert(0x22C9));
            AddSingle(symbols, "ltlarr;", Convert(0x2976));
            AddSingle(symbols, "ltquest;", Convert(0x2A7B));
            AddSingle(symbols, "ltri;", Convert(0x25C3));
            AddSingle(symbols, "ltrie;", Convert(0x22B4));
            AddSingle(symbols, "ltrif;", Convert(0x25C2));
            AddSingle(symbols, "ltrPar;", Convert(0x2996));
            AddSingle(symbols, "lurdshar;", Convert(0x294A));
            AddSingle(symbols, "luruhar;", Convert(0x2966));
            AddSingle(symbols, "lvertneqq;", Convert(0x2268, 0xFE00));
            AddSingle(symbols, "lvnE;", Convert(0x2268, 0xFE00));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigL()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Lacute;", Convert(0x0139));
            AddSingle(symbols, "Lambda;", Convert(0x039B));
            AddSingle(symbols, "Lang;", Convert(0x27EA));
            AddSingle(symbols, "Laplacetrf;", Convert(0x2112));
            AddSingle(symbols, "Larr;", Convert(0x219E));
            AddSingle(symbols, "Lcaron;", Convert(0x013D));
            AddSingle(symbols, "Lcedil;", Convert(0x013B));
            AddSingle(symbols, "Lcy;", Convert(0x041B));
            AddSingle(symbols, "LeftAngleBracket;", Convert(0x27E8));
            AddSingle(symbols, "LeftArrow;", Convert(0x2190));
            AddSingle(symbols, "Leftarrow;", Convert(0x21D0));
            AddSingle(symbols, "LeftArrowBar;", Convert(0x21E4));
            AddSingle(symbols, "LeftArrowRightArrow;", Convert(0x21C6));
            AddSingle(symbols, "LeftCeiling;", Convert(0x2308));
            AddSingle(symbols, "LeftDoubleBracket;", Convert(0x27E6));
            AddSingle(symbols, "LeftDownTeeVector;", Convert(0x2961));
            AddSingle(symbols, "LeftDownVector;", Convert(0x21C3));
            AddSingle(symbols, "LeftDownVectorBar;", Convert(0x2959));
            AddSingle(symbols, "LeftFloor;", Convert(0x230A));
            AddSingle(symbols, "LeftRightArrow;", Convert(0x2194));
            AddSingle(symbols, "Leftrightarrow;", Convert(0x21D4));
            AddSingle(symbols, "LeftRightVector;", Convert(0x294E));
            AddSingle(symbols, "LeftTee;", Convert(0x22A3));
            AddSingle(symbols, "LeftTeeArrow;", Convert(0x21A4));
            AddSingle(symbols, "LeftTeeVector;", Convert(0x295A));
            AddSingle(symbols, "LeftTriangle;", Convert(0x22B2));
            AddSingle(symbols, "LeftTriangleBar;", Convert(0x29CF));
            AddSingle(symbols, "LeftTriangleEqual;", Convert(0x22B4));
            AddSingle(symbols, "LeftUpDownVector;", Convert(0x2951));
            AddSingle(symbols, "LeftUpTeeVector;", Convert(0x2960));
            AddSingle(symbols, "LeftUpVector;", Convert(0x21BF));
            AddSingle(symbols, "LeftUpVectorBar;", Convert(0x2958));
            AddSingle(symbols, "LeftVector;", Convert(0x21BC));
            AddSingle(symbols, "LeftVectorBar;", Convert(0x2952));
            AddSingle(symbols, "LessEqualGreater;", Convert(0x22DA));
            AddSingle(symbols, "LessFullEqual;", Convert(0x2266));
            AddSingle(symbols, "LessGreater;", Convert(0x2276));
            AddSingle(symbols, "LessLess;", Convert(0x2AA1));
            AddSingle(symbols, "LessSlantEqual;", Convert(0x2A7D));
            AddSingle(symbols, "LessTilde;", Convert(0x2272));
            AddSingle(symbols, "Lfr;", Convert(0x1D50F));
            AddSingle(symbols, "LJcy;", Convert(0x0409));
            AddSingle(symbols, "Ll;", Convert(0x22D8));
            AddSingle(symbols, "Lleftarrow;", Convert(0x21DA));
            AddSingle(symbols, "Lmidot;", Convert(0x013F));
            AddSingle(symbols, "LongLeftArrow;", Convert(0x27F5));
            AddSingle(symbols, "Longleftarrow;", Convert(0x27F8));
            AddSingle(symbols, "LongLeftRightArrow;", Convert(0x27F7));
            AddSingle(symbols, "Longleftrightarrow;", Convert(0x27FA));
            AddSingle(symbols, "LongRightArrow;", Convert(0x27F6));
            AddSingle(symbols, "Longrightarrow;", Convert(0x27F9));
            AddSingle(symbols, "Lopf;", Convert(0x1D543));
            AddSingle(symbols, "LowerLeftArrow;", Convert(0x2199));
            AddSingle(symbols, "LowerRightArrow;", Convert(0x2198));
            AddSingle(symbols, "Lscr;", Convert(0x2112));
            AddSingle(symbols, "Lsh;", Convert(0x21B0));
            AddSingle(symbols, "Lstrok;", Convert(0x0141));
            AddBoth(symbols, "LT;", Convert(0x003C));
            AddSingle(symbols, "Lt;", Convert(0x226A));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleM()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "macr;", Convert(0x00AF));
            AddSingle(symbols, "male;", Convert(0x2642));
            AddSingle(symbols, "malt;", Convert(0x2720));
            AddSingle(symbols, "maltese;", Convert(0x2720));
            AddSingle(symbols, "map;", Convert(0x21A6));
            AddSingle(symbols, "mapsto;", Convert(0x21A6));
            AddSingle(symbols, "mapstodown;", Convert(0x21A7));
            AddSingle(symbols, "mapstoleft;", Convert(0x21A4));
            AddSingle(symbols, "mapstoup;", Convert(0x21A5));
            AddSingle(symbols, "marker;", Convert(0x25AE));
            AddSingle(symbols, "mcomma;", Convert(0x2A29));
            AddSingle(symbols, "mcy;", Convert(0x043C));
            AddSingle(symbols, "mdash;", Convert(0x2014));
            AddSingle(symbols, "mDDot;", Convert(0x223A));
            AddSingle(symbols, "measuredangle;", Convert(0x2221));
            AddSingle(symbols, "mfr;", Convert(0x1D52A));
            AddSingle(symbols, "mho;", Convert(0x2127));
            AddBoth(symbols, "micro;", Convert(0x00B5));
            AddSingle(symbols, "mid;", Convert(0x2223));
            AddSingle(symbols, "midast;", Convert(0x002A));
            AddSingle(symbols, "midcir;", Convert(0x2AF0));
            AddBoth(symbols, "middot;", Convert(0x00B7));
            AddSingle(symbols, "minus;", Convert(0x2212));
            AddSingle(symbols, "minusb;", Convert(0x229F));
            AddSingle(symbols, "minusd;", Convert(0x2238));
            AddSingle(symbols, "minusdu;", Convert(0x2A2A));
            AddSingle(symbols, "mlcp;", Convert(0x2ADB));
            AddSingle(symbols, "mldr;", Convert(0x2026));
            AddSingle(symbols, "mnplus;", Convert(0x2213));
            AddSingle(symbols, "models;", Convert(0x22A7));
            AddSingle(symbols, "mopf;", Convert(0x1D55E));
            AddSingle(symbols, "mp;", Convert(0x2213));
            AddSingle(symbols, "mscr;", Convert(0x1D4C2));
            AddSingle(symbols, "mstpos;", Convert(0x223E));
            AddSingle(symbols, "mu;", Convert(0x03BC));
            AddSingle(symbols, "multimap;", Convert(0x22B8));
            AddSingle(symbols, "mumap;", Convert(0x22B8));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigM()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Map;", Convert(0x2905));
            AddSingle(symbols, "Mcy;", Convert(0x041C));
            AddSingle(symbols, "MediumSpace;", Convert(0x205F));
            AddSingle(symbols, "Mellintrf;", Convert(0x2133));
            AddSingle(symbols, "Mfr;", Convert(0x1D510));
            AddSingle(symbols, "MinusPlus;", Convert(0x2213));
            AddSingle(symbols, "Mopf;", Convert(0x1D544));
            AddSingle(symbols, "Mscr;", Convert(0x2133));
            AddSingle(symbols, "Mu;", Convert(0x039C));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleN()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "nabla;", Convert(0x2207));
            AddSingle(symbols, "nacute;", Convert(0x0144));
            AddSingle(symbols, "nang;", Convert(0x2220, 0x20D2));
            AddSingle(symbols, "nap;", Convert(0x2249));
            AddSingle(symbols, "napE;", Convert(0x2A70, 0x0338));
            AddSingle(symbols, "napid;", Convert(0x224B, 0x0338));
            AddSingle(symbols, "napos;", Convert(0x0149));
            AddSingle(symbols, "napprox;", Convert(0x2249));
            AddSingle(symbols, "natur;", Convert(0x266E));
            AddSingle(symbols, "natural;", Convert(0x266E));
            AddSingle(symbols, "naturals;", Convert(0x2115));
            AddBoth(symbols, "nbsp;", Convert(0x00A0));
            AddSingle(symbols, "nbump;", Convert(0x224E, 0x0338));
            AddSingle(symbols, "nbumpe;", Convert(0x224F, 0x0338));
            AddSingle(symbols, "ncap;", Convert(0x2A43));
            AddSingle(symbols, "ncaron;", Convert(0x0148));
            AddSingle(symbols, "ncedil;", Convert(0x0146));
            AddSingle(symbols, "ncong;", Convert(0x2247));
            AddSingle(symbols, "ncongdot;", Convert(0x2A6D, 0x0338));
            AddSingle(symbols, "ncup;", Convert(0x2A42));
            AddSingle(symbols, "ncy;", Convert(0x043D));
            AddSingle(symbols, "ndash;", Convert(0x2013));
            AddSingle(symbols, "ne;", Convert(0x2260));
            AddSingle(symbols, "nearhk;", Convert(0x2924));
            AddSingle(symbols, "neArr;", Convert(0x21D7));
            AddSingle(symbols, "nearr;", Convert(0x2197));
            AddSingle(symbols, "nearrow;", Convert(0x2197));
            AddSingle(symbols, "nedot;", Convert(0x2250, 0x0338));
            AddSingle(symbols, "nequiv;", Convert(0x2262));
            AddSingle(symbols, "nesear;", Convert(0x2928));
            AddSingle(symbols, "nesim;", Convert(0x2242, 0x0338));
            AddSingle(symbols, "nexist;", Convert(0x2204));
            AddSingle(symbols, "nexists;", Convert(0x2204));
            AddSingle(symbols, "nfr;", Convert(0x1D52B));
            AddSingle(symbols, "ngE;", Convert(0x2267, 0x0338));
            AddSingle(symbols, "nge;", Convert(0x2271));
            AddSingle(symbols, "ngeq;", Convert(0x2271));
            AddSingle(symbols, "ngeqq;", Convert(0x2267, 0x0338));
            AddSingle(symbols, "ngeqslant;", Convert(0x2A7E, 0x0338));
            AddSingle(symbols, "nges;", Convert(0x2A7E, 0x0338));
            AddSingle(symbols, "nGg;", Convert(0x22D9, 0x0338));
            AddSingle(symbols, "ngsim;", Convert(0x2275));
            AddSingle(symbols, "nGt;", Convert(0x226B, 0x20D2));
            AddSingle(symbols, "ngt;", Convert(0x226F));
            AddSingle(symbols, "ngtr;", Convert(0x226F));
            AddSingle(symbols, "nGtv;", Convert(0x226B, 0x0338));
            AddSingle(symbols, "nhArr;", Convert(0x21CE));
            AddSingle(symbols, "nharr;", Convert(0x21AE));
            AddSingle(symbols, "nhpar;", Convert(0x2AF2));
            AddSingle(symbols, "ni;", Convert(0x220B));
            AddSingle(symbols, "nis;", Convert(0x22FC));
            AddSingle(symbols, "nisd;", Convert(0x22FA));
            AddSingle(symbols, "niv;", Convert(0x220B));
            AddSingle(symbols, "njcy;", Convert(0x045A));
            AddSingle(symbols, "nlArr;", Convert(0x21CD));
            AddSingle(symbols, "nlarr;", Convert(0x219A));
            AddSingle(symbols, "nldr;", Convert(0x2025));
            AddSingle(symbols, "nlE;", Convert(0x2266, 0x0338));
            AddSingle(symbols, "nle;", Convert(0x2270));
            AddSingle(symbols, "nLeftarrow;", Convert(0x21CD));
            AddSingle(symbols, "nleftarrow;", Convert(0x219A));
            AddSingle(symbols, "nLeftrightarrow;", Convert(0x21CE));
            AddSingle(symbols, "nleftrightarrow;", Convert(0x21AE));
            AddSingle(symbols, "nleq;", Convert(0x2270));
            AddSingle(symbols, "nleqq;", Convert(0x2266, 0x0338));
            AddSingle(symbols, "nleqslant;", Convert(0x2A7D, 0x0338));
            AddSingle(symbols, "nles;", Convert(0x2A7D, 0x0338));
            AddSingle(symbols, "nless;", Convert(0x226E));
            AddSingle(symbols, "nLl;", Convert(0x22D8, 0x0338));
            AddSingle(symbols, "nlsim;", Convert(0x2274));
            AddSingle(symbols, "nLt;", Convert(0x226A, 0x20D2));
            AddSingle(symbols, "nlt;", Convert(0x226E));
            AddSingle(symbols, "nltri;", Convert(0x22EA));
            AddSingle(symbols, "nltrie;", Convert(0x22EC));
            AddSingle(symbols, "nLtv;", Convert(0x226A, 0x0338));
            AddSingle(symbols, "nmid;", Convert(0x2224));
            AddSingle(symbols, "nopf;", Convert(0x1D55F));
            AddBoth(symbols, "not;", Convert(0x00AC));
            AddSingle(symbols, "notin;", Convert(0x2209));
            AddSingle(symbols, "notindot;", Convert(0x22F5, 0x0338));
            AddSingle(symbols, "notinE;", Convert(0x22F9, 0x0338));
            AddSingle(symbols, "notinva;", Convert(0x2209));
            AddSingle(symbols, "notinvb;", Convert(0x22F7));
            AddSingle(symbols, "notinvc;", Convert(0x22F6));
            AddSingle(symbols, "notni;", Convert(0x220C));
            AddSingle(symbols, "notniva;", Convert(0x220C));
            AddSingle(symbols, "notnivb;", Convert(0x22FE));
            AddSingle(symbols, "notnivc;", Convert(0x22FD));
            AddSingle(symbols, "npar;", Convert(0x2226));
            AddSingle(symbols, "nparallel;", Convert(0x2226));
            AddSingle(symbols, "nparsl;", Convert(0x2AFD, 0x20E5));
            AddSingle(symbols, "npart;", Convert(0x2202, 0x0338));
            AddSingle(symbols, "npolint;", Convert(0x2A14));
            AddSingle(symbols, "npr;", Convert(0x2280));
            AddSingle(symbols, "nprcue;", Convert(0x22E0));
            AddSingle(symbols, "npre;", Convert(0x2AAF, 0x0338));
            AddSingle(symbols, "nprec;", Convert(0x2280));
            AddSingle(symbols, "npreceq;", Convert(0x2AAF, 0x0338));
            AddSingle(symbols, "nrArr;", Convert(0x21CF));
            AddSingle(symbols, "nrarr;", Convert(0x219B));
            AddSingle(symbols, "nrarrc;", Convert(0x2933, 0x0338));
            AddSingle(symbols, "nrarrw;", Convert(0x219D, 0x0338));
            AddSingle(symbols, "nRightarrow;", Convert(0x21CF));
            AddSingle(symbols, "nrightarrow;", Convert(0x219B));
            AddSingle(symbols, "nrtri;", Convert(0x22EB));
            AddSingle(symbols, "nrtrie;", Convert(0x22ED));
            AddSingle(symbols, "nsc;", Convert(0x2281));
            AddSingle(symbols, "nsccue;", Convert(0x22E1));
            AddSingle(symbols, "nsce;", Convert(0x2AB0, 0x0338));
            AddSingle(symbols, "nscr;", Convert(0x1D4C3));
            AddSingle(symbols, "nshortmid;", Convert(0x2224));
            AddSingle(symbols, "nshortparallel;", Convert(0x2226));
            AddSingle(symbols, "nsim;", Convert(0x2241));
            AddSingle(symbols, "nsime;", Convert(0x2244));
            AddSingle(symbols, "nsimeq;", Convert(0x2244));
            AddSingle(symbols, "nsmid;", Convert(0x2224));
            AddSingle(symbols, "nspar;", Convert(0x2226));
            AddSingle(symbols, "nsqsube;", Convert(0x22E2));
            AddSingle(symbols, "nsqsupe;", Convert(0x22E3));
            AddSingle(symbols, "nsub;", Convert(0x2284));
            AddSingle(symbols, "nsubE;", Convert(0x2AC5, 0x0338));
            AddSingle(symbols, "nsube;", Convert(0x2288));
            AddSingle(symbols, "nsubset;", Convert(0x2282, 0x20D2));
            AddSingle(symbols, "nsubseteq;", Convert(0x2288));
            AddSingle(symbols, "nsubseteqq;", Convert(0x2AC5, 0x0338));
            AddSingle(symbols, "nsucc;", Convert(0x2281));
            AddSingle(symbols, "nsucceq;", Convert(0x2AB0, 0x0338));
            AddSingle(symbols, "nsup;", Convert(0x2285));
            AddSingle(symbols, "nsupE;", Convert(0x2AC6, 0x0338));
            AddSingle(symbols, "nsupe;", Convert(0x2289));
            AddSingle(symbols, "nsupset;", Convert(0x2283, 0x20D2));
            AddSingle(symbols, "nsupseteq;", Convert(0x2289));
            AddSingle(symbols, "nsupseteqq;", Convert(0x2AC6, 0x0338));
            AddSingle(symbols, "ntgl;", Convert(0x2279));
            AddBoth(symbols, "ntilde;", Convert(0x00F1));
            AddSingle(symbols, "ntlg;", Convert(0x2278));
            AddSingle(symbols, "ntriangleleft;", Convert(0x22EA));
            AddSingle(symbols, "ntrianglelefteq;", Convert(0x22EC));
            AddSingle(symbols, "ntriangleright;", Convert(0x22EB));
            AddSingle(symbols, "ntrianglerighteq;", Convert(0x22ED));
            AddSingle(symbols, "nu;", Convert(0x03BD));
            AddSingle(symbols, "num;", Convert(0x0023));
            AddSingle(symbols, "numero;", Convert(0x2116));
            AddSingle(symbols, "numsp;", Convert(0x2007));
            AddSingle(symbols, "nvap;", Convert(0x224D, 0x20D2));
            AddSingle(symbols, "nVDash;", Convert(0x22AF));
            AddSingle(symbols, "nVdash;", Convert(0x22AE));
            AddSingle(symbols, "nvDash;", Convert(0x22AD));
            AddSingle(symbols, "nvdash;", Convert(0x22AC));
            AddSingle(symbols, "nvge;", Convert(0x2265, 0x20D2));
            AddSingle(symbols, "nvgt;", Convert(0x003E, 0x20D2));
            AddSingle(symbols, "nvHarr;", Convert(0x2904));
            AddSingle(symbols, "nvinfin;", Convert(0x29DE));
            AddSingle(symbols, "nvlArr;", Convert(0x2902));
            AddSingle(symbols, "nvle;", Convert(0x2264, 0x20D2));
            AddSingle(symbols, "nvlt;", Convert(0x003C, 0x20D2));
            AddSingle(symbols, "nvltrie;", Convert(0x22B4, 0x20D2));
            AddSingle(symbols, "nvrArr;", Convert(0x2903));
            AddSingle(symbols, "nvrtrie;", Convert(0x22B5, 0x20D2));
            AddSingle(symbols, "nvsim;", Convert(0x223C, 0x20D2));
            AddSingle(symbols, "nwarhk;", Convert(0x2923));
            AddSingle(symbols, "nwArr;", Convert(0x21D6));
            AddSingle(symbols, "nwarr;", Convert(0x2196));
            AddSingle(symbols, "nwarrow;", Convert(0x2196));
            AddSingle(symbols, "nwnear;", Convert(0x2927));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigN()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Nacute;", Convert(0x0143));
            AddSingle(symbols, "Ncaron;", Convert(0x0147));
            AddSingle(symbols, "Ncedil;", Convert(0x0145));
            AddSingle(symbols, "NegativeMediumSpace;", Convert(0x200B));
            AddSingle(symbols, "NegativeThickSpace;", Convert(0x200B));
            AddSingle(symbols, "NegativeThinSpace;", Convert(0x200B));
            AddSingle(symbols, "NegativeVeryThinSpace;", Convert(0x200B));
            AddSingle(symbols, "NestedGreaterGreater;", Convert(0x226B));
            AddSingle(symbols, "NestedLessLess;", Convert(0x226A));
            AddSingle(symbols, "Ncy;", Convert(0x041D));
            AddSingle(symbols, "Nfr;", Convert(0x1D511));
            AddSingle(symbols, "NoBreak;", Convert(0x2060));
            AddSingle(symbols, "NonBreakingSpace;", Convert(0x00A0));
            AddSingle(symbols, "Nopf;", Convert(0x2115));
            AddSingle(symbols, "NewLine;", Convert(0x000A));
            AddSingle(symbols, "NJcy;", Convert(0x040A));
            AddSingle(symbols, "Not;", Convert(0x2AEC));
            AddSingle(symbols, "NotCongruent;", Convert(0x2262));
            AddSingle(symbols, "NotCupCap;", Convert(0x226D));
            AddSingle(symbols, "NotDoubleVerticalBar;", Convert(0x2226));
            AddSingle(symbols, "NotElement;", Convert(0x2209));
            AddSingle(symbols, "NotEqual;", Convert(0x2260));
            AddSingle(symbols, "NotEqualTilde;", Convert(0x2242, 0x0338));
            AddSingle(symbols, "NotExists;", Convert(0x2204));
            AddSingle(symbols, "NotGreater;", Convert(0x226F));
            AddSingle(symbols, "NotGreaterEqual;", Convert(0x2271));
            AddSingle(symbols, "NotGreaterFullEqual;", Convert(0x2267, 0x0338));
            AddSingle(symbols, "NotGreaterGreater;", Convert(0x226B, 0x0338));
            AddSingle(symbols, "NotGreaterLess;", Convert(0x2279));
            AddSingle(symbols, "NotGreaterSlantEqual;", Convert(0x2A7E, 0x0338));
            AddSingle(symbols, "NotGreaterTilde;", Convert(0x2275));
            AddSingle(symbols, "NotHumpDownHump;", Convert(0x224E, 0x0338));
            AddSingle(symbols, "NotHumpEqual;", Convert(0x224F, 0x0338));
            AddSingle(symbols, "NotLeftTriangle;", Convert(0x22EA));
            AddSingle(symbols, "NotLeftTriangleBar;", Convert(0x29CF, 0x0338));
            AddSingle(symbols, "NotLeftTriangleEqual;", Convert(0x22EC));
            AddSingle(symbols, "NotLess;", Convert(0x226E));
            AddSingle(symbols, "NotLessEqual;", Convert(0x2270));
            AddSingle(symbols, "NotLessGreater;", Convert(0x2278));
            AddSingle(symbols, "NotLessLess;", Convert(0x226A, 0x0338));
            AddSingle(symbols, "NotLessSlantEqual;", Convert(0x2A7D, 0x0338));
            AddSingle(symbols, "NotLessTilde;", Convert(0x2274));
            AddSingle(symbols, "NotNestedGreaterGreater;", Convert(0x2AA2, 0x0338));
            AddSingle(symbols, "NotNestedLessLess;", Convert(0x2AA1, 0x0338));
            AddSingle(symbols, "NotPrecedes;", Convert(0x2280));
            AddSingle(symbols, "NotPrecedesEqual;", Convert(0x2AAF, 0x0338));
            AddSingle(symbols, "NotPrecedesSlantEqual;", Convert(0x22E0));
            AddSingle(symbols, "NotReverseElement;", Convert(0x220C));
            AddSingle(symbols, "NotRightTriangle;", Convert(0x22EB));
            AddSingle(symbols, "NotRightTriangleBar;", Convert(0x29D0, 0x0338));
            AddSingle(symbols, "NotRightTriangleEqual;", Convert(0x22ED));
            AddSingle(symbols, "NotSquareSubset;", Convert(0x228F, 0x0338));
            AddSingle(symbols, "NotSquareSubsetEqual;", Convert(0x22E2));
            AddSingle(symbols, "NotSquareSuperset;", Convert(0x2290, 0x0338));
            AddSingle(symbols, "NotSquareSupersetEqual;", Convert(0x22E3));
            AddSingle(symbols, "NotSubset;", Convert(0x2282, 0x20D2));
            AddSingle(symbols, "NotSubsetEqual;", Convert(0x2288));
            AddSingle(symbols, "NotSucceeds;", Convert(0x2281));
            AddSingle(symbols, "NotSucceedsEqual;", Convert(0x2AB0, 0x0338));
            AddSingle(symbols, "NotSucceedsSlantEqual;", Convert(0x22E1));
            AddSingle(symbols, "NotSucceedsTilde;", Convert(0x227F, 0x0338));
            AddSingle(symbols, "NotSuperset;", Convert(0x2283, 0x20D2));
            AddSingle(symbols, "NotSupersetEqual;", Convert(0x2289));
            AddSingle(symbols, "NotTilde;", Convert(0x2241));
            AddSingle(symbols, "NotTildeEqual;", Convert(0x2244));
            AddSingle(symbols, "NotTildeFullEqual;", Convert(0x2247));
            AddSingle(symbols, "NotTildeTilde;", Convert(0x2249));
            AddSingle(symbols, "NotVerticalBar;", Convert(0x2224));
            AddSingle(symbols, "Nscr;", Convert(0x1D4A9));
            AddBoth(symbols, "Ntilde;", Convert(0x00D1));
            AddSingle(symbols, "Nu;", Convert(0x039D));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleO()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "oacute;", Convert(0x00F3));
            AddSingle(symbols, "oast;", Convert(0x229B));
            AddSingle(symbols, "ocir;", Convert(0x229A));
            AddBoth(symbols, "ocirc;", Convert(0x00F4));
            AddSingle(symbols, "ocy;", Convert(0x043E));
            AddSingle(symbols, "odash;", Convert(0x229D));
            AddSingle(symbols, "odblac;", Convert(0x0151));
            AddSingle(symbols, "odiv;", Convert(0x2A38));
            AddSingle(symbols, "odot;", Convert(0x2299));
            AddSingle(symbols, "odsold;", Convert(0x29BC));
            AddSingle(symbols, "oelig;", Convert(0x0153));
            AddSingle(symbols, "ofcir;", Convert(0x29BF));
            AddSingle(symbols, "ofr;", Convert(0x1D52C));
            AddSingle(symbols, "ogon;", Convert(0x02DB));
            AddBoth(symbols, "ograve;", Convert(0x00F2));
            AddSingle(symbols, "ogt;", Convert(0x29C1));
            AddSingle(symbols, "ohbar;", Convert(0x29B5));
            AddSingle(symbols, "ohm;", Convert(0x03A9));
            AddSingle(symbols, "oint;", Convert(0x222E));
            AddSingle(symbols, "olarr;", Convert(0x21BA));
            AddSingle(symbols, "olcir;", Convert(0x29BE));
            AddSingle(symbols, "olcross;", Convert(0x29BB));
            AddSingle(symbols, "oline;", Convert(0x203E));
            AddSingle(symbols, "olt;", Convert(0x29C0));
            AddSingle(symbols, "omacr;", Convert(0x014D));
            AddSingle(symbols, "omega;", Convert(0x03C9));
            AddSingle(symbols, "omicron;", Convert(0x03BF));
            AddSingle(symbols, "omid;", Convert(0x29B6));
            AddSingle(symbols, "ominus;", Convert(0x2296));
            AddSingle(symbols, "oopf;", Convert(0x1D560));
            AddSingle(symbols, "opar;", Convert(0x29B7));
            AddSingle(symbols, "operp;", Convert(0x29B9));
            AddSingle(symbols, "oplus;", Convert(0x2295));
            AddSingle(symbols, "or;", Convert(0x2228));
            AddSingle(symbols, "orarr;", Convert(0x21BB));
            AddSingle(symbols, "ord;", Convert(0x2A5D));
            AddSingle(symbols, "order;", Convert(0x2134));
            AddSingle(symbols, "orderof;", Convert(0x2134));
            AddBoth(symbols, "ordf;", Convert(0x00AA));
            AddBoth(symbols, "ordm;", Convert(0x00BA));
            AddSingle(symbols, "origof;", Convert(0x22B6));
            AddSingle(symbols, "oror;", Convert(0x2A56));
            AddSingle(symbols, "orslope;", Convert(0x2A57));
            AddSingle(symbols, "orv;", Convert(0x2A5B));
            AddSingle(symbols, "oS;", Convert(0x24C8));
            AddSingle(symbols, "oscr;", Convert(0x2134));
            AddBoth(symbols, "oslash;", Convert(0x00F8));
            AddSingle(symbols, "osol;", Convert(0x2298));
            AddBoth(symbols, "otilde;", Convert(0x00F5));
            AddSingle(symbols, "otimes;", Convert(0x2297));
            AddSingle(symbols, "otimesas;", Convert(0x2A36));
            AddBoth(symbols, "ouml;", Convert(0x00F6));
            AddSingle(symbols, "ovbar;", Convert(0x233D));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigO()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "Oacute;", Convert(0x00D3));
            AddBoth(symbols, "Ocirc;", Convert(0x00D4));
            AddSingle(symbols, "Ocy;", Convert(0x041E));
            AddSingle(symbols, "Odblac;", Convert(0x0150));
            AddSingle(symbols, "OElig;", Convert(0x0152));
            AddSingle(symbols, "Ofr;", Convert(0x1D512));
            AddBoth(symbols, "Ograve;", Convert(0x00D2));
            AddSingle(symbols, "Omacr;", Convert(0x014C));
            AddSingle(symbols, "Omega;", Convert(0x03A9));
            AddSingle(symbols, "Omicron;", Convert(0x039F));
            AddSingle(symbols, "Oopf;", Convert(0x1D546));
            AddSingle(symbols, "OpenCurlyDoubleQuote;", Convert(0x201C));
            AddSingle(symbols, "OpenCurlyQuote;", Convert(0x2018));
            AddSingle(symbols, "Or;", Convert(0x2A54));
            AddSingle(symbols, "Oscr;", Convert(0x1D4AA));
            AddBoth(symbols, "Oslash;", Convert(0x00D8));
            AddBoth(symbols, "Otilde;", Convert(0x00D5));
            AddSingle(symbols, "Otimes;", Convert(0x2A37));
            AddBoth(symbols, "Ouml;", Convert(0x00D6));
            AddSingle(symbols, "OverBar;", Convert(0x203E));
            AddSingle(symbols, "OverBrace;", Convert(0x23DE));
            AddSingle(symbols, "OverBracket;", Convert(0x23B4));
            AddSingle(symbols, "OverParenthesis;", Convert(0x23DC));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleP()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "pfr;", Convert(0x1D52D));
            AddSingle(symbols, "par;", Convert(0x2225));
            AddBoth(symbols, "para;", Convert(0x00B6));
            AddSingle(symbols, "parallel;", Convert(0x2225));
            AddSingle(symbols, "parsim;", Convert(0x2AF3));
            AddSingle(symbols, "parsl;", Convert(0x2AFD));
            AddSingle(symbols, "part;", Convert(0x2202));
            AddSingle(symbols, "pcy;", Convert(0x043F));
            AddSingle(symbols, "percnt;", Convert(0x0025));
            AddSingle(symbols, "period;", Convert(0x002E));
            AddSingle(symbols, "permil;", Convert(0x2030));
            AddSingle(symbols, "perp;", Convert(0x22A5));
            AddSingle(symbols, "pertenk;", Convert(0x2031));
            AddSingle(symbols, "phi;", Convert(0x03C6));
            AddSingle(symbols, "phiv;", Convert(0x03D5));
            AddSingle(symbols, "phmmat;", Convert(0x2133));
            AddSingle(symbols, "phone;", Convert(0x260E));
            AddSingle(symbols, "pi;", Convert(0x03C0));
            AddSingle(symbols, "pitchfork;", Convert(0x22D4));
            AddSingle(symbols, "piv;", Convert(0x03D6));
            AddSingle(symbols, "planck;", Convert(0x210F));
            AddSingle(symbols, "planckh;", Convert(0x210E));
            AddSingle(symbols, "plankv;", Convert(0x210F));
            AddSingle(symbols, "plus;", Convert(0x002B));
            AddSingle(symbols, "plusacir;", Convert(0x2A23));
            AddSingle(symbols, "plusb;", Convert(0x229E));
            AddSingle(symbols, "pluscir;", Convert(0x2A22));
            AddSingle(symbols, "plusdo;", Convert(0x2214));
            AddSingle(symbols, "plusdu;", Convert(0x2A25));
            AddSingle(symbols, "pluse;", Convert(0x2A72));
            AddBoth(symbols, "plusmn;", Convert(0x00B1));
            AddSingle(symbols, "plussim;", Convert(0x2A26));
            AddSingle(symbols, "plustwo;", Convert(0x2A27));
            AddSingle(symbols, "pm;", Convert(0x00B1));
            AddSingle(symbols, "pointint;", Convert(0x2A15));
            AddSingle(symbols, "popf;", Convert(0x1D561));
            AddBoth(symbols, "pound;", Convert(0x00A3));
            AddSingle(symbols, "pr;", Convert(0x227A));
            AddSingle(symbols, "prap;", Convert(0x2AB7));
            AddSingle(symbols, "prcue;", Convert(0x227C));
            AddSingle(symbols, "prE;", Convert(0x2AB3));
            AddSingle(symbols, "pre;", Convert(0x2AAF));
            AddSingle(symbols, "prec;", Convert(0x227A));
            AddSingle(symbols, "precapprox;", Convert(0x2AB7));
            AddSingle(symbols, "preccurlyeq;", Convert(0x227C));
            AddSingle(symbols, "preceq;", Convert(0x2AAF));
            AddSingle(symbols, "precnapprox;", Convert(0x2AB9));
            AddSingle(symbols, "precneqq;", Convert(0x2AB5));
            AddSingle(symbols, "precnsim;", Convert(0x22E8));
            AddSingle(symbols, "precsim;", Convert(0x227E));
            AddSingle(symbols, "prime;", Convert(0x2032));
            AddSingle(symbols, "primes;", Convert(0x2119));
            AddSingle(symbols, "prnap;", Convert(0x2AB9));
            AddSingle(symbols, "prnE;", Convert(0x2AB5));
            AddSingle(symbols, "prnsim;", Convert(0x22E8));
            AddSingle(symbols, "prod;", Convert(0x220F));
            AddSingle(symbols, "profalar;", Convert(0x232E));
            AddSingle(symbols, "profline;", Convert(0x2312));
            AddSingle(symbols, "profsurf;", Convert(0x2313));
            AddSingle(symbols, "prop;", Convert(0x221D));
            AddSingle(symbols, "propto;", Convert(0x221D));
            AddSingle(symbols, "prsim;", Convert(0x227E));
            AddSingle(symbols, "prurel;", Convert(0x22B0));
            AddSingle(symbols, "pscr;", Convert(0x1D4C5));
            AddSingle(symbols, "psi;", Convert(0x03C8));
            AddSingle(symbols, "puncsp;", Convert(0x2008));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigP()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "PartialD;", Convert(0x2202));
            AddSingle(symbols, "Pcy;", Convert(0x041F));
            AddSingle(symbols, "Pfr;", Convert(0x1D513));
            AddSingle(symbols, "Phi;", Convert(0x03A6));
            AddSingle(symbols, "Pi;", Convert(0x03A0));
            AddSingle(symbols, "PlusMinus;", Convert(0x00B1));
            AddSingle(symbols, "Poincareplane;", Convert(0x210C));
            AddSingle(symbols, "Popf;", Convert(0x2119));
            AddSingle(symbols, "Pr;", Convert(0x2ABB));
            AddSingle(symbols, "Precedes;", Convert(0x227A));
            AddSingle(symbols, "PrecedesEqual;", Convert(0x2AAF));
            AddSingle(symbols, "PrecedesSlantEqual;", Convert(0x227C));
            AddSingle(symbols, "PrecedesTilde;", Convert(0x227E));
            AddSingle(symbols, "Prime;", Convert(0x2033));
            AddSingle(symbols, "Product;", Convert(0x220F));
            AddSingle(symbols, "Proportion;", Convert(0x2237));
            AddSingle(symbols, "Proportional;", Convert(0x221D));
            AddSingle(symbols, "Pscr;", Convert(0x1D4AB));
            AddSingle(symbols, "Psi;", Convert(0x03A8));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleQ()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "qfr;", Convert(0x1D52E));
            AddSingle(symbols, "qint;", Convert(0x2A0C));
            AddSingle(symbols, "qopf;", Convert(0x1D562));
            AddSingle(symbols, "qprime;", Convert(0x2057));
            AddSingle(symbols, "qscr;", Convert(0x1D4C6));
            AddSingle(symbols, "quaternions;", Convert(0x210D));
            AddSingle(symbols, "quatint;", Convert(0x2A16));
            AddSingle(symbols, "quest;", Convert(0x003F));
            AddSingle(symbols, "questeq;", Convert(0x225F));
            AddBoth(symbols, "quot;", Convert(0x0022));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigQ()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Qfr;", Convert(0x1D514));
            AddSingle(symbols, "Qopf;", Convert(0x211A));
            AddSingle(symbols, "Qscr;", Convert(0x1D4AC));
            AddBoth(symbols, "QUOT;", Convert(0x0022));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleR()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "rAarr;", Convert(0x21DB));
            AddSingle(symbols, "race;", Convert(0x223D, 0x0331));
            AddSingle(symbols, "racute;", Convert(0x0155));
            AddSingle(symbols, "radic;", Convert(0x221A));
            AddSingle(symbols, "raemptyv;", Convert(0x29B3));
            AddSingle(symbols, "rang;", Convert(0x27E9));
            AddSingle(symbols, "rangd;", Convert(0x2992));
            AddSingle(symbols, "range;", Convert(0x29A5));
            AddSingle(symbols, "rangle;", Convert(0x27E9));
            AddBoth(symbols, "raquo;", Convert(0x00BB));
            AddSingle(symbols, "rArr;", Convert(0x21D2));
            AddSingle(symbols, "rarr;", Convert(0x2192));
            AddSingle(symbols, "rarrap;", Convert(0x2975));
            AddSingle(symbols, "rarrb;", Convert(0x21E5));
            AddSingle(symbols, "rarrbfs;", Convert(0x2920));
            AddSingle(symbols, "rarrc;", Convert(0x2933));
            AddSingle(symbols, "rarrfs;", Convert(0x291E));
            AddSingle(symbols, "rarrhk;", Convert(0x21AA));
            AddSingle(symbols, "rarrlp;", Convert(0x21AC));
            AddSingle(symbols, "rarrpl;", Convert(0x2945));
            AddSingle(symbols, "rarrsim;", Convert(0x2974));
            AddSingle(symbols, "rarrtl;", Convert(0x21A3));
            AddSingle(symbols, "rarrw;", Convert(0x219D));
            AddSingle(symbols, "rAtail;", Convert(0x291C));
            AddSingle(symbols, "ratail;", Convert(0x291A));
            AddSingle(symbols, "ratio;", Convert(0x2236));
            AddSingle(symbols, "rationals;", Convert(0x211A));
            AddSingle(symbols, "rBarr;", Convert(0x290F));
            AddSingle(symbols, "rbarr;", Convert(0x290D));
            AddSingle(symbols, "rbbrk;", Convert(0x2773));
            AddSingle(symbols, "rbrace;", Convert(0x007D));
            AddSingle(symbols, "rbrack;", Convert(0x005D));
            AddSingle(symbols, "rbrke;", Convert(0x298C));
            AddSingle(symbols, "rbrksld;", Convert(0x298E));
            AddSingle(symbols, "rbrkslu;", Convert(0x2990));
            AddSingle(symbols, "rcaron;", Convert(0x0159));
            AddSingle(symbols, "rcedil;", Convert(0x0157));
            AddSingle(symbols, "rceil;", Convert(0x2309));
            AddSingle(symbols, "rcub;", Convert(0x007D));
            AddSingle(symbols, "rcy;", Convert(0x0440));
            AddSingle(symbols, "rdca;", Convert(0x2937));
            AddSingle(symbols, "rdldhar;", Convert(0x2969));
            AddSingle(symbols, "rdquo;", Convert(0x201D));
            AddSingle(symbols, "rdquor;", Convert(0x201D));
            AddSingle(symbols, "rdsh;", Convert(0x21B3));
            AddSingle(symbols, "real;", Convert(0x211C));
            AddSingle(symbols, "realine;", Convert(0x211B));
            AddSingle(symbols, "realpart;", Convert(0x211C));
            AddSingle(symbols, "reals;", Convert(0x211D));
            AddSingle(symbols, "rect;", Convert(0x25AD));
            AddBoth(symbols, "reg;", Convert(0x00AE));
            AddSingle(symbols, "rfisht;", Convert(0x297D));
            AddSingle(symbols, "rfloor;", Convert(0x230B));
            AddSingle(symbols, "rfr;", Convert(0x1D52F));
            AddSingle(symbols, "rHar;", Convert(0x2964));
            AddSingle(symbols, "rhard;", Convert(0x21C1));
            AddSingle(symbols, "rharu;", Convert(0x21C0));
            AddSingle(symbols, "rharul;", Convert(0x296C));
            AddSingle(symbols, "rho;", Convert(0x03C1));
            AddSingle(symbols, "rhov;", Convert(0x03F1));
            AddSingle(symbols, "rightarrow;", Convert(0x2192));
            AddSingle(symbols, "rightarrowtail;", Convert(0x21A3));
            AddSingle(symbols, "rightharpoondown;", Convert(0x21C1));
            AddSingle(symbols, "rightharpoonup;", Convert(0x21C0));
            AddSingle(symbols, "rightleftarrows;", Convert(0x21C4));
            AddSingle(symbols, "rightleftharpoons;", Convert(0x21CC));
            AddSingle(symbols, "rightrightarrows;", Convert(0x21C9));
            AddSingle(symbols, "rightsquigarrow;", Convert(0x219D));
            AddSingle(symbols, "rightthreetimes;", Convert(0x22CC));
            AddSingle(symbols, "ring;", Convert(0x02DA));
            AddSingle(symbols, "risingdotseq;", Convert(0x2253));
            AddSingle(symbols, "rlarr;", Convert(0x21C4));
            AddSingle(symbols, "rlhar;", Convert(0x21CC));
            AddSingle(symbols, "rlm;", Convert(0x200F));
            AddSingle(symbols, "rmoust;", Convert(0x23B1));
            AddSingle(symbols, "rmoustache;", Convert(0x23B1));
            AddSingle(symbols, "rnmid;", Convert(0x2AEE));
            AddSingle(symbols, "roang;", Convert(0x27ED));
            AddSingle(symbols, "roarr;", Convert(0x21FE));
            AddSingle(symbols, "robrk;", Convert(0x27E7));
            AddSingle(symbols, "ropar;", Convert(0x2986));
            AddSingle(symbols, "ropf;", Convert(0x1D563));
            AddSingle(symbols, "roplus;", Convert(0x2A2E));
            AddSingle(symbols, "rotimes;", Convert(0x2A35));
            AddSingle(symbols, "rpar;", Convert(0x0029));
            AddSingle(symbols, "rpargt;", Convert(0x2994));
            AddSingle(symbols, "rppolint;", Convert(0x2A12));
            AddSingle(symbols, "rrarr;", Convert(0x21C9));
            AddSingle(symbols, "rsaquo;", Convert(0x203A));
            AddSingle(symbols, "rscr;", Convert(0x1D4C7));
            AddSingle(symbols, "rsh;", Convert(0x21B1));
            AddSingle(symbols, "rsqb;", Convert(0x005D));
            AddSingle(symbols, "rsquo;", Convert(0x2019));
            AddSingle(symbols, "rsquor;", Convert(0x2019));
            AddSingle(symbols, "rthree;", Convert(0x22CC));
            AddSingle(symbols, "rtimes;", Convert(0x22CA));
            AddSingle(symbols, "rtri;", Convert(0x25B9));
            AddSingle(symbols, "rtrie;", Convert(0x22B5));
            AddSingle(symbols, "rtrif;", Convert(0x25B8));
            AddSingle(symbols, "rtriltri;", Convert(0x29CE));
            AddSingle(symbols, "ruluhar;", Convert(0x2968));
            AddSingle(symbols, "rx;", Convert(0x211E));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigR()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Racute;", Convert(0x0154));
            AddSingle(symbols, "Rang;", Convert(0x27EB));
            AddSingle(symbols, "Rarr;", Convert(0x21A0));
            AddSingle(symbols, "Rarrtl;", Convert(0x2916));
            AddSingle(symbols, "RBarr;", Convert(0x2910));
            AddSingle(symbols, "Rcaron;", Convert(0x0158));
            AddSingle(symbols, "Rcedil;", Convert(0x0156));
            AddSingle(symbols, "Rcy;", Convert(0x0420));
            AddSingle(symbols, "Re;", Convert(0x211C));
            AddBoth(symbols, "REG;", Convert(0x00AE));
            AddSingle(symbols, "ReverseElement;", Convert(0x220B));
            AddSingle(symbols, "ReverseEquilibrium;", Convert(0x21CB));
            AddSingle(symbols, "ReverseUpEquilibrium;", Convert(0x296F));
            AddSingle(symbols, "Rfr;", Convert(0x211C));
            AddSingle(symbols, "Rho;", Convert(0x03A1));
            AddSingle(symbols, "RightAngleBracket;", Convert(0x27E9));
            AddSingle(symbols, "RightArrow;", Convert(0x2192));
            AddSingle(symbols, "Rightarrow;", Convert(0x21D2));
            AddSingle(symbols, "RightArrowBar;", Convert(0x21E5));
            AddSingle(symbols, "RightArrowLeftArrow;", Convert(0x21C4));
            AddSingle(symbols, "RightCeiling;", Convert(0x2309));
            AddSingle(symbols, "RightDoubleBracket;", Convert(0x27E7));
            AddSingle(symbols, "RightDownTeeVector;", Convert(0x295D));
            AddSingle(symbols, "RightDownVector;", Convert(0x21C2));
            AddSingle(symbols, "RightDownVectorBar;", Convert(0x2955));
            AddSingle(symbols, "RightFloor;", Convert(0x230B));
            AddSingle(symbols, "RightTee;", Convert(0x22A2));
            AddSingle(symbols, "RightTeeArrow;", Convert(0x21A6));
            AddSingle(symbols, "RightTeeVector;", Convert(0x295B));
            AddSingle(symbols, "RightTriangle;", Convert(0x22B3));
            AddSingle(symbols, "RightTriangleBar;", Convert(0x29D0));
            AddSingle(symbols, "RightTriangleEqual;", Convert(0x22B5));
            AddSingle(symbols, "RightUpDownVector;", Convert(0x294F));
            AddSingle(symbols, "RightUpTeeVector;", Convert(0x295C));
            AddSingle(symbols, "RightUpVector;", Convert(0x21BE));
            AddSingle(symbols, "RightUpVectorBar;", Convert(0x2954));
            AddSingle(symbols, "RightVector;", Convert(0x21C0));
            AddSingle(symbols, "RightVectorBar;", Convert(0x2953));
            AddSingle(symbols, "Ropf;", Convert(0x211D));
            AddSingle(symbols, "RoundImplies;", Convert(0x2970));
            AddSingle(symbols, "Rrightarrow;", Convert(0x21DB));
            AddSingle(symbols, "Rscr;", Convert(0x211B));
            AddSingle(symbols, "Rsh;", Convert(0x21B1));
            AddSingle(symbols, "RuleDelayed;", Convert(0x29F4));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleS()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "sacute;", Convert(0x015B));
            AddSingle(symbols, "sbquo;", Convert(0x201A));
            AddSingle(symbols, "sc;", Convert(0x227B));
            AddSingle(symbols, "scap;", Convert(0x2AB8));
            AddSingle(symbols, "scaron;", Convert(0x0161));
            AddSingle(symbols, "sccue;", Convert(0x227D));
            AddSingle(symbols, "scE;", Convert(0x2AB4));
            AddSingle(symbols, "sce;", Convert(0x2AB0));
            AddSingle(symbols, "scedil;", Convert(0x015F));
            AddSingle(symbols, "scirc;", Convert(0x015D));
            AddSingle(symbols, "scnap;", Convert(0x2ABA));
            AddSingle(symbols, "scnE;", Convert(0x2AB6));
            AddSingle(symbols, "scnsim;", Convert(0x22E9));
            AddSingle(symbols, "scpolint;", Convert(0x2A13));
            AddSingle(symbols, "scsim;", Convert(0x227F));
            AddSingle(symbols, "scy;", Convert(0x0441));
            AddSingle(symbols, "sdot;", Convert(0x22C5));
            AddSingle(symbols, "sdotb;", Convert(0x22A1));
            AddSingle(symbols, "sdote;", Convert(0x2A66));
            AddSingle(symbols, "searhk;", Convert(0x2925));
            AddSingle(symbols, "seArr;", Convert(0x21D8));
            AddSingle(symbols, "searr;", Convert(0x2198));
            AddSingle(symbols, "searrow;", Convert(0x2198));
            AddBoth(symbols, "sect;", Convert(0x00A7));
            AddSingle(symbols, "semi;", Convert(0x003B));
            AddSingle(symbols, "seswar;", Convert(0x2929));
            AddSingle(symbols, "setminus;", Convert(0x2216));
            AddSingle(symbols, "setmn;", Convert(0x2216));
            AddSingle(symbols, "sext;", Convert(0x2736));
            AddSingle(symbols, "sfr;", Convert(0x1D530));
            AddSingle(symbols, "sfrown;", Convert(0x2322));
            AddSingle(symbols, "sharp;", Convert(0x266F));
            AddSingle(symbols, "shchcy;", Convert(0x0449));
            AddSingle(symbols, "shcy;", Convert(0x0448));
            AddSingle(symbols, "shortmid;", Convert(0x2223));
            AddSingle(symbols, "shortparallel;", Convert(0x2225));
            AddBoth(symbols, "shy;", Convert(0x00AD));
            AddSingle(symbols, "sigma;", Convert(0x03C3));
            AddSingle(symbols, "sigmaf;", Convert(0x03C2));
            AddSingle(symbols, "sigmav;", Convert(0x03C2));
            AddSingle(symbols, "sim;", Convert(0x223C));
            AddSingle(symbols, "simdot;", Convert(0x2A6A));
            AddSingle(symbols, "sime;", Convert(0x2243));
            AddSingle(symbols, "simeq;", Convert(0x2243));
            AddSingle(symbols, "simg;", Convert(0x2A9E));
            AddSingle(symbols, "simgE;", Convert(0x2AA0));
            AddSingle(symbols, "siml;", Convert(0x2A9D));
            AddSingle(symbols, "simlE;", Convert(0x2A9F));
            AddSingle(symbols, "simne;", Convert(0x2246));
            AddSingle(symbols, "simplus;", Convert(0x2A24));
            AddSingle(symbols, "simrarr;", Convert(0x2972));
            AddSingle(symbols, "slarr;", Convert(0x2190));
            AddSingle(symbols, "smallsetminus;", Convert(0x2216));
            AddSingle(symbols, "smashp;", Convert(0x2A33));
            AddSingle(symbols, "smeparsl;", Convert(0x29E4));
            AddSingle(symbols, "smid;", Convert(0x2223));
            AddSingle(symbols, "smile;", Convert(0x2323));
            AddSingle(symbols, "smt;", Convert(0x2AAA));
            AddSingle(symbols, "smte;", Convert(0x2AAC));
            AddSingle(symbols, "smtes;", Convert(0x2AAC, 0xFE00));
            AddSingle(symbols, "softcy;", Convert(0x044C));
            AddSingle(symbols, "sol;", Convert(0x002F));
            AddSingle(symbols, "solb;", Convert(0x29C4));
            AddSingle(symbols, "solbar;", Convert(0x233F));
            AddSingle(symbols, "sopf;", Convert(0x1D564));
            AddSingle(symbols, "spades;", Convert(0x2660));
            AddSingle(symbols, "spadesuit;", Convert(0x2660));
            AddSingle(symbols, "spar;", Convert(0x2225));
            AddSingle(symbols, "sqcap;", Convert(0x2293));
            AddSingle(symbols, "sqcaps;", Convert(0x2293, 0xFE00));
            AddSingle(symbols, "sqcup;", Convert(0x2294));
            AddSingle(symbols, "sqcups;", Convert(0x2294, 0xFE00));
            AddSingle(symbols, "sqsub;", Convert(0x228F));
            AddSingle(symbols, "sqsube;", Convert(0x2291));
            AddSingle(symbols, "sqsubset;", Convert(0x228F));
            AddSingle(symbols, "sqsubseteq;", Convert(0x2291));
            AddSingle(symbols, "sqsup;", Convert(0x2290));
            AddSingle(symbols, "sqsupe;", Convert(0x2292));
            AddSingle(symbols, "sqsupset;", Convert(0x2290));
            AddSingle(symbols, "sqsupseteq;", Convert(0x2292));
            AddSingle(symbols, "squ;", Convert(0x25A1));
            AddSingle(symbols, "square;", Convert(0x25A1));
            AddSingle(symbols, "squarf;", Convert(0x25AA));
            AddSingle(symbols, "squf;", Convert(0x25AA));
            AddSingle(symbols, "srarr;", Convert(0x2192));
            AddSingle(symbols, "sscr;", Convert(0x1D4C8));
            AddSingle(symbols, "ssetmn;", Convert(0x2216));
            AddSingle(symbols, "ssmile;", Convert(0x2323));
            AddSingle(symbols, "sstarf;", Convert(0x22C6));
            AddSingle(symbols, "star;", Convert(0x2606));
            AddSingle(symbols, "starf;", Convert(0x2605));
            AddSingle(symbols, "straightepsilon;", Convert(0x03F5));
            AddSingle(symbols, "straightphi;", Convert(0x03D5));
            AddSingle(symbols, "strns;", Convert(0x00AF));
            AddSingle(symbols, "sub;", Convert(0x2282));
            AddSingle(symbols, "subdot;", Convert(0x2ABD));
            AddSingle(symbols, "subE;", Convert(0x2AC5));
            AddSingle(symbols, "sube;", Convert(0x2286));
            AddSingle(symbols, "subedot;", Convert(0x2AC3));
            AddSingle(symbols, "submult;", Convert(0x2AC1));
            AddSingle(symbols, "subnE;", Convert(0x2ACB));
            AddSingle(symbols, "subne;", Convert(0x228A));
            AddSingle(symbols, "subplus;", Convert(0x2ABF));
            AddSingle(symbols, "subrarr;", Convert(0x2979));
            AddSingle(symbols, "subset;", Convert(0x2282));
            AddSingle(symbols, "subseteq;", Convert(0x2286));
            AddSingle(symbols, "subseteqq;", Convert(0x2AC5));
            AddSingle(symbols, "subsetneq;", Convert(0x228A));
            AddSingle(symbols, "subsetneqq;", Convert(0x2ACB));
            AddSingle(symbols, "subsim;", Convert(0x2AC7));
            AddSingle(symbols, "subsub;", Convert(0x2AD5));
            AddSingle(symbols, "subsup;", Convert(0x2AD3));
            AddSingle(symbols, "succ;", Convert(0x227B));
            AddSingle(symbols, "succapprox;", Convert(0x2AB8));
            AddSingle(symbols, "succcurlyeq;", Convert(0x227D));
            AddSingle(symbols, "succeq;", Convert(0x2AB0));
            AddSingle(symbols, "succnapprox;", Convert(0x2ABA));
            AddSingle(symbols, "succneqq;", Convert(0x2AB6));
            AddSingle(symbols, "succnsim;", Convert(0x22E9));
            AddSingle(symbols, "succsim;", Convert(0x227F));
            AddSingle(symbols, "sum;", Convert(0x2211));
            AddSingle(symbols, "sung;", Convert(0x266A));
            AddSingle(symbols, "sup;", Convert(0x2283));
            AddBoth(symbols, "sup1;", Convert(0x00B9));
            AddBoth(symbols, "sup2;", Convert(0x00B2));
            AddBoth(symbols, "sup3;", Convert(0x00B3));
            AddSingle(symbols, "supdot;", Convert(0x2ABE));
            AddSingle(symbols, "supdsub;", Convert(0x2AD8));
            AddSingle(symbols, "supE;", Convert(0x2AC6));
            AddSingle(symbols, "supe;", Convert(0x2287));
            AddSingle(symbols, "supedot;", Convert(0x2AC4));
            AddSingle(symbols, "suphsol;", Convert(0x27C9));
            AddSingle(symbols, "suphsub;", Convert(0x2AD7));
            AddSingle(symbols, "suplarr;", Convert(0x297B));
            AddSingle(symbols, "supmult;", Convert(0x2AC2));
            AddSingle(symbols, "supnE;", Convert(0x2ACC));
            AddSingle(symbols, "supne;", Convert(0x228B));
            AddSingle(symbols, "supplus;", Convert(0x2AC0));
            AddSingle(symbols, "supset;", Convert(0x2283));
            AddSingle(symbols, "supseteq;", Convert(0x2287));
            AddSingle(symbols, "supseteqq;", Convert(0x2AC6));
            AddSingle(symbols, "supsetneq;", Convert(0x228B));
            AddSingle(symbols, "supsetneqq;", Convert(0x2ACC));
            AddSingle(symbols, "supsim;", Convert(0x2AC8));
            AddSingle(symbols, "supsub;", Convert(0x2AD4));
            AddSingle(symbols, "supsup;", Convert(0x2AD6));
            AddSingle(symbols, "swarhk;", Convert(0x2926));
            AddSingle(symbols, "swArr;", Convert(0x21D9));
            AddSingle(symbols, "swarr;", Convert(0x2199));
            AddSingle(symbols, "swarrow;", Convert(0x2199));
            AddSingle(symbols, "swnwar;", Convert(0x292A));
            AddBoth(symbols, "szlig;", Convert(0x00DF));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigS()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Sacute;", Convert(0x015A));
            AddSingle(symbols, "Sc;", Convert(0x2ABC));
            AddSingle(symbols, "Scaron;", Convert(0x0160));
            AddSingle(symbols, "Scedil;", Convert(0x015E));
            AddSingle(symbols, "Scirc;", Convert(0x015C));
            AddSingle(symbols, "Scy;", Convert(0x0421));
            AddSingle(symbols, "Sfr;", Convert(0x1D516));
            AddSingle(symbols, "SHCHcy;", Convert(0x0429));
            AddSingle(symbols, "SHcy;", Convert(0x0428));
            AddSingle(symbols, "ShortDownArrow;", Convert(0x2193));
            AddSingle(symbols, "ShortLeftArrow;", Convert(0x2190));
            AddSingle(symbols, "ShortRightArrow;", Convert(0x2192));
            AddSingle(symbols, "ShortUpArrow;", Convert(0x2191));
            AddSingle(symbols, "Sigma;", Convert(0x03A3));
            AddSingle(symbols, "SmallCircle;", Convert(0x2218));
            AddSingle(symbols, "SOFTcy;", Convert(0x042C));
            AddSingle(symbols, "Sopf;", Convert(0x1D54A));
            AddSingle(symbols, "Sqrt;", Convert(0x221A));
            AddSingle(symbols, "Square;", Convert(0x25A1));
            AddSingle(symbols, "SquareIntersection;", Convert(0x2293));
            AddSingle(symbols, "SquareSubset;", Convert(0x228F));
            AddSingle(symbols, "SquareSubsetEqual;", Convert(0x2291));
            AddSingle(symbols, "SquareSuperset;", Convert(0x2290));
            AddSingle(symbols, "SquareSupersetEqual;", Convert(0x2292));
            AddSingle(symbols, "SquareUnion;", Convert(0x2294));
            AddSingle(symbols, "Sscr;", Convert(0x1D4AE));
            AddSingle(symbols, "Star;", Convert(0x22C6));
            AddSingle(symbols, "Sub;", Convert(0x22D0));
            AddSingle(symbols, "Subset;", Convert(0x22D0));
            AddSingle(symbols, "SubsetEqual;", Convert(0x2286));
            AddSingle(symbols, "Succeeds;", Convert(0x227B));
            AddSingle(symbols, "SucceedsEqual;", Convert(0x2AB0));
            AddSingle(symbols, "SucceedsSlantEqual;", Convert(0x227D));
            AddSingle(symbols, "SucceedsTilde;", Convert(0x227F));
            AddSingle(symbols, "SuchThat;", Convert(0x220B));
            AddSingle(symbols, "Sum;", Convert(0x2211));
            AddSingle(symbols, "Sup;", Convert(0x22D1));
            AddSingle(symbols, "Superset;", Convert(0x2283));
            AddSingle(symbols, "SupersetEqual;", Convert(0x2287));
            AddSingle(symbols, "Supset;", Convert(0x22D1));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleT()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "target;", Convert(0x2316));
            AddSingle(symbols, "tau;", Convert(0x03C4));
            AddSingle(symbols, "tbrk;", Convert(0x23B4));
            AddSingle(symbols, "tcaron;", Convert(0x0165));
            AddSingle(symbols, "tcedil;", Convert(0x0163));
            AddSingle(symbols, "tcy;", Convert(0x0442));
            AddSingle(symbols, "tdot;", Convert(0x20DB));
            AddSingle(symbols, "telrec;", Convert(0x2315));
            AddSingle(symbols, "tfr;", Convert(0x1D531));
            AddSingle(symbols, "there4;", Convert(0x2234));
            AddSingle(symbols, "therefore;", Convert(0x2234));
            AddSingle(symbols, "theta;", Convert(0x03B8));
            AddSingle(symbols, "thetasym;", Convert(0x03D1));
            AddSingle(symbols, "thetav;", Convert(0x03D1));
            AddSingle(symbols, "thickapprox;", Convert(0x2248));
            AddSingle(symbols, "thicksim;", Convert(0x223C));
            AddSingle(symbols, "thinsp;", Convert(0x2009));
            AddSingle(symbols, "thkap;", Convert(0x2248));
            AddSingle(symbols, "thksim;", Convert(0x223C));
            AddBoth(symbols, "thorn;", Convert(0x00FE));
            AddSingle(symbols, "tilde;", Convert(0x02DC));
            AddBoth(symbols, "times;", Convert(0x00D7));
            AddSingle(symbols, "timesb;", Convert(0x22A0));
            AddSingle(symbols, "timesbar;", Convert(0x2A31));
            AddSingle(symbols, "timesd;", Convert(0x2A30));
            AddSingle(symbols, "tint;", Convert(0x222D));
            AddSingle(symbols, "toea;", Convert(0x2928));
            AddSingle(symbols, "top;", Convert(0x22A4));
            AddSingle(symbols, "topbot;", Convert(0x2336));
            AddSingle(symbols, "topcir;", Convert(0x2AF1));
            AddSingle(symbols, "topf;", Convert(0x1D565));
            AddSingle(symbols, "topfork;", Convert(0x2ADA));
            AddSingle(symbols, "tosa;", Convert(0x2929));
            AddSingle(symbols, "tprime;", Convert(0x2034));
            AddSingle(symbols, "trade;", Convert(0x2122));
            AddSingle(symbols, "triangle;", Convert(0x25B5));
            AddSingle(symbols, "triangledown;", Convert(0x25BF));
            AddSingle(symbols, "triangleleft;", Convert(0x25C3));
            AddSingle(symbols, "trianglelefteq;", Convert(0x22B4));
            AddSingle(symbols, "triangleq;", Convert(0x225C));
            AddSingle(symbols, "triangleright;", Convert(0x25B9));
            AddSingle(symbols, "trianglerighteq;", Convert(0x22B5));
            AddSingle(symbols, "tridot;", Convert(0x25EC));
            AddSingle(symbols, "trie;", Convert(0x225C));
            AddSingle(symbols, "triminus;", Convert(0x2A3A));
            AddSingle(symbols, "triplus;", Convert(0x2A39));
            AddSingle(symbols, "trisb;", Convert(0x29CD));
            AddSingle(symbols, "tritime;", Convert(0x2A3B));
            AddSingle(symbols, "trpezium;", Convert(0x23E2));
            AddSingle(symbols, "tscr;", Convert(0x1D4C9));
            AddSingle(symbols, "tscy;", Convert(0x0446));
            AddSingle(symbols, "tshcy;", Convert(0x045B));
            AddSingle(symbols, "tstrok;", Convert(0x0167));
            AddSingle(symbols, "twixt;", Convert(0x226C));
            AddSingle(symbols, "twoheadleftarrow;", Convert(0x219E));
            AddSingle(symbols, "twoheadrightarrow;", Convert(0x21A0));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigT()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Tab;", Convert(0x0009));
            AddSingle(symbols, "Tau;", Convert(0x03A4));
            AddSingle(symbols, "Tcaron;", Convert(0x0164));
            AddSingle(symbols, "Tcedil;", Convert(0x0162));
            AddSingle(symbols, "Tcy;", Convert(0x0422));
            AddSingle(symbols, "Tfr;", Convert(0x1D517));
            AddSingle(symbols, "Therefore;", Convert(0x2234));
            AddSingle(symbols, "Theta;", Convert(0x0398));
            AddSingle(symbols, "ThickSpace;", Convert(0x205F, 0x200A));
            AddSingle(symbols, "ThinSpace;", Convert(0x2009));
            AddBoth(symbols, "THORN;", Convert(0x00DE));
            AddSingle(symbols, "Tilde;", Convert(0x223C));
            AddSingle(symbols, "TildeEqual;", Convert(0x2243));
            AddSingle(symbols, "TildeFullEqual;", Convert(0x2245));
            AddSingle(symbols, "TildeTilde;", Convert(0x2248));
            AddSingle(symbols, "Topf;", Convert(0x1D54B));
            AddSingle(symbols, "TRADE;", Convert(0x2122));
            AddSingle(symbols, "TripleDot;", Convert(0x20DB));
            AddSingle(symbols, "Tscr;", Convert(0x1D4AF));
            AddSingle(symbols, "TScy;", Convert(0x0426));
            AddSingle(symbols, "TSHcy;", Convert(0x040B));
            AddSingle(symbols, "Tstrok;", Convert(0x0166));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleU()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "uacute;", Convert(0x00FA));
            AddSingle(symbols, "uArr;", Convert(0x21D1));
            AddSingle(symbols, "uarr;", Convert(0x2191));
            AddSingle(symbols, "ubrcy;", Convert(0x045E));
            AddSingle(symbols, "ubreve;", Convert(0x016D));
            AddBoth(symbols, "ucirc;", Convert(0x00FB));
            AddSingle(symbols, "ucy;", Convert(0x0443));
            AddSingle(symbols, "udarr;", Convert(0x21C5));
            AddSingle(symbols, "udblac;", Convert(0x0171));
            AddSingle(symbols, "udhar;", Convert(0x296E));
            AddSingle(symbols, "ufisht;", Convert(0x297E));
            AddSingle(symbols, "ufr;", Convert(0x1D532));
            AddBoth(symbols, "ugrave;", Convert(0x00F9));
            AddSingle(symbols, "uHar;", Convert(0x2963));
            AddSingle(symbols, "uharl;", Convert(0x21BF));
            AddSingle(symbols, "uharr;", Convert(0x21BE));
            AddSingle(symbols, "uhblk;", Convert(0x2580));
            AddSingle(symbols, "ulcorn;", Convert(0x231C));
            AddSingle(symbols, "ulcorner;", Convert(0x231C));
            AddSingle(symbols, "ulcrop;", Convert(0x230F));
            AddSingle(symbols, "ultri;", Convert(0x25F8));
            AddSingle(symbols, "umacr;", Convert(0x016B));
            AddBoth(symbols, "uml;", Convert(0x00A8));
            AddSingle(symbols, "uogon;", Convert(0x0173));
            AddSingle(symbols, "uopf;", Convert(0x1D566));
            AddSingle(symbols, "uparrow;", Convert(0x2191));
            AddSingle(symbols, "updownarrow;", Convert(0x2195));
            AddSingle(symbols, "upharpoonleft;", Convert(0x21BF));
            AddSingle(symbols, "upharpoonright;", Convert(0x21BE));
            AddSingle(symbols, "uplus;", Convert(0x228E));
            AddSingle(symbols, "upsi;", Convert(0x03C5));
            AddSingle(symbols, "upsih;", Convert(0x03D2));
            AddSingle(symbols, "upsilon;", Convert(0x03C5));
            AddSingle(symbols, "upuparrows;", Convert(0x21C8));
            AddSingle(symbols, "urcorn;", Convert(0x231D));
            AddSingle(symbols, "urcorner;", Convert(0x231D));
            AddSingle(symbols, "urcrop;", Convert(0x230E));
            AddSingle(symbols, "uring;", Convert(0x016F));
            AddSingle(symbols, "urtri;", Convert(0x25F9));
            AddSingle(symbols, "uscr;", Convert(0x1D4CA));
            AddSingle(symbols, "utdot;", Convert(0x22F0));
            AddSingle(symbols, "utilde;", Convert(0x0169));
            AddSingle(symbols, "utri;", Convert(0x25B5));
            AddSingle(symbols, "utrif;", Convert(0x25B4));
            AddSingle(symbols, "uuarr;", Convert(0x21C8));
            AddBoth(symbols, "uuml;", Convert(0x00FC));
            AddSingle(symbols, "uwangle;", Convert(0x29A7));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigU()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "Uacute;", Convert(0x00DA));
            AddSingle(symbols, "Uarr;", Convert(0x219F));
            AddSingle(symbols, "Uarrocir;", Convert(0x2949));
            AddSingle(symbols, "Ubrcy;", Convert(0x040E));
            AddSingle(symbols, "Ubreve;", Convert(0x016C));
            AddBoth(symbols, "Ucirc;", Convert(0x00DB));
            AddSingle(symbols, "Ucy;", Convert(0x0423));
            AddSingle(symbols, "Udblac;", Convert(0x0170));
            AddSingle(symbols, "Ufr;", Convert(0x1D518));
            AddBoth(symbols, "Ugrave;", Convert(0x00D9));
            AddSingle(symbols, "Umacr;", Convert(0x016A));
            AddSingle(symbols, "UnderBar;", Convert(0x005F));
            AddSingle(symbols, "UnderBrace;", Convert(0x23DF));
            AddSingle(symbols, "UnderBracket;", Convert(0x23B5));
            AddSingle(symbols, "UnderParenthesis;", Convert(0x23DD));
            AddSingle(symbols, "Union;", Convert(0x22C3));
            AddSingle(symbols, "UnionPlus;", Convert(0x228E));
            AddSingle(symbols, "Uogon;", Convert(0x0172));
            AddSingle(symbols, "Uopf;", Convert(0x1D54C));
            AddSingle(symbols, "UpArrow;", Convert(0x2191));
            AddSingle(symbols, "Uparrow;", Convert(0x21D1));
            AddSingle(symbols, "UpArrowBar;", Convert(0x2912));
            AddSingle(symbols, "UpArrowDownArrow;", Convert(0x21C5));
            AddSingle(symbols, "UpDownArrow;", Convert(0x2195));
            AddSingle(symbols, "Updownarrow;", Convert(0x21D5));
            AddSingle(symbols, "UpEquilibrium;", Convert(0x296E));
            AddSingle(symbols, "UpperLeftArrow;", Convert(0x2196));
            AddSingle(symbols, "UpperRightArrow;", Convert(0x2197));
            AddSingle(symbols, "Upsi;", Convert(0x03D2));
            AddSingle(symbols, "Upsilon;", Convert(0x03A5));
            AddSingle(symbols, "UpTee;", Convert(0x22A5));
            AddSingle(symbols, "UpTeeArrow;", Convert(0x21A5));
            AddSingle(symbols, "Uring;", Convert(0x016E));
            AddSingle(symbols, "Uscr;", Convert(0x1D4B0));
            AddSingle(symbols, "Utilde;", Convert(0x0168));
            AddBoth(symbols, "Uuml;", Convert(0x00DC));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleV()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "vangrt;", Convert(0x299C));
            AddSingle(symbols, "varepsilon;", Convert(0x03F5));
            AddSingle(symbols, "varkappa;", Convert(0x03F0));
            AddSingle(symbols, "varnothing;", Convert(0x2205));
            AddSingle(symbols, "varphi;", Convert(0x03D5));
            AddSingle(symbols, "varpi;", Convert(0x03D6));
            AddSingle(symbols, "varpropto;", Convert(0x221D));
            AddSingle(symbols, "vArr;", Convert(0x21D5));
            AddSingle(symbols, "varr;", Convert(0x2195));
            AddSingle(symbols, "varrho;", Convert(0x03F1));
            AddSingle(symbols, "varsigma;", Convert(0x03C2));
            AddSingle(symbols, "varsubsetneq;", Convert(0x228A, 0xFE00));
            AddSingle(symbols, "varsubsetneqq;", Convert(0x2ACB, 0xFE00));
            AddSingle(symbols, "varsupsetneq;", Convert(0x228B, 0xFE00));
            AddSingle(symbols, "varsupsetneqq;", Convert(0x2ACC, 0xFE00));
            AddSingle(symbols, "vartheta;", Convert(0x03D1));
            AddSingle(symbols, "vartriangleleft;", Convert(0x22B2));
            AddSingle(symbols, "vartriangleright;", Convert(0x22B3));
            AddSingle(symbols, "vBar;", Convert(0x2AE8));
            AddSingle(symbols, "vBarv;", Convert(0x2AE9));
            AddSingle(symbols, "vcy;", Convert(0x0432));
            AddSingle(symbols, "vDash;", Convert(0x22A8));
            AddSingle(symbols, "vdash;", Convert(0x22A2));
            AddSingle(symbols, "vee;", Convert(0x2228));
            AddSingle(symbols, "veebar;", Convert(0x22BB));
            AddSingle(symbols, "veeeq;", Convert(0x225A));
            AddSingle(symbols, "vellip;", Convert(0x22EE));
            AddSingle(symbols, "verbar;", Convert(0x007C));
            AddSingle(symbols, "vert;", Convert(0x007C));
            AddSingle(symbols, "vfr;", Convert(0x1D533));
            AddSingle(symbols, "vltri;", Convert(0x22B2));
            AddSingle(symbols, "vnsub;", Convert(0x2282, 0x20D2));
            AddSingle(symbols, "vnsup;", Convert(0x2283, 0x20D2));
            AddSingle(symbols, "vopf;", Convert(0x1D567));
            AddSingle(symbols, "vprop;", Convert(0x221D));
            AddSingle(symbols, "vrtri;", Convert(0x22B3));
            AddSingle(symbols, "vscr;", Convert(0x1D4CB));
            AddSingle(symbols, "vsubnE;", Convert(0x2ACB, 0xFE00));
            AddSingle(symbols, "vsubne;", Convert(0x228A, 0xFE00));
            AddSingle(symbols, "vsupnE;", Convert(0x2ACC, 0xFE00));
            AddSingle(symbols, "vsupne;", Convert(0x228B, 0xFE00));
            AddSingle(symbols, "vzigzag;", Convert(0x299A));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigV()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Vbar;", Convert(0x2AEB));
            AddSingle(symbols, "Vcy;", Convert(0x0412));
            AddSingle(symbols, "VDash;", Convert(0x22AB));
            AddSingle(symbols, "Vdash;", Convert(0x22A9));
            AddSingle(symbols, "Vdashl;", Convert(0x2AE6));
            AddSingle(symbols, "Vee;", Convert(0x22C1));
            AddSingle(symbols, "Verbar;", Convert(0x2016));
            AddSingle(symbols, "Vert;", Convert(0x2016));
            AddSingle(symbols, "VerticalBar;", Convert(0x2223));
            AddSingle(symbols, "VerticalLine;", Convert(0x007C));
            AddSingle(symbols, "VerticalSeparator;", Convert(0x2758));
            AddSingle(symbols, "VerticalTilde;", Convert(0x2240));
            AddSingle(symbols, "VeryThinSpace;", Convert(0x200A));
            AddSingle(symbols, "Vfr;", Convert(0x1D519));
            AddSingle(symbols, "Vopf;", Convert(0x1D54D));
            AddSingle(symbols, "Vscr;", Convert(0x1D4B1));
            AddSingle(symbols, "Vvdash;", Convert(0x22AA));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleW()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "wcirc;", Convert(0x0175));
            AddSingle(symbols, "wedbar;", Convert(0x2A5F));
            AddSingle(symbols, "wedge;", Convert(0x2227));
            AddSingle(symbols, "wedgeq;", Convert(0x2259));
            AddSingle(symbols, "weierp;", Convert(0x2118));
            AddSingle(symbols, "wfr;", Convert(0x1D534));
            AddSingle(symbols, "wopf;", Convert(0x1D568));
            AddSingle(symbols, "wp;", Convert(0x2118));
            AddSingle(symbols, "wr;", Convert(0x2240));
            AddSingle(symbols, "wreath;", Convert(0x2240));
            AddSingle(symbols, "wscr;", Convert(0x1D4CC));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigW()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Wcirc;", Convert(0x0174));
            AddSingle(symbols, "Wedge;", Convert(0x22C0));
            AddSingle(symbols, "Wfr;", Convert(0x1D51A));
            AddSingle(symbols, "Wopf;", Convert(0x1D54E));
            AddSingle(symbols, "Wscr;", Convert(0x1D4B2));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleX()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "xcap;", Convert(0x22C2));
            AddSingle(symbols, "xcirc;", Convert(0x25EF));
            AddSingle(symbols, "xcup;", Convert(0x22C3));
            AddSingle(symbols, "xdtri;", Convert(0x25BD));
            AddSingle(symbols, "xfr;", Convert(0x1D535));
            AddSingle(symbols, "xhArr;", Convert(0x27FA));
            AddSingle(symbols, "xharr;", Convert(0x27F7));
            AddSingle(symbols, "xi;", Convert(0x03BE));
            AddSingle(symbols, "xlArr;", Convert(0x27F8));
            AddSingle(symbols, "xlarr;", Convert(0x27F5));
            AddSingle(symbols, "xmap;", Convert(0x27FC));
            AddSingle(symbols, "xnis;", Convert(0x22FB));
            AddSingle(symbols, "xodot;", Convert(0x2A00));
            AddSingle(symbols, "xopf;", Convert(0x1D569));
            AddSingle(symbols, "xoplus;", Convert(0x2A01));
            AddSingle(symbols, "xotime;", Convert(0x2A02));
            AddSingle(symbols, "xrArr;", Convert(0x27F9));
            AddSingle(symbols, "xrarr;", Convert(0x27F6));
            AddSingle(symbols, "xscr;", Convert(0x1D4CD));
            AddSingle(symbols, "xsqcup;", Convert(0x2A06));
            AddSingle(symbols, "xuplus;", Convert(0x2A04));
            AddSingle(symbols, "xutri;", Convert(0x25B3));
            AddSingle(symbols, "xvee;", Convert(0x22C1));
            AddSingle(symbols, "xwedge;", Convert(0x22C0));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigX()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Xfr;", Convert(0x1D51B));
            AddSingle(symbols, "Xi;", Convert(0x039E));
            AddSingle(symbols, "Xopf;", Convert(0x1D54F));
            AddSingle(symbols, "Xscr;", Convert(0x1D4B3));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleY()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "yacute;", Convert(0x00FD));
            AddSingle(symbols, "yacy;", Convert(0x044F));
            AddSingle(symbols, "ycirc;", Convert(0x0177));
            AddSingle(symbols, "ycy;", Convert(0x044B));
            AddBoth(symbols, "yen;", Convert(0x00A5));
            AddSingle(symbols, "yfr;", Convert(0x1D536));
            AddSingle(symbols, "yicy;", Convert(0x0457));
            AddSingle(symbols, "yopf;", Convert(0x1D56A));
            AddSingle(symbols, "yscr;", Convert(0x1D4CE));
            AddSingle(symbols, "yucy;", Convert(0x044E));
            AddBoth(symbols, "yuml;", Convert(0x00FF));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigY()
        {
            var symbols = new Dictionary<String, String>();
            AddBoth(symbols, "Yacute;", Convert(0x00DD));
            AddSingle(symbols, "YAcy;", Convert(0x042F));
            AddSingle(symbols, "Ycirc;", Convert(0x0176));
            AddSingle(symbols, "Ycy;", Convert(0x042B));
            AddSingle(symbols, "Yfr;", Convert(0x1D51C));
            AddSingle(symbols, "YIcy;", Convert(0x0407));
            AddSingle(symbols, "Yopf;", Convert(0x1D550));
            AddSingle(symbols, "Yscr;", Convert(0x1D4B4));
            AddSingle(symbols, "YUcy;", Convert(0x042E));
            AddSingle(symbols, "Yuml;", Convert(0x0178));
            return symbols;
        }

        Dictionary<String, String> GetSymbolLittleZ()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "zacute;", Convert(0x017A));
            AddSingle(symbols, "zcaron;", Convert(0x017E));
            AddSingle(symbols, "zcy;", Convert(0x0437));
            AddSingle(symbols, "zdot;", Convert(0x017C));
            AddSingle(symbols, "zeetrf;", Convert(0x2128));
            AddSingle(symbols, "zeta;", Convert(0x03B6));
            AddSingle(symbols, "zfr;", Convert(0x1D537));
            AddSingle(symbols, "zhcy;", Convert(0x0436));
            AddSingle(symbols, "zigrarr;", Convert(0x21DD));
            AddSingle(symbols, "zopf;", Convert(0x1D56B));
            AddSingle(symbols, "zscr;", Convert(0x1D4CF));
            AddSingle(symbols, "zwj;", Convert(0x200D));
            AddSingle(symbols, "zwnj;", Convert(0x200C));
            return symbols;
        }

        Dictionary<String, String> GetSymbolBigZ()
        {
            var symbols = new Dictionary<String, String>();
            AddSingle(symbols, "Zacute;", Convert(0x0179));
            AddSingle(symbols, "Zcaron;", Convert(0x017D));
            AddSingle(symbols, "Zcy;", Convert(0x0417));
            AddSingle(symbols, "Zdot;", Convert(0x017B));
            AddSingle(symbols, "ZeroWidthSpace;", Convert(0x200B));
            AddSingle(symbols, "Zeta;", Convert(0x0396));
            AddSingle(symbols, "Zfr;", Convert(0x2128));
            AddSingle(symbols, "ZHcy;", Convert(0x0416));
            AddSingle(symbols, "Zopf;", Convert(0x2124));
            AddSingle(symbols, "Zscr;", Convert(0x1D4B5));
            return symbols;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a symbol (that ended with a semicolon) specified by its entity
        /// name.
        /// </summary>
        /// <param name="name">
        /// The name of the entity, specified by &amp;NAME; in the Html code.
        /// </param>
        /// <returns>The string with the symbol or null.</returns>
        public String GetSymbol(String name)
        {
            var symbol = default(String);
            var symbols = default(Dictionary<String, String>);

            if (!String.IsNullOrEmpty(name) && _entities.TryGetValue(name[0], out symbols))
                symbols.TryGetValue(name, out symbol);

            return symbol;
        }

        /// <summary>
        /// Converts a given number into its unicode character.
        /// </summary>
        /// <param name="code">The code to convert.</param>
        /// <returns>The array containing the character.</returns>
        static String Convert(Int32 code)
        {
            return code.ConvertFromUtf32();
        }

        /// <summary>
        /// Converts a set of two numbers into their unicode characters.
        /// </summary>
        /// <param name="leading">The first (leading) character code.</param>
        /// <param name="trailing">The second (trailing) character code.</param>
        /// <returns>The array containing the two characters.</returns>
        static String Convert(Int32 leading, Int32 trailing)
        {
            return leading.ConvertFromUtf32() + trailing.ConvertFromUtf32();
        }

        /// <summary>
        /// Determines if the code is an invalid number.
        /// </summary>
        /// <param name="code">The code to examine.</param>
        /// <returns>True if it is an invalid number, false otherwise.</returns>
        public static Boolean IsInvalidNumber(Int32 code)
        {
            /*
             * Otherwise, if the number is
             *     in the range 0xD800 to 0xDFFF (surrogate Unicode zone)
             *     or less than 0x0 or is greater than 0x10FFFF,
             * then this is a parse error.
             * Return a U+FFFD REPLACEMENT CHARACTER.
             */

            return (code >= 0xD800 && code <= 0xDFFF) || (code < 0x0) || (code > 0x10FFFF);
        }

        /// <summary>
        /// Determines if the given code is actually in the table of common
        /// redirections.
        /// </summary>
        /// <param name="code">The code to examine.</param>
        /// <returns>True if the code is in the table, else false.</returns>
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
        /// <returns>
        /// True if it is within an invalid range, false otherwise.
        /// </returns>
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
                    (code == 0x8FFFF || code == 0x9FFFE) ||
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

        static void AddSingle(Dictionary<String, String> symbols, String key, String value)
        {
            symbols.Add(key, value);
        }

        static void AddBoth(Dictionary<String, String> symbols, String key, String value)
        {
            symbols.Add(key, value);
            symbols.Add(key.Remove(key.Length - 1), value);
        }

        #endregion
    }
}

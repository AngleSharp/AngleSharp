using System;
using System.Text;

namespace AngleSharp
{
    /// <summary>
    /// Represents the list of all Html entities.
    /// </summary>
    static class Entities
    {
        /// <summary>
        /// Gets an identity specified by its name.
        /// </summary>
        /// <param name="name">The name of the identity, specified by &amp;NAME; in the Html source code.</param>
        /// <returns>The array containing the found entity or empty.</returns>
        public static char[] GetSymbol(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            switch (name[0])
            {
                case 'a':
                case 'A':
                    return GetSymbolA(name);
                case 'b':
                case 'B':
                    return GetSymbolB(name);
                case 'c':
                case 'C':
                    return GetSymbolC(name);
                case 'd':
                case 'D':
                    return GetSymbolD(name);
                case 'e':
                case 'E':
                    return GetSymbolE(name);
                case 'f':
                case 'F':
                    return GetSymbolF(name);
                case 'g':
                case 'G':
                    return GetSymbolG(name);
                case 'h':
                case 'H':
                    return GetSymbolH(name);
                case 'i':
                case 'I':
                    return GetSymbolI(name);
                case 'j':
                case 'J':
                    return GetSymbolJ(name);
                case 'k':
                case 'K':
                    return GetSymbolK(name);
                case 'l':
                case 'L':
                    return GetSymbolL(name);
                case 'm':
                case 'M':
                    return GetSymbolM(name);
                case 'n':
                case 'N':
                    return GetSymbolN(name);
                case 'o':
                case 'O':
                    return GetSymbolO(name);
                case 'p':
                case 'P':
                    return GetSymbolP(name);
                case 'q':
                case 'Q':
                    return GetSymbolQ(name);
                case 'r':
                case 'R':
                    return GetSymbolR(name);
                case 's':
                case 'S':
                    return GetSymbolS(name);
                case 't':
                case 'T':
                    return GetSymbolT(name);
                case 'u':
                case 'U':
                    return GetSymbolU(name);
                case 'v':
                case 'V':
                    return GetSymbolV(name);
                case 'w':
                case 'W':
                    return GetSymbolW(name);
                case 'x':
                case 'X':
                    return GetSymbolX(name);
                case 'y':
                case 'Y':
                    return GetSymbolY(name);
                case 'z':
                case 'Z':
                    return GetSymbolZ(name);
            }

            return null;
        }

        #region Symbol Methods

        static char[] GetSymbolA(string name)
        {
            switch (name)
            {
                case "Aacute": return Convert(0x00C1);
                case "aacute": return Convert(0x00E1);
                case "Abreve": return Convert(0x0102);
                case "abreve": return Convert(0x0103);
                case "ac": return Convert(0x223E);
                case "acd": return Convert(0x223F);
                case "acE": return Convert(0x223E, 0x0333);
                case "Acirc": return Convert(0x00C2);
                case "acirc": return Convert(0x00E2);
                case "acute": return Convert(0x00B4);
                case "Acy": return Convert(0x0410);
                case "acy": return Convert(0x0430);
                case "AElig": return Convert(0x00C6);
                case "aelig": return Convert(0x00E6);
                case "af": return Convert(0x2061);
                case "Afr": return Convert(0xD835, 0xDD04);
                case "afr": return Convert(0xD835, 0xDD1E);
                case "Agrave": return Convert(0x00C0);
                case "agrave": return Convert(0x00E0);
                case "alefsym": return Convert(0x2135);
                case "aleph": return Convert(0x2135);
                case "Alpha": return Convert(0x0391);
                case "alpha": return Convert(0x03B1);
                case "Amacr": return Convert(0x0100);
                case "amacr": return Convert(0x0101);
                case "amalg": return Convert(0x2A3F);
                case "AMP": return Convert(0x0026);
                case "amp": return Convert(0x0026);
                case "And": return Convert(0x2A53);
                case "and": return Convert(0x2227);
                case "andand": return Convert(0x2A55);
                case "andd": return Convert(0x2A5C);
                case "andslope": return Convert(0x2A58);
                case "andv": return Convert(0x2A5A);
                case "ang": return Convert(0x2220);
                case "ange": return Convert(0x29A4);
                case "angle": return Convert(0x2220);
                case "angmsd": return Convert(0x2221);
                case "angmsdaa": return Convert(0x29A8);
                case "angmsdab": return Convert(0x29A9);
                case "angmsdac": return Convert(0x29AA);
                case "angmsdad": return Convert(0x29AB);
                case "angmsdae": return Convert(0x29AC);
                case "angmsdaf": return Convert(0x29AD);
                case "angmsdag": return Convert(0x29AE);
                case "angmsdah": return Convert(0x29AF);
                case "angrt": return Convert(0x221F);
                case "angrtvb": return Convert(0x22BE);
                case "angrtvbd": return Convert(0x299D);
                case "angsph": return Convert(0x2222);
                case "angst": return Convert(0x00C5);
                case "angzarr": return Convert(0x237C);
                case "Aogon": return Convert(0x0104);
                case "aogon": return Convert(0x0105);
                case "Aopf": return Convert(0xD835, 0xDD38);
                case "aopf": return Convert(0xD835, 0xDD52);
                case "ap": return Convert(0x2248);
                case "apacir": return Convert(0x2A6F);
                case "apE": return Convert(0x2A70);
                case "ape": return Convert(0x224A);
                case "apid": return Convert(0x224B);
                case "apos": return Convert(0x0027);
                case "ApplyFunction": return Convert(0x2061);
                case "approx": return Convert(0x2248);
                case "approxeq": return Convert(0x224A);
                case "Aring": return Convert(0x00C5);
                case "aring": return Convert(0x00E5);
                case "Ascr": return Convert(0xD835, 0xDC9C);
                case "ascr": return Convert(0xD835, 0xDCB6);
                case "Assign": return Convert(0x2254);
                case "ast": return Convert(0x002A);
                case "asymp": return Convert(0x2248);
                case "asympeq": return Convert(0x224D);
                case "Atilde": return Convert(0x00C3);
                case "atilde": return Convert(0x00E3);
                case "Auml": return Convert(0x00C4);
                case "auml": return Convert(0x00E4);
                case "awconint": return Convert(0x2233);
                case "awint": return Convert(0x2A11);
            }

            return null;
        }

        static char[] GetSymbolB(string name)
        {
            switch (name)
            {
                case "backcong": return Convert(0x224C);
                case "backepsilon": return Convert(0x03F6);
                case "backprime": return Convert(0x2035);
                case "backsim": return Convert(0x223D);
                case "backsimeq": return Convert(0x22CD);
                case "Backslash": return Convert(0x2216);
                case "Barv": return Convert(0x2AE7);
                case "barvee": return Convert(0x22BD);
                case "Barwed": return Convert(0x2306);
                case "barwed": return Convert(0x2305);
                case "barwedge": return Convert(0x2305);
                case "bbrk": return Convert(0x23B5);
                case "bbrktbrk": return Convert(0x23B6);
                case "bcong": return Convert(0x224C);
                case "Bcy": return Convert(0x0411);
                case "bcy": return Convert(0x0431);
                case "bdquo": return Convert(0x201E);
                case "becaus": return Convert(0x2235);
                case "Because": return Convert(0x2235);
                case "because": return Convert(0x2235);
                case "bemptyv": return Convert(0x29B0);
                case "bepsi": return Convert(0x03F6);
                case "bernou": return Convert(0x212C);
                case "Bernoullis": return Convert(0x212C);
                case "Beta": return Convert(0x0392);
                case "beta": return Convert(0x03B2);
                case "beth": return Convert(0x2136);
                case "between": return Convert(0x226C);
                case "Bfr": return Convert(0xD835, 0xDD05);
                case "bfr": return Convert(0xD835, 0xDD1F);
                case "bigcap": return Convert(0x22C2);
                case "bigcirc": return Convert(0x25EF);
                case "bigcup": return Convert(0x22C3);
                case "bigodot": return Convert(0x2A00);
                case "bigoplus": return Convert(0x2A01);
                case "bigotimes": return Convert(0x2A02);
                case "bigsqcup": return Convert(0x2A06);
                case "bigstar": return Convert(0x2605);
                case "bigtriangledown": return Convert(0x25BD);
                case "bigtriangleup": return Convert(0x25B3);
                case "biguplus": return Convert(0x2A04);
                case "bigvee": return Convert(0x22C1);
                case "bigwedge": return Convert(0x22C0);
                case "bkarow": return Convert(0x290D);
                case "blacklozenge": return Convert(0x29EB);
                case "blacksquare": return Convert(0x25AA);
                case "blacktriangle": return Convert(0x25B4);
                case "blacktriangledown": return Convert(0x25BE);
                case "blacktriangleleft": return Convert(0x25C2);
                case "blacktriangleright": return Convert(0x25B8);
                case "blank": return Convert(0x2423);
                case "blk12": return Convert(0x2592);
                case "blk14": return Convert(0x2591);
                case "blk34": return Convert(0x2593);
                case "block": return Convert(0x2588);
                case "bne": return Convert(0x003D, 0x20E5);
                case "bnequiv": return Convert(0x2261, 0x20E5);
                case "bNot": return Convert(0x2AED);
                case "bnot": return Convert(0x2310);
                case "Bopf": return Convert(0xD835, 0xDD39);
                case "bopf": return Convert(0xD835, 0xDD53);
                case "bot": return Convert(0x22A5);
                case "bottom": return Convert(0x22A5);
                case "bowtie": return Convert(0x22C8);
                case "boxbox": return Convert(0x29C9);
                case "boxDL": return Convert(0x2557);
                case "boxDl": return Convert(0x2556);
                case "boxdL": return Convert(0x2555);
                case "boxdl": return Convert(0x2510);
                case "boxDR": return Convert(0x2554);
                case "boxDr": return Convert(0x2553);
                case "boxdR": return Convert(0x2552);
                case "boxdr": return Convert(0x250C);
                case "boxH": return Convert(0x2550);
                case "boxh": return Convert(0x2500);
                case "boxHD": return Convert(0x2566);
                case "boxHd": return Convert(0x2564);
                case "boxhD": return Convert(0x2565);
                case "boxhd": return Convert(0x252C);
                case "boxHU": return Convert(0x2569);
                case "boxHu": return Convert(0x2567);
                case "boxhU": return Convert(0x2568);
                case "boxhu": return Convert(0x2534);
                case "boxminus": return Convert(0x229F);
                case "boxplus": return Convert(0x229E);
                case "boxtimes": return Convert(0x22A0);
                case "boxUL": return Convert(0x255D);
                case "boxUl": return Convert(0x255C);
                case "boxuL": return Convert(0x255B);
                case "boxul": return Convert(0x2518);
                case "boxUR": return Convert(0x255A);
                case "boxUr": return Convert(0x2559);
                case "boxuR": return Convert(0x2558);
                case "boxur": return Convert(0x2514);
                case "boxV": return Convert(0x2551);
                case "boxv": return Convert(0x2502);
                case "boxVH": return Convert(0x256C);
                case "boxVh": return Convert(0x256B);
                case "boxvH": return Convert(0x256A);
                case "boxvh": return Convert(0x253C);
                case "boxVL": return Convert(0x2563);
                case "boxVl": return Convert(0x2562);
                case "boxvL": return Convert(0x2561);
                case "boxvl": return Convert(0x2524);
                case "boxVR": return Convert(0x2560);
                case "boxVr": return Convert(0x255F);
                case "boxvR": return Convert(0x255E);
                case "boxvr": return Convert(0x251C);
                case "bprime": return Convert(0x2035);
                case "Breve": return Convert(0x02D8);
                case "breve": return Convert(0x02D8);
                case "brvbar": return Convert(0x00A6);
                case "Bscr": return Convert(0x212C);
                case "bscr": return Convert(0xD835, 0xDCB7);
                case "bsemi": return Convert(0x204F);
                case "bsim": return Convert(0x223D);
                case "bsime": return Convert(0x22CD);
                case "bsol": return Convert(0x005C);
                case "bsolb": return Convert(0x29C5);
                case "bsolhsub": return Convert(0x27C8);
                case "bull": return Convert(0x2022);
                case "bullet": return Convert(0x2022);
                case "bump": return Convert(0x224E);
                case "bumpE": return Convert(0x2AAE);
                case "bumpe": return Convert(0x224F);
                case "Bumpeq": return Convert(0x224E);
                case "bumpeq": return Convert(0x224F);
            }

            return null;
        }

        static char[] GetSymbolC(string name)
        {
            switch (name)
            {
                case "Cacute": return Convert(0x0106);
                case "cacute": return Convert(0x0107);
                case "Cap": return Convert(0x22D2);
                case "cap": return Convert(0x2229);
                case "capand": return Convert(0x2A44);
                case "capbrcup": return Convert(0x2A49);
                case "capcap": return Convert(0x2A4B);
                case "capcup": return Convert(0x2A47);
                case "capdot": return Convert(0x2A40);
                case "CapitalDifferentialD": return Convert(0x2145);
                case "caps": return Convert(0x2229, 0xFE00);
                case "caret": return Convert(0x2041);
                case "caron": return Convert(0x02C7);
                case "Cayleys": return Convert(0x212D);
                case "ccaps": return Convert(0x2A4D);
                case "Ccaron": return Convert(0x010C);
                case "ccaron": return Convert(0x010D);
                case "Ccedil": return Convert(0x00C7);
                case "ccedil": return Convert(0x00E7);
                case "Ccirc": return Convert(0x0108);
                case "ccirc": return Convert(0x0109);
                case "Cconint": return Convert(0x2230);
                case "ccups": return Convert(0x2A4C);
                case "ccupssm": return Convert(0x2A50);
                case "Cdot": return Convert(0x010A);
                case "cdot": return Convert(0x010B);
                case "cedil": return Convert(0x00B8);
                case "Cedilla": return Convert(0x00B8);
                case "cemptyv": return Convert(0x29B2);
                case "cent": return Convert(0x00A2);
                case "CenterDot": return Convert(0x00B7);
                case "centerdot": return Convert(0x00B7);
                case "Cfr": return Convert(0x212D);
                case "cfr": return Convert(0xD835, 0xDD20);
                case "CHcy": return Convert(0x0427);
                case "chcy": return Convert(0x0447);
                case "check": return Convert(0x2713);
                case "checkmark": return Convert(0x2713);
                case "Chi": return Convert(0x03A7);
                case "chi": return Convert(0x03C7);
                case "cir": return Convert(0x25CB);
                case "circ": return Convert(0x02C6);
                case "circeq": return Convert(0x2257);
                case "circlearrowleft": return Convert(0x21BA);
                case "circlearrowright": return Convert(0x21BB);
                case "circledast": return Convert(0x229B);
                case "circledcirc": return Convert(0x229A);
                case "circleddash": return Convert(0x229D);
                case "CircleDot": return Convert(0x2299);
                case "circledR": return Convert(0x00AE);
                case "circledS": return Convert(0x24C8);
                case "CircleMinus": return Convert(0x2296);
                case "CirclePlus": return Convert(0x2295);
                case "CircleTimes": return Convert(0x2297);
                case "cirE": return Convert(0x29C3);
                case "cire": return Convert(0x2257);
                case "cirfnint": return Convert(0x2A10);
                case "cirmid": return Convert(0x2AEF);
                case "cirscir": return Convert(0x29C2);
                case "ClockwiseContourIntegral": return Convert(0x2232);
                case "CloseCurlyDoubleQuote": return Convert(0x201D);
                case "CloseCurlyQuote": return Convert(0x2019);
                case "clubs": return Convert(0x2663);
                case "clubsuit": return Convert(0x2663);
                case "Colon": return Convert(0x2237);
                case "colon": return Convert(0x003A);
                case "Colone": return Convert(0x2A74);
                case "colone": return Convert(0x2254);
                case "coloneq": return Convert(0x2254);
                case "comma": return Convert(0x002C);
                case "commat": return Convert(0x0040);
                case "comp": return Convert(0x2201);
                case "compfn": return Convert(0x2218);
                case "complement": return Convert(0x2201);
                case "complexes": return Convert(0x2102);
                case "cong": return Convert(0x2245);
                case "congdot": return Convert(0x2A6D);
                case "Congruent": return Convert(0x2261);
                case "Conint": return Convert(0x222F);
                case "conint": return Convert(0x222E);
                case "ContourIntegral": return Convert(0x222E);
                case "Copf": return Convert(0x2102);
                case "copf": return Convert(0xD835, 0xDD54);
                case "coprod": return Convert(0x2210);
                case "Coproduct": return Convert(0x2210);
                case "COPY": return Convert(0x00A9);
                case "copy": return Convert(0x00A9);
                case "copysr": return Convert(0x2117);
                case "CounterClockwiseContourIntegral": return Convert(0x2233);
                case "crarr": return Convert(0x21B5);
                case "Cross": return Convert(0x2A2F);
                case "cross": return Convert(0x2717);
                case "Cscr": return Convert(0xD835, 0xDC9E);
                case "cscr": return Convert(0xD835, 0xDCB8);
                case "csub": return Convert(0x2ACF);
                case "csube": return Convert(0x2AD1);
                case "csup": return Convert(0x2AD0);
                case "csupe": return Convert(0x2AD2);
                case "ctdot": return Convert(0x22EF);
                case "cudarrl": return Convert(0x2938);
                case "cudarrr": return Convert(0x2935);
                case "cuepr": return Convert(0x22DE);
                case "cuesc": return Convert(0x22DF);
                case "cularr": return Convert(0x21B6);
                case "cularrp": return Convert(0x293D);
                case "Cup": return Convert(0x22D3);
                case "cup": return Convert(0x222A);
                case "cupbrcap": return Convert(0x2A48);
                case "CupCap": return Convert(0x224D);
                case "cupcap": return Convert(0x2A46);
                case "cupcup": return Convert(0x2A4A);
                case "cupdot": return Convert(0x228D);
                case "cupor": return Convert(0x2A45);
                case "cups": return Convert(0x222A, 0xFE00);
                case "curarr": return Convert(0x21B7);
                case "curarrm": return Convert(0x293C);
                case "curlyeqprec": return Convert(0x22DE);
                case "curlyeqsucc": return Convert(0x22DF);
                case "curlyvee": return Convert(0x22CE);
                case "curlywedge": return Convert(0x22CF);
                case "curren": return Convert(0x00A4);
                case "curvearrowleft": return Convert(0x21B6);
                case "curvearrowright": return Convert(0x21B7);
                case "cuvee": return Convert(0x22CE);
                case "cuwed": return Convert(0x22CF);
                case "cwconint": return Convert(0x2232);
                case "cwint": return Convert(0x2231);
                case "cylcty": return Convert(0x232D);
            }

            return null;
        }

        static char[] GetSymbolD(string name)
        {
            switch (name)
            {
                case "Dagger": return Convert(0x2021);
                case "dagger": return Convert(0x2020);
                case "daleth": return Convert(0x2138);
                case "Darr": return Convert(0x21A1);
                case "dArr": return Convert(0x21D3);
                case "darr": return Convert(0x2193);
                case "dash": return Convert(0x2010);
                case "Dashv": return Convert(0x2AE4);
                case "dashv": return Convert(0x22A3);
                case "dbkarow": return Convert(0x290F);
                case "dblac": return Convert(0x02DD);
                case "Dcaron": return Convert(0x010E);
                case "dcaron": return Convert(0x010F);
                case "Dcy": return Convert(0x0414);
                case "dcy": return Convert(0x0434);
                case "DD": return Convert(0x2145);
                case "dd": return Convert(0x2146);
                case "ddagger": return Convert(0x2021);
                case "ddarr": return Convert(0x21CA);
                case "DDotrahd": return Convert(0x2911);
                case "ddotseq": return Convert(0x2A77);
                case "deg": return Convert(0x00B0);
                case "Del": return Convert(0x2207);
                case "Delta": return Convert(0x0394);
                case "delta": return Convert(0x03B4);
                case "demptyv": return Convert(0x29B1);
                case "dfisht": return Convert(0x297F);
                case "Dfr": return Convert(0xD835, 0xDD07);
                case "dfr": return Convert(0xD835, 0xDD21);
                case "dHar": return Convert(0x2965);
                case "dharl": return Convert(0x21C3);
                case "dharr": return Convert(0x21C2);
                case "DiacriticalAcute": return Convert(0x00B4);
                case "DiacriticalDot": return Convert(0x02D9);
                case "DiacriticalDoubleAcute": return Convert(0x02DD);
                case "DiacriticalGrave": return Convert(0x0060);
                case "DiacriticalTilde": return Convert(0x02DC);
                case "diam": return Convert(0x22C4);
                case "Diamond": return Convert(0x22C4);
                case "diamond": return Convert(0x22C4);
                case "diamondsuit": return Convert(0x2666);
                case "diams": return Convert(0x2666);
                case "die": return Convert(0x00A8);
                case "DifferentialD": return Convert(0x2146);
                case "digamma": return Convert(0x03DD);
                case "disin": return Convert(0x22F2);
                case "div": return Convert(0x00F7);
                case "divide": return Convert(0x00F7);
                case "divideontimes": return Convert(0x22C7);
                case "divonx": return Convert(0x22C7);
                case "DJcy": return Convert(0x0402);
                case "djcy": return Convert(0x0452);
                case "dlcorn": return Convert(0x231E);
                case "dlcrop": return Convert(0x230D);
                case "dollar": return Convert(0x0024);
                case "Dopf": return Convert(0xD835, 0xDD3B);
                case "dopf": return Convert(0xD835, 0xDD55);
                case "Dot": return Convert(0x00A8);
                case "dot": return Convert(0x02D9);
                case "DotDot": return Convert(0x20DC);
                case "doteq": return Convert(0x2250);
                case "doteqdot": return Convert(0x2251);
                case "DotEqual": return Convert(0x2250);
                case "dotminus": return Convert(0x2238);
                case "dotplus": return Convert(0x2214);
                case "dotsquare": return Convert(0x22A1);
                case "doublebarwedge": return Convert(0x2306);
                case "DoubleContourIntegral": return Convert(0x222F);
                case "DoubleDot": return Convert(0x00A8);
                case "DoubleDownArrow": return Convert(0x21D3);
                case "DoubleLeftArrow": return Convert(0x21D0);
                case "DoubleLeftRightArrow": return Convert(0x21D4);
                case "DoubleLeftTee": return Convert(0x2AE4);
                case "DoubleLongLeftArrow": return Convert(0x27F8);
                case "DoubleLongLeftRightArrow": return Convert(0x27FA);
                case "DoubleLongRightArrow": return Convert(0x27F9);
                case "DoubleRightArrow": return Convert(0x21D2);
                case "DoubleRightTee": return Convert(0x22A8);
                case "DoubleUpArrow": return Convert(0x21D1);
                case "DoubleUpDownArrow": return Convert(0x21D5);
                case "DoubleVerticalBar": return Convert(0x2225);
                case "DownArrow": return Convert(0x2193);
                case "Downarrow": return Convert(0x21D3);
                case "downarrow": return Convert(0x2193);
                case "DownArrowBar": return Convert(0x2913);
                case "DownArrowUpArrow": return Convert(0x21F5);
                case "DownBreve": return Convert(0x0311);
                case "downdownarrows": return Convert(0x21CA);
                case "downharpoonleft": return Convert(0x21C3);
                case "downharpoonright": return Convert(0x21C2);
                case "DownLeftRightVector": return Convert(0x2950);
                case "DownLeftTeeVector": return Convert(0x295E);
                case "DownLeftVector": return Convert(0x21BD);
                case "DownLeftVectorBar": return Convert(0x2956);
                case "DownRightTeeVector": return Convert(0x295F);
                case "DownRightVector": return Convert(0x21C1);
                case "DownRightVectorBar": return Convert(0x2957);
                case "DownTee": return Convert(0x22A4);
                case "DownTeeArrow": return Convert(0x21A7);
                case "drbkarow": return Convert(0x2910);
                case "drcorn": return Convert(0x231F);
                case "drcrop": return Convert(0x230C);
                case "Dscr": return Convert(0xD835, 0xDC9F);
                case "dscr": return Convert(0xD835, 0xDCB9);
                case "DScy": return Convert(0x0405);
                case "dscy": return Convert(0x0455);
                case "dsol": return Convert(0x29F6);
                case "Dstrok": return Convert(0x0110);
                case "dstrok": return Convert(0x0111);
                case "dtdot": return Convert(0x22F1);
                case "dtri": return Convert(0x25BF);
                case "dtrif": return Convert(0x25BE);
                case "duarr": return Convert(0x21F5);
                case "duhar": return Convert(0x296F);
                case "dwangle": return Convert(0x29A6);
                case "DZcy": return Convert(0x040F);
                case "dzcy": return Convert(0x045F);
                case "dzigrarr": return Convert(0x27FF);
            }

            return null;
        }

        static char[] GetSymbolE(string name)
        {
            switch (name)
            {
                case "Eacute": return Convert(0x00C9);
                case "eacute": return Convert(0x00E9);
                case "easter": return Convert(0x2A6E);
                case "Ecaron": return Convert(0x011A);
                case "ecaron": return Convert(0x011B);
                case "ecir": return Convert(0x2256);
                case "Ecirc": return Convert(0x00CA);
                case "ecirc": return Convert(0x00EA);
                case "ecolon": return Convert(0x2255);
                case "Ecy": return Convert(0x042D);
                case "ecy": return Convert(0x044D);
                case "eDDot": return Convert(0x2A77);
                case "Edot": return Convert(0x0116);
                case "eDot": return Convert(0x2251);
                case "edot": return Convert(0x0117);
                case "ee": return Convert(0x2147);
                case "efDot": return Convert(0x2252);
                case "Efr": return Convert(0xD835, 0xDD08);
                case "efr": return Convert(0xD835, 0xDD22);
                case "eg": return Convert(0x2A9A);
                case "Egrave": return Convert(0x00C8);
                case "egrave": return Convert(0x00E8);
                case "egs": return Convert(0x2A96);
                case "egsdot": return Convert(0x2A98);
                case "el": return Convert(0x2A99);
                case "Element": return Convert(0x2208);
                case "elinters": return Convert(0x23E7);
                case "ell": return Convert(0x2113);
                case "els": return Convert(0x2A95);
                case "elsdot": return Convert(0x2A97);
                case "Emacr": return Convert(0x0112);
                case "emacr": return Convert(0x0113);
                case "empty": return Convert(0x2205);
                case "emptyset": return Convert(0x2205);
                case "EmptySmallSquare": return Convert(0x25FB);
                case "emptyv": return Convert(0x2205);
                case "EmptyVerySmallSquare": return Convert(0x25AB);
                case "emsp": return Convert(0x2003);
                case "emsp13": return Convert(0x2004);
                case "emsp14": return Convert(0x2005);
                case "ENG": return Convert(0x014A);
                case "eng": return Convert(0x014B);
                case "ensp": return Convert(0x2002);
                case "Eogon": return Convert(0x0118);
                case "eogon": return Convert(0x0119);
                case "Eopf": return Convert(0xD835, 0xDD3C);
                case "eopf": return Convert(0xD835, 0xDD56);
                case "epar": return Convert(0x22D5);
                case "eparsl": return Convert(0x29E3);
                case "eplus": return Convert(0x2A71);
                case "epsi": return Convert(0x03B5);
                case "Epsilon": return Convert(0x0395);
                case "epsilon": return Convert(0x03B5);
                case "epsiv": return Convert(0x03F5);
                case "eqcirc": return Convert(0x2256);
                case "eqcolon": return Convert(0x2255);
                case "eqsim": return Convert(0x2242);
                case "eqslantgtr": return Convert(0x2A96);
                case "eqslantless": return Convert(0x2A95);
                case "Equal": return Convert(0x2A75);
                case "equals": return Convert(0x003D);
                case "EqualTilde": return Convert(0x2242);
                case "equest": return Convert(0x225F);
                case "Equilibrium": return Convert(0x21CC);
                case "equiv": return Convert(0x2261);
                case "equivDD": return Convert(0x2A78);
                case "eqvparsl": return Convert(0x29E5);
                case "erarr": return Convert(0x2971);
                case "erDot": return Convert(0x2253);
                case "Escr": return Convert(0x2130);
                case "escr": return Convert(0x212F);
                case "esdot": return Convert(0x2250);
                case "Esim": return Convert(0x2A73);
                case "esim": return Convert(0x2242);
                case "Eta": return Convert(0x0397);
                case "eta": return Convert(0x03B7);
                case "ETH": return Convert(0x00D0);
                case "eth": return Convert(0x00F0);
                case "Euml": return Convert(0x00CB);
                case "euml": return Convert(0x00EB);
                case "euro": return Convert(0x20AC);
                case "excl": return Convert(0x0021);
                case "exist": return Convert(0x2203);
                case "Exists": return Convert(0x2203);
                case "expectation": return Convert(0x2130);
                case "ExponentialE": return Convert(0x2147);
                case "exponentiale": return Convert(0x2147);
            }

            return null;
        }

        static char[] GetSymbolF(string name)
        {
            switch (name)
            {
                case "fallingdotseq": return Convert(0x2252);
                case "Fcy": return Convert(0x0424);
                case "fcy": return Convert(0x0444);
                case "female": return Convert(0x2640);
                case "ffilig": return Convert(0xFB03);
                case "fflig": return Convert(0xFB00);
                case "ffllig": return Convert(0xFB04);
                case "Ffr": return Convert(0xD835, 0xDD09);
                case "ffr": return Convert(0xD835, 0xDD23);
                case "filig": return Convert(0xFB01);
                case "FilledSmallSquare": return Convert(0x25FC);
                case "FilledVerySmallSquare": return Convert(0x25AA);
                case "fjlig": return Convert(0x0066, 0x006A);
                case "flat": return Convert(0x266D);
                case "fllig": return Convert(0xFB02);
                case "fltns": return Convert(0x25B1);
                case "fnof": return Convert(0x0192);
                case "Fopf": return Convert(0xD835, 0xDD3D);
                case "fopf": return Convert(0xD835, 0xDD57);
                case "ForAll": return Convert(0x2200);
                case "forall": return Convert(0x2200);
                case "fork": return Convert(0x22D4);
                case "forkv": return Convert(0x2AD9);
                case "Fouriertrf": return Convert(0x2131);
                case "fpartint": return Convert(0x2A0D);
                case "frac12": return Convert(0x00BD);
                case "frac13": return Convert(0x2153);
                case "frac14": return Convert(0x00BC);
                case "frac15": return Convert(0x2155);
                case "frac16": return Convert(0x2159);
                case "frac18": return Convert(0x215B);
                case "frac23": return Convert(0x2154);
                case "frac25": return Convert(0x2156);
                case "frac34": return Convert(0x00BE);
                case "frac35": return Convert(0x2157);
                case "frac38": return Convert(0x215C);
                case "frac45": return Convert(0x2158);
                case "frac56": return Convert(0x215A);
                case "frac58": return Convert(0x215D);
                case "frac78": return Convert(0x215E);
                case "frasl": return Convert(0x2044);
                case "frown": return Convert(0x2322);
                case "Fscr": return Convert(0x2131);
                case "fscr": return Convert(0xD835, 0xDCBB);
            }

            return null;
        }

        static char[] GetSymbolG(string name)
        {
            switch (name)
            {
                case "gacute": return Convert(0x01F5);
                case "Gamma": return Convert(0x0393);
                case "gamma": return Convert(0x03B3);
                case "Gammad": return Convert(0x03DC);
                case "gammad": return Convert(0x03DD);
                case "gap": return Convert(0x2A86);
                case "Gbreve": return Convert(0x011E);
                case "gbreve": return Convert(0x011F);
                case "Gcedil": return Convert(0x0122);
                case "Gcirc": return Convert(0x011C);
                case "gcirc": return Convert(0x011D);
                case "Gcy": return Convert(0x0413);
                case "gcy": return Convert(0x0433);
                case "Gdot": return Convert(0x0120);
                case "gdot": return Convert(0x0121);
                case "gE": return Convert(0x2267);
                case "ge": return Convert(0x2265);
                case "gEl": return Convert(0x2A8C);
                case "gel": return Convert(0x22DB);
                case "geq": return Convert(0x2265);
                case "geqq": return Convert(0x2267);
                case "geqslant": return Convert(0x2A7E);
                case "ges": return Convert(0x2A7E);
                case "gescc": return Convert(0x2AA9);
                case "gesdot": return Convert(0x2A80);
                case "gesdoto": return Convert(0x2A82);
                case "gesdotol": return Convert(0x2A84);
                case "gesl": return Convert(0x22DB, 0xFE00);
                case "gesles": return Convert(0x2A94);
                case "Gfr": return Convert(0xD835, 0xDD0A);
                case "gfr": return Convert(0xD835, 0xDD24);
                case "Gg": return Convert(0x22D9);
                case "gg": return Convert(0x226B);
                case "ggg": return Convert(0x22D9);
                case "gimel": return Convert(0x2137);
                case "GJcy": return Convert(0x0403);
                case "gjcy": return Convert(0x0453);
                case "gl": return Convert(0x2277);
                case "gla": return Convert(0x2AA5);
                case "glE": return Convert(0x2A92);
                case "glj": return Convert(0x2AA4);
                case "gnap": return Convert(0x2A8A);
                case "gnapprox": return Convert(0x2A8A);
                case "gnE": return Convert(0x2269);
                case "gne": return Convert(0x2A88);
                case "gneq": return Convert(0x2A88);
                case "gneqq": return Convert(0x2269);
                case "gnsim": return Convert(0x22E7);
                case "Gopf": return Convert(0xD835, 0xDD3E);
                case "gopf": return Convert(0xD835, 0xDD58);
                case "grave": return Convert(0x0060);
                case "GreaterEqual": return Convert(0x2265);
                case "GreaterEqualLess": return Convert(0x22DB);
                case "GreaterFullEqual": return Convert(0x2267);
                case "GreaterGreater": return Convert(0x2AA2);
                case "GreaterLess": return Convert(0x2277);
                case "GreaterSlantEqual": return Convert(0x2A7E);
                case "GreaterTilde": return Convert(0x2273);
                case "Gscr": return Convert(0xD835, 0xDCA2);
                case "gscr": return Convert(0x210A);
                case "gsim": return Convert(0x2273);
                case "gsime": return Convert(0x2A8E);
                case "gsiml": return Convert(0x2A90);
                case "GT": return Convert(0x003E);
                case "Gt": return Convert(0x226B);
                case "gt": return Convert(0x003E);
                case "gtcc": return Convert(0x2AA7);
                case "gtcir": return Convert(0x2A7A);
                case "gtdot": return Convert(0x22D7);
                case "gtlPar": return Convert(0x2995);
                case "gtquest": return Convert(0x2A7C);
                case "gtrapprox": return Convert(0x2A86);
                case "gtrarr": return Convert(0x2978);
                case "gtrdot": return Convert(0x22D7);
                case "gtreqless": return Convert(0x22DB);
                case "gtreqqless": return Convert(0x2A8C);
                case "gtrless": return Convert(0x2277);
                case "gtrsim": return Convert(0x2273);
                case "gvertneqq": return Convert(0x2269, 0xFE00);
                case "gvnE": return Convert(0x2269, 0xFE00);
            }

            return null;
        }

        static char[] GetSymbolH(string name)
        {
            switch (name)
            {
                case "Hacek": return Convert(0x02C7);
                case "hairsp": return Convert(0x200A);
                case "half": return Convert(0x00BD);
                case "hamilt": return Convert(0x210B);
                case "HARDcy": return Convert(0x042A);
                case "hardcy": return Convert(0x044A);
                case "hArr": return Convert(0x21D4);
                case "harr": return Convert(0x2194);
                case "harrcir": return Convert(0x2948);
                case "harrw": return Convert(0x21AD);
                case "Hat": return Convert(0x005E);
                case "hbar": return Convert(0x210F);
                case "Hcirc": return Convert(0x0124);
                case "hcirc": return Convert(0x0125);
                case "hearts": return Convert(0x2665);
                case "heartsuit": return Convert(0x2665);
                case "hellip": return Convert(0x2026);
                case "hercon": return Convert(0x22B9);
                case "Hfr": return Convert(0x210C);
                case "hfr": return Convert(0xD835, 0xDD25);
                case "HilbertSpace": return Convert(0x210B);
                case "hksearow": return Convert(0x2925);
                case "hkswarow": return Convert(0x2926);
                case "hoarr": return Convert(0x21FF);
                case "homtht": return Convert(0x223B);
                case "hookleftarrow": return Convert(0x21A9);
                case "hookrightarrow": return Convert(0x21AA);
                case "Hopf": return Convert(0x210D);
                case "hopf": return Convert(0xD835, 0xDD59);
                case "horbar": return Convert(0x2015);
                case "HorizontalLine": return Convert(0x2500);
                case "Hscr": return Convert(0x210B);
                case "hscr": return Convert(0xD835, 0xDCBD);
                case "hslash": return Convert(0x210F);
                case "Hstrok": return Convert(0x0126);
                case "hstrok": return Convert(0x0127);
                case "HumpDownHump": return Convert(0x224E);
                case "HumpEqual": return Convert(0x224F);
                case "hybull": return Convert(0x2043);
                case "hyphen": return Convert(0x2010);
            }

            return null;
        }

        static char[] GetSymbolI(string name)
        {
            switch (name)
            {
                case "Iacute": return Convert(0x00CD);
                case "iacute": return Convert(0x00ED);
                case "ic": return Convert(0x2063);
                case "Icirc": return Convert(0x00CE);
                case "icirc": return Convert(0x00EE);
                case "Icy": return Convert(0x0418);
                case "icy": return Convert(0x0438);
                case "Idot": return Convert(0x0130);
                case "IEcy": return Convert(0x0415);
                case "iecy": return Convert(0x0435);
                case "iexcl": return Convert(0x00A1);
                case "iff": return Convert(0x21D4);
                case "Ifr": return Convert(0x2111);
                case "ifr": return Convert(0xD835, 0xDD26);
                case "Igrave": return Convert(0x00CC);
                case "igrave": return Convert(0x00EC);
                case "ii": return Convert(0x2148);
                case "iiiint": return Convert(0x2A0C);
                case "iiint": return Convert(0x222D);
                case "iinfin": return Convert(0x29DC);
                case "iiota": return Convert(0x2129);
                case "IJlig": return Convert(0x0132);
                case "ijlig": return Convert(0x0133);
                case "Im": return Convert(0x2111);
                case "Imacr": return Convert(0x012A);
                case "imacr": return Convert(0x012B);
                case "image": return Convert(0x2111);
                case "ImaginaryI": return Convert(0x2148);
                case "imagline": return Convert(0x2110);
                case "imagpart": return Convert(0x2111);
                case "imath": return Convert(0x0131);
                case "imof": return Convert(0x22B7);
                case "imped": return Convert(0x01B5);
                case "Implies": return Convert(0x21D2);
                case "in": return Convert(0x2208);
                case "incare": return Convert(0x2105);
                case "infin": return Convert(0x221E);
                case "infintie": return Convert(0x29DD);
                case "inodot": return Convert(0x0131);
                case "Int": return Convert(0x222C);
                case "int": return Convert(0x222B);
                case "intcal": return Convert(0x22BA);
                case "integers": return Convert(0x2124);
                case "Integral": return Convert(0x222B);
                case "intercal": return Convert(0x22BA);
                case "Intersection": return Convert(0x22C2);
                case "intlarhk": return Convert(0x2A17);
                case "intprod": return Convert(0x2A3C);
                case "InvisibleComma": return Convert(0x2063);
                case "InvisibleTimes": return Convert(0x2062);
                case "IOcy": return Convert(0x0401);
                case "iocy": return Convert(0x0451);
                case "Iogon": return Convert(0x012E);
                case "iogon": return Convert(0x012F);
                case "Iopf": return Convert(0xD835, 0xDD40);
                case "iopf": return Convert(0xD835, 0xDD5A);
                case "Iota": return Convert(0x0399);
                case "iota": return Convert(0x03B9);
                case "iprod": return Convert(0x2A3C);
                case "iquest": return Convert(0x00BF);
                case "Iscr": return Convert(0x2110);
                case "iscr": return Convert(0xD835, 0xDCBE);
                case "isin": return Convert(0x2208);
                case "isindot": return Convert(0x22F5);
                case "isinE": return Convert(0x22F9);
                case "isins": return Convert(0x22F4);
                case "isinsv": return Convert(0x22F3);
                case "isinv": return Convert(0x2208);
                case "it": return Convert(0x2062);
                case "Itilde": return Convert(0x0128);
                case "itilde": return Convert(0x0129);
                case "Iukcy": return Convert(0x0406);
                case "iukcy": return Convert(0x0456);
                case "Iuml": return Convert(0x00CF);
                case "iuml": return Convert(0x00EF);
            }

            return null;
        }

        static char[] GetSymbolJ(string name)
        {
            switch (name)
            {
                case "Jcirc": return Convert(0x0134);
                case "jcirc": return Convert(0x0135);
                case "Jcy": return Convert(0x0419);
                case "jcy": return Convert(0x0439);
                case "Jfr": return Convert(0xD835, 0xDD0D);
                case "jfr": return Convert(0xD835, 0xDD27);
                case "jmath": return Convert(0x0237);
                case "Jopf": return Convert(0xD835, 0xDD41);
                case "jopf": return Convert(0xD835, 0xDD5B);
                case "Jscr": return Convert(0xD835, 0xDCA5);
                case "jscr": return Convert(0xD835, 0xDCBF);
                case "Jsercy": return Convert(0x0408);
                case "jsercy": return Convert(0x0458);
                case "Jukcy": return Convert(0x0404);
                case "jukcy": return Convert(0x0454);
            }

            return null;
        }

        static char[] GetSymbolK(string name)
        {
            switch (name)
            {
                case "Kappa": return Convert(0x039A);
                case "kappa": return Convert(0x03BA);
                case "kappav": return Convert(0x03F0);
                case "Kcedil": return Convert(0x0136);
                case "kcedil": return Convert(0x0137);
                case "Kcy": return Convert(0x041A);
                case "kcy": return Convert(0x043A);
                case "Kfr": return Convert(0xD835, 0xDD0E);
                case "kfr": return Convert(0xD835, 0xDD28);
                case "kgreen": return Convert(0x0138);
                case "KHcy": return Convert(0x0425);
                case "khcy": return Convert(0x0445);
                case "KJcy": return Convert(0x040C);
                case "kjcy": return Convert(0x045C);
                case "Kopf": return Convert(0xD835, 0xDD42);
                case "kopf": return Convert(0xD835, 0xDD5C);
                case "Kscr": return Convert(0xD835, 0xDCA6);
                case "kscr": return Convert(0xD835, 0xDCC0);
            }

            return null;
        }

        static char[] GetSymbolL(string name)
        {
            switch (name)
            {
                case "lAarr": return Convert(0x21DA);
                case "Lacute": return Convert(0x0139);
                case "lacute": return Convert(0x013A);
                case "laemptyv": return Convert(0x29B4);
                case "lagran": return Convert(0x2112);
                case "Lambda": return Convert(0x039B);
                case "lambda": return Convert(0x03BB);
                case "Lang": return Convert(0x27EA);
                case "lang": return Convert(0x27E8);
                case "langd": return Convert(0x2991);
                case "langle": return Convert(0x27E8);
                case "lap": return Convert(0x2A85);
                case "Laplacetrf": return Convert(0x2112);
                case "laquo": return Convert(0x00AB);
                case "Larr": return Convert(0x219E);
                case "lArr": return Convert(0x21D0);
                case "larr": return Convert(0x2190);
                case "larrb": return Convert(0x21E4);
                case "larrbfs": return Convert(0x291F);
                case "larrfs": return Convert(0x291D);
                case "larrhk": return Convert(0x21A9);
                case "larrlp": return Convert(0x21AB);
                case "larrpl": return Convert(0x2939);
                case "larrsim": return Convert(0x2973);
                case "larrtl": return Convert(0x21A2);
                case "lat": return Convert(0x2AAB);
                case "lAtail": return Convert(0x291B);
                case "latail": return Convert(0x2919);
                case "late": return Convert(0x2AAD);
                case "lates": return Convert(0x2AAD, 0xFE00);
                case "lBarr": return Convert(0x290E);
                case "lbarr": return Convert(0x290C);
                case "lbbrk": return Convert(0x2772);
                case "lbrace": return Convert(0x007B);
                case "lbrack": return Convert(0x005B);
                case "lbrke": return Convert(0x298B);
                case "lbrksld": return Convert(0x298F);
                case "lbrkslu": return Convert(0x298D);
                case "Lcaron": return Convert(0x013D);
                case "lcaron": return Convert(0x013E);
                case "Lcedil": return Convert(0x013B);
                case "lcedil": return Convert(0x013C);
                case "lceil": return Convert(0x2308);
                case "lcub": return Convert(0x007B);
                case "Lcy": return Convert(0x041B);
                case "lcy": return Convert(0x043B);
                case "ldca": return Convert(0x2936);
                case "ldquo": return Convert(0x201C);
                case "ldquor": return Convert(0x201E);
                case "ldrdhar": return Convert(0x2967);
                case "ldrushar": return Convert(0x294B);
                case "ldsh": return Convert(0x21B2);
                case "lE": return Convert(0x2266);
                case "le": return Convert(0x2264);
                case "LeftAngleBracket": return Convert(0x27E8);
                case "LeftArrow": return Convert(0x2190);
                case "Leftarrow": return Convert(0x21D0);
                case "leftarrow": return Convert(0x2190);
                case "LeftArrowBar": return Convert(0x21E4);
                case "LeftArrowRightArrow": return Convert(0x21C6);
                case "leftarrowtail": return Convert(0x21A2);
                case "LeftCeiling": return Convert(0x2308);
                case "LeftDoubleBracket": return Convert(0x27E6);
                case "LeftDownTeeVector": return Convert(0x2961);
                case "LeftDownVector": return Convert(0x21C3);
                case "LeftDownVectorBar": return Convert(0x2959);
                case "LeftFloor": return Convert(0x230A);
                case "leftharpoondown": return Convert(0x21BD);
                case "leftharpoonup": return Convert(0x21BC);
                case "leftleftarrows": return Convert(0x21C7);
                case "LeftRightArrow": return Convert(0x2194);
                case "Leftrightarrow": return Convert(0x21D4);
                case "leftrightarrow": return Convert(0x2194);
                case "leftrightarrows": return Convert(0x21C6);
                case "leftrightharpoons": return Convert(0x21CB);
                case "leftrightsquigarrow": return Convert(0x21AD);
                case "LeftRightVector": return Convert(0x294E);
                case "LeftTee": return Convert(0x22A3);
                case "LeftTeeArrow": return Convert(0x21A4);
                case "LeftTeeVector": return Convert(0x295A);
                case "leftthreetimes": return Convert(0x22CB);
                case "LeftTriangle": return Convert(0x22B2);
                case "LeftTriangleBar": return Convert(0x29CF);
                case "LeftTriangleEqual": return Convert(0x22B4);
                case "LeftUpDownVector": return Convert(0x2951);
                case "LeftUpTeeVector": return Convert(0x2960);
                case "LeftUpVector": return Convert(0x21BF);
                case "LeftUpVectorBar": return Convert(0x2958);
                case "LeftVector": return Convert(0x21BC);
                case "LeftVectorBar": return Convert(0x2952);
                case "lEg": return Convert(0x2A8B);
                case "leg": return Convert(0x22DA);
                case "leq": return Convert(0x2264);
                case "leqq": return Convert(0x2266);
                case "leqslant": return Convert(0x2A7D);
                case "les": return Convert(0x2A7D);
                case "lescc": return Convert(0x2AA8);
                case "lesdot": return Convert(0x2A7F);
                case "lesdoto": return Convert(0x2A81);
                case "lesdotor": return Convert(0x2A83);
                case "lesg": return Convert(0x22DA, 0xFE00);
                case "lesges": return Convert(0x2A93);
                case "lessapprox": return Convert(0x2A85);
                case "lessdot": return Convert(0x22D6);
                case "lesseqgtr": return Convert(0x22DA);
                case "lesseqqgtr": return Convert(0x2A8B);
                case "LessEqualGreater": return Convert(0x22DA);
                case "LessFullEqual": return Convert(0x2266);
                case "LessGreater": return Convert(0x2276);
                case "lessgtr": return Convert(0x2276);
                case "LessLess": return Convert(0x2AA1);
                case "lesssim": return Convert(0x2272);
                case "LessSlantEqual": return Convert(0x2A7D);
                case "LessTilde": return Convert(0x2272);
                case "lfisht": return Convert(0x297C);
                case "lfloor": return Convert(0x230A);
                case "Lfr": return Convert(0xD835, 0xDD0F);
                case "lfr": return Convert(0xD835, 0xDD29);
                case "lg": return Convert(0x2276);
                case "lgE": return Convert(0x2A91);
                case "lHar": return Convert(0x2962);
                case "lhard": return Convert(0x21BD);
                case "lharu": return Convert(0x21BC);
                case "lharul": return Convert(0x296A);
                case "lhblk": return Convert(0x2584);
                case "LJcy": return Convert(0x0409);
                case "ljcy": return Convert(0x0459);
                case "Ll": return Convert(0x22D8);
                case "ll": return Convert(0x226A);
                case "llarr": return Convert(0x21C7);
                case "llcorner": return Convert(0x231E);
                case "Lleftarrow": return Convert(0x21DA);
                case "llhard": return Convert(0x296B);
                case "lltri": return Convert(0x25FA);
                case "Lmidot": return Convert(0x013F);
                case "lmidot": return Convert(0x0140);
                case "lmoust": return Convert(0x23B0);
                case "lmoustache": return Convert(0x23B0);
                case "lnap": return Convert(0x2A89);
                case "lnapprox": return Convert(0x2A89);
                case "lnE": return Convert(0x2268);
                case "lne": return Convert(0x2A87);
                case "lneq": return Convert(0x2A87);
                case "lneqq": return Convert(0x2268);
                case "lnsim": return Convert(0x22E6);
                case "loang": return Convert(0x27EC);
                case "loarr": return Convert(0x21FD);
                case "lobrk": return Convert(0x27E6);
                case "LongLeftArrow": return Convert(0x27F5);
                case "Longleftarrow": return Convert(0x27F8);
                case "longleftarrow": return Convert(0x27F5);
                case "LongLeftRightArrow": return Convert(0x27F7);
                case "Longleftrightarrow": return Convert(0x27FA);
                case "longleftrightarrow": return Convert(0x27F7);
                case "longmapsto": return Convert(0x27FC);
                case "LongRightArrow": return Convert(0x27F6);
                case "Longrightarrow": return Convert(0x27F9);
                case "longrightarrow": return Convert(0x27F6);
                case "looparrowleft": return Convert(0x21AB);
                case "looparrowright": return Convert(0x21AC);
                case "lopar": return Convert(0x2985);
                case "Lopf": return Convert(0xD835, 0xDD43);
                case "lopf": return Convert(0xD835, 0xDD5D);
                case "loplus": return Convert(0x2A2D);
                case "lotimes": return Convert(0x2A34);
                case "lowast": return Convert(0x2217);
                case "lowbar": return Convert(0x005F);
                case "LowerLeftArrow": return Convert(0x2199);
                case "LowerRightArrow": return Convert(0x2198);
                case "loz": return Convert(0x25CA);
                case "lozenge": return Convert(0x25CA);
                case "lozf": return Convert(0x29EB);
                case "lpar": return Convert(0x0028);
                case "lparlt": return Convert(0x2993);
                case "lrarr": return Convert(0x21C6);
                case "lrcorner": return Convert(0x231F);
                case "lrhar": return Convert(0x21CB);
                case "lrhard": return Convert(0x296D);
                case "lrm": return Convert(0x200E);
                case "lrtri": return Convert(0x22BF);
                case "lsaquo": return Convert(0x2039);
                case "Lscr": return Convert(0x2112);
                case "lscr": return Convert(0xD835, 0xDCC1);
                case "Lsh": return Convert(0x21B0);
                case "lsh": return Convert(0x21B0);
                case "lsim": return Convert(0x2272);
                case "lsime": return Convert(0x2A8D);
                case "lsimg": return Convert(0x2A8F);
                case "lsqb": return Convert(0x005B);
                case "lsquo": return Convert(0x2018);
                case "lsquor": return Convert(0x201A);
                case "Lstrok": return Convert(0x0141);
                case "lstrok": return Convert(0x0142);
                case "LT": return Convert(0x003C);
                case "Lt": return Convert(0x226A);
                case "lt": return Convert(0x003C);
                case "ltcc": return Convert(0x2AA6);
                case "ltcir": return Convert(0x2A79);
                case "ltdot": return Convert(0x22D6);
                case "lthree": return Convert(0x22CB);
                case "ltimes": return Convert(0x22C9);
                case "ltlarr": return Convert(0x2976);
                case "ltquest": return Convert(0x2A7B);
                case "ltri": return Convert(0x25C3);
                case "ltrie": return Convert(0x22B4);
                case "ltrif": return Convert(0x25C2);
                case "ltrPar": return Convert(0x2996);
                case "lurdshar": return Convert(0x294A);
                case "luruhar": return Convert(0x2966);
                case "lvertneqq": return Convert(0x2268, 0xFE00);
                case "lvnE": return Convert(0x2268, 0xFE00);
            }

            return null;
        }

        static char[] GetSymbolM(string name)
        {
            switch (name)
            {
                case "macr": return Convert(0x00AF);
                case "male": return Convert(0x2642);
                case "malt": return Convert(0x2720);
                case "maltese": return Convert(0x2720);
                case "Map": return Convert(0x2905);
                case "map": return Convert(0x21A6);
                case "mapsto": return Convert(0x21A6);
                case "mapstodown": return Convert(0x21A7);
                case "mapstoleft": return Convert(0x21A4);
                case "mapstoup": return Convert(0x21A5);
                case "marker": return Convert(0x25AE);
                case "mcomma": return Convert(0x2A29);
                case "Mcy": return Convert(0x041C);
                case "mcy": return Convert(0x043C);
                case "mdash": return Convert(0x2014);
                case "mDDot": return Convert(0x223A);
                case "measuredangle": return Convert(0x2221);
                case "MediumSpace": return Convert(0x205F);
                case "Mellintrf": return Convert(0x2133);
                case "Mfr": return Convert(0xD835, 0xDD10);
                case "mfr": return Convert(0xD835, 0xDD2A);
                case "mho": return Convert(0x2127);
                case "micro": return Convert(0x00B5);
                case "mid": return Convert(0x2223);
                case "midast": return Convert(0x002A);
                case "midcir": return Convert(0x2AF0);
                case "middot": return Convert(0x00B7);
                case "minus": return Convert(0x2212);
                case "minusb": return Convert(0x229F);
                case "minusd": return Convert(0x2238);
                case "minusdu": return Convert(0x2A2A);
                case "MinusPlus": return Convert(0x2213);
                case "mlcp": return Convert(0x2ADB);
                case "mldr": return Convert(0x2026);
                case "mnplus": return Convert(0x2213);
                case "models": return Convert(0x22A7);
                case "Mopf": return Convert(0xD835, 0xDD44);
                case "mopf": return Convert(0xD835, 0xDD5E);
                case "mp": return Convert(0x2213);
                case "Mscr": return Convert(0x2133);
                case "mscr": return Convert(0xD835, 0xDCC2);
                case "mstpos": return Convert(0x223E);
                case "Mu": return Convert(0x039C);
                case "mu": return Convert(0x03BC);
                case "multimap": return Convert(0x22B8);
                case "mumap": return Convert(0x22B8);
            }

            return null;
        }

        static char[] GetSymbolN(string name)
        {
            switch (name)
            {
                case "nabla": return Convert(0x2207);
                case "Nacute": return Convert(0x0143);
                case "nacute": return Convert(0x0144);
                case "nang": return Convert(0x2220, 0x20D2);
                case "nap": return Convert(0x2249);
                case "napE": return Convert(0x2A70, 0x0338);
                case "napid": return Convert(0x224B, 0x0338);
                case "napos": return Convert(0x0149);
                case "napprox": return Convert(0x2249);
                case "natur": return Convert(0x266E);
                case "natural": return Convert(0x266E);
                case "naturals": return Convert(0x2115);
                case "nbsp": return Convert(0x00A0);
                case "nbump": return Convert(0x224E, 0x0338);
                case "nbumpe": return Convert(0x224F, 0x0338);
                case "ncap": return Convert(0x2A43);
                case "Ncaron": return Convert(0x0147);
                case "ncaron": return Convert(0x0148);
                case "Ncedil": return Convert(0x0145);
                case "ncedil": return Convert(0x0146);
                case "ncong": return Convert(0x2247);
                case "ncongdot": return Convert(0x2A6D, 0x0338);
                case "ncup": return Convert(0x2A42);
                case "Ncy": return Convert(0x041D);
                case "ncy": return Convert(0x043D);
                case "ndash": return Convert(0x2013);
                case "ne": return Convert(0x2260);
                case "nearhk": return Convert(0x2924);
                case "neArr": return Convert(0x21D7);
                case "nearr": return Convert(0x2197);
                case "nearrow": return Convert(0x2197);
                case "nedot": return Convert(0x2250, 0x0338);
                case "NegativeMediumSpace": return Convert(0x200B);
                case "NegativeThickSpace": return Convert(0x200B);
                case "NegativeThinSpace": return Convert(0x200B);
                case "NegativeVeryThinSpace": return Convert(0x200B);
                case "nequiv": return Convert(0x2262);
                case "nesear": return Convert(0x2928);
                case "nesim": return Convert(0x2242, 0x0338);
                case "NestedGreaterGreater": return Convert(0x226B);
                case "NestedLessLess": return Convert(0x226A);
                case "NewLine": return Convert(0x000A);
                case "nexist": return Convert(0x2204);
                case "nexists": return Convert(0x2204);
                case "Nfr": return Convert(0xD835, 0xDD11);
                case "nfr": return Convert(0xD835, 0xDD2B);
                case "ngE": return Convert(0x2267, 0x0338);
                case "nge": return Convert(0x2271);
                case "ngeq": return Convert(0x2271);
                case "ngeqq": return Convert(0x2267, 0x0338);
                case "ngeqslant": return Convert(0x2A7E, 0x0338);
                case "nges": return Convert(0x2A7E, 0x0338);
                case "nGg": return Convert(0x22D9, 0x0338);
                case "ngsim": return Convert(0x2275);
                case "nGt": return Convert(0x226B, 0x20D2);
                case "ngt": return Convert(0x226F);
                case "ngtr": return Convert(0x226F);
                case "nGtv": return Convert(0x226B, 0x0338);
                case "nhArr": return Convert(0x21CE);
                case "nharr": return Convert(0x21AE);
                case "nhpar": return Convert(0x2AF2);
                case "ni": return Convert(0x220B);
                case "nis": return Convert(0x22FC);
                case "nisd": return Convert(0x22FA);
                case "niv": return Convert(0x220B);
                case "NJcy": return Convert(0x040A);
                case "njcy": return Convert(0x045A);
                case "nlArr": return Convert(0x21CD);
                case "nlarr": return Convert(0x219A);
                case "nldr": return Convert(0x2025);
                case "nlE": return Convert(0x2266, 0x0338);
                case "nle": return Convert(0x2270);
                case "nLeftarrow": return Convert(0x21CD);
                case "nleftarrow": return Convert(0x219A);
                case "nLeftrightarrow": return Convert(0x21CE);
                case "nleftrightarrow": return Convert(0x21AE);
                case "nleq": return Convert(0x2270);
                case "nleqq": return Convert(0x2266, 0x0338);
                case "nleqslant": return Convert(0x2A7D, 0x0338);
                case "nles": return Convert(0x2A7D, 0x0338);
                case "nless": return Convert(0x226E);
                case "nLl": return Convert(0x22D8, 0x0338);
                case "nlsim": return Convert(0x2274);
                case "nLt": return Convert(0x226A, 0x20D2);
                case "nlt": return Convert(0x226E);
                case "nltri": return Convert(0x22EA);
                case "nltrie": return Convert(0x22EC);
                case "nLtv": return Convert(0x226A, 0x0338);
                case "nmid": return Convert(0x2224);
                case "NoBreak": return Convert(0x2060);
                case "NonBreakingSpace": return Convert(0x00A0);
                case "Nopf": return Convert(0x2115);
                case "nopf": return Convert(0xD835, 0xDD5F);
                case "Not": return Convert(0x2AEC);
                case "not": return Convert(0x00AC);
                case "NotCongruent": return Convert(0x2262);
                case "NotCupCap": return Convert(0x226D);
                case "NotDoubleVerticalBar": return Convert(0x2226);
                case "NotElement": return Convert(0x2209);
                case "NotEqual": return Convert(0x2260);
                case "NotEqualTilde": return Convert(0x2242, 0x0338);
                case "NotExists": return Convert(0x2204);
                case "NotGreater": return Convert(0x226F);
                case "NotGreaterEqual": return Convert(0x2271);
                case "NotGreaterFullEqual": return Convert(0x2267, 0x0338);
                case "NotGreaterGreater": return Convert(0x226B, 0x0338);
                case "NotGreaterLess": return Convert(0x2279);
                case "NotGreaterSlantEqual": return Convert(0x2A7E, 0x0338);
                case "NotGreaterTilde": return Convert(0x2275);
                case "NotHumpDownHump": return Convert(0x224E, 0x0338);
                case "NotHumpEqual": return Convert(0x224F, 0x0338);
                case "notin": return Convert(0x2209);
                case "notindot": return Convert(0x22F5, 0x0338);
                case "notinE": return Convert(0x22F9, 0x0338);
                case "notinva": return Convert(0x2209);
                case "notinvb": return Convert(0x22F7);
                case "notinvc": return Convert(0x22F6);
                case "NotLeftTriangle": return Convert(0x22EA);
                case "NotLeftTriangleBar": return Convert(0x29CF, 0x0338);
                case "NotLeftTriangleEqual": return Convert(0x22EC);
                case "NotLess": return Convert(0x226E);
                case "NotLessEqual": return Convert(0x2270);
                case "NotLessGreater": return Convert(0x2278);
                case "NotLessLess": return Convert(0x226A, 0x0338);
                case "NotLessSlantEqual": return Convert(0x2A7D, 0x0338);
                case "NotLessTilde": return Convert(0x2274);
                case "NotNestedGreaterGreater": return Convert(0x2AA2, 0x0338);
                case "NotNestedLessLess": return Convert(0x2AA1, 0x0338);
                case "notni": return Convert(0x220C);
                case "notniva": return Convert(0x220C);
                case "notnivb": return Convert(0x22FE);
                case "notnivc": return Convert(0x22FD);
                case "NotPrecedes": return Convert(0x2280);
                case "NotPrecedesEqual": return Convert(0x2AAF, 0x0338);
                case "NotPrecedesSlantEqual": return Convert(0x22E0);
                case "NotReverseElement": return Convert(0x220C);
                case "NotRightTriangle": return Convert(0x22EB);
                case "NotRightTriangleBar": return Convert(0x29D0, 0x0338);
                case "NotRightTriangleEqual": return Convert(0x22ED);
                case "NotSquareSubset": return Convert(0x228F, 0x0338);
                case "NotSquareSubsetEqual": return Convert(0x22E2);
                case "NotSquareSuperset": return Convert(0x2290, 0x0338);
                case "NotSquareSupersetEqual": return Convert(0x22E3);
                case "NotSubset": return Convert(0x2282, 0x20D2);
                case "NotSubsetEqual": return Convert(0x2288);
                case "NotSucceeds": return Convert(0x2281);
                case "NotSucceedsEqual": return Convert(0x2AB0, 0x0338);
                case "NotSucceedsSlantEqual": return Convert(0x22E1);
                case "NotSucceedsTilde": return Convert(0x227F, 0x0338);
                case "NotSuperset": return Convert(0x2283, 0x20D2);
                case "NotSupersetEqual": return Convert(0x2289);
                case "NotTilde": return Convert(0x2241);
                case "NotTildeEqual": return Convert(0x2244);
                case "NotTildeFullEqual": return Convert(0x2247);
                case "NotTildeTilde": return Convert(0x2249);
                case "NotVerticalBar": return Convert(0x2224);
                case "npar": return Convert(0x2226);
                case "nparallel": return Convert(0x2226);
                case "nparsl": return Convert(0x2AFD, 0x20E5);
                case "npart": return Convert(0x2202, 0x0338);
                case "npolint": return Convert(0x2A14);
                case "npr": return Convert(0x2280);
                case "nprcue": return Convert(0x22E0);
                case "npre": return Convert(0x2AAF, 0x0338);
                case "nprec": return Convert(0x2280);
                case "npreceq": return Convert(0x2AAF, 0x0338);
                case "nrArr": return Convert(0x21CF);
                case "nrarr": return Convert(0x219B);
                case "nrarrc": return Convert(0x2933, 0x0338);
                case "nrarrw": return Convert(0x219D, 0x0338);
                case "nRightarrow": return Convert(0x21CF);
                case "nrightarrow": return Convert(0x219B);
                case "nrtri": return Convert(0x22EB);
                case "nrtrie": return Convert(0x22ED);
                case "nsc": return Convert(0x2281);
                case "nsccue": return Convert(0x22E1);
                case "nsce": return Convert(0x2AB0, 0x0338);
                case "Nscr": return Convert(0xD835, 0xDCA9);
                case "nscr": return Convert(0xD835, 0xDCC3);
                case "nshortmid": return Convert(0x2224);
                case "nshortparallel": return Convert(0x2226);
                case "nsim": return Convert(0x2241);
                case "nsime": return Convert(0x2244);
                case "nsimeq": return Convert(0x2244);
                case "nsmid": return Convert(0x2224);
                case "nspar": return Convert(0x2226);
                case "nsqsube": return Convert(0x22E2);
                case "nsqsupe": return Convert(0x22E3);
                case "nsub": return Convert(0x2284);
                case "nsubE": return Convert(0x2AC5, 0x0338);
                case "nsube": return Convert(0x2288);
                case "nsubset": return Convert(0x2282, 0x20D2);
                case "nsubseteq": return Convert(0x2288);
                case "nsubseteqq": return Convert(0x2AC5, 0x0338);
                case "nsucc": return Convert(0x2281);
                case "nsucceq": return Convert(0x2AB0, 0x0338);
                case "nsup": return Convert(0x2285);
                case "nsupE": return Convert(0x2AC6, 0x0338);
                case "nsupe": return Convert(0x2289);
                case "nsupset": return Convert(0x2283, 0x20D2);
                case "nsupseteq": return Convert(0x2289);
                case "nsupseteqq": return Convert(0x2AC6, 0x0338);
                case "ntgl": return Convert(0x2279);
                case "Ntilde": return Convert(0x00D1);
                case "ntilde": return Convert(0x00F1);
                case "ntlg": return Convert(0x2278);
                case "ntriangleleft": return Convert(0x22EA);
                case "ntrianglelefteq": return Convert(0x22EC);
                case "ntriangleright": return Convert(0x22EB);
                case "ntrianglerighteq": return Convert(0x22ED);
                case "Nu": return Convert(0x039D);
                case "nu": return Convert(0x03BD);
                case "num": return Convert(0x0023);
                case "numero": return Convert(0x2116);
                case "numsp": return Convert(0x2007);
                case "nvap": return Convert(0x224D, 0x20D2);
                case "nVDash": return Convert(0x22AF);
                case "nVdash": return Convert(0x22AE);
                case "nvDash": return Convert(0x22AD);
                case "nvdash": return Convert(0x22AC);
                case "nvge": return Convert(0x2265, 0x20D2);
                case "nvgt": return Convert(0x003E, 0x20D2);
                case "nvHarr": return Convert(0x2904);
                case "nvinfin": return Convert(0x29DE);
                case "nvlArr": return Convert(0x2902);
                case "nvle": return Convert(0x2264, 0x20D2);
                case "nvlt": return Convert(0x003C, 0x20D2);
                case "nvltrie": return Convert(0x22B4, 0x20D2);
                case "nvrArr": return Convert(0x2903);
                case "nvrtrie": return Convert(0x22B5, 0x20D2);
                case "nvsim": return Convert(0x223C, 0x20D2);
                case "nwarhk": return Convert(0x2923);
                case "nwArr": return Convert(0x21D6);
                case "nwarr": return Convert(0x2196);
                case "nwarrow": return Convert(0x2196);
                case "nwnear": return Convert(0x2927);
            }

            return null;
        }

        static char[] GetSymbolO(string name)
        {
            switch (name)
            {
                case "Oacute": return Convert(0x00D3);
                case "oacute": return Convert(0x00F3);
                case "oast": return Convert(0x229B);
                case "ocir": return Convert(0x229A);
                case "Ocirc": return Convert(0x00D4);
                case "ocirc": return Convert(0x00F4);
                case "Ocy": return Convert(0x041E);
                case "ocy": return Convert(0x043E);
                case "odash": return Convert(0x229D);
                case "Odblac": return Convert(0x0150);
                case "odblac": return Convert(0x0151);
                case "odiv": return Convert(0x2A38);
                case "odot": return Convert(0x2299);
                case "odsold": return Convert(0x29BC);
                case "OElig": return Convert(0x0152);
                case "oelig": return Convert(0x0153);
                case "ofcir": return Convert(0x29BF);
                case "Ofr": return Convert(0xD835, 0xDD12);
                case "ofr": return Convert(0xD835, 0xDD2C);
                case "ogon": return Convert(0x02DB);
                case "Ograve": return Convert(0x00D2);
                case "ograve": return Convert(0x00F2);
                case "ogt": return Convert(0x29C1);
                case "ohbar": return Convert(0x29B5);
                case "ohm": return Convert(0x03A9);
                case "oint": return Convert(0x222E);
                case "olarr": return Convert(0x21BA);
                case "olcir": return Convert(0x29BE);
                case "olcross": return Convert(0x29BB);
                case "oline": return Convert(0x203E);
                case "olt": return Convert(0x29C0);
                case "Omacr": return Convert(0x014C);
                case "omacr": return Convert(0x014D);
                case "Omega": return Convert(0x03A9);
                case "omega": return Convert(0x03C9);
                case "Omicron": return Convert(0x039F);
                case "omicron": return Convert(0x03BF);
                case "omid": return Convert(0x29B6);
                case "ominus": return Convert(0x2296);
                case "Oopf": return Convert(0xD835, 0xDD46);
                case "oopf": return Convert(0xD835, 0xDD60);
                case "opar": return Convert(0x29B7);
                case "OpenCurlyDoubleQuote": return Convert(0x201C);
                case "OpenCurlyQuote": return Convert(0x2018);
                case "operp": return Convert(0x29B9);
                case "oplus": return Convert(0x2295);
                case "Or": return Convert(0x2A54);
                case "or": return Convert(0x2228);
                case "orarr": return Convert(0x21BB);
                case "ord": return Convert(0x2A5D);
                case "order": return Convert(0x2134);
                case "orderof": return Convert(0x2134);
                case "ordf": return Convert(0x00AA);
                case "ordm": return Convert(0x00BA);
                case "origof": return Convert(0x22B6);
                case "oror": return Convert(0x2A56);
                case "orslope": return Convert(0x2A57);
                case "orv": return Convert(0x2A5B);
                case "oS": return Convert(0x24C8);
                case "Oscr": return Convert(0xD835, 0xDCAA);
                case "oscr": return Convert(0x2134);
                case "Oslash": return Convert(0x00D8);
                case "oslash": return Convert(0x00F8);
                case "osol": return Convert(0x2298);
                case "Otilde": return Convert(0x00D5);
                case "otilde": return Convert(0x00F5);
                case "Otimes": return Convert(0x2A37);
                case "otimes": return Convert(0x2297);
                case "otimesas": return Convert(0x2A36);
                case "Ouml": return Convert(0x00D6);
                case "ouml": return Convert(0x00F6);
                case "ovbar": return Convert(0x233D);
                case "OverBar": return Convert(0x203E);
                case "OverBrace": return Convert(0x23DE);
                case "OverBracket": return Convert(0x23B4);
                case "OverParenthesis": return Convert(0x23DC);
            }

            return null;
        }

        static char[] GetSymbolP(string name)
        {
            switch (name)
            {
                case "par": return Convert(0x2225);
                case "para": return Convert(0x00B6);
                case "parallel": return Convert(0x2225);
                case "parsim": return Convert(0x2AF3);
                case "parsl": return Convert(0x2AFD);
                case "part": return Convert(0x2202);
                case "PartialD": return Convert(0x2202);
                case "Pcy": return Convert(0x041F);
                case "pcy": return Convert(0x043F);
                case "percnt": return Convert(0x0025);
                case "period": return Convert(0x002E);
                case "permil": return Convert(0x2030);
                case "perp": return Convert(0x22A5);
                case "pertenk": return Convert(0x2031);
                case "Pfr": return Convert(0xD835, 0xDD13);
                case "pfr": return Convert(0xD835, 0xDD2D);
                case "Phi": return Convert(0x03A6);
                case "phi": return Convert(0x03C6);
                case "phiv": return Convert(0x03D5);
                case "phmmat": return Convert(0x2133);
                case "phone": return Convert(0x260E);
                case "Pi": return Convert(0x03A0);
                case "pi": return Convert(0x03C0);
                case "pitchfork": return Convert(0x22D4);
                case "piv": return Convert(0x03D6);
                case "planck": return Convert(0x210F);
                case "planckh": return Convert(0x210E);
                case "plankv": return Convert(0x210F);
                case "plus": return Convert(0x002B);
                case "plusacir": return Convert(0x2A23);
                case "plusb": return Convert(0x229E);
                case "pluscir": return Convert(0x2A22);
                case "plusdo": return Convert(0x2214);
                case "plusdu": return Convert(0x2A25);
                case "pluse": return Convert(0x2A72);
                case "PlusMinus": return Convert(0x00B1);
                case "plusmn": return Convert(0x00B1);
                case "plussim": return Convert(0x2A26);
                case "plustwo": return Convert(0x2A27);
                case "pm": return Convert(0x00B1);
                case "Poincareplane": return Convert(0x210C);
                case "pointint": return Convert(0x2A15);
                case "Popf": return Convert(0x2119);
                case "popf": return Convert(0xD835, 0xDD61);
                case "pound": return Convert(0x00A3);
                case "Pr": return Convert(0x2ABB);
                case "pr": return Convert(0x227A);
                case "prap": return Convert(0x2AB7);
                case "prcue": return Convert(0x227C);
                case "prE": return Convert(0x2AB3);
                case "pre": return Convert(0x2AAF);
                case "prec": return Convert(0x227A);
                case "precapprox": return Convert(0x2AB7);
                case "preccurlyeq": return Convert(0x227C);
                case "Precedes": return Convert(0x227A);
                case "PrecedesEqual": return Convert(0x2AAF);
                case "PrecedesSlantEqual": return Convert(0x227C);
                case "PrecedesTilde": return Convert(0x227E);
                case "preceq": return Convert(0x2AAF);
                case "precnapprox": return Convert(0x2AB9);
                case "precneqq": return Convert(0x2AB5);
                case "precnsim": return Convert(0x22E8);
                case "precsim": return Convert(0x227E);
                case "Prime": return Convert(0x2033);
                case "prime": return Convert(0x2032);
                case "primes": return Convert(0x2119);
                case "prnap": return Convert(0x2AB9);
                case "prnE": return Convert(0x2AB5);
                case "prnsim": return Convert(0x22E8);
                case "prod": return Convert(0x220F);
                case "Product": return Convert(0x220F);
                case "profalar": return Convert(0x232E);
                case "profline": return Convert(0x2312);
                case "profsurf": return Convert(0x2313);
                case "prop": return Convert(0x221D);
                case "Proportion": return Convert(0x2237);
                case "Proportional": return Convert(0x221D);
                case "propto": return Convert(0x221D);
                case "prsim": return Convert(0x227E);
                case "prurel": return Convert(0x22B0);
                case "Pscr": return Convert(0xD835, 0xDCAB);
                case "pscr": return Convert(0xD835, 0xDCC5);
                case "Psi": return Convert(0x03A8);
                case "psi": return Convert(0x03C8);
                case "puncsp": return Convert(0x2008);
            }

            return null;
        }

        static char[] GetSymbolQ(string name)
        {
            switch (name)
            {
                case "Qfr": return Convert(0xD835, 0xDD14);
                case "qfr": return Convert(0xD835, 0xDD2E);
                case "qint": return Convert(0x2A0C);
                case "Qopf": return Convert(0x211A);
                case "qopf": return Convert(0xD835, 0xDD62);
                case "qprime": return Convert(0x2057);
                case "Qscr": return Convert(0xD835, 0xDCAC);
                case "qscr": return Convert(0xD835, 0xDCC6);
                case "quaternions": return Convert(0x210D);
                case "quatint": return Convert(0x2A16);
                case "quest": return Convert(0x003F);
                case "questeq": return Convert(0x225F);
                case "QUOT": return Convert(0x0022);
                case "quot": return Convert(0x0022);
            }

            return null;
        }

        static char[] GetSymbolR(string name)
        {
            switch (name)
            {
                case "rAarr": return Convert(0x21DB);
                case "race": return Convert(0x223D, 0x0331);
                case "Racute": return Convert(0x0154);
                case "racute": return Convert(0x0155);
                case "radic": return Convert(0x221A);
                case "raemptyv": return Convert(0x29B3);
                case "Rang": return Convert(0x27EB);
                case "rang": return Convert(0x27E9);
                case "rangd": return Convert(0x2992);
                case "range": return Convert(0x29A5);
                case "rangle": return Convert(0x27E9);
                case "raquo": return Convert(0x00BB);
                case "Rarr": return Convert(0x21A0);
                case "rArr": return Convert(0x21D2);
                case "rarr": return Convert(0x2192);
                case "rarrap": return Convert(0x2975);
                case "rarrb": return Convert(0x21E5);
                case "rarrbfs": return Convert(0x2920);
                case "rarrc": return Convert(0x2933);
                case "rarrfs": return Convert(0x291E);
                case "rarrhk": return Convert(0x21AA);
                case "rarrlp": return Convert(0x21AC);
                case "rarrpl": return Convert(0x2945);
                case "rarrsim": return Convert(0x2974);
                case "Rarrtl": return Convert(0x2916);
                case "rarrtl": return Convert(0x21A3);
                case "rarrw": return Convert(0x219D);
                case "rAtail": return Convert(0x291C);
                case "ratail": return Convert(0x291A);
                case "ratio": return Convert(0x2236);
                case "rationals": return Convert(0x211A);
                case "RBarr": return Convert(0x2910);
                case "rBarr": return Convert(0x290F);
                case "rbarr": return Convert(0x290D);
                case "rbbrk": return Convert(0x2773);
                case "rbrace": return Convert(0x007D);
                case "rbrack": return Convert(0x005D);
                case "rbrke": return Convert(0x298C);
                case "rbrksld": return Convert(0x298E);
                case "rbrkslu": return Convert(0x2990);
                case "Rcaron": return Convert(0x0158);
                case "rcaron": return Convert(0x0159);
                case "Rcedil": return Convert(0x0156);
                case "rcedil": return Convert(0x0157);
                case "rceil": return Convert(0x2309);
                case "rcub": return Convert(0x007D);
                case "Rcy": return Convert(0x0420);
                case "rcy": return Convert(0x0440);
                case "rdca": return Convert(0x2937);
                case "rdldhar": return Convert(0x2969);
                case "rdquo": return Convert(0x201D);
                case "rdquor": return Convert(0x201D);
                case "rdsh": return Convert(0x21B3);
                case "Re": return Convert(0x211C);
                case "real": return Convert(0x211C);
                case "realine": return Convert(0x211B);
                case "realpart": return Convert(0x211C);
                case "reals": return Convert(0x211D);
                case "rect": return Convert(0x25AD);
                case "REG": return Convert(0x00AE);
                case "reg": return Convert(0x00AE);
                case "ReverseElement": return Convert(0x220B);
                case "ReverseEquilibrium": return Convert(0x21CB);
                case "ReverseUpEquilibrium": return Convert(0x296F);
                case "rfisht": return Convert(0x297D);
                case "rfloor": return Convert(0x230B);
                case "Rfr": return Convert(0x211C);
                case "rfr": return Convert(0xD835, 0xDD2F);
                case "rHar": return Convert(0x2964);
                case "rhard": return Convert(0x21C1);
                case "rharu": return Convert(0x21C0);
                case "rharul": return Convert(0x296C);
                case "Rho": return Convert(0x03A1);
                case "rho": return Convert(0x03C1);
                case "rhov": return Convert(0x03F1);
                case "RightAngleBracket": return Convert(0x27E9);
                case "RightArrow": return Convert(0x2192);
                case "Rightarrow": return Convert(0x21D2);
                case "rightarrow": return Convert(0x2192);
                case "RightArrowBar": return Convert(0x21E5);
                case "RightArrowLeftArrow": return Convert(0x21C4);
                case "rightarrowtail": return Convert(0x21A3);
                case "RightCeiling": return Convert(0x2309);
                case "RightDoubleBracket": return Convert(0x27E7);
                case "RightDownTeeVector": return Convert(0x295D);
                case "RightDownVector": return Convert(0x21C2);
                case "RightDownVectorBar": return Convert(0x2955);
                case "RightFloor": return Convert(0x230B);
                case "rightharpoondown": return Convert(0x21C1);
                case "rightharpoonup": return Convert(0x21C0);
                case "rightleftarrows": return Convert(0x21C4);
                case "rightleftharpoons": return Convert(0x21CC);
                case "rightrightarrows": return Convert(0x21C9);
                case "rightsquigarrow": return Convert(0x219D);
                case "RightTee": return Convert(0x22A2);
                case "RightTeeArrow": return Convert(0x21A6);
                case "RightTeeVector": return Convert(0x295B);
                case "rightthreetimes": return Convert(0x22CC);
                case "RightTriangle": return Convert(0x22B3);
                case "RightTriangleBar": return Convert(0x29D0);
                case "RightTriangleEqual": return Convert(0x22B5);
                case "RightUpDownVector": return Convert(0x294F);
                case "RightUpTeeVector": return Convert(0x295C);
                case "RightUpVector": return Convert(0x21BE);
                case "RightUpVectorBar": return Convert(0x2954);
                case "RightVector": return Convert(0x21C0);
                case "RightVectorBar": return Convert(0x2953);
                case "ring": return Convert(0x02DA);
                case "risingdotseq": return Convert(0x2253);
                case "rlarr": return Convert(0x21C4);
                case "rlhar": return Convert(0x21CC);
                case "rlm": return Convert(0x200F);
                case "rmoust": return Convert(0x23B1);
                case "rmoustache": return Convert(0x23B1);
                case "rnmid": return Convert(0x2AEE);
                case "roang": return Convert(0x27ED);
                case "roarr": return Convert(0x21FE);
                case "robrk": return Convert(0x27E7);
                case "ropar": return Convert(0x2986);
                case "Ropf": return Convert(0x211D);
                case "ropf": return Convert(0xD835, 0xDD63);
                case "roplus": return Convert(0x2A2E);
                case "rotimes": return Convert(0x2A35);
                case "RoundImplies": return Convert(0x2970);
                case "rpar": return Convert(0x0029);
                case "rpargt": return Convert(0x2994);
                case "rppolint": return Convert(0x2A12);
                case "rrarr": return Convert(0x21C9);
                case "Rrightarrow": return Convert(0x21DB);
                case "rsaquo": return Convert(0x203A);
                case "Rscr": return Convert(0x211B);
                case "rscr": return Convert(0xD835, 0xDCC7);
                case "Rsh": return Convert(0x21B1);
                case "rsh": return Convert(0x21B1);
                case "rsqb": return Convert(0x005D);
                case "rsquo": return Convert(0x2019);
                case "rsquor": return Convert(0x2019);
                case "rthree": return Convert(0x22CC);
                case "rtimes": return Convert(0x22CA);
                case "rtri": return Convert(0x25B9);
                case "rtrie": return Convert(0x22B5);
                case "rtrif": return Convert(0x25B8);
                case "rtriltri": return Convert(0x29CE);
                case "RuleDelayed": return Convert(0x29F4);
                case "ruluhar": return Convert(0x2968);
                case "rx": return Convert(0x211E);
            }

            return null;
        }

        static char[] GetSymbolS(string name)
        {
            switch (name)
            {
                case "Sacute": return Convert(0x015A);
                case "sacute": return Convert(0x015B);
                case "sbquo": return Convert(0x201A);
                case "Sc": return Convert(0x2ABC);
                case "sc": return Convert(0x227B);
                case "scap": return Convert(0x2AB8);
                case "Scaron": return Convert(0x0160);
                case "scaron": return Convert(0x0161);
                case "sccue": return Convert(0x227D);
                case "scE": return Convert(0x2AB4);
                case "sce": return Convert(0x2AB0);
                case "Scedil": return Convert(0x015E);
                case "scedil": return Convert(0x015F);
                case "Scirc": return Convert(0x015C);
                case "scirc": return Convert(0x015D);
                case "scnap": return Convert(0x2ABA);
                case "scnE": return Convert(0x2AB6);
                case "scnsim": return Convert(0x22E9);
                case "scpolint": return Convert(0x2A13);
                case "scsim": return Convert(0x227F);
                case "Scy": return Convert(0x0421);
                case "scy": return Convert(0x0441);
                case "sdot": return Convert(0x22C5);
                case "sdotb": return Convert(0x22A1);
                case "sdote": return Convert(0x2A66);
                case "searhk": return Convert(0x2925);
                case "seArr": return Convert(0x21D8);
                case "searr": return Convert(0x2198);
                case "searrow": return Convert(0x2198);
                case "sect": return Convert(0x00A7);
                case "semi": return Convert(0x003B);
                case "seswar": return Convert(0x2929);
                case "setminus": return Convert(0x2216);
                case "setmn": return Convert(0x2216);
                case "sext": return Convert(0x2736);
                case "Sfr": return Convert(0xD835, 0xDD16);
                case "sfr": return Convert(0xD835, 0xDD30);
                case "sfrown": return Convert(0x2322);
                case "sharp": return Convert(0x266F);
                case "SHCHcy": return Convert(0x0429);
                case "shchcy": return Convert(0x0449);
                case "SHcy": return Convert(0x0428);
                case "shcy": return Convert(0x0448);
                case "ShortDownArrow": return Convert(0x2193);
                case "ShortLeftArrow": return Convert(0x2190);
                case "shortmid": return Convert(0x2223);
                case "shortparallel": return Convert(0x2225);
                case "ShortRightArrow": return Convert(0x2192);
                case "ShortUpArrow": return Convert(0x2191);
                case "shy": return Convert(0x00AD);
                case "Sigma": return Convert(0x03A3);
                case "sigma": return Convert(0x03C3);
                case "sigmaf": return Convert(0x03C2);
                case "sigmav": return Convert(0x03C2);
                case "sim": return Convert(0x223C);
                case "simdot": return Convert(0x2A6A);
                case "sime": return Convert(0x2243);
                case "simeq": return Convert(0x2243);
                case "simg": return Convert(0x2A9E);
                case "simgE": return Convert(0x2AA0);
                case "siml": return Convert(0x2A9D);
                case "simlE": return Convert(0x2A9F);
                case "simne": return Convert(0x2246);
                case "simplus": return Convert(0x2A24);
                case "simrarr": return Convert(0x2972);
                case "slarr": return Convert(0x2190);
                case "SmallCircle": return Convert(0x2218);
                case "smallsetminus": return Convert(0x2216);
                case "smashp": return Convert(0x2A33);
                case "smeparsl": return Convert(0x29E4);
                case "smid": return Convert(0x2223);
                case "smile": return Convert(0x2323);
                case "smt": return Convert(0x2AAA);
                case "smte": return Convert(0x2AAC);
                case "smtes": return Convert(0x2AAC, 0xFE00);
                case "SOFTcy": return Convert(0x042C);
                case "softcy": return Convert(0x044C);
                case "sol": return Convert(0x002F);
                case "solb": return Convert(0x29C4);
                case "solbar": return Convert(0x233F);
                case "Sopf": return Convert(0xD835, 0xDD4A);
                case "sopf": return Convert(0xD835, 0xDD64);
                case "spades": return Convert(0x2660);
                case "spadesuit": return Convert(0x2660);
                case "spar": return Convert(0x2225);
                case "sqcap": return Convert(0x2293);
                case "sqcaps": return Convert(0x2293, 0xFE00);
                case "sqcup": return Convert(0x2294);
                case "sqcups": return Convert(0x2294, 0xFE00);
                case "Sqrt": return Convert(0x221A);
                case "sqsub": return Convert(0x228F);
                case "sqsube": return Convert(0x2291);
                case "sqsubset": return Convert(0x228F);
                case "sqsubseteq": return Convert(0x2291);
                case "sqsup": return Convert(0x2290);
                case "sqsupe": return Convert(0x2292);
                case "sqsupset": return Convert(0x2290);
                case "sqsupseteq": return Convert(0x2292);
                case "squ": return Convert(0x25A1);
                case "Square": return Convert(0x25A1);
                case "square": return Convert(0x25A1);
                case "SquareIntersection": return Convert(0x2293);
                case "SquareSubset": return Convert(0x228F);
                case "SquareSubsetEqual": return Convert(0x2291);
                case "SquareSuperset": return Convert(0x2290);
                case "SquareSupersetEqual": return Convert(0x2292);
                case "SquareUnion": return Convert(0x2294);
                case "squarf": return Convert(0x25AA);
                case "squf": return Convert(0x25AA);
                case "srarr": return Convert(0x2192);
                case "Sscr": return Convert(0xD835, 0xDCAE);
                case "sscr": return Convert(0xD835, 0xDCC8);
                case "ssetmn": return Convert(0x2216);
                case "ssmile": return Convert(0x2323);
                case "sstarf": return Convert(0x22C6);
                case "Star": return Convert(0x22C6);
                case "star": return Convert(0x2606);
                case "starf": return Convert(0x2605);
                case "straightepsilon": return Convert(0x03F5);
                case "straightphi": return Convert(0x03D5);
                case "strns": return Convert(0x00AF);
                case "Sub": return Convert(0x22D0);
                case "sub": return Convert(0x2282);
                case "subdot": return Convert(0x2ABD);
                case "subE": return Convert(0x2AC5);
                case "sube": return Convert(0x2286);
                case "subedot": return Convert(0x2AC3);
                case "submult": return Convert(0x2AC1);
                case "subnE": return Convert(0x2ACB);
                case "subne": return Convert(0x228A);
                case "subplus": return Convert(0x2ABF);
                case "subrarr": return Convert(0x2979);
                case "Subset": return Convert(0x22D0);
                case "subset": return Convert(0x2282);
                case "subseteq": return Convert(0x2286);
                case "subseteqq": return Convert(0x2AC5);
                case "SubsetEqual": return Convert(0x2286);
                case "subsetneq": return Convert(0x228A);
                case "subsetneqq": return Convert(0x2ACB);
                case "subsim": return Convert(0x2AC7);
                case "subsub": return Convert(0x2AD5);
                case "subsup": return Convert(0x2AD3);
                case "succ": return Convert(0x227B);
                case "succapprox": return Convert(0x2AB8);
                case "succcurlyeq": return Convert(0x227D);
                case "Succeeds": return Convert(0x227B);
                case "SucceedsEqual": return Convert(0x2AB0);
                case "SucceedsSlantEqual": return Convert(0x227D);
                case "SucceedsTilde": return Convert(0x227F);
                case "succeq": return Convert(0x2AB0);
                case "succnapprox": return Convert(0x2ABA);
                case "succneqq": return Convert(0x2AB6);
                case "succnsim": return Convert(0x22E9);
                case "succsim": return Convert(0x227F);
                case "SuchThat": return Convert(0x220B);
                case "Sum": return Convert(0x2211);
                case "sum": return Convert(0x2211);
                case "sung": return Convert(0x266A);
                case "Sup": return Convert(0x22D1);
                case "sup": return Convert(0x2283);
                case "sup1": return Convert(0x00B9);
                case "sup2": return Convert(0x00B2);
                case "sup3": return Convert(0x00B3);
                case "supdot": return Convert(0x2ABE);
                case "supdsub": return Convert(0x2AD8);
                case "supE": return Convert(0x2AC6);
                case "supe": return Convert(0x2287);
                case "supedot": return Convert(0x2AC4);
                case "Superset": return Convert(0x2283);
                case "SupersetEqual": return Convert(0x2287);
                case "suphsol": return Convert(0x27C9);
                case "suphsub": return Convert(0x2AD7);
                case "suplarr": return Convert(0x297B);
                case "supmult": return Convert(0x2AC2);
                case "supnE": return Convert(0x2ACC);
                case "supne": return Convert(0x228B);
                case "supplus": return Convert(0x2AC0);
                case "Supset": return Convert(0x22D1);
                case "supset": return Convert(0x2283);
                case "supseteq": return Convert(0x2287);
                case "supseteqq": return Convert(0x2AC6);
                case "supsetneq": return Convert(0x228B);
                case "supsetneqq": return Convert(0x2ACC);
                case "supsim": return Convert(0x2AC8);
                case "supsub": return Convert(0x2AD4);
                case "supsup": return Convert(0x2AD6);
                case "swarhk": return Convert(0x2926);
                case "swArr": return Convert(0x21D9);
                case "swarr": return Convert(0x2199);
                case "swarrow": return Convert(0x2199);
                case "swnwar": return Convert(0x292A);
                case "szlig": return Convert(0x00DF);
            }

            return null;
        }

        static char[] GetSymbolT(string name)
        {
            switch (name)
            {
                case "Tab": return Convert(0x0009);
                case "target": return Convert(0x2316);
                case "Tau": return Convert(0x03A4);
                case "tau": return Convert(0x03C4);
                case "tbrk": return Convert(0x23B4);
                case "Tcaron": return Convert(0x0164);
                case "tcaron": return Convert(0x0165);
                case "Tcedil": return Convert(0x0162);
                case "tcedil": return Convert(0x0163);
                case "Tcy": return Convert(0x0422);
                case "tcy": return Convert(0x0442);
                case "tdot": return Convert(0x20DB);
                case "telrec": return Convert(0x2315);
                case "Tfr": return Convert(0xD835, 0xDD17);
                case "tfr": return Convert(0xD835, 0xDD31);
                case "there4": return Convert(0x2234);
                case "Therefore": return Convert(0x2234);
                case "therefore": return Convert(0x2234);
                case "Theta": return Convert(0x0398);
                case "theta": return Convert(0x03B8);
                case "thetasym": return Convert(0x03D1);
                case "thetav": return Convert(0x03D1);
                case "thickapprox": return Convert(0x2248);
                case "thicksim": return Convert(0x223C);
                case "ThickSpace": return Convert(0x205F, 0x200A);
                case "thinsp": return Convert(0x2009);
                case "ThinSpace": return Convert(0x2009);
                case "thkap": return Convert(0x2248);
                case "thksim": return Convert(0x223C);
                case "THORN": return Convert(0x00DE);
                case "thorn": return Convert(0x00FE);
                case "Tilde": return Convert(0x223C);
                case "tilde": return Convert(0x02DC);
                case "TildeEqual": return Convert(0x2243);
                case "TildeFullEqual": return Convert(0x2245);
                case "TildeTilde": return Convert(0x2248);
                case "times": return Convert(0x00D7);
                case "timesb": return Convert(0x22A0);
                case "timesbar": return Convert(0x2A31);
                case "timesd": return Convert(0x2A30);
                case "tint": return Convert(0x222D);
                case "toea": return Convert(0x2928);
                case "top": return Convert(0x22A4);
                case "topbot": return Convert(0x2336);
                case "topcir": return Convert(0x2AF1);
                case "Topf": return Convert(0xD835, 0xDD4B);
                case "topf": return Convert(0xD835, 0xDD65);
                case "topfork": return Convert(0x2ADA);
                case "tosa": return Convert(0x2929);
                case "tprime": return Convert(0x2034);
                case "TRADE": return Convert(0x2122);
                case "trade": return Convert(0x2122);
                case "triangle": return Convert(0x25B5);
                case "triangledown": return Convert(0x25BF);
                case "triangleleft": return Convert(0x25C3);
                case "trianglelefteq": return Convert(0x22B4);
                case "triangleq": return Convert(0x225C);
                case "triangleright": return Convert(0x25B9);
                case "trianglerighteq": return Convert(0x22B5);
                case "tridot": return Convert(0x25EC);
                case "trie": return Convert(0x225C);
                case "triminus": return Convert(0x2A3A);
                case "TripleDot": return Convert(0x20DB);
                case "triplus": return Convert(0x2A39);
                case "trisb": return Convert(0x29CD);
                case "tritime": return Convert(0x2A3B);
                case "trpezium": return Convert(0x23E2);
                case "Tscr": return Convert(0xD835, 0xDCAF);
                case "tscr": return Convert(0xD835, 0xDCC9);
                case "TScy": return Convert(0x0426);
                case "tscy": return Convert(0x0446);
                case "TSHcy": return Convert(0x040B);
                case "tshcy": return Convert(0x045B);
                case "Tstrok": return Convert(0x0166);
                case "tstrok": return Convert(0x0167);
                case "twixt": return Convert(0x226C);
                case "twoheadleftarrow": return Convert(0x219E);
                case "twoheadrightarrow": return Convert(0x21A0);
            }

            return null;
        }

        static char[] GetSymbolU(string name)
        {
            switch (name)
            {
                case "Uacute": return Convert(0x00DA);
                case "uacute": return Convert(0x00FA);
                case "Uarr": return Convert(0x219F);
                case "uArr": return Convert(0x21D1);
                case "uarr": return Convert(0x2191);
                case "Uarrocir": return Convert(0x2949);
                case "Ubrcy": return Convert(0x040E);
                case "ubrcy": return Convert(0x045E);
                case "Ubreve": return Convert(0x016C);
                case "ubreve": return Convert(0x016D);
                case "Ucirc": return Convert(0x00DB);
                case "ucirc": return Convert(0x00FB);
                case "Ucy": return Convert(0x0423);
                case "ucy": return Convert(0x0443);
                case "udarr": return Convert(0x21C5);
                case "Udblac": return Convert(0x0170);
                case "udblac": return Convert(0x0171);
                case "udhar": return Convert(0x296E);
                case "ufisht": return Convert(0x297E);
                case "Ufr": return Convert(0xD835, 0xDD18);
                case "ufr": return Convert(0xD835, 0xDD32);
                case "Ugrave": return Convert(0x00D9);
                case "ugrave": return Convert(0x00F9);
                case "uHar": return Convert(0x2963);
                case "uharl": return Convert(0x21BF);
                case "uharr": return Convert(0x21BE);
                case "uhblk": return Convert(0x2580);
                case "ulcorn": return Convert(0x231C);
                case "ulcorner": return Convert(0x231C);
                case "ulcrop": return Convert(0x230F);
                case "ultri": return Convert(0x25F8);
                case "Umacr": return Convert(0x016A);
                case "umacr": return Convert(0x016B);
                case "uml": return Convert(0x00A8);
                case "UnderBar": return Convert(0x005F);
                case "UnderBrace": return Convert(0x23DF);
                case "UnderBracket": return Convert(0x23B5);
                case "UnderParenthesis": return Convert(0x23DD);
                case "Union": return Convert(0x22C3);
                case "UnionPlus": return Convert(0x228E);
                case "Uogon": return Convert(0x0172);
                case "uogon": return Convert(0x0173);
                case "Uopf": return Convert(0xD835, 0xDD4C);
                case "uopf": return Convert(0xD835, 0xDD66);
                case "UpArrow": return Convert(0x2191);
                case "Uparrow": return Convert(0x21D1);
                case "uparrow": return Convert(0x2191);
                case "UpArrowBar": return Convert(0x2912);
                case "UpArrowDownArrow": return Convert(0x21C5);
                case "UpDownArrow": return Convert(0x2195);
                case "Updownarrow": return Convert(0x21D5);
                case "updownarrow": return Convert(0x2195);
                case "UpEquilibrium": return Convert(0x296E);
                case "upharpoonleft": return Convert(0x21BF);
                case "upharpoonright": return Convert(0x21BE);
                case "uplus": return Convert(0x228E);
                case "UpperLeftArrow": return Convert(0x2196);
                case "UpperRightArrow": return Convert(0x2197);
                case "Upsi": return Convert(0x03D2);
                case "upsi": return Convert(0x03C5);
                case "upsih": return Convert(0x03D2);
                case "Upsilon": return Convert(0x03A5);
                case "upsilon": return Convert(0x03C5);
                case "UpTee": return Convert(0x22A5);
                case "UpTeeArrow": return Convert(0x21A5);
                case "upuparrows": return Convert(0x21C8);
                case "urcorn": return Convert(0x231D);
                case "urcorner": return Convert(0x231D);
                case "urcrop": return Convert(0x230E);
                case "Uring": return Convert(0x016E);
                case "uring": return Convert(0x016F);
                case "urtri": return Convert(0x25F9);
                case "Uscr": return Convert(0xD835, 0xDCB0);
                case "uscr": return Convert(0xD835, 0xDCCA);
                case "utdot": return Convert(0x22F0);
                case "Utilde": return Convert(0x0168);
                case "utilde": return Convert(0x0169);
                case "utri": return Convert(0x25B5);
                case "utrif": return Convert(0x25B4);
                case "uuarr": return Convert(0x21C8);
                case "Uuml": return Convert(0x00DC);
                case "uuml": return Convert(0x00FC);
                case "uwangle": return Convert(0x29A7);
            }

            return null;
        }

        static char[] GetSymbolV(string name)
        {
            switch (name)
            {
                case "vangrt": return Convert(0x299C);
                case "varepsilon": return Convert(0x03F5);
                case "varkappa": return Convert(0x03F0);
                case "varnothing": return Convert(0x2205);
                case "varphi": return Convert(0x03D5);
                case "varpi": return Convert(0x03D6);
                case "varpropto": return Convert(0x221D);
                case "vArr": return Convert(0x21D5);
                case "varr": return Convert(0x2195);
                case "varrho": return Convert(0x03F1);
                case "varsigma": return Convert(0x03C2);
                case "varsubsetneq": return Convert(0x228A, 0xFE00);
                case "varsubsetneqq": return Convert(0x2ACB, 0xFE00);
                case "varsupsetneq": return Convert(0x228B, 0xFE00);
                case "varsupsetneqq": return Convert(0x2ACC, 0xFE00);
                case "vartheta": return Convert(0x03D1);
                case "vartriangleleft": return Convert(0x22B2);
                case "vartriangleright": return Convert(0x22B3);
                case "Vbar": return Convert(0x2AEB);
                case "vBar": return Convert(0x2AE8);
                case "vBarv": return Convert(0x2AE9);
                case "Vcy": return Convert(0x0412);
                case "vcy": return Convert(0x0432);
                case "VDash": return Convert(0x22AB);
                case "Vdash": return Convert(0x22A9);
                case "vDash": return Convert(0x22A8);
                case "vdash": return Convert(0x22A2);
                case "Vdashl": return Convert(0x2AE6);
                case "Vee": return Convert(0x22C1);
                case "vee": return Convert(0x2228);
                case "veebar": return Convert(0x22BB);
                case "veeeq": return Convert(0x225A);
                case "vellip": return Convert(0x22EE);
                case "Verbar": return Convert(0x2016);
                case "verbar": return Convert(0x007C);
                case "Vert": return Convert(0x2016);
                case "vert": return Convert(0x007C);
                case "VerticalBar": return Convert(0x2223);
                case "VerticalLine": return Convert(0x007C);
                case "VerticalSeparator": return Convert(0x2758);
                case "VerticalTilde": return Convert(0x2240);
                case "VeryThinSpace": return Convert(0x200A);
                case "Vfr": return Convert(0xD835, 0xDD19);
                case "vfr": return Convert(0xD835, 0xDD33);
                case "vltri": return Convert(0x22B2);
                case "vnsub": return Convert(0x2282, 0x20D2);
                case "vnsup": return Convert(0x2283, 0x20D2);
                case "Vopf": return Convert(0xD835, 0xDD4D);
                case "vopf": return Convert(0xD835, 0xDD67);
                case "vprop": return Convert(0x221D);
                case "vrtri": return Convert(0x22B3);
                case "Vscr": return Convert(0xD835, 0xDCB1);
                case "vscr": return Convert(0xD835, 0xDCCB);
                case "vsubnE": return Convert(0x2ACB, 0xFE00);
                case "vsubne": return Convert(0x228A, 0xFE00);
                case "vsupnE": return Convert(0x2ACC, 0xFE00);
                case "vsupne": return Convert(0x228B, 0xFE00);
                case "Vvdash": return Convert(0x22AA);
                case "vzigzag": return Convert(0x299A);
            }

            return null;
        }

        static char[] GetSymbolW(string name)
        {
            switch (name)
            {
                case "Wcirc": return Convert(0x0174);
                case "wcirc": return Convert(0x0175);
                case "wedbar": return Convert(0x2A5F);
                case "Wedge": return Convert(0x22C0);
                case "wedge": return Convert(0x2227);
                case "wedgeq": return Convert(0x2259);
                case "weierp": return Convert(0x2118);
                case "Wfr": return Convert(0xD835, 0xDD1A);
                case "wfr": return Convert(0xD835, 0xDD34);
                case "Wopf": return Convert(0xD835, 0xDD4E);
                case "wopf": return Convert(0xD835, 0xDD68);
                case "wp": return Convert(0x2118);
                case "wr": return Convert(0x2240);
                case "wreath": return Convert(0x2240);
                case "Wscr": return Convert(0xD835, 0xDCB2);
                case "wscr": return Convert(0xD835, 0xDCCC);
            }

            return null;
        }

        static char[] GetSymbolX(string name)
        {
            switch (name)
            {
                case "xcap": return Convert(0x22C2);
                case "xcirc": return Convert(0x25EF);
                case "xcup": return Convert(0x22C3);
                case "xdtri": return Convert(0x25BD);
                case "Xfr": return Convert(0xD835, 0xDD1B);
                case "xfr": return Convert(0xD835, 0xDD35);
                case "xhArr": return Convert(0x27FA);
                case "xharr": return Convert(0x27F7);
                case "Xi": return Convert(0x039E);
                case "xi": return Convert(0x03BE);
                case "xlArr": return Convert(0x27F8);
                case "xlarr": return Convert(0x27F5);
                case "xmap": return Convert(0x27FC);
                case "xnis": return Convert(0x22FB);
                case "xodot": return Convert(0x2A00);
                case "Xopf": return Convert(0xD835, 0xDD4F);
                case "xopf": return Convert(0xD835, 0xDD69);
                case "xoplus": return Convert(0x2A01);
                case "xotime": return Convert(0x2A02);
                case "xrArr": return Convert(0x27F9);
                case "xrarr": return Convert(0x27F6);
                case "Xscr": return Convert(0xD835, 0xDCB3);
                case "xscr": return Convert(0xD835, 0xDCCD);
                case "xsqcup": return Convert(0x2A06);
                case "xuplus": return Convert(0x2A04);
                case "xutri": return Convert(0x25B3);
                case "xvee": return Convert(0x22C1);
                case "xwedge": return Convert(0x22C0);
            }

            return null;
        }

        static char[] GetSymbolY(string name)
        {
            switch (name)
            {
                case "Yacute": return Convert(0x00DD);
                case "yacute": return Convert(0x00FD);
                case "YAcy": return Convert(0x042F);
                case "yacy": return Convert(0x044F);
                case "Ycirc": return Convert(0x0176);
                case "ycirc": return Convert(0x0177);
                case "Ycy": return Convert(0x042B);
                case "ycy": return Convert(0x044B);
                case "yen": return Convert(0x00A5);
                case "Yfr": return Convert(0xD835, 0xDD1C);
                case "yfr": return Convert(0xD835, 0xDD36);
                case "YIcy": return Convert(0x0407);
                case "yicy": return Convert(0x0457);
                case "Yopf": return Convert(0xD835, 0xDD50);
                case "yopf": return Convert(0xD835, 0xDD6A);
                case "Yscr": return Convert(0xD835, 0xDCB4);
                case "yscr": return Convert(0xD835, 0xDCCE);
                case "YUcy": return Convert(0x042E);
                case "yucy": return Convert(0x044E);
                case "Yuml": return Convert(0x0178);
                case "yuml": return Convert(0x00FF);
            }

            return null;
        }

        static char[] GetSymbolZ(string name)
        {
            switch (name)
            {
                case "Zacute": return Convert(0x0179);
                case "zacute": return Convert(0x017A);
                case "Zcaron": return Convert(0x017D);
                case "zcaron": return Convert(0x017E);
                case "Zcy": return Convert(0x0417);
                case "zcy": return Convert(0x0437);
                case "Zdot": return Convert(0x017B);
                case "zdot": return Convert(0x017C);
                case "zeetrf": return Convert(0x2128);
                case "ZeroWidthSpace": return Convert(0x200B);
                case "Zeta": return Convert(0x0396);
                case "zeta": return Convert(0x03B6);
                case "Zfr": return Convert(0x2128);
                case "zfr": return Convert(0xD835, 0xDD37);
                case "ZHcy": return Convert(0x0416);
                case "zhcy": return Convert(0x0436);
                case "zigrarr": return Convert(0x21DD);
                case "Zopf": return Convert(0x2124);
                case "zopf": return Convert(0xD835, 0xDD6B);
                case "Zscr": return Convert(0xD835, 0xDCB5);
                case "zscr": return Convert(0xD835, 0xDCCF);
                case "zwj": return Convert(0x200D);
                case "zwnj": return Convert(0x200C);
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Converts a given number into its unicode character.
        /// </summary>
        /// <param name="code">The code to convert.</param>
        /// <returns>The array containing the character.</returns>
        public static char[] Convert(int code)
        {
            return new char[] { (char)code };
        }

        /// <summary>
        /// Converts a set of two numbers into their unicode characters.
        /// </summary>
        /// <param name="leadingCode">The first (leading) character code.</param>
        /// <param name="trailingCode">The second (trailing) character code.</param>
        /// <returns>The array containing the two characters.</returns>
        public static char[] Convert(int leadingCode, int trailingCode)
        {
            return new char[] { (char)leadingCode, (char)trailingCode };
        }

        /// <summary>
        /// Determines if the code is an invalid number.
        /// </summary>
        /// <param name="code">The code to examine.</param>
        /// <returns>True if it is an invalid number, false otherwise.</returns>
        public static bool IsInvalidNumber(int code)
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
        public static bool IsInCharacterTable(int code)
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
        public static char[] GetSymbolFromTable(int code)
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
        public static bool IsInInvalidRange(int code)
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
    }
}

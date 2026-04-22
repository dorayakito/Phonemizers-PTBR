using System;
using System.Collections.Generic;
using System.Linq;
using OpenUtau.Api;
using OpenUtau.Core.Ustx;

namespace OpenUtau.Plugins {
    [Phonemizer("Portuguese CATIPA DiffSinger Phonemizer", "DIFF CATIPA", "xiao", "PT")]
    public class CatipaDiffSingerPhonemizer : Phonemizer {
        protected USinger singer;

        public override void SetSinger(USinger singer) => this.singer = singer;

        private readonly string[] vowels = { "a", "e", "i", "o", "u", "a'", "e'", "i'", "o'", "u'", "a~", "e~", "i~", "o~", "u~" };
        private readonly string[] consonants = { "b", "d", "dj", "f", "g", "h", "j", "k", "l", "lh", "m", "n", "nh", "p", "r", "rr", "rrr", "wr", "s", "t", "tch", "v", "w", "x", "y", "z" };

        private static readonly Dictionary<string, string> g2p = new Dictionary<string, string>();

        static CatipaDiffSingerPhonemizer() {
            var vBase = new Dictionary<string, string> {
                { "a", "a" }, { "á", "a" }, { "à", "a" }, { "ã", "a~" }, { "â", "a'" },
                { "e", "e" }, { "é", "e'" }, { "ê", "e" },
                { "i", "i" }, { "í", "i" },
                { "o", "o" }, { "ó", "o'" }, { "ô", "o" }, { "õ", "o~" },
                { "u", "u" }, { "ú", "u" }
            };

            foreach (var v in vBase) g2p[v.Key] = v.Value;

            string[] cBase = { "b", "d", "f", "j", "k", "l", "m", "n", "p", "s", "t", "v", "z" };

            foreach (var c in cBase) {
                foreach (var v in vBase) {
                    string cTarget = c;
                    if (c == "d" && v.Key == "i") cTarget = "dj";
                    if (c == "t" && v.Key == "i") cTarget = "tch";
                    g2p[c + v.Key] = cTarget + " " + v.Value;
                }
            }

            foreach (var v in vBase) {
                g2p["r" + v.Key] = "rr " + v.Value;
                g2p["c" + v.Key] = (new[] { "e", "é", "ê", "i", "í" }.Contains(v.Key) ? "s" : "k") + " " + v.Value;
                g2p["g" + v.Key] = (new[] { "e", "é", "ê", "i", "í" }.Contains(v.Key) ? "j" : "g") + " " + v.Value;
                g2p["nh" + v.Key] = "nh " + v.Value;
                g2p["lh" + v.Key] = "lh " + v.Value;
                g2p["x" + v.Key] = "x " + v.Value;
                g2p["h" + v.Key] = v.Value;
            }
        }

        public override Result Process(Note[] notes, Note? prev, Note? next, Note? prevNeighbour, Note? nextNeighbour, Note[] prevNeighbours) {
            var lyric = notes[0].lyric.ToLower();
            if (lyric == "+" && prevNeighbour != null) {
                return new Result { phonemes = new Phoneme[0] };
            }

            string phonemeStr = g2p.ContainsKey(lyric) ? g2p[lyric] : lyric;
            var phonemeList = phonemeStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            return new Result { 
                phonemes = phonemeList.Select(p => new Phoneme { phoneme = p }).ToArray() 
            };
        }
    }
}

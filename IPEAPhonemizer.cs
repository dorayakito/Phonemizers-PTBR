using System.Linq;
using System.Collections.Generic;
using OpenUtau.Api;
using OpenUtau.Core.G2p;
using OpenUtau.Plugin.Builtin;

namespace IpeaPhonemizer {
    [Phonemizer("IPÊ-A CVVC Phonemizer", "IPÊ-A CVVC", "ly ft. xiao", "PT")]
    public class IpeaCVVCPhonemizer : SyllableBasedPhonemizer {
        private readonly string[] vowels = { "a", "e", "i", "o", "u", "E", "O", "6", "am", "em", "im", "om", "um", "Ao" };
        private readonly string[] consonants = { "b", "d", "f", "g", "k", "l", "m", "n", "p", "r", "s", "t", "v", "z", "X", "Z", "L", "J", "R", "tS", "dZ", "w", "j", "w~", "j~" };
        private readonly Dictionary<string, string> dictionaryReplacements = new Dictionary<string, string> {
            { "a~", "am" }, { "e~", "em" }, { "i~", "im" }, { "o~", "om" }, { "u~", "um" }, { "w~", "Ao" }, { "j~", "im" },
            { "X", "ch" }, { "Z", "j" }, { "L", "lh" }, { "J", "nh" }, { "R", "rr" }, { "tS", "tch" }, { "dZ", "dj" },
        };

        protected override string GetDictionaryName() => "pt";
        protected override IG2p LoadBaseDictionary() => new PortugueseG2p();
        protected override string[] GetVowels() => vowels;
        protected override string[] GetConsonants() => consonants;
        protected override Dictionary<string, string> GetDictionaryPhonemesReplacement() => dictionaryReplacements;

        protected override List<string> ProcessSyllable(Syllable syllable) {
            var phonemes = new List<string>();
            var (prevV, cc, v) = (syllable.prevV, syllable.cc, syllable.v);
            string phoneme = string.Empty;

            if (syllable.IsStartingV) phoneme = $"- {v}";
            else if (syllable.IsVV) {
                if (CanMakeAliasExtension(syllable)) phoneme = null;
                else {
                    phoneme = $"{prevV} {v}";
                    if (!HasOto(phoneme, syllable.vowelTone)) phoneme = $"{prevV}{v}";
                    if (!HasOto(phoneme, syllable.vowelTone)) {
                        string alt = prevV == "i" ? "y" : (prevV == "o" || prevV == "u" ? "w" : string.Empty);
                        phoneme = $"{alt} {v}";
                    }
                    if (!HasOto(phoneme, syllable.vowelTone)) phoneme = v;
                }
            } else if (syllable.IsVCV) {
                var vcv = new[] { $"{prevV} {cc[0]}", $"{prevV} {cc[0].ToLower()}", $"{prevV}{cc[0]}" }.FirstOrDefault(a => HasOto(a, syllable.vowelTone));
                if (vcv != null) phonemes.Add(vcv);
            }

            if (syllable.IsStartingCV || syllable.IsVCV) {
                for (int i = 0; i < cc.Length - 1; i++) {
                    var vc = $"{cc[i]} {cc[i + 1]}";
                    if (HasOto(vc, syllable.vowelTone)) phonemes.Add(vc);
                }
                phoneme = $"{cc.Last()} {v}";
            }
            phonemes.Add(phoneme);
            return phonemes;
        }

        protected override List<string> ProcessEnding(Ending ending) {
            var (prevV, cc) = (ending.prevV, ending.cc);
            string phoneme = ending.IsEndingV ? $"{prevV} -" : $"{prevV} {cc[0]}";
            if (!ending.IsEndingV && !HasOto(phoneme, ending.tone)) phoneme = $"{prevV}{cc[0]}";
            return new List<string> { phoneme };
        }
    }
}

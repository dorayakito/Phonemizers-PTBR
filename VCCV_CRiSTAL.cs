using System;
using System.Collections.Generic;
using System.Linq;
using OpenUtau.Api;
using OpenUtau.Core.Ustx;

namespace OpenUtau.Plugin.Builtin {
    [Phonemizer("VCCV CRiSTAL", "VCCV CRiSTAL", "xiao", "PT")]
    public class CRiSTALVCCVPhonemizer : SyllableBasedPhonemizer {
        private readonly string[] vowels = { "a", "an", "ax", "e", "eh", "en", "i", "in", "o", "oh", "on", "u", "un" };
        private readonly string[] consonants = { "b", "ch", "d", "dj", "f", "g", "h", "j", "k", "l", "lh", "m", "n", "nh", "p", "r", "rr", "rw", "R","s", "sh", "t", "v", "w", "x", "y", "z" };

        protected override string[] GetVowels() => vowels;
        protected override string[] GetConsonants() => consonants;
        protected override string GetDictionaryName() => "";
        protected override Dictionary<string, string> GetDictionaryPhonemesReplacement() => new Dictionary<string, string>();
        protected override double GetTransitionBasicLengthMs(string alias = "") => 70.0;

        protected override IG2p LoadBaseDictionary() {
            return null;
        }

        protected override string[] GetSymbols(Note note) {
            if (string.IsNullOrEmpty(note.lyric)) {
                return new string[0];
            }
            var parts = note.lyric.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var symbols = new List<string>();
            var allValid = vowels.Concat(consonants).OrderByDescending(s => s.Length).ToArray();

            foreach (var part in parts) {
                string remaining = part;
                while (remaining.Length > 0) {
                    bool found = false;
                    foreach (var s in allValid) {
                        if (remaining.StartsWith(s)) {
                            symbols.Add(s);
                            remaining = remaining.Substring(s.Length);
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        symbols.Add(remaining[0].ToString());
                        remaining = remaining.Substring(1);
                    }
                }
            }
            return symbols.ToArray();
        }

        protected override List<string> ProcessSyllable(Syllable syllable) {
            var prevV = syllable.prevV;
            var v = syllable.v;
            var cc = syllable.cc;
            var phonemes = new List<string>();

            if (syllable.IsStartingV) {
                if (!TryAddPhoneme(phonemes, syllable.vowelTone, $"- {v}", v)) {
                    phonemes.Add(v);
                }
            } else if (syllable.IsVV) {
                if (!TryAddPhoneme(phonemes, syllable.vowelTone, $"{prevV} {v}", v)) {
                    phonemes.Add(v);
                }
            } else if (syllable.IsStartingCV) {
                // Try [- C1 C2 V]
                var rccv = $"- {string.Join(" ", cc)} {v}";
                if (HasOto(rccv, syllable.vowelTone)) {
                    phonemes.Add(rccv);
                } else {
                    var rcv = $"- {cc[0]} {v}";
                    if (cc.Length == 1 && HasOto(rcv, syllable.vowelTone)) {
                        phonemes.Add(rcv);
                    } else {
                        // Start with [- C1]
                        if (!TryAddPhoneme(phonemes, syllable.tone, $"- {cc[0]}", cc[0])) {
                            phonemes.Add(cc[0]);
                        }
                        // Chain clusters
                        for (int i = 0; i < cc.Length - 1; i++) {
                            TryAddPhoneme(phonemes, syllable.tone, $"{cc[i]} {cc[i + 1]}");
                        }
                        // End with [Cn V]
                        phonemes.Add($"{cc.Last()} {v}");
                    }
                }
            } else {
                if (syllable.IsVCVWithOneConsonant) {
                    TryAddPhoneme(phonemes, syllable.tone, $"{prevV} {cc[0]}");
                    phonemes.Add($"{cc[0]} {v}");
                } else {
                    TryAddPhoneme(phonemes, syllable.tone, $"{prevV} {cc[0]}");
                    for (int i = 0; i < cc.Length - 1; i++) {
                        TryAddPhoneme(phonemes, syllable.tone, $"{cc[i]} {cc[i + 1]}");
                    }
                    phonemes.Add($"{cc.Last()} {v}");
                }
            }
            return phonemes;
        }
        protected override List<string> ProcessEnding(Ending ending) {
            var prevV = ending.prevV;
            var cc = ending.cc;
            var phonemes = new List<string>();

            if (ending.IsEndingV) {
                TryAddPhoneme(phonemes, ending.tone, $"{prevV} -", $"{prevV}-");
            } else {
                TryAddPhoneme(phonemes, ending.tone, $"{prevV} {cc[0]}");
                for (int i = 0; i < cc.Length - 1; i++) {
                    TryAddPhoneme(phonemes, ending.tone, $"{cc[i]} {cc[i + 1]}");
                }
                TryAddPhoneme(phonemes, ending.tone, $"{cc.Last()} -", $"{cc.Last()}-");
            }
            return phonemes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt
{
    public static class StaticStrings
    {
        public static string LongString1 => @"A hash function is any function that can be used to map data of arbitrary size to fixed-size values.[1] The values returned by a hash function are called hash values, hash codes, digests, or simply hashes. Hash functions are often used in combination with a hash table, a common data structure used in computer software for rapid data lookup. Hash functions accelerate table or database lookup by detecting duplicated records in a large file. One such application is finding similar stretches in DNA sequences. They are also useful in cryptography. A cryptographic hash function allows one to easily verify whether some input data map onto a given hash value, but if the input data is unknown it is deliberately difficult to reconstruct it (or any equivalent alternatives) by knowing the stored hash value. This is used for assuring integrity of transmitted data, and is the building block for HMACs, which provide message authentication.";
        public static string LongString2 => @"Hash functions are related to (and often confused with) checksums, check digits, fingerprints, lossy compression, randomization functions, error-correcting codes, and ciphers. Although the concepts overlap to some extent, each one has its own uses and requirements and is designed and optimized differently. The HashKeeper database maintained by the American National Drug Intelligence Center, for instance, is more aptly described as a catalogue of file fingerprints than of hash values.";
        public static string LongString3 => @"Hash functions are used in hash tables,[2] to quickly locate a data record (e.g., a dictionary definition) given its search key (the headword). Specifically, the hash function is used to map the search key to a list; the index gives the place in the hash table where the corresponding record should be stored. Hash tables are also used to implement associative arrays and dynamic sets.[3]";
        public static string LongString4 => @"Typically, the domain of a hash function (the set of possible keys) is larger than its range (the number of different table indices), and so it will map several different keys to the same index which could result in collisions. So then, each slot of a hash table is associated with (implicitly or explicitly) a set of records, rather than a single record. For this reason, each slot of a hash table is often called a bucket[4], and hash values are also called bucket listing[citation needed] or a bucket index.";

        public static string ShortString1 => @"Hash functions:";
        public static string ShortString2 => @"Please wait outside of the house.";
        public static string ShortString3 => @"The waves were crashing on the shore; it was a lovely sight.";
        public static string ShortString4 => @"Christmas is coming.";
    }
}

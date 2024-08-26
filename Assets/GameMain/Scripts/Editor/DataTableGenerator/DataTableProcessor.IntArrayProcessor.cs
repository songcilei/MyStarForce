using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StarForce.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        public sealed class IntArrayProcessor : GenericDataProcessor<int[]>
        {
            public override bool IsSystem {
                get
                {
                    return false;
                }
            }
            public override string LanguageKeyword {
                get
                {
                    return "int[]";
                }
            } 
            public override string[] GetTypeStrings()
            {
                return new string[] { "int[]", "system.int[]" };
            }
            public override int[] Parse(string value)
            {
                throw new System.NotImplementedException();
            }
            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value).ToString());
            }


        }
    }
}


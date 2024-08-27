using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StarForce.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        public sealed class FloatArrayProcessor : GenericDataProcessor<float[]>
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
                    return "float[]";
                }
            } 
            public override string[] GetTypeStrings()
            {
                return new string[] { "float[]", "system.float[]" };
            }
            public override float[] Parse(string value)
            {
                return DataTableExtension.ParseFloatArray(value);
            }
            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value).ToString());
            }


        }
    }
}


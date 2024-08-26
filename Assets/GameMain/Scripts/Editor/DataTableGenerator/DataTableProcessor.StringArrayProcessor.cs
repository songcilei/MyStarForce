using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StarForce.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class StringArrayProcessor : GenericDataProcessor<string[]>
        {
            public override bool IsSystem {
                get
                {
                    return false;
                }
            }
            //配置的类型关键字
            public override string LanguageKeyword {
                get
                {
                    return "string[]";
                }
            } 
            //C#中我们需要的类型
            public override string[] GetTypeStrings()
            {
                return new string[] { "string[]", "system.string[]" };
            }
            //excel中单元格中内容，解析为我们需要的格式
            public override string[] Parse(string value)
            {
                return DataTableExtension.ParseStringArray(value);
            }
            //数据转成二进制，用于txt转bytes格式
            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value).ToString());
            }


        }
    }
}


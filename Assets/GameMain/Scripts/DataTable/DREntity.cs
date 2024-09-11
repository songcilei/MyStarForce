//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2024-09-11 17:05:29.305
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 实体表。
    /// </summary>
    public class DREntity : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取实体编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取资源名称。
        /// </summary>
        public string AssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取状态机检测距离。
        /// </summary>
        public float FSMDistance
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取模型Id。
        /// </summary>
        public int[] ModelID
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取默认时间轴速度。
        /// </summary>
        public float Spd
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取dfgd。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取sdf。
        /// </summary>
        public float Grow
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取sgdf。
        /// </summary>
        public int[] Skills
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击。
        /// </summary>
        public int Atk
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取法伤。
        /// </summary>
        public int Mag
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取防御。
        /// </summary>
        public int Def
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取魔抗。
        /// </summary>
        public int Mdf
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取血量。
        /// </summary>
        public int Hp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取蓝量。
        /// </summary>
        public int Mp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取幸运值。
        /// </summary>
        public int Luck
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取AABB半径。
        /// </summary>
        public float Radius
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            AssetName = columnStrings[index++];
            FSMDistance = float.Parse(columnStrings[index++]);
            ModelID = DataTableExtension.ParseIntArray(columnStrings[index++]);
            Spd = float.Parse(columnStrings[index++]);
            Level = int.Parse(columnStrings[index++]);
            Grow = float.Parse(columnStrings[index++]);
            Skills = DataTableExtension.ParseIntArray(columnStrings[index++]);
            Atk = int.Parse(columnStrings[index++]);
            Mag = int.Parse(columnStrings[index++]);
            Def = int.Parse(columnStrings[index++]);
            Mdf = int.Parse(columnStrings[index++]);
            Hp = int.Parse(columnStrings[index++]);
            Mp = int.Parse(columnStrings[index++]);
            Luck = int.Parse(columnStrings[index++]);
            Radius = float.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    AssetName = binaryReader.ReadString();
                    FSMDistance = binaryReader.ReadSingle();
                    string[] ModelID_str = binaryReader.ReadString().Split(",");
                    List<int> ModelIDList = new List<int>();
                    foreach(var t in ModelID_str)
                    {
                       ModelIDList.Add(int.Parse(t));
                    }
                    ModelID = ModelIDList.ToArray();
                    Spd = binaryReader.ReadSingle();
                    Level = binaryReader.Read7BitEncodedInt32();
                    Grow = binaryReader.ReadSingle();
                    string[] Skills_str = binaryReader.ReadString().Split(",");
                    List<int> SkillsList = new List<int>();
                    foreach(var t in Skills_str)
                    {
                       SkillsList.Add(int.Parse(t));
                    }
                    Skills = SkillsList.ToArray();
                    Atk = binaryReader.Read7BitEncodedInt32();
                    Mag = binaryReader.Read7BitEncodedInt32();
                    Def = binaryReader.Read7BitEncodedInt32();
                    Mdf = binaryReader.Read7BitEncodedInt32();
                    Hp = binaryReader.Read7BitEncodedInt32();
                    Mp = binaryReader.Read7BitEncodedInt32();
                    Luck = binaryReader.Read7BitEncodedInt32();
                    Radius = binaryReader.ReadSingle();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}

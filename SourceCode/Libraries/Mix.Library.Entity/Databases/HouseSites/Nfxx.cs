using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using System;

namespace Mix.Library.Entities.Databases.HouseSites
{
    /// <summary>
    /// 农房信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "nfxx", DisableSyncStructure = true)]
    public partial class Nfxx
    {
        [JsonProperty, Column(Name = "id", StringLength = 50, IsPrimary = true)]
        [Name("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 附件id
        /// </summary>
        [JsonProperty, Column(Name = "fjid", StringLength = -2)]
        [Name("附件id")]
        public string Fjid { get; set; }

        /// <summary>
        /// 房屋层数
        /// </summary>
        [JsonProperty, Column(Name = "fwcs")]
        [Name("房屋层数")]
        public int? Fwcs { get; set; }

        /// <summary>
        /// 房屋结构
        /// </summary>
        [JsonProperty, Column(Name = "fwjg")]
        [Name("房屋结构")]
        public int? Fwjg { get; set; }

        /// <summary>
        /// 基础信息id
        /// </summary>
        [JsonProperty, Column(Name = "jcxx_id", StringLength = 50)]
        [Name("基础信息id")]
        public string JcxxId { get; set; }

        /// <summary>
        /// 流转对象
        /// </summary>
        [JsonProperty, Column(Name = "lzdx")]
        [Name("流转对象")]
        public string Lzdx { get; set; }

        /// <summary>
        /// 流转后用途
        /// </summary>
        [JsonProperty, Column(Name = "lzhyt")]
        [Name("流转后用途")]
        public int? Lzhyt { get; set; }

        /// <summary>
        /// 流转面积
        /// </summary>
        [JsonProperty, Column(Name = "lzmj", DbType = "numeric")]
        [Name("流转面积")]
        public decimal? Lzmj { get; set; }

        /// <summary>
        /// 流转前用途
        /// </summary>
        [JsonProperty, Column(Name = "lzqyt")]
        [Name("流转前用途")]
        public int? Lzqyt { get; set; }

        /// <summary>
        /// 流转形式
        /// </summary>
        [JsonProperty, Column(Name = "lzxs")]
        [Name("流转形式")]
        public int? Lzxs { get; set; }

        /// <summary>
        /// 农房面积
        /// </summary>
        [JsonProperty, Column(Name = "nfmj", DbType = "numeric")]
        [Name("农房面积")]
        public decimal? Nfmj { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty, Column(Name = "px")]
        [Name("排序")]
        public int? Px { get; set; }

        /// <summary>
        /// 是否办理产权
        /// </summary>
        [JsonProperty, Column(Name = "sfblcq")]
        [Name("是否办理产权")]
        public bool? Sfblcq { get; set; }

        /// <summary>
        /// 是否空闲
        /// </summary>
        [JsonProperty, Column(Name = "sfkx")]
        [Name("是否空闲")]
        public bool? Sfkx { get; set; }

        /// <summary>
        /// 是否流转
        /// </summary>
        [JsonProperty, Column(Name = "sflz")]
        [Name("是否流转")]
        public bool? Sflz { get; set; }

        /// <summary>
        /// 是否危房
        /// </summary>
        [JsonProperty, Column(Name = "sfwf")]
        [Name("是否危房")]
        public bool? Sfwf { get; set; }

        /// <summary>
        /// 是否意愿流转
        /// </summary>
        [JsonProperty, Column(Name = "sfyylz")]
        [Name("是否意愿流转")]
        public bool? Sfyylz { get; set; }

        /// <summary>
        /// 是否组织成员
        /// </summary>
        [JsonProperty, Column(Name = "sfzzcy")]
        [Name("是否组织成员")]
        public bool? Sfzzcy { get; set; }

        /// <summary>
        /// 数量描述
        /// </summary>
        [JsonProperty, Column(Name = "slms")]
        [Name("数量描述")]
        public string Slms { get; set; }

        /// <summary>
        /// 宅基地来源
        /// </summary>
        [JsonProperty, Column(Name = "zjdly")]
        [Name("宅基地来源")]
        public int? Zjdly { get; set; }

        /// <summary>
        /// 宅基地面积
        /// </summary>
        [JsonProperty, Column(Name = "zjdmj", DbType = "numeric")]
        [Name("宅基地面积")]
        public decimal? Zjdmj { get; set; }
    }
}
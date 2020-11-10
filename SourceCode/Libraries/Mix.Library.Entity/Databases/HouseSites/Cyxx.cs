using System;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Mix.Library.Entities.Databases.HouseSites
{
    /// <summary>
    /// 成员信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "cyxx", DisableSyncStructure = true)]
    public partial class Cyxx
    {
        [JsonProperty, Column(Name = "id", StringLength = 50, IsPrimary = true)]
        [Name("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 从事工作
        /// </summary>
        [JsonProperty, Column(Name = "csgz")]
        [Name("从事工作")]
        public string Csgz { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonProperty, Column(Name = "csrq")]
        [Name("出生日期")]
        public DateTime? Csrq { get; set; }

        /// <summary>
        /// 基础信息id
        /// </summary>
        [JsonProperty, Column(Name = "jcxx_id", StringLength = 50)]
        [Name("基础信息id")]
        public string JcxxId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty, Column(Name = "px")]
        [Name("排序")]
        public int? Px { get; set; }

        /// <summary>
        /// 是否党员
        /// </summary>
        [JsonProperty, Column(Name = "sfdy")]
        [Name("是否党员")]
        public bool? Sfdy { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty, Column(Name = "sjhm", StringLength = 20)]
        [Name("手机号码")]
        public string Sjhm { get; set; }

        /// <summary>
        /// 成员性别，1男0女
        /// </summary>
        [JsonProperty, Column(Name = "xb")]
        [Name("成员性别，1男0女")]
        public int? Xb { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [JsonProperty, Column(Name = "xl")]
        [Name("学历")]
        public int? Xl { get; set; }

        /// <summary>
        /// 成员姓名
        /// </summary>
        [JsonProperty, Column(Name = "xm")]
        [Name("成员姓名")]
        public string Xm { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty, Column(Name = "zjhm", StringLength = 20)]
        [Name("证件号码")]
        public string Zjhm { get; set; }
    }
}
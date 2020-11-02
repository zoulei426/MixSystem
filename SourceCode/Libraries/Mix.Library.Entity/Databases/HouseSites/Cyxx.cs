using System;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using Mix.Core;

namespace Mix.Library.Entities.Databases.HouseSites
{
    /// <summary>
    /// 成员信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "cyxx", DisableSyncStructure = false)]
    public partial class Cyxx : Entity<Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty, Column(Name = "id")]
        public new Guid Id { get; set; }

        /// <summary>
        /// 从事工作
        /// </summary>
        [JsonProperty, Column(Name = "csgz")]
        public string Csgz { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonProperty, Column(Name = "csrq")]
        public DateTime? Csrq { get; set; }

        /// <summary>
        /// 基础信息id
        /// </summary>
        [JsonProperty, Column(Name = "jcxx_id", StringLength = 50)]
        public Guid JcxxId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty, Column(Name = "px")]
        public int? Px { get; set; }

        /// <summary>
        /// 是否党员
        /// </summary>
        [JsonProperty, Column(Name = "sfdy")]
        public bool? Sfdy { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty, Column(Name = "sjhm", StringLength = 20)]
        public string Sjhm { get; set; }

        /// <summary>
        /// 成员性别，1男0女
        /// </summary>
        [JsonProperty, Column(Name = "xb")]
        public int? Xb { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [JsonProperty, Column(Name = "xl")]
        public int? Xl { get; set; }

        /// <summary>
        /// 成员姓名
        /// </summary>
        [JsonProperty, Column(Name = "xm")]
        public string Xm { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty, Column(Name = "zjhm", StringLength = 20)]
        public string Zjhm { get; set; }
    }
}
using FreeSql.DataAnnotations;
using Mix.Core;
using Newtonsoft.Json;

namespace Mix.Library.Entities.Databases.HouseSites
{

    /// <summary>
    /// 农房信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "nfxx", DisableSyncStructure = false)]
    public partial class Nfxx : Entity<string>
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty, Column(Name = "id", StringLength = 50, IsPrimary = true)]
        public new string Id { get; set; }

        /// <summary>
        /// 附件id
        /// </summary>
        [JsonProperty, Column(Name = "fjid", StringLength = -2)]
        public string Fjid { get; set; }

        /// <summary>
        /// 房屋层数
        /// </summary>
        [JsonProperty, Column(Name = "fwcs")]
        public int? Fwcs { get; set; }

        /// <summary>
        /// 房屋结构
        /// </summary>
        [JsonProperty, Column(Name = "fwjg")]
        public int? Fwjg { get; set; }

        /// <summary>
        /// 基础信息id
        /// </summary>
        [JsonProperty, Column(Name = "jcxx_id", StringLength = 50)]
        public string JcxxId { get; set; }

        /// <summary>
        /// 流转对象
        /// </summary>
        [JsonProperty, Column(Name = "lzdx")]
        public string Lzdx { get; set; }

        /// <summary>
        /// 流转后用途
        /// </summary>
        [JsonProperty, Column(Name = "lzhyt")]
        public int? Lzhyt { get; set; }

        /// <summary>
        /// 流转面积
        /// </summary>
        [JsonProperty, Column(Name = "lzmj", DbType = "numeric")]
        public decimal? Lzmj { get; set; }

        /// <summary>
        /// 流转前用途
        /// </summary>
        [JsonProperty, Column(Name = "lzqyt")]
        public int? Lzqyt { get; set; }

        /// <summary>
        /// 流转形式
        /// </summary>
        [JsonProperty, Column(Name = "lzxs")]
        public int? Lzxs { get; set; }

        /// <summary>
        /// 农房面积
        /// </summary>
        [JsonProperty, Column(Name = "nfmj", DbType = "numeric")]
        public decimal? Nfmj { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty, Column(Name = "px")]
        public int? Px { get; set; }

        /// <summary>
        /// 是否办理产权
        /// </summary>
        [JsonProperty, Column(Name = "sfblcq")]
        public bool? Sfblcq { get; set; }

        /// <summary>
        /// 是否空闲
        /// </summary>
        [JsonProperty, Column(Name = "sfkx")]
        public bool? Sfkx { get; set; }

        /// <summary>
        /// 是否流转
        /// </summary>
        [JsonProperty, Column(Name = "sflz")]
        public bool? Sflz { get; set; }

        /// <summary>
        /// 是否危房
        /// </summary>
        [JsonProperty, Column(Name = "sfwf")]
        public bool? Sfwf { get; set; }

        /// <summary>
        /// 是否意愿流转
        /// </summary>
        [JsonProperty, Column(Name = "sfyylz")]
        public bool? Sfyylz { get; set; }

        /// <summary>
        /// 是否组织成员
        /// </summary>
        [JsonProperty, Column(Name = "sfzzcy")]
        public bool? Sfzzcy { get; set; }

        /// <summary>
        /// 数量描述
        /// </summary>
        [JsonProperty, Column(Name = "slms")]
        public string Slms { get; set; }

        /// <summary>
        /// 宅基地来源
        /// </summary>
        [JsonProperty, Column(Name = "zjdly")]
        public int? Zjdly { get; set; }

        /// <summary>
        /// 宅基地面积
        /// </summary>
        [JsonProperty, Column(Name = "zjdmj", DbType = "numeric")]
        public decimal? Zjdmj { get; set; }

    }

}

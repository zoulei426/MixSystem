using FreeSql.DataAnnotations;
using Mix.Core;
using Newtonsoft.Json;
using System;

namespace Mix.Library.Entities.Databases.HouseSites
{
    /// <summary>
    /// 基础信息表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "jcxx", DisableSyncStructure = false)]
    public partial class Jcxx : Entity<Guid>
    {
        /// <summary>
        /// 承包地流转对象
        /// </summary>
        //[JsonProperty, Column(Name = "cbdlzdxmc")]
        public string Cbdlzdxmc { get; set; }

        /// <summary>
        /// 承包地流转面积
        /// </summary>
        //[JsonProperty, Column(Name = "cbdlzmj", DbType = "numeric")]
        public decimal? Cbdlzmj { get; set; }

        /// <summary>
        /// 承包地流转形式
        /// </summary>
        //[JsonProperty, Column(Name = "cbdlzxs")]
        public int? Cbdlzxs { get; set; }

        /// <summary>
        /// 承包地面积
        /// </summary>
        //[JsonProperty, Column(Name = "cbdmj", DbType = "numeric")]
        public decimal? Cbdmj { get; set; }

        /// <summary>
        /// 承包地是否流转
        /// </summary>
        //[JsonProperty, Column(Name = "cbdsflz")]
        public bool? Cbdsflz { get; set; }

        /// <summary>
        /// 承包地是否是本集体经济组织成员
        /// </summary>
        //[JsonProperty, Column(Name = "cbdsfsbjtjjzzcy")]
        public bool? Cbdsfsbjtjjzzcy { get; set; }

        /// <summary>
        /// 承包地种植类型
        /// </summary>
        //[JsonProperty, Column(Name = "cbdzzlx")]
        public int? Cbdzzlx { get; set; }

        /// <summary>
        /// 财产收入
        /// </summary>
        //[JsonProperty, Column(Name = "ccxsr", DbType = "numeric")]
        public decimal? Ccxsr { get; set; }

        /// <summary>
        /// 地址编码
        /// </summary>
        [JsonProperty, Column(Name = "dzbm")]
        public string Dzbm { get; set; }

        /// <summary>
        /// 地址详情
        /// </summary>
        [JsonProperty, Column(Name = "dzxq")]
        public string Dzxq { get; set; }

        /// <summary>
        /// 分红收入
        /// </summary>
        [JsonProperty, Column(Name = "fhxsr", DbType = "numeric")]
        public decimal? Fhxsr { get; set; }

        /// <summary>
        /// 附件id
        /// </summary>
        [JsonProperty, Column(Name = "fjid", StringLength = 50)]
        public string Fjid { get; set; }

        /// <summary>
        /// 房屋坐标
        /// </summary>
        [JsonProperty, Column(Name = "fwzb")]
        public string Fwzb { get; set; }

        /// <summary>
        /// 工资收入
        /// </summary>
        [JsonProperty, Column(Name = "gzxsr", DbType = "numeric")]
        public decimal? Gzxsr { get; set; }

        /// <summary>
        /// 户主姓名
        /// </summary>
        [JsonProperty, Column(Name = "hzxm")]
        public string Hzxm { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [JsonProperty, Column(Name = "jd")]
        public string Jd { get; set; }

        /// <summary>
        /// 家庭年收入
        /// </summary>
        [JsonProperty, Column(Name = "jtnsr", DbType = "numeric")]
        public decimal? Jtnsr { get; set; }

        /// <summary>
        /// 家庭人数
        /// </summary>
        [JsonProperty, Column(Name = "jtrs")]
        public int? Jtrs { get; set; }

        /// <summary>
        /// 经营收入
        /// </summary>
        [JsonProperty, Column(Name = "jyxsr", DbType = "numeric")]
        public decimal? Jyxsr { get; set; }

        /// <summary>
        /// 林地流转对象
        /// </summary>
        [JsonProperty, Column(Name = "ldlzdxmc")]
        public string Ldlzdxmc { get; set; }

        /// <summary>
        /// 林地流转面积
        /// </summary>
        [JsonProperty, Column(Name = "ldlzmj", DbType = "numeric")]
        public decimal? Ldlzmj { get; set; }

        /// <summary>
        /// 林地流转形式
        /// </summary>
        [JsonProperty, Column(Name = "ldlzxs")]
        public int? Ldlzxs { get; set; }

        /// <summary>
        /// 林地面积
        /// </summary>
        [JsonProperty, Column(Name = "ldmj", DbType = "numeric")]
        public decimal? Ldmj { get; set; }

        /// <summary>
        /// 林地是否流转
        /// </summary>
        [JsonProperty, Column(Name = "ldsflz")]
        public bool? Ldsflz { get; set; }

        /// <summary>
        /// 林地是否是本集体经济组织成员
        /// </summary>
        [JsonProperty, Column(Name = "ldsfsbjtjjzzcy")]
        public bool? Ldsfsbjtjjzzcy { get; set; }

        /// <summary>
        /// 调查员openid
        /// </summary>
        [JsonProperty, Column(Name = "open_id")]
        public string OpenId { get; set; }

        /// <summary>
        /// 是否初始导入
        /// </summary>
        [JsonProperty, Column(Name = "sfcsdr")]
        public bool? Sfcsdr { get; set; }

        /// <summary>
        /// 是否贫困户
        /// </summary>
        [JsonProperty, Column(Name = "sfpkh")]
        public bool? Sfpkh { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty, Column(Name = "sjhm", StringLength = 20)]
        public string Sjhm { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty, Column(Name = "wd")]
        public string Wd { get; set; }

        /// <summary>
        /// 政策收入
        /// </summary>
        [JsonProperty, Column(Name = "zcxsr", DbType = "numeric")]
        public decimal? Zcxsr { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty, Column(Name = "zjhm")]
        public string Zjhm { get; set; }
    }
}
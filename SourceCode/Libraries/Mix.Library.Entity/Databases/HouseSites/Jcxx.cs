using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using System;

namespace Mix.Library.Entities.Databases.HouseSites
{
    /// <summary>
    /// 基础信息表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "jcxx", DisableSyncStructure = true)]
    public partial class Jcxx
    {
        [JsonProperty, Column(Name = "id", StringLength = 50, IsPrimary = true)]
        [Name("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 承包地流转对象
        /// </summary>
        [JsonProperty, Column(Name = "cbdlzdxmc")]
        [Name("承包地流转对象")]
        public string Cbdlzdxmc { get; set; }

        /// <summary>
        /// 承包地流转面积
        /// </summary>
        [JsonProperty, Column(Name = "cbdlzmj", DbType = "numeric")]
        [Name("承包地流转面积")]
        public decimal? Cbdlzmj { get; set; }

        /// <summary>
        /// 承包地流转形式
        /// </summary>
        [JsonProperty, Column(Name = "cbdlzxs")]
        [Name("承包地流转形式")]
        public int? Cbdlzxs { get; set; }

        /// <summary>
        /// 承包地面积
        /// </summary>
        [JsonProperty, Column(Name = "cbdmj", DbType = "numeric")]
        [Name("承包地面积")]
        public decimal? Cbdmj { get; set; }

        /// <summary>
        /// 承包地是否流转
        /// </summary>
        [JsonProperty, Column(Name = "cbdsflz")]
        [Name("承包地是否流转")]
        public bool? Cbdsflz { get; set; }

        /// <summary>
        /// 承包地是否是本集体经济组织成员
        /// </summary>
        [JsonProperty, Column(Name = "cbdsfsbjtjjzzcy")]
        [Name("承包地是否是本集体经济组织成员")]
        public bool? Cbdsfsbjtjjzzcy { get; set; }

        /// <summary>
        /// 承包地种植类型
        /// </summary>
        [JsonProperty, Column(Name = "cbdzzlx")]
        [Name("承包地种植类型")]
        public int? Cbdzzlx { get; set; }

        /// <summary>
        /// 财产收入
        /// </summary>
        [JsonProperty, Column(Name = "ccxsr", DbType = "numeric")]
        [Name("财产收入")]
        public decimal? Ccxsr { get; set; }

        /// <summary>
        /// 地址编码
        /// </summary>
        [JsonProperty, Column(Name = "dzbm")]
        [Name("地址编码")]
        public string Dzbm { get; set; }

        /// <summary>
        /// 地址详情
        /// </summary>
        [JsonProperty, Column(Name = "dzxq")]
        [Name("地址详情")]
        public string Dzxq { get; set; }

        /// <summary>
        /// 分红收入
        /// </summary>
        [JsonProperty, Column(Name = "fhxsr", DbType = "numeric")]
        [Name("分红收入")]
        public decimal? Fhxsr { get; set; }

        /// <summary>
        /// 附件id
        /// </summary>
        [JsonProperty, Column(Name = "fjid", StringLength = 50)]
        [Name("附件id")]
        public string Fjid { get; set; }

        /// <summary>
        /// 房屋坐标
        /// </summary>
        [JsonProperty, Column(Name = "fwzb")]
        [Name("房屋坐标")]
        public string Fwzb { get; set; }

        /// <summary>
        /// 工资收入
        /// </summary>
        [JsonProperty, Column(Name = "gzxsr", DbType = "numeric")]
        [Name("工资收入")]
        public decimal? Gzxsr { get; set; }

        /// <summary>
        /// 户主姓名
        /// </summary>
        [JsonProperty, Column(Name = "hzxm")]
        [Name("户主姓名")]
        public string Hzxm { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [JsonProperty, Column(Name = "jd")]
        [Name("经度")]
        public string Jd { get; set; }

        /// <summary>
        /// 家庭年收入
        /// </summary>
        [JsonProperty, Column(Name = "jtnsr", DbType = "numeric")]
        [Name("家庭年收入")]
        public decimal? Jtnsr { get; set; }

        /// <summary>
        /// 家庭人数
        /// </summary>
        [JsonProperty, Column(Name = "jtrs")]
        [Name("家庭人数")]
        public int? Jtrs { get; set; }

        /// <summary>
        /// 经营收入
        /// </summary>
        [JsonProperty, Column(Name = "jyxsr", DbType = "numeric")]
        [Name("经营收入")]
        public decimal? Jyxsr { get; set; }

        /// <summary>
        /// 林地流转对象
        /// </summary>
        [JsonProperty, Column(Name = "ldlzdxmc")]
        [Name("林地流转对象")]
        public string Ldlzdxmc { get; set; }

        /// <summary>
        /// 林地流转面积
        /// </summary>
        [JsonProperty, Column(Name = "ldlzmj", DbType = "numeric")]
        [Name("林地流转面积")]
        public decimal? Ldlzmj { get; set; }

        /// <summary>
        /// 林地流转形式
        /// </summary>
        [JsonProperty, Column(Name = "ldlzxs")]
        [Name("林地流转形式")]
        public int? Ldlzxs { get; set; }

        /// <summary>
        /// 林地面积
        /// </summary>
        [JsonProperty, Column(Name = "ldmj", DbType = "numeric")]
        [Name("林地面积")]
        public decimal? Ldmj { get; set; }

        /// <summary>
        /// 林地是否流转
        /// </summary>
        [JsonProperty, Column(Name = "ldsflz")]
        [Name("林地是否流转")]
        public bool? Ldsflz { get; set; }

        /// <summary>
        /// 林地是否是本集体经济组织成员
        /// </summary>
        [JsonProperty, Column(Name = "ldsfsbjtjjzzcy")]
        [Name("林地是否是本集体经济组织成员")]
        public bool? Ldsfsbjtjjzzcy { get; set; }

        /// <summary>
        /// 调查员openid
        /// </summary>
        [JsonProperty, Column(Name = "open_id")]
        [Name("调查员openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 是否初始导入
        /// </summary>
        [JsonProperty, Column(Name = "sfcsdr")]
        [Name("是否初始导入")]
        public bool? Sfcsdr { get; set; }

        /// <summary>
        /// 是否贫困户
        /// </summary>
        [JsonProperty, Column(Name = "sfpkh")]
        [Name("是否贫困户")]
        public bool? Sfpkh { get; set; }

        /// <summary>
        /// 是否完善
        /// </summary>
        [JsonProperty, Column(Name = "sfws")]
        [Name("是否完善")]
        public bool? Sfws { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty, Column(Name = "sjhm", StringLength = 20)]
        [Name("手机号码")]
        public string Sjhm { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty, Column(Name = "wd")]
        [Name("纬度")]
        public string Wd { get; set; }

        /// <summary>
        /// 政策收入
        /// </summary>
        [JsonProperty, Column(Name = "zcxsr", DbType = "numeric")]
        [Name("政策收入")]
        public decimal? Zcxsr { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty, Column(Name = "zjhm")]
        [Name("证件号码")]
        public string Zjhm { get; set; }
    }
}
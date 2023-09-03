using System.Xml.Serialization;

namespace CtaCargo.CctImportacao.Domain.Model.Iata.HouseMasterManifest;

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:housemanifest:1")]
[System.Xml.Serialization.XmlRootAttribute("HouseManifest", Namespace = "iata:housemanifest:1", IsNullable = false)]
public partial class HouseManifestType
{

    private MessageHeaderDocumentType messageHeaderDocumentField;

    private BusinessHeaderDocumentType businessHeaderDocumentField;

    private MasterConsignmentType masterConsignmentField;

    /// <remarks/>
    public MessageHeaderDocumentType MessageHeaderDocument
    {
        get
        {
            return this.messageHeaderDocumentField;
        }
        set
        {
            this.messageHeaderDocumentField = value;
        }
    }

    /// <remarks/>
    public BusinessHeaderDocumentType BusinessHeaderDocument
    {
        get
        {
            return this.businessHeaderDocumentField;
        }
        set
        {
            this.businessHeaderDocumentField = value;
        }
    }

    /// <remarks/>
    public MasterConsignmentType MasterConsignment
    {
        get
        {
            return this.masterConsignmentField;
        }
        set
        {
            this.masterConsignmentField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("MessageHeaderDocument", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class MessageHeaderDocumentType
{

    private IDType idField;

    private TextType nameField;

    private DocumentCodeType typeCodeField;

    private System.DateTime issueDateTimeField;

    private CodeType purposeCodeField;

    private IDType versionIDField;

    private IDType conversationIDField;

    private SenderPartyType[] senderPartyField;

    private RecipientPartyType[] recipientPartyField;

    /// <remarks/>
    public IDType ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public TextType Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public DocumentCodeType TypeCode
    {
        get
        {
            return this.typeCodeField;
        }
        set
        {
            this.typeCodeField = value;
        }
    }

    /// <remarks/>
    [XmlIgnore]
    public System.DateTime IssueDateTime
    {
        get
        {
            return this.issueDateTimeField;
        }
        set
        {
            this.issueDateTimeField = value;
        }
    }

    [XmlElementAttribute("IssueDateTime")]
    public string XIssueDateTime
    {
        get { return issueDateTimeField.ToString(XmlUtil.DateTimeFormaRFB); }
        set { issueDateTimeField = System.DateTime.Parse(value); }
    }
    /// <remarks/>
    public CodeType PurposeCode
    {
        get
        {
            return this.purposeCodeField;
        }
        set
        {
            this.purposeCodeField = value;
        }
    }

    /// <remarks/>
    public IDType VersionID
    {
        get
        {
            return this.versionIDField;
        }
        set
        {
            this.versionIDField = value;
        }
    }

    /// <remarks/>
    public IDType ConversationID
    {
        get
        {
            return this.conversationIDField;
        }
        set
        {
            this.conversationIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("SenderParty")]
    public SenderPartyType[] SenderParty
    {
        get
        {
            return this.senderPartyField;
        }
        set
        {
            this.senderPartyField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("RecipientParty")]
    public RecipientPartyType[] RecipientParty
    {
        get
        {
            return this.recipientPartyField;
        }
        set
        {
            this.recipientPartyField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:8")]
public partial class IDType
{

    private string schemeIDField;

    private string schemeNameField;

    private AgencyIdentificationCodeContentType schemeAgencyIDField;

    private bool schemeAgencyIDFieldSpecified;

    private string schemeAgencyNameField;

    private string schemeVersionIDField;

    private string schemeDataURIField;

    private string schemeURIField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string schemeID
    {
        get
        {
            return this.schemeIDField;
        }
        set
        {
            this.schemeIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string schemeName
    {
        get
        {
            return this.schemeNameField;
        }
        set
        {
            this.schemeNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public AgencyIdentificationCodeContentType schemeAgencyID
    {
        get
        {
            return this.schemeAgencyIDField;
        }
        set
        {
            this.schemeAgencyIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool schemeAgencyIDSpecified
    {
        get
        {
            return this.schemeAgencyIDFieldSpecified;
        }
        set
        {
            this.schemeAgencyIDFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string schemeAgencyName
    {
        get
        {
            return this.schemeAgencyNameField;
        }
        set
        {
            this.schemeAgencyNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string schemeVersionID
    {
        get
        {
            return this.schemeVersionIDField;
        }
        set
        {
            this.schemeVersionIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
    public string schemeDataURI
    {
        get
        {
            return this.schemeDataURIField;
        }
        set
        {
            this.schemeDataURIField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
    public string schemeURI
    {
        get
        {
            return this.schemeURIField;
        }
        set
        {
            this.schemeURIField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute(DataType = "token")]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:6:3055:D09A")]
[System.Xml.Serialization.XmlRootAttribute("AgencyIdentificationCode", Namespace = "urn:un:unece:uncefact:codelist:standard:6:3055:D09A", IsNullable = false)]
public enum AgencyIdentificationCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1")]
    Item1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2")]
    Item2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3")]
    Item3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4")]
    Item4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5")]
    Item5,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6")]
    Item6,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("7")]
    Item7,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("8")]
    Item8,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("9")]
    Item9,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("10")]
    Item10,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("11")]
    Item11,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("12")]
    Item12,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("13")]
    Item13,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("14")]
    Item14,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("15")]
    Item15,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("16")]
    Item16,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("17")]
    Item17,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("18")]
    Item18,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("19")]
    Item19,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("20")]
    Item20,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("21")]
    Item21,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("22")]
    Item22,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("23")]
    Item23,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("24")]
    Item24,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("25")]
    Item25,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("26")]
    Item26,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("27")]
    Item27,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("28")]
    Item28,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("29")]
    Item29,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("30")]
    Item30,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("31")]
    Item31,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("32")]
    Item32,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("33")]
    Item33,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("34")]
    Item34,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("35")]
    Item35,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("36")]
    Item36,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("37")]
    Item37,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("38")]
    Item38,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("39")]
    Item39,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("40")]
    Item40,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("41")]
    Item41,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("42")]
    Item42,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("43")]
    Item43,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("44")]
    Item44,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("45")]
    Item45,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("46")]
    Item46,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("47")]
    Item47,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("48")]
    Item48,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("49")]
    Item49,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("50")]
    Item50,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("51")]
    Item51,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("52")]
    Item52,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("53")]
    Item53,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("54")]
    Item54,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("55")]
    Item55,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("56")]
    Item56,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("57")]
    Item57,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("58")]
    Item58,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("59")]
    Item59,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("60")]
    Item60,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("61")]
    Item61,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("62")]
    Item62,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("63")]
    Item63,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("64")]
    Item64,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("65")]
    Item65,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("66")]
    Item66,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("67")]
    Item67,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("68")]
    Item68,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("69")]
    Item69,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("70")]
    Item70,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("71")]
    Item71,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("72")]
    Item72,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("73")]
    Item73,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("74")]
    Item74,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("75")]
    Item75,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("76")]
    Item76,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("77")]
    Item77,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("78")]
    Item78,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("79")]
    Item79,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("80")]
    Item80,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("81")]
    Item81,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("82")]
    Item82,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("83")]
    Item83,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("84")]
    Item84,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("85")]
    Item85,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("86")]
    Item86,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("87")]
    Item87,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("88")]
    Item88,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("89")]
    Item89,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("90")]
    Item90,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("91")]
    Item91,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("92")]
    Item92,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("93")]
    Item93,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("94")]
    Item94,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("95")]
    Item95,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("96")]
    Item96,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("97")]
    Item97,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("98")]
    Item98,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("99")]
    Item99,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("100")]
    Item100,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("101")]
    Item101,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("102")]
    Item102,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("103")]
    Item103,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("104")]
    Item104,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("105")]
    Item105,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("106")]
    Item106,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("107")]
    Item107,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("108")]
    Item108,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("109")]
    Item109,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("110")]
    Item110,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("111")]
    Item111,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("112")]
    Item112,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("113")]
    Item113,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("114")]
    Item114,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("116")]
    Item116,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("117")]
    Item117,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("118")]
    Item118,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("119")]
    Item119,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("120")]
    Item120,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("121")]
    Item121,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("122")]
    Item122,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("123")]
    Item123,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("124")]
    Item124,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("125")]
    Item125,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("126")]
    Item126,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("127")]
    Item127,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("128")]
    Item128,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("129")]
    Item129,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("130")]
    Item130,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("131")]
    Item131,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("132")]
    Item132,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("133")]
    Item133,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("134")]
    Item134,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("135")]
    Item135,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("136")]
    Item136,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("137")]
    Item137,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("138")]
    Item138,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("139")]
    Item139,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("140")]
    Item140,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("141")]
    Item141,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("142")]
    Item142,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("143")]
    Item143,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("144")]
    Item144,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("145")]
    Item145,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("146")]
    Item146,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("147")]
    Item147,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("148")]
    Item148,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("149")]
    Item149,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("150")]
    Item150,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("151")]
    Item151,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("152")]
    Item152,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("153")]
    Item153,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("154")]
    Item154,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("155")]
    Item155,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("156")]
    Item156,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("157")]
    Item157,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("158")]
    Item158,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("159")]
    Item159,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("160")]
    Item160,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("161")]
    Item161,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("162")]
    Item162,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("163")]
    Item163,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("164")]
    Item164,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("165")]
    Item165,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("166")]
    Item166,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("167")]
    Item167,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("168")]
    Item168,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("169")]
    Item169,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("170")]
    Item170,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("171")]
    Item171,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("172")]
    Item172,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("173")]
    Item173,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("174")]
    Item174,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("175")]
    Item175,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("176")]
    Item176,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("177")]
    Item177,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("178")]
    Item178,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("179")]
    Item179,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("180")]
    Item180,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("181")]
    Item181,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("182")]
    Item182,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("183")]
    Item183,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("184")]
    Item184,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("185")]
    Item185,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("186")]
    Item186,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("187")]
    Item187,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("188")]
    Item188,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("189")]
    Item189,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("190")]
    Item190,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("191")]
    Item191,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("192")]
    Item192,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("193")]
    Item193,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("194")]
    Item194,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("195")]
    Item195,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("196")]
    Item196,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("197")]
    Item197,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("198")]
    Item198,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("199")]
    Item199,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("200")]
    Item200,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("201")]
    Item201,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("202")]
    Item202,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("203")]
    Item203,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("204")]
    Item204,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("205")]
    Item205,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("206")]
    Item206,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("207")]
    Item207,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("208")]
    Item208,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("209")]
    Item209,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("210")]
    Item210,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("211")]
    Item211,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("212")]
    Item212,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("213")]
    Item213,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("214")]
    Item214,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("215")]
    Item215,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("216")]
    Item216,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("217")]
    Item217,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("218")]
    Item218,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("219")]
    Item219,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("220")]
    Item220,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("221")]
    Item221,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("222")]
    Item222,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("223")]
    Item223,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("224")]
    Item224,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("225")]
    Item225,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("226")]
    Item226,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("227")]
    Item227,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("228")]
    Item228,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("229")]
    Item229,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("230")]
    Item230,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("231")]
    Item231,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("232")]
    Item232,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("233")]
    Item233,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("234")]
    Item234,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("235")]
    Item235,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("236")]
    Item236,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("237")]
    Item237,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("238")]
    Item238,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("239")]
    Item239,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("240")]
    Item240,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("241")]
    Item241,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("244")]
    Item244,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("245")]
    Item245,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("246")]
    Item246,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("247")]
    Item247,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("248")]
    Item248,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("249")]
    Item249,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("250")]
    Item250,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("251")]
    Item251,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("252")]
    Item252,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("253")]
    Item253,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("254")]
    Item254,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("255")]
    Item255,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("256")]
    Item256,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("257")]
    Item257,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("258")]
    Item258,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("259")]
    Item259,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("260")]
    Item260,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("261")]
    Item261,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("262")]
    Item262,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("263")]
    Item263,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("264")]
    Item264,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("265")]
    Item265,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("266")]
    Item266,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("267")]
    Item267,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("268")]
    Item268,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("269")]
    Item269,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("270")]
    Item270,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("271")]
    Item271,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("272")]
    Item272,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("273")]
    Item273,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("274")]
    Item274,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("275")]
    Item275,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("276")]
    Item276,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("277")]
    Item277,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("278")]
    Item278,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("279")]
    Item279,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("280")]
    Item280,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("281")]
    Item281,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("282")]
    Item282,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("283")]
    Item283,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("284")]
    Item284,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("285")]
    Item285,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("286")]
    Item286,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("287")]
    Item287,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("288")]
    Item288,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("289")]
    Item289,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("290")]
    Item290,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("291")]
    Item291,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("292")]
    Item292,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("293")]
    Item293,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("294")]
    Item294,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("295")]
    Item295,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("296")]
    Item296,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("297")]
    Item297,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("298")]
    Item298,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("299")]
    Item299,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("300")]
    Item300,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("301")]
    Item301,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("302")]
    Item302,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("303")]
    Item303,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("304")]
    Item304,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("305")]
    Item305,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("306")]
    Item306,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("307")]
    Item307,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("309")]
    Item309,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("310")]
    Item310,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("311")]
    Item311,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("312")]
    Item312,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("313")]
    Item313,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("314")]
    Item314,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("315")]
    Item315,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("316")]
    Item316,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("317")]
    Item317,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("318")]
    Item318,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("319")]
    Item319,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("320")]
    Item320,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("321")]
    Item321,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("322")]
    Item322,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("323")]
    Item323,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("324")]
    Item324,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("325")]
    Item325,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("326")]
    Item326,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("327")]
    Item327,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("328")]
    Item328,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("329")]
    Item329,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("330")]
    Item330,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("331")]
    Item331,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("332")]
    Item332,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("333")]
    Item333,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("334")]
    Item334,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("335")]
    Item335,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("336")]
    Item336,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("337")]
    Item337,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("338")]
    Item338,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("339")]
    Item339,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("340")]
    Item340,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("341")]
    Item341,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("342")]
    Item342,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("343")]
    Item343,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("344")]
    Item344,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("345")]
    Item345,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("346")]
    Item346,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("347")]
    Item347,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("348")]
    Item348,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("349")]
    Item349,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("350")]
    Item350,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("351")]
    Item351,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("352")]
    Item352,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("353")]
    Item353,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("354")]
    Item354,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("355")]
    Item355,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("356")]
    Item356,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("357")]
    Item357,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("358")]
    Item358,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("359")]
    Item359,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("360")]
    Item360,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("361")]
    Item361,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("362")]
    Item362,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("363")]
    Item363,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("364")]
    Item364,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("365")]
    Item365,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("366")]
    Item366,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("367")]
    Item367,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("368")]
    Item368,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("369")]
    Item369,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("370")]
    Item370,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("371")]
    Item371,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("372")]
    Item372,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("373")]
    Item373,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("374")]
    Item374,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("375")]
    Item375,

    /// <remarks/>
    ZZZ,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("ReferenceDocument", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class ReferenceDocumentType
{

    private IDType idField;

    private System.DateTime issueDateTimeField;

    private bool issueDateTimeFieldSpecified;

    private DocumentCodeType typeCodeField;

    private TextType nameField;

    private bool copyIndicatorField;

    private bool copyIndicatorFieldSpecified;

    /// <remarks/>
    public IDType ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public System.DateTime IssueDateTime
    {
        get
        {
            return this.issueDateTimeField;
        }
        set
        {
            this.issueDateTimeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IssueDateTimeSpecified
    {
        get
        {
            return this.issueDateTimeFieldSpecified;
        }
        set
        {
            this.issueDateTimeFieldSpecified = value;
        }
    }

    /// <remarks/>
    public DocumentCodeType TypeCode
    {
        get
        {
            return this.typeCodeField;
        }
        set
        {
            this.typeCodeField = value;
        }
    }

    /// <remarks/>
    public TextType Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public bool CopyIndicator
    {
        get
        {
            return this.copyIndicatorField;
        }
        set
        {
            this.copyIndicatorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CopyIndicatorSpecified
    {
        get
        {
            return this.copyIndicatorFieldSpecified;
        }
        set
        {
            this.copyIndicatorFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:QualifiedDataType:7")]
public partial class DocumentCodeType
{

    private string listIDField;

    private AgencyIdentificationCodeContentType listAgencyIDField;

    private bool listAgencyIDFieldSpecified;

    private string listVersionIDField;

    private string nameField;

    private string listURIField;

    private DocumentNameCodeContentType valueField;

    public DocumentCodeType()
    {
        this.listIDField = "1001";
        this.listAgencyIDField = AgencyIdentificationCodeContentType.Item6;
        this.listVersionIDField = "D09A";
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string listID
    {
        get
        {
            return this.listIDField;
        }
        set
        {
            this.listIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public AgencyIdentificationCodeContentType listAgencyID
    {
        get
        {
            return this.listAgencyIDField;
        }
        set
        {
            this.listAgencyIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool listAgencyIDSpecified
    {
        get
        {
            return this.listAgencyIDFieldSpecified;
        }
        set
        {
            this.listAgencyIDFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string listVersionID
    {
        get
        {
            return this.listVersionIDField;
        }
        set
        {
            this.listVersionIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
    public string listURI
    {
        get
        {
            return this.listURIField;
        }
        set
        {
            this.listURIField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public DocumentNameCodeContentType Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:DocumentNameCode:D09A")]
[System.Xml.Serialization.XmlRootAttribute("DocumentNameCode", Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:DocumentNameCode:D09A", IsNullable = false)]
public enum DocumentNameCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1")]
    Item1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2")]
    Item2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3")]
    Item3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4")]
    Item4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5")]
    Item5,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6")]
    Item6,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("7")]
    Item7,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("8")]
    Item8,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("9")]
    Item9,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("10")]
    Item10,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("11")]
    Item11,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("12")]
    Item12,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("13")]
    Item13,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("14")]
    Item14,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("15")]
    Item15,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("16")]
    Item16,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("17")]
    Item17,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("18")]
    Item18,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("19")]
    Item19,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("20")]
    Item20,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("21")]
    Item21,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("22")]
    Item22,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("23")]
    Item23,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("24")]
    Item24,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("25")]
    Item25,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("26")]
    Item26,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("27")]
    Item27,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("28")]
    Item28,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("29")]
    Item29,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("30")]
    Item30,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("31")]
    Item31,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("32")]
    Item32,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("33")]
    Item33,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("34")]
    Item34,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("35")]
    Item35,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("36")]
    Item36,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("37")]
    Item37,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("38")]
    Item38,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("39")]
    Item39,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("40")]
    Item40,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("41")]
    Item41,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("42")]
    Item42,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("43")]
    Item43,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("44")]
    Item44,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("45")]
    Item45,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("46")]
    Item46,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("47")]
    Item47,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("48")]
    Item48,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("49")]
    Item49,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("50")]
    Item50,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("51")]
    Item51,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("52")]
    Item52,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("53")]
    Item53,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("54")]
    Item54,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("55")]
    Item55,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("56")]
    Item56,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("57")]
    Item57,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("58")]
    Item58,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("59")]
    Item59,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("60")]
    Item60,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("61")]
    Item61,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("62")]
    Item62,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("63")]
    Item63,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("64")]
    Item64,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("65")]
    Item65,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("66")]
    Item66,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("67")]
    Item67,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("68")]
    Item68,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("69")]
    Item69,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("70")]
    Item70,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("71")]
    Item71,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("72")]
    Item72,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("73")]
    Item73,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("74")]
    Item74,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("75")]
    Item75,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("76")]
    Item76,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("77")]
    Item77,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("78")]
    Item78,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("79")]
    Item79,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("80")]
    Item80,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("81")]
    Item81,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("82")]
    Item82,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("83")]
    Item83,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("84")]
    Item84,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("85")]
    Item85,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("86")]
    Item86,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("87")]
    Item87,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("88")]
    Item88,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("89")]
    Item89,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("90")]
    Item90,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("91")]
    Item91,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("92")]
    Item92,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("93")]
    Item93,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("94")]
    Item94,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("95")]
    Item95,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("96")]
    Item96,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("97")]
    Item97,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("98")]
    Item98,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("99")]
    Item99,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("100")]
    Item100,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("101")]
    Item101,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("102")]
    Item102,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("103")]
    Item103,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("104")]
    Item104,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("105")]
    Item105,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("106")]
    Item106,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("107")]
    Item107,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("108")]
    Item108,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("109")]
    Item109,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("110")]
    Item110,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("111")]
    Item111,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("112")]
    Item112,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("113")]
    Item113,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("114")]
    Item114,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("115")]
    Item115,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("116")]
    Item116,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("117")]
    Item117,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("118")]
    Item118,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("119")]
    Item119,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("120")]
    Item120,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("121")]
    Item121,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("122")]
    Item122,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("123")]
    Item123,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("124")]
    Item124,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("125")]
    Item125,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("126")]
    Item126,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("127")]
    Item127,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("128")]
    Item128,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("129")]
    Item129,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("130")]
    Item130,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("131")]
    Item131,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("132")]
    Item132,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("133")]
    Item133,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("134")]
    Item134,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("135")]
    Item135,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("136")]
    Item136,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("137")]
    Item137,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("138")]
    Item138,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("139")]
    Item139,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("140")]
    Item140,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("141")]
    Item141,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("142")]
    Item142,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("143")]
    Item143,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("144")]
    Item144,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("145")]
    Item145,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("146")]
    Item146,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("147")]
    Item147,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("148")]
    Item148,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("149")]
    Item149,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("150")]
    Item150,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("151")]
    Item151,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("152")]
    Item152,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("153")]
    Item153,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("154")]
    Item154,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("155")]
    Item155,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("156")]
    Item156,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("157")]
    Item157,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("158")]
    Item158,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("159")]
    Item159,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("160")]
    Item160,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("161")]
    Item161,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("162")]
    Item162,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("163")]
    Item163,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("164")]
    Item164,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("165")]
    Item165,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("166")]
    Item166,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("167")]
    Item167,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("168")]
    Item168,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("169")]
    Item169,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("170")]
    Item170,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("171")]
    Item171,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("172")]
    Item172,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("173")]
    Item173,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("174")]
    Item174,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("175")]
    Item175,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("176")]
    Item176,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("177")]
    Item177,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("178")]
    Item178,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("179")]
    Item179,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("180")]
    Item180,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("181")]
    Item181,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("182")]
    Item182,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("183")]
    Item183,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("184")]
    Item184,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("185")]
    Item185,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("186")]
    Item186,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("187")]
    Item187,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("188")]
    Item188,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("189")]
    Item189,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("190")]
    Item190,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("191")]
    Item191,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("192")]
    Item192,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("193")]
    Item193,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("194")]
    Item194,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("195")]
    Item195,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("196")]
    Item196,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("197")]
    Item197,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("198")]
    Item198,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("199")]
    Item199,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("200")]
    Item200,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("201")]
    Item201,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("202")]
    Item202,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("203")]
    Item203,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("204")]
    Item204,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("205")]
    Item205,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("206")]
    Item206,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("207")]
    Item207,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("208")]
    Item208,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("209")]
    Item209,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("210")]
    Item210,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("211")]
    Item211,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("212")]
    Item212,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("213")]
    Item213,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("214")]
    Item214,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("215")]
    Item215,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("216")]
    Item216,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("217")]
    Item217,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("218")]
    Item218,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("219")]
    Item219,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("220")]
    Item220,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("221")]
    Item221,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("222")]
    Item222,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("223")]
    Item223,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("224")]
    Item224,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("225")]
    Item225,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("226")]
    Item226,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("227")]
    Item227,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("228")]
    Item228,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("229")]
    Item229,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("230")]
    Item230,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("231")]
    Item231,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("232")]
    Item232,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("233")]
    Item233,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("234")]
    Item234,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("235")]
    Item235,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("236")]
    Item236,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("237")]
    Item237,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("238")]
    Item238,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("239")]
    Item239,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("240")]
    Item240,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("241")]
    Item241,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("242")]
    Item242,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("243")]
    Item243,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("244")]
    Item244,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("245")]
    Item245,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("246")]
    Item246,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("247")]
    Item247,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("248")]
    Item248,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("249")]
    Item249,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("250")]
    Item250,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("251")]
    Item251,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("252")]
    Item252,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("253")]
    Item253,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("254")]
    Item254,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("255")]
    Item255,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("256")]
    Item256,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("257")]
    Item257,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("258")]
    Item258,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("259")]
    Item259,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("260")]
    Item260,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("261")]
    Item261,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("262")]
    Item262,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("263")]
    Item263,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("264")]
    Item264,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("265")]
    Item265,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("266")]
    Item266,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("267")]
    Item267,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("268")]
    Item268,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("269")]
    Item269,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("270")]
    Item270,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("271")]
    Item271,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("272")]
    Item272,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("273")]
    Item273,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("274")]
    Item274,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("275")]
    Item275,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("276")]
    Item276,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("277")]
    Item277,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("278")]
    Item278,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("279")]
    Item279,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("280")]
    Item280,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("281")]
    Item281,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("282")]
    Item282,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("283")]
    Item283,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("284")]
    Item284,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("285")]
    Item285,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("286")]
    Item286,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("287")]
    Item287,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("288")]
    Item288,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("289")]
    Item289,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("290")]
    Item290,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("291")]
    Item291,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("292")]
    Item292,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("293")]
    Item293,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("294")]
    Item294,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("295")]
    Item295,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("296")]
    Item296,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("297")]
    Item297,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("298")]
    Item298,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("299")]
    Item299,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("300")]
    Item300,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("301")]
    Item301,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("302")]
    Item302,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("303")]
    Item303,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("304")]
    Item304,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("305")]
    Item305,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("306")]
    Item306,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("307")]
    Item307,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("308")]
    Item308,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("309")]
    Item309,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("310")]
    Item310,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("311")]
    Item311,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("312")]
    Item312,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("313")]
    Item313,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("314")]
    Item314,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("315")]
    Item315,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("316")]
    Item316,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("317")]
    Item317,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("318")]
    Item318,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("319")]
    Item319,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("320")]
    Item320,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("321")]
    Item321,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("322")]
    Item322,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("323")]
    Item323,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("324")]
    Item324,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("325")]
    Item325,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("326")]
    Item326,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("327")]
    Item327,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("328")]
    Item328,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("329")]
    Item329,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("330")]
    Item330,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("331")]
    Item331,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("332")]
    Item332,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("333")]
    Item333,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("334")]
    Item334,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("335")]
    Item335,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("336")]
    Item336,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("337")]
    Item337,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("338")]
    Item338,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("339")]
    Item339,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("340")]
    Item340,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("341")]
    Item341,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("342")]
    Item342,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("343")]
    Item343,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("344")]
    Item344,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("345")]
    Item345,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("346")]
    Item346,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("347")]
    Item347,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("348")]
    Item348,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("349")]
    Item349,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("350")]
    Item350,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("351")]
    Item351,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("352")]
    Item352,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("353")]
    Item353,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("354")]
    Item354,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("355")]
    Item355,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("356")]
    Item356,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("357")]
    Item357,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("358")]
    Item358,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("359")]
    Item359,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("360")]
    Item360,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("361")]
    Item361,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("362")]
    Item362,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("363")]
    Item363,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("364")]
    Item364,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("365")]
    Item365,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("366")]
    Item366,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("367")]
    Item367,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("368")]
    Item368,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("369")]
    Item369,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("370")]
    Item370,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("371")]
    Item371,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("372")]
    Item372,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("373")]
    Item373,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("374")]
    Item374,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("375")]
    Item375,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("376")]
    Item376,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("377")]
    Item377,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("378")]
    Item378,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("379")]
    Item379,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("380")]
    Item380,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("381")]
    Item381,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("382")]
    Item382,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("383")]
    Item383,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("384")]
    Item384,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("385")]
    Item385,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("386")]
    Item386,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("387")]
    Item387,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("388")]
    Item388,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("389")]
    Item389,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("390")]
    Item390,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("391")]
    Item391,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("392")]
    Item392,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("393")]
    Item393,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("394")]
    Item394,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("395")]
    Item395,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("396")]
    Item396,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("397")]
    Item397,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("398")]
    Item398,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("399")]
    Item399,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("400")]
    Item400,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("401")]
    Item401,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("402")]
    Item402,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("403")]
    Item403,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("404")]
    Item404,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("405")]
    Item405,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("406")]
    Item406,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("407")]
    Item407,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("408")]
    Item408,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("409")]
    Item409,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("410")]
    Item410,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("411")]
    Item411,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("412")]
    Item412,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("413")]
    Item413,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("414")]
    Item414,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("415")]
    Item415,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("416")]
    Item416,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("417")]
    Item417,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("418")]
    Item418,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("419")]
    Item419,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("420")]
    Item420,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("421")]
    Item421,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("422")]
    Item422,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("423")]
    Item423,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("424")]
    Item424,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("425")]
    Item425,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("426")]
    Item426,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("427")]
    Item427,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("428")]
    Item428,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("429")]
    Item429,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("430")]
    Item430,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("431")]
    Item431,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("432")]
    Item432,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("433")]
    Item433,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("434")]
    Item434,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("435")]
    Item435,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("436")]
    Item436,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("437")]
    Item437,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("438")]
    Item438,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("439")]
    Item439,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("440")]
    Item440,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("441")]
    Item441,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("442")]
    Item442,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("443")]
    Item443,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("444")]
    Item444,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("445")]
    Item445,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("446")]
    Item446,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("447")]
    Item447,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("448")]
    Item448,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("449")]
    Item449,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("450")]
    Item450,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("451")]
    Item451,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("452")]
    Item452,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("453")]
    Item453,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("454")]
    Item454,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("455")]
    Item455,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("456")]
    Item456,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("457")]
    Item457,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("458")]
    Item458,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("459")]
    Item459,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("460")]
    Item460,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("461")]
    Item461,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("462")]
    Item462,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("463")]
    Item463,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("464")]
    Item464,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("465")]
    Item465,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("466")]
    Item466,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("467")]
    Item467,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("468")]
    Item468,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("469")]
    Item469,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("470")]
    Item470,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("481")]
    Item481,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("482")]
    Item482,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("483")]
    Item483,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("484")]
    Item484,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("485")]
    Item485,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("486")]
    Item486,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("487")]
    Item487,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("488")]
    Item488,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("489")]
    Item489,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("490")]
    Item490,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("491")]
    Item491,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("493")]
    Item493,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("494")]
    Item494,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("495")]
    Item495,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("496")]
    Item496,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("497")]
    Item497,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("498")]
    Item498,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("499")]
    Item499,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("520")]
    Item520,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("521")]
    Item521,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("522")]
    Item522,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("523")]
    Item523,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("524")]
    Item524,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("525")]
    Item525,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("526")]
    Item526,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("527")]
    Item527,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("528")]
    Item528,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("529")]
    Item529,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("530")]
    Item530,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("531")]
    Item531,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("532")]
    Item532,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("533")]
    Item533,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("534")]
    Item534,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("535")]
    Item535,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("536")]
    Item536,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("537")]
    Item537,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("538")]
    Item538,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("550")]
    Item550,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("552")]
    Item552,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("553")]
    Item553,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("575")]
    Item575,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("580")]
    Item580,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("610")]
    Item610,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("621")]
    Item621,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("622")]
    Item622,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("623")]
    Item623,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("624")]
    Item624,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("630")]
    Item630,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("631")]
    Item631,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("632")]
    Item632,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("633")]
    Item633,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("635")]
    Item635,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("640")]
    Item640,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("650")]
    Item650,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("655")]
    Item655,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("700")]
    Item700,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("701")]
    Item701,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("702")]
    Item702,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("703")]
    Item703,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("704")]
    Item704,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("705")]
    Item705,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("706")]
    Item706,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("707")]
    Item707,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("708")]
    Item708,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("709")]
    Item709,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("710")]
    Item710,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("711")]
    Item711,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("712")]
    Item712,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("713")]
    Item713,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("714")]
    Item714,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("715")]
    Item715,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("716")]
    Item716,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("720")]
    Item720,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("722")]
    Item722,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("723")]
    Item723,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("724")]
    Item724,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("730")]
    Item730,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("740")]
    Item740,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("741")]
    Item741,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("743")]
    Item743,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("744")]
    Item744,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("745")]
    Item745,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("746")]
    Item746,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("750")]
    Item750,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("760")]
    Item760,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("761")]
    Item761,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("763")]
    Item763,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("764")]
    Item764,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("765")]
    Item765,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("766")]
    Item766,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("770")]
    Item770,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("775")]
    Item775,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("780")]
    Item780,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("781")]
    Item781,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("782")]
    Item782,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("783")]
    Item783,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("784")]
    Item784,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("785")]
    Item785,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("786")]
    Item786,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("787")]
    Item787,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("788")]
    Item788,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("789")]
    Item789,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("790")]
    Item790,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("791")]
    Item791,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("792")]
    Item792,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("793")]
    Item793,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("794")]
    Item794,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("795")]
    Item795,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("796")]
    Item796,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("797")]
    Item797,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("798")]
    Item798,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("799")]
    Item799,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("810")]
    Item810,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("811")]
    Item811,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("812")]
    Item812,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("820")]
    Item820,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("821")]
    Item821,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("822")]
    Item822,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("823")]
    Item823,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("824")]
    Item824,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("825")]
    Item825,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("830")]
    Item830,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("833")]
    Item833,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("840")]
    Item840,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("841")]
    Item841,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("850")]
    Item850,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("851")]
    Item851,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("852")]
    Item852,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("853")]
    Item853,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("855")]
    Item855,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("856")]
    Item856,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("860")]
    Item860,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("861")]
    Item861,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("862")]
    Item862,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("863")]
    Item863,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("864")]
    Item864,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("865")]
    Item865,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("870")]
    Item870,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("890")]
    Item890,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("895")]
    Item895,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("896")]
    Item896,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("901")]
    Item901,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("910")]
    Item910,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("911")]
    Item911,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("913")]
    Item913,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("914")]
    Item914,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("915")]
    Item915,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("916")]
    Item916,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("917")]
    Item917,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("925")]
    Item925,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("926")]
    Item926,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("927")]
    Item927,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("929")]
    Item929,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("930")]
    Item930,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("931")]
    Item931,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("932")]
    Item932,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("933")]
    Item933,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("934")]
    Item934,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("935")]
    Item935,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("936")]
    Item936,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("937")]
    Item937,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("938")]
    Item938,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("940")]
    Item940,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("941")]
    Item941,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("950")]
    Item950,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("951")]
    Item951,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("952")]
    Item952,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("953")]
    Item953,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("954")]
    Item954,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("955")]
    Item955,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("960")]
    Item960,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("961")]
    Item961,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("962")]
    Item962,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("963")]
    Item963,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("964")]
    Item964,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("965")]
    Item965,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("966")]
    Item966,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("970")]
    Item970,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("971")]
    Item971,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("972")]
    Item972,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("974")]
    Item974,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("975")]
    Item975,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("976")]
    Item976,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("977")]
    Item977,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("978")]
    Item978,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("979")]
    Item979,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("990")]
    Item990,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("991")]
    Item991,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("995")]
    Item995,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("996")]
    Item996,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("998")]
    Item998,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:8")]
public partial class TextType
{

    private string languageIDField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "language")]
    public string languageID
    {
        get
        {
            return this.languageIDField;
        }
        set
        {
            this.languageIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("OSIInstructions", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class OSIInstructionsType
{

    private TextType descriptionField;

    private CodeType descriptionCodeField;

    /// <remarks/>
    public TextType Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public CodeType DescriptionCode
    {
        get
        {
            return this.descriptionCodeField;
        }
        set
        {
            this.descriptionCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:8")]
public partial class CodeType
{

    private string listIDField;

    private AgencyIdentificationCodeContentType listAgencyIDField;

    private bool listAgencyIDFieldSpecified;

    private string listAgencyNameField;

    private string listNameField;

    private string listVersionIDField;

    private string nameField;

    private string languageIDField;

    private string listURIField;

    private string listSchemeURIField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string listID
    {
        get
        {
            return this.listIDField;
        }
        set
        {
            this.listIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public AgencyIdentificationCodeContentType listAgencyID
    {
        get
        {
            return this.listAgencyIDField;
        }
        set
        {
            this.listAgencyIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool listAgencyIDSpecified
    {
        get
        {
            return this.listAgencyIDFieldSpecified;
        }
        set
        {
            this.listAgencyIDFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string listAgencyName
    {
        get
        {
            return this.listAgencyNameField;
        }
        set
        {
            this.listAgencyNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string listName
    {
        get
        {
            return this.listNameField;
        }
        set
        {
            this.listNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string listVersionID
    {
        get
        {
            return this.listVersionIDField;
        }
        set
        {
            this.listVersionIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "language")]
    public string languageID
    {
        get
        {
            return this.languageIDField;
        }
        set
        {
            this.languageIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
    public string listURI
    {
        get
        {
            return this.listURIField;
        }
        set
        {
            this.listURIField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
    public string listSchemeURI
    {
        get
        {
            return this.listSchemeURIField;
        }
        set
        {
            this.listSchemeURIField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute(DataType = "token")]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("SSRInstructions", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class SSRInstructionsType
{

    private TextType descriptionField;

    private CodeType descriptionCodeField;

    /// <remarks/>
    public TextType Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public CodeType DescriptionCode
    {
        get
        {
            return this.descriptionCodeField;
        }
        set
        {
            this.descriptionCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("SPHInstructions", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class SPHInstructionsType
{

    private TextType descriptionField;

    private CodeType descriptionCodeField;

    /// <remarks/>
    public TextType Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public CodeType DescriptionCode
    {
        get
        {
            return this.descriptionCodeField;
        }
        set
        {
            this.descriptionCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("HouseConsignment", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class HouseConsignmentType
{

    private decimal sequenceNumericField;

    private MeasureType grossWeightMeasureField;

    private QuantityType packageQuantityField;

    private QuantityType totalPieceQuantityField;

    private TextType summaryDescriptionField;

    private TransportContractDocumentType transportContractDocumentField;

    private OriginLocationType originLocationField;

    private FinalDestinationLocationType finalDestinationLocationField;

    private SPHInstructionsType[] handlingSPHInstructionsField;

    private SSRInstructionsType[] handlingSSRInstructionsField;

    private OSIInstructionsType[] handlingOSIInstructionsField;

    private CustomsNoteType[] includedCustomsNoteField;

    private ReferenceDocumentType[] associatedReferenceDocumentField;

    /// <remarks/>
    public decimal SequenceNumeric
    {
        get
        {
            return this.sequenceNumericField;
        }
        set
        {
            this.sequenceNumericField = value;
        }
    }

    /// <remarks/>
    public MeasureType GrossWeightMeasure
    {
        get
        {
            return this.grossWeightMeasureField;
        }
        set
        {
            this.grossWeightMeasureField = value;
        }
    }

    /// <remarks/>
    public QuantityType PackageQuantity
    {
        get
        {
            return this.packageQuantityField;
        }
        set
        {
            this.packageQuantityField = value;
        }
    }

    /// <remarks/>
    public QuantityType TotalPieceQuantity
    {
        get
        {
            return this.totalPieceQuantityField;
        }
        set
        {
            this.totalPieceQuantityField = value;
        }
    }

    /// <remarks/>
    public TextType SummaryDescription
    {
        get
        {
            return this.summaryDescriptionField;
        }
        set
        {
            this.summaryDescriptionField = value;
        }
    }

    /// <remarks/>
    public TransportContractDocumentType TransportContractDocument
    {
        get
        {
            return this.transportContractDocumentField;
        }
        set
        {
            this.transportContractDocumentField = value;
        }
    }

    /// <remarks/>
    public OriginLocationType OriginLocation
    {
        get
        {
            return this.originLocationField;
        }
        set
        {
            this.originLocationField = value;
        }
    }

    /// <remarks/>
    public FinalDestinationLocationType FinalDestinationLocation
    {
        get
        {
            return this.finalDestinationLocationField;
        }
        set
        {
            this.finalDestinationLocationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("HandlingSPHInstructions")]
    public SPHInstructionsType[] HandlingSPHInstructions
    {
        get
        {
            return this.handlingSPHInstructionsField;
        }
        set
        {
            this.handlingSPHInstructionsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("HandlingSSRInstructions")]
    public SSRInstructionsType[] HandlingSSRInstructions
    {
        get
        {
            return this.handlingSSRInstructionsField;
        }
        set
        {
            this.handlingSSRInstructionsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("HandlingOSIInstructions")]
    public OSIInstructionsType[] HandlingOSIInstructions
    {
        get
        {
            return this.handlingOSIInstructionsField;
        }
        set
        {
            this.handlingOSIInstructionsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("IncludedCustomsNote")]
    public CustomsNoteType[] IncludedCustomsNote
    {
        get
        {
            return this.includedCustomsNoteField;
        }
        set
        {
            this.includedCustomsNoteField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AssociatedReferenceDocument")]
    public ReferenceDocumentType[] AssociatedReferenceDocument
    {
        get
        {
            return this.associatedReferenceDocumentField;
        }
        set
        {
            this.associatedReferenceDocumentField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:8")]
public partial class MeasureType
{

    private MeasurementUnitCommonCodeContentType unitCodeField;

    private bool unitCodeFieldSpecified;

    private decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public MeasurementUnitCommonCodeContentType unitCode
    {
        get
        {
            return this.unitCodeField;
        }
        set
        {
            this.unitCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool unitCodeSpecified
    {
        get
        {
            return this.unitCodeFieldSpecified;
        }
        set
        {
            this.unitCodeFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:6:Recommendation20:6")]
[System.Xml.Serialization.XmlRootAttribute("MeasurementUnitCommonCode", Namespace = "urn:un:unece:uncefact:codelist:standard:6:Recommendation20:6", IsNullable = false)]
public enum MeasurementUnitCommonCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("05")]
    Item05,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("06")]
    Item06,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("08")]
    Item08,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("10")]
    Item10,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("11")]
    Item11,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("13")]
    Item13,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("14")]
    Item14,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("15")]
    Item15,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("16")]
    Item16,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("17")]
    Item17,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("18")]
    Item18,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("19")]
    Item19,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("20")]
    Item20,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("21")]
    Item21,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("22")]
    Item22,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("23")]
    Item23,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("24")]
    Item24,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("25")]
    Item25,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("26")]
    Item26,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("27")]
    Item27,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("28")]
    Item28,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("29")]
    Item29,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("30")]
    Item30,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("31")]
    Item31,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("32")]
    Item32,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("33")]
    Item33,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("34")]
    Item34,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("35")]
    Item35,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("36")]
    Item36,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("37")]
    Item37,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("38")]
    Item38,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("40")]
    Item40,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("41")]
    Item41,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("43")]
    Item43,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("44")]
    Item44,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("45")]
    Item45,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("46")]
    Item46,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("47")]
    Item47,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("48")]
    Item48,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("53")]
    Item53,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("54")]
    Item54,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("56")]
    Item56,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("57")]
    Item57,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("58")]
    Item58,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("59")]
    Item59,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("60")]
    Item60,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("61")]
    Item61,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("62")]
    Item62,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("63")]
    Item63,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("64")]
    Item64,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("66")]
    Item66,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("69")]
    Item69,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("71")]
    Item71,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("72")]
    Item72,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("73")]
    Item73,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("74")]
    Item74,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("76")]
    Item76,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("77")]
    Item77,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("78")]
    Item78,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("80")]
    Item80,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("81")]
    Item81,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("84")]
    Item84,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("85")]
    Item85,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("87")]
    Item87,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("89")]
    Item89,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("90")]
    Item90,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("91")]
    Item91,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("92")]
    Item92,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("93")]
    Item93,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("94")]
    Item94,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("95")]
    Item95,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("96")]
    Item96,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("97")]
    Item97,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("98")]
    Item98,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1A")]
    Item1A,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1B")]
    Item1B,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1C")]
    Item1C,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1D")]
    Item1D,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1E")]
    Item1E,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1F")]
    Item1F,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1G")]
    Item1G,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1H")]
    Item1H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1I")]
    Item1I,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1J")]
    Item1J,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1K")]
    Item1K,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1L")]
    Item1L,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1M")]
    Item1M,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1X")]
    Item1X,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2A")]
    Item2A,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2B")]
    Item2B,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2C")]
    Item2C,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2G")]
    Item2G,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2H")]
    Item2H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2I")]
    Item2I,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2J")]
    Item2J,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2K")]
    Item2K,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2L")]
    Item2L,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2M")]
    Item2M,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2N")]
    Item2N,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2P")]
    Item2P,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2Q")]
    Item2Q,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2R")]
    Item2R,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2U")]
    Item2U,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2V")]
    Item2V,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2W")]
    Item2W,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2X")]
    Item2X,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2Y")]
    Item2Y,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2Z")]
    Item2Z,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3B")]
    Item3B,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3C")]
    Item3C,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3E")]
    Item3E,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3G")]
    Item3G,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3H")]
    Item3H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3I")]
    Item3I,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4A")]
    Item4A,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4B")]
    Item4B,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4C")]
    Item4C,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4E")]
    Item4E,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4G")]
    Item4G,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4H")]
    Item4H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4K")]
    Item4K,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4L")]
    Item4L,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4M")]
    Item4M,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4N")]
    Item4N,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4O")]
    Item4O,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4P")]
    Item4P,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4Q")]
    Item4Q,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4R")]
    Item4R,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4T")]
    Item4T,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4U")]
    Item4U,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4W")]
    Item4W,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4X")]
    Item4X,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5A")]
    Item5A,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5B")]
    Item5B,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5C")]
    Item5C,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5E")]
    Item5E,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5F")]
    Item5F,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5G")]
    Item5G,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5H")]
    Item5H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5I")]
    Item5I,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5J")]
    Item5J,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5K")]
    Item5K,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5P")]
    Item5P,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5Q")]
    Item5Q,

    /// <remarks/>
    A1,

    /// <remarks/>
    A10,

    /// <remarks/>
    A11,

    /// <remarks/>
    A12,

    /// <remarks/>
    A13,

    /// <remarks/>
    A14,

    /// <remarks/>
    A15,

    /// <remarks/>
    A16,

    /// <remarks/>
    A17,

    /// <remarks/>
    A18,

    /// <remarks/>
    A19,

    /// <remarks/>
    A2,

    /// <remarks/>
    A20,

    /// <remarks/>
    A21,

    /// <remarks/>
    A22,

    /// <remarks/>
    A23,

    /// <remarks/>
    A24,

    /// <remarks/>
    A25,

    /// <remarks/>
    A26,

    /// <remarks/>
    A27,

    /// <remarks/>
    A28,

    /// <remarks/>
    A29,

    /// <remarks/>
    A3,

    /// <remarks/>
    A30,

    /// <remarks/>
    A31,

    /// <remarks/>
    A32,

    /// <remarks/>
    A33,

    /// <remarks/>
    A34,

    /// <remarks/>
    A35,

    /// <remarks/>
    A36,

    /// <remarks/>
    A37,

    /// <remarks/>
    A38,

    /// <remarks/>
    A39,

    /// <remarks/>
    A4,

    /// <remarks/>
    A40,

    /// <remarks/>
    A41,

    /// <remarks/>
    A42,

    /// <remarks/>
    A43,

    /// <remarks/>
    A44,

    /// <remarks/>
    A45,

    /// <remarks/>
    A47,

    /// <remarks/>
    A48,

    /// <remarks/>
    A49,

    /// <remarks/>
    A5,

    /// <remarks/>
    A50,

    /// <remarks/>
    A51,

    /// <remarks/>
    A52,

    /// <remarks/>
    A53,

    /// <remarks/>
    A54,

    /// <remarks/>
    A55,

    /// <remarks/>
    A56,

    /// <remarks/>
    A57,

    /// <remarks/>
    A58,

    /// <remarks/>
    A59,

    /// <remarks/>
    A6,

    /// <remarks/>
    A60,

    /// <remarks/>
    A61,

    /// <remarks/>
    A62,

    /// <remarks/>
    A63,

    /// <remarks/>
    A64,

    /// <remarks/>
    A65,

    /// <remarks/>
    A66,

    /// <remarks/>
    A67,

    /// <remarks/>
    A68,

    /// <remarks/>
    A69,

    /// <remarks/>
    A7,

    /// <remarks/>
    A70,

    /// <remarks/>
    A71,

    /// <remarks/>
    A73,

    /// <remarks/>
    A74,

    /// <remarks/>
    A75,

    /// <remarks/>
    A76,

    /// <remarks/>
    A77,

    /// <remarks/>
    A78,

    /// <remarks/>
    A79,

    /// <remarks/>
    A8,

    /// <remarks/>
    A80,

    /// <remarks/>
    A81,

    /// <remarks/>
    A82,

    /// <remarks/>
    A83,

    /// <remarks/>
    A84,

    /// <remarks/>
    A85,

    /// <remarks/>
    A86,

    /// <remarks/>
    A87,

    /// <remarks/>
    A88,

    /// <remarks/>
    A89,

    /// <remarks/>
    A9,

    /// <remarks/>
    A90,

    /// <remarks/>
    A91,

    /// <remarks/>
    A93,

    /// <remarks/>
    A94,

    /// <remarks/>
    A95,

    /// <remarks/>
    A96,

    /// <remarks/>
    A97,

    /// <remarks/>
    A98,

    /// <remarks/>
    A99,

    /// <remarks/>
    AA,

    /// <remarks/>
    AB,

    /// <remarks/>
    ACR,

    /// <remarks/>
    ACT,

    /// <remarks/>
    AD,

    /// <remarks/>
    AE,

    /// <remarks/>
    AH,

    /// <remarks/>
    AI,

    /// <remarks/>
    AJ,

    /// <remarks/>
    AK,

    /// <remarks/>
    AL,

    /// <remarks/>
    AM,

    /// <remarks/>
    AMH,

    /// <remarks/>
    AMP,

    /// <remarks/>
    ANN,

    /// <remarks/>
    AP,

    /// <remarks/>
    APZ,

    /// <remarks/>
    AQ,

    /// <remarks/>
    AR,

    /// <remarks/>
    ARE,

    /// <remarks/>
    AS,

    /// <remarks/>
    ASM,

    /// <remarks/>
    ASU,

    /// <remarks/>
    ATM,

    /// <remarks/>
    ATT,

    /// <remarks/>
    AV,

    /// <remarks/>
    AW,

    /// <remarks/>
    AY,

    /// <remarks/>
    AZ,

    /// <remarks/>
    B0,

    /// <remarks/>
    B1,

    /// <remarks/>
    B10,

    /// <remarks/>
    B11,

    /// <remarks/>
    B12,

    /// <remarks/>
    B13,

    /// <remarks/>
    B14,

    /// <remarks/>
    B15,

    /// <remarks/>
    B16,

    /// <remarks/>
    B17,

    /// <remarks/>
    B18,

    /// <remarks/>
    B19,

    /// <remarks/>
    B2,

    /// <remarks/>
    B20,

    /// <remarks/>
    B21,

    /// <remarks/>
    B22,

    /// <remarks/>
    B23,

    /// <remarks/>
    B24,

    /// <remarks/>
    B25,

    /// <remarks/>
    B26,

    /// <remarks/>
    B27,

    /// <remarks/>
    B28,

    /// <remarks/>
    B29,

    /// <remarks/>
    B3,

    /// <remarks/>
    B30,

    /// <remarks/>
    B31,

    /// <remarks/>
    B32,

    /// <remarks/>
    B33,

    /// <remarks/>
    B34,

    /// <remarks/>
    B35,

    /// <remarks/>
    B36,

    /// <remarks/>
    B37,

    /// <remarks/>
    B38,

    /// <remarks/>
    B39,

    /// <remarks/>
    B4,

    /// <remarks/>
    B40,

    /// <remarks/>
    B41,

    /// <remarks/>
    B42,

    /// <remarks/>
    B43,

    /// <remarks/>
    B44,

    /// <remarks/>
    B45,

    /// <remarks/>
    B46,

    /// <remarks/>
    B47,

    /// <remarks/>
    B48,

    /// <remarks/>
    B49,

    /// <remarks/>
    B5,

    /// <remarks/>
    B50,

    /// <remarks/>
    B51,

    /// <remarks/>
    B52,

    /// <remarks/>
    B53,

    /// <remarks/>
    B54,

    /// <remarks/>
    B55,

    /// <remarks/>
    B56,

    /// <remarks/>
    B57,

    /// <remarks/>
    B58,

    /// <remarks/>
    B59,

    /// <remarks/>
    B6,

    /// <remarks/>
    B60,

    /// <remarks/>
    B61,

    /// <remarks/>
    B62,

    /// <remarks/>
    B63,

    /// <remarks/>
    B64,

    /// <remarks/>
    B65,

    /// <remarks/>
    B66,

    /// <remarks/>
    B67,

    /// <remarks/>
    B68,

    /// <remarks/>
    B69,

    /// <remarks/>
    B7,

    /// <remarks/>
    B70,

    /// <remarks/>
    B71,

    /// <remarks/>
    B72,

    /// <remarks/>
    B73,

    /// <remarks/>
    B74,

    /// <remarks/>
    B75,

    /// <remarks/>
    B76,

    /// <remarks/>
    B77,

    /// <remarks/>
    B78,

    /// <remarks/>
    B79,

    /// <remarks/>
    B8,

    /// <remarks/>
    B80,

    /// <remarks/>
    B81,

    /// <remarks/>
    B82,

    /// <remarks/>
    B83,

    /// <remarks/>
    B84,

    /// <remarks/>
    B85,

    /// <remarks/>
    B86,

    /// <remarks/>
    B87,

    /// <remarks/>
    B88,

    /// <remarks/>
    B89,

    /// <remarks/>
    B9,

    /// <remarks/>
    B90,

    /// <remarks/>
    B91,

    /// <remarks/>
    B92,

    /// <remarks/>
    B93,

    /// <remarks/>
    B94,

    /// <remarks/>
    B95,

    /// <remarks/>
    B96,

    /// <remarks/>
    B97,

    /// <remarks/>
    B98,

    /// <remarks/>
    B99,

    /// <remarks/>
    BAR,

    /// <remarks/>
    BB,

    /// <remarks/>
    BD,

    /// <remarks/>
    BE,

    /// <remarks/>
    BFT,

    /// <remarks/>
    BG,

    /// <remarks/>
    BH,

    /// <remarks/>
    BHP,

    /// <remarks/>
    BIL,

    /// <remarks/>
    BJ,

    /// <remarks/>
    BK,

    /// <remarks/>
    BL,

    /// <remarks/>
    BLD,

    /// <remarks/>
    BLL,

    /// <remarks/>
    BO,

    /// <remarks/>
    BP,

    /// <remarks/>
    BQL,

    /// <remarks/>
    BR,

    /// <remarks/>
    BT,

    /// <remarks/>
    BTU,

    /// <remarks/>
    BUA,

    /// <remarks/>
    BUI,

    /// <remarks/>
    BW,

    /// <remarks/>
    BX,

    /// <remarks/>
    BZ,

    /// <remarks/>
    C0,

    /// <remarks/>
    C1,

    /// <remarks/>
    C10,

    /// <remarks/>
    C11,

    /// <remarks/>
    C12,

    /// <remarks/>
    C13,

    /// <remarks/>
    C14,

    /// <remarks/>
    C15,

    /// <remarks/>
    C16,

    /// <remarks/>
    C17,

    /// <remarks/>
    C18,

    /// <remarks/>
    C19,

    /// <remarks/>
    C2,

    /// <remarks/>
    C20,

    /// <remarks/>
    C21,

    /// <remarks/>
    C22,

    /// <remarks/>
    C23,

    /// <remarks/>
    C24,

    /// <remarks/>
    C25,

    /// <remarks/>
    C26,

    /// <remarks/>
    C27,

    /// <remarks/>
    C28,

    /// <remarks/>
    C29,

    /// <remarks/>
    C3,

    /// <remarks/>
    C30,

    /// <remarks/>
    C31,

    /// <remarks/>
    C32,

    /// <remarks/>
    C33,

    /// <remarks/>
    C34,

    /// <remarks/>
    C35,

    /// <remarks/>
    C36,

    /// <remarks/>
    C37,

    /// <remarks/>
    C38,

    /// <remarks/>
    C39,

    /// <remarks/>
    C4,

    /// <remarks/>
    C40,

    /// <remarks/>
    C41,

    /// <remarks/>
    C42,

    /// <remarks/>
    C43,

    /// <remarks/>
    C44,

    /// <remarks/>
    C45,

    /// <remarks/>
    C46,

    /// <remarks/>
    C47,

    /// <remarks/>
    C48,

    /// <remarks/>
    C49,

    /// <remarks/>
    C5,

    /// <remarks/>
    C50,

    /// <remarks/>
    C51,

    /// <remarks/>
    C52,

    /// <remarks/>
    C53,

    /// <remarks/>
    C54,

    /// <remarks/>
    C55,

    /// <remarks/>
    C56,

    /// <remarks/>
    C57,

    /// <remarks/>
    C58,

    /// <remarks/>
    C59,

    /// <remarks/>
    C6,

    /// <remarks/>
    C60,

    /// <remarks/>
    C61,

    /// <remarks/>
    C62,

    /// <remarks/>
    C63,

    /// <remarks/>
    C64,

    /// <remarks/>
    C65,

    /// <remarks/>
    C66,

    /// <remarks/>
    C67,

    /// <remarks/>
    C68,

    /// <remarks/>
    C69,

    /// <remarks/>
    C7,

    /// <remarks/>
    C70,

    /// <remarks/>
    C71,

    /// <remarks/>
    C72,

    /// <remarks/>
    C73,

    /// <remarks/>
    C74,

    /// <remarks/>
    C75,

    /// <remarks/>
    C76,

    /// <remarks/>
    C77,

    /// <remarks/>
    C78,

    /// <remarks/>
    C79,

    /// <remarks/>
    C8,

    /// <remarks/>
    C80,

    /// <remarks/>
    C81,

    /// <remarks/>
    C82,

    /// <remarks/>
    C83,

    /// <remarks/>
    C84,

    /// <remarks/>
    C85,

    /// <remarks/>
    C86,

    /// <remarks/>
    C87,

    /// <remarks/>
    C88,

    /// <remarks/>
    C89,

    /// <remarks/>
    C9,

    /// <remarks/>
    C90,

    /// <remarks/>
    C91,

    /// <remarks/>
    C92,

    /// <remarks/>
    C93,

    /// <remarks/>
    C94,

    /// <remarks/>
    C95,

    /// <remarks/>
    C96,

    /// <remarks/>
    C97,

    /// <remarks/>
    C98,

    /// <remarks/>
    C99,

    /// <remarks/>
    CA,

    /// <remarks/>
    CCT,

    /// <remarks/>
    CDL,

    /// <remarks/>
    CEL,

    /// <remarks/>
    CEN,

    /// <remarks/>
    CG,

    /// <remarks/>
    CGM,

    /// <remarks/>
    CH,

    /// <remarks/>
    CJ,

    /// <remarks/>
    CK,

    /// <remarks/>
    CKG,

    /// <remarks/>
    CL,

    /// <remarks/>
    CLF,

    /// <remarks/>
    CLT,

    /// <remarks/>
    CMK,

    /// <remarks/>
    CMQ,

    /// <remarks/>
    CMT,

    /// <remarks/>
    CNP,

    /// <remarks/>
    CNT,

    /// <remarks/>
    CO,

    /// <remarks/>
    COU,

    /// <remarks/>
    CQ,

    /// <remarks/>
    CR,

    /// <remarks/>
    CS,

    /// <remarks/>
    CT,

    /// <remarks/>
    CTG,

    /// <remarks/>
    CTM,

    /// <remarks/>
    CTN,

    /// <remarks/>
    CU,

    /// <remarks/>
    CUR,

    /// <remarks/>
    CV,

    /// <remarks/>
    CWA,

    /// <remarks/>
    CWI,

    /// <remarks/>
    CY,

    /// <remarks/>
    CZ,

    /// <remarks/>
    D03,

    /// <remarks/>
    D04,

    /// <remarks/>
    D1,

    /// <remarks/>
    D10,

    /// <remarks/>
    D11,

    /// <remarks/>
    D12,

    /// <remarks/>
    D13,

    /// <remarks/>
    D14,

    /// <remarks/>
    D15,

    /// <remarks/>
    D16,

    /// <remarks/>
    D17,

    /// <remarks/>
    D18,

    /// <remarks/>
    D19,

    /// <remarks/>
    D2,

    /// <remarks/>
    D20,

    /// <remarks/>
    D21,

    /// <remarks/>
    D22,

    /// <remarks/>
    D23,

    /// <remarks/>
    D24,

    /// <remarks/>
    D25,

    /// <remarks/>
    D26,

    /// <remarks/>
    D27,

    /// <remarks/>
    D28,

    /// <remarks/>
    D29,

    /// <remarks/>
    D30,

    /// <remarks/>
    D31,

    /// <remarks/>
    D32,

    /// <remarks/>
    D33,

    /// <remarks/>
    D34,

    /// <remarks/>
    D35,

    /// <remarks/>
    D36,

    /// <remarks/>
    D37,

    /// <remarks/>
    D38,

    /// <remarks/>
    D39,

    /// <remarks/>
    D40,

    /// <remarks/>
    D41,

    /// <remarks/>
    D42,

    /// <remarks/>
    D43,

    /// <remarks/>
    D44,

    /// <remarks/>
    D45,

    /// <remarks/>
    D46,

    /// <remarks/>
    D47,

    /// <remarks/>
    D48,

    /// <remarks/>
    D49,

    /// <remarks/>
    D5,

    /// <remarks/>
    D50,

    /// <remarks/>
    D51,

    /// <remarks/>
    D52,

    /// <remarks/>
    D53,

    /// <remarks/>
    D54,

    /// <remarks/>
    D55,

    /// <remarks/>
    D56,

    /// <remarks/>
    D57,

    /// <remarks/>
    D58,

    /// <remarks/>
    D59,

    /// <remarks/>
    D6,

    /// <remarks/>
    D60,

    /// <remarks/>
    D61,

    /// <remarks/>
    D62,

    /// <remarks/>
    D63,

    /// <remarks/>
    D64,

    /// <remarks/>
    D65,

    /// <remarks/>
    D66,

    /// <remarks/>
    D67,

    /// <remarks/>
    D68,

    /// <remarks/>
    D69,

    /// <remarks/>
    D7,

    /// <remarks/>
    D70,

    /// <remarks/>
    D71,

    /// <remarks/>
    D72,

    /// <remarks/>
    D73,

    /// <remarks/>
    D74,

    /// <remarks/>
    D75,

    /// <remarks/>
    D76,

    /// <remarks/>
    D77,

    /// <remarks/>
    D78,

    /// <remarks/>
    D79,

    /// <remarks/>
    D8,

    /// <remarks/>
    D80,

    /// <remarks/>
    D81,

    /// <remarks/>
    D82,

    /// <remarks/>
    D83,

    /// <remarks/>
    D85,

    /// <remarks/>
    D86,

    /// <remarks/>
    D87,

    /// <remarks/>
    D88,

    /// <remarks/>
    D89,

    /// <remarks/>
    D9,

    /// <remarks/>
    D90,

    /// <remarks/>
    D91,

    /// <remarks/>
    D92,

    /// <remarks/>
    D93,

    /// <remarks/>
    D94,

    /// <remarks/>
    D95,

    /// <remarks/>
    D96,

    /// <remarks/>
    D97,

    /// <remarks/>
    D98,

    /// <remarks/>
    D99,

    /// <remarks/>
    DAA,

    /// <remarks/>
    DAD,

    /// <remarks/>
    DAY,

    /// <remarks/>
    DB,

    /// <remarks/>
    DC,

    /// <remarks/>
    DD,

    /// <remarks/>
    DE,

    /// <remarks/>
    DEC,

    /// <remarks/>
    DG,

    /// <remarks/>
    DI,

    /// <remarks/>
    DJ,

    /// <remarks/>
    DLT,

    /// <remarks/>
    DMA,

    /// <remarks/>
    DMK,

    /// <remarks/>
    DMO,

    /// <remarks/>
    DMQ,

    /// <remarks/>
    DMT,

    /// <remarks/>
    DN,

    /// <remarks/>
    DPC,

    /// <remarks/>
    DPR,

    /// <remarks/>
    DPT,

    /// <remarks/>
    DQ,

    /// <remarks/>
    DR,

    /// <remarks/>
    DRA,

    /// <remarks/>
    DRI,

    /// <remarks/>
    DRL,

    /// <remarks/>
    DRM,

    /// <remarks/>
    DS,

    /// <remarks/>
    DT,

    /// <remarks/>
    DTN,

    /// <remarks/>
    DU,

    /// <remarks/>
    DWT,

    /// <remarks/>
    DX,

    /// <remarks/>
    DY,

    /// <remarks/>
    DZN,

    /// <remarks/>
    DZP,

    /// <remarks/>
    E01,

    /// <remarks/>
    E07,

    /// <remarks/>
    E08,

    /// <remarks/>
    E09,

    /// <remarks/>
    E10,

    /// <remarks/>
    E11,

    /// <remarks/>
    E12,

    /// <remarks/>
    E14,

    /// <remarks/>
    E15,

    /// <remarks/>
    E16,

    /// <remarks/>
    E17,

    /// <remarks/>
    E18,

    /// <remarks/>
    E19,

    /// <remarks/>
    E2,

    /// <remarks/>
    E20,

    /// <remarks/>
    E21,

    /// <remarks/>
    E22,

    /// <remarks/>
    E23,

    /// <remarks/>
    E25,

    /// <remarks/>
    E27,

    /// <remarks/>
    E28,

    /// <remarks/>
    E3,

    /// <remarks/>
    E30,

    /// <remarks/>
    E31,

    /// <remarks/>
    E32,

    /// <remarks/>
    E33,

    /// <remarks/>
    E34,

    /// <remarks/>
    E35,

    /// <remarks/>
    E36,

    /// <remarks/>
    E37,

    /// <remarks/>
    E38,

    /// <remarks/>
    E39,

    /// <remarks/>
    E4,

    /// <remarks/>
    E40,

    /// <remarks/>
    E41,

    /// <remarks/>
    E42,

    /// <remarks/>
    E43,

    /// <remarks/>
    E44,

    /// <remarks/>
    E45,

    /// <remarks/>
    E46,

    /// <remarks/>
    E47,

    /// <remarks/>
    E48,

    /// <remarks/>
    E49,

    /// <remarks/>
    E5,

    /// <remarks/>
    E50,

    /// <remarks/>
    E51,

    /// <remarks/>
    E52,

    /// <remarks/>
    E53,

    /// <remarks/>
    E54,

    /// <remarks/>
    E55,

    /// <remarks/>
    E56,

    /// <remarks/>
    E57,

    /// <remarks/>
    E58,

    /// <remarks/>
    E59,

    /// <remarks/>
    E60,

    /// <remarks/>
    E61,

    /// <remarks/>
    E62,

    /// <remarks/>
    E63,

    /// <remarks/>
    E64,

    /// <remarks/>
    E65,

    /// <remarks/>
    E66,

    /// <remarks/>
    E67,

    /// <remarks/>
    E68,

    /// <remarks/>
    E69,

    /// <remarks/>
    E70,

    /// <remarks/>
    E71,

    /// <remarks/>
    E72,

    /// <remarks/>
    E73,

    /// <remarks/>
    E74,

    /// <remarks/>
    E75,

    /// <remarks/>
    E76,

    /// <remarks/>
    E77,

    /// <remarks/>
    E78,

    /// <remarks/>
    E79,

    /// <remarks/>
    E80,

    /// <remarks/>
    E81,

    /// <remarks/>
    E82,

    /// <remarks/>
    E83,

    /// <remarks/>
    E84,

    /// <remarks/>
    E85,

    /// <remarks/>
    E86,

    /// <remarks/>
    E87,

    /// <remarks/>
    E88,

    /// <remarks/>
    E89,

    /// <remarks/>
    E90,

    /// <remarks/>
    E91,

    /// <remarks/>
    E92,

    /// <remarks/>
    E93,

    /// <remarks/>
    E94,

    /// <remarks/>
    E95,

    /// <remarks/>
    E96,

    /// <remarks/>
    E97,

    /// <remarks/>
    E98,

    /// <remarks/>
    E99,

    /// <remarks/>
    EA,

    /// <remarks/>
    EB,

    /// <remarks/>
    EC,

    /// <remarks/>
    EP,

    /// <remarks/>
    EQ,

    /// <remarks/>
    EV,

    /// <remarks/>
    F01,

    /// <remarks/>
    F02,

    /// <remarks/>
    F03,

    /// <remarks/>
    F04,

    /// <remarks/>
    F05,

    /// <remarks/>
    F06,

    /// <remarks/>
    F07,

    /// <remarks/>
    F08,

    /// <remarks/>
    F1,

    /// <remarks/>
    F10,

    /// <remarks/>
    F11,

    /// <remarks/>
    F12,

    /// <remarks/>
    F13,

    /// <remarks/>
    F14,

    /// <remarks/>
    F15,

    /// <remarks/>
    F16,

    /// <remarks/>
    F17,

    /// <remarks/>
    F18,

    /// <remarks/>
    F19,

    /// <remarks/>
    F20,

    /// <remarks/>
    F21,

    /// <remarks/>
    F22,

    /// <remarks/>
    F23,

    /// <remarks/>
    F24,

    /// <remarks/>
    F25,

    /// <remarks/>
    F26,

    /// <remarks/>
    F27,

    /// <remarks/>
    F28,

    /// <remarks/>
    F29,

    /// <remarks/>
    F30,

    /// <remarks/>
    F31,

    /// <remarks/>
    F32,

    /// <remarks/>
    F33,

    /// <remarks/>
    F34,

    /// <remarks/>
    F35,

    /// <remarks/>
    F36,

    /// <remarks/>
    F37,

    /// <remarks/>
    F38,

    /// <remarks/>
    F39,

    /// <remarks/>
    F40,

    /// <remarks/>
    F41,

    /// <remarks/>
    F42,

    /// <remarks/>
    F43,

    /// <remarks/>
    F44,

    /// <remarks/>
    F45,

    /// <remarks/>
    F46,

    /// <remarks/>
    F47,

    /// <remarks/>
    F48,

    /// <remarks/>
    F49,

    /// <remarks/>
    F50,

    /// <remarks/>
    F51,

    /// <remarks/>
    F52,

    /// <remarks/>
    F53,

    /// <remarks/>
    F54,

    /// <remarks/>
    F55,

    /// <remarks/>
    F56,

    /// <remarks/>
    F57,

    /// <remarks/>
    F58,

    /// <remarks/>
    F59,

    /// <remarks/>
    F60,

    /// <remarks/>
    F61,

    /// <remarks/>
    F62,

    /// <remarks/>
    F63,

    /// <remarks/>
    F64,

    /// <remarks/>
    F65,

    /// <remarks/>
    F66,

    /// <remarks/>
    F67,

    /// <remarks/>
    F68,

    /// <remarks/>
    F69,

    /// <remarks/>
    F70,

    /// <remarks/>
    F71,

    /// <remarks/>
    F72,

    /// <remarks/>
    F73,

    /// <remarks/>
    F74,

    /// <remarks/>
    F75,

    /// <remarks/>
    F76,

    /// <remarks/>
    F77,

    /// <remarks/>
    F78,

    /// <remarks/>
    F79,

    /// <remarks/>
    F80,

    /// <remarks/>
    F81,

    /// <remarks/>
    F82,

    /// <remarks/>
    F83,

    /// <remarks/>
    F84,

    /// <remarks/>
    F85,

    /// <remarks/>
    F86,

    /// <remarks/>
    F87,

    /// <remarks/>
    F88,

    /// <remarks/>
    F89,

    /// <remarks/>
    F9,

    /// <remarks/>
    F90,

    /// <remarks/>
    F91,

    /// <remarks/>
    F92,

    /// <remarks/>
    F93,

    /// <remarks/>
    F94,

    /// <remarks/>
    F95,

    /// <remarks/>
    F96,

    /// <remarks/>
    F97,

    /// <remarks/>
    F98,

    /// <remarks/>
    F99,

    /// <remarks/>
    FAH,

    /// <remarks/>
    FAR,

    /// <remarks/>
    FB,

    /// <remarks/>
    FBM,

    /// <remarks/>
    FC,

    /// <remarks/>
    FD,

    /// <remarks/>
    FE,

    /// <remarks/>
    FF,

    /// <remarks/>
    FG,

    /// <remarks/>
    FH,

    /// <remarks/>
    FIT,

    /// <remarks/>
    FL,

    /// <remarks/>
    FM,

    /// <remarks/>
    FOT,

    /// <remarks/>
    FP,

    /// <remarks/>
    FR,

    /// <remarks/>
    FS,

    /// <remarks/>
    FTK,

    /// <remarks/>
    FTQ,

    /// <remarks/>
    G01,

    /// <remarks/>
    G04,

    /// <remarks/>
    G05,

    /// <remarks/>
    G06,

    /// <remarks/>
    G08,

    /// <remarks/>
    G09,

    /// <remarks/>
    G10,

    /// <remarks/>
    G11,

    /// <remarks/>
    G12,

    /// <remarks/>
    G13,

    /// <remarks/>
    G14,

    /// <remarks/>
    G15,

    /// <remarks/>
    G16,

    /// <remarks/>
    G17,

    /// <remarks/>
    G18,

    /// <remarks/>
    G19,

    /// <remarks/>
    G2,

    /// <remarks/>
    G20,

    /// <remarks/>
    G21,

    /// <remarks/>
    G23,

    /// <remarks/>
    G24,

    /// <remarks/>
    G25,

    /// <remarks/>
    G26,

    /// <remarks/>
    G27,

    /// <remarks/>
    G28,

    /// <remarks/>
    G29,

    /// <remarks/>
    G3,

    /// <remarks/>
    G30,

    /// <remarks/>
    G31,

    /// <remarks/>
    G32,

    /// <remarks/>
    G33,

    /// <remarks/>
    G34,

    /// <remarks/>
    G35,

    /// <remarks/>
    G36,

    /// <remarks/>
    G37,

    /// <remarks/>
    G38,

    /// <remarks/>
    G39,

    /// <remarks/>
    G40,

    /// <remarks/>
    G41,

    /// <remarks/>
    G42,

    /// <remarks/>
    G43,

    /// <remarks/>
    G44,

    /// <remarks/>
    G45,

    /// <remarks/>
    G46,

    /// <remarks/>
    G47,

    /// <remarks/>
    G48,

    /// <remarks/>
    G49,

    /// <remarks/>
    G50,

    /// <remarks/>
    G51,

    /// <remarks/>
    G52,

    /// <remarks/>
    G53,

    /// <remarks/>
    G54,

    /// <remarks/>
    G55,

    /// <remarks/>
    G56,

    /// <remarks/>
    G57,

    /// <remarks/>
    G58,

    /// <remarks/>
    G59,

    /// <remarks/>
    G60,

    /// <remarks/>
    G61,

    /// <remarks/>
    G62,

    /// <remarks/>
    G63,

    /// <remarks/>
    G64,

    /// <remarks/>
    G65,

    /// <remarks/>
    G66,

    /// <remarks/>
    G67,

    /// <remarks/>
    G68,

    /// <remarks/>
    G69,

    /// <remarks/>
    G7,

    /// <remarks/>
    G70,

    /// <remarks/>
    G71,

    /// <remarks/>
    G72,

    /// <remarks/>
    G73,

    /// <remarks/>
    G74,

    /// <remarks/>
    G75,

    /// <remarks/>
    G76,

    /// <remarks/>
    G77,

    /// <remarks/>
    G78,

    /// <remarks/>
    G79,

    /// <remarks/>
    G80,

    /// <remarks/>
    G81,

    /// <remarks/>
    G82,

    /// <remarks/>
    G83,

    /// <remarks/>
    G84,

    /// <remarks/>
    G85,

    /// <remarks/>
    G86,

    /// <remarks/>
    G87,

    /// <remarks/>
    G88,

    /// <remarks/>
    G89,

    /// <remarks/>
    G90,

    /// <remarks/>
    G91,

    /// <remarks/>
    G92,

    /// <remarks/>
    G93,

    /// <remarks/>
    G94,

    /// <remarks/>
    G95,

    /// <remarks/>
    G96,

    /// <remarks/>
    G97,

    /// <remarks/>
    G98,

    /// <remarks/>
    G99,

    /// <remarks/>
    GB,

    /// <remarks/>
    GBQ,

    /// <remarks/>
    GC,

    /// <remarks/>
    GD,

    /// <remarks/>
    GDW,

    /// <remarks/>
    GE,

    /// <remarks/>
    GF,

    /// <remarks/>
    GFI,

    /// <remarks/>
    GGR,

    /// <remarks/>
    GH,

    /// <remarks/>
    GIA,

    /// <remarks/>
    GIC,

    /// <remarks/>
    GII,

    /// <remarks/>
    GIP,

    /// <remarks/>
    GJ,

    /// <remarks/>
    GK,

    /// <remarks/>
    GL,

    /// <remarks/>
    GLD,

    /// <remarks/>
    GLI,

    /// <remarks/>
    GLL,

    /// <remarks/>
    GM,

    /// <remarks/>
    GN,

    /// <remarks/>
    GO,

    /// <remarks/>
    GP,

    /// <remarks/>
    GQ,

    /// <remarks/>
    GRM,

    /// <remarks/>
    GRN,

    /// <remarks/>
    GRO,

    /// <remarks/>
    GRT,

    /// <remarks/>
    GT,

    /// <remarks/>
    GV,

    /// <remarks/>
    GW,

    /// <remarks/>
    GWH,

    /// <remarks/>
    GY,

    /// <remarks/>
    GZ,

    /// <remarks/>
    H03,

    /// <remarks/>
    H04,

    /// <remarks/>
    H05,

    /// <remarks/>
    H06,

    /// <remarks/>
    H07,

    /// <remarks/>
    H08,

    /// <remarks/>
    H09,

    /// <remarks/>
    H1,

    /// <remarks/>
    H10,

    /// <remarks/>
    H11,

    /// <remarks/>
    H12,

    /// <remarks/>
    H13,

    /// <remarks/>
    H14,

    /// <remarks/>
    H15,

    /// <remarks/>
    H16,

    /// <remarks/>
    H18,

    /// <remarks/>
    H19,

    /// <remarks/>
    H2,

    /// <remarks/>
    H20,

    /// <remarks/>
    H21,

    /// <remarks/>
    H22,

    /// <remarks/>
    H23,

    /// <remarks/>
    H24,

    /// <remarks/>
    H25,

    /// <remarks/>
    H26,

    /// <remarks/>
    H27,

    /// <remarks/>
    H28,

    /// <remarks/>
    H29,

    /// <remarks/>
    H30,

    /// <remarks/>
    H31,

    /// <remarks/>
    H32,

    /// <remarks/>
    H33,

    /// <remarks/>
    H34,

    /// <remarks/>
    H35,

    /// <remarks/>
    H36,

    /// <remarks/>
    H37,

    /// <remarks/>
    H38,

    /// <remarks/>
    H39,

    /// <remarks/>
    H40,

    /// <remarks/>
    H41,

    /// <remarks/>
    H42,

    /// <remarks/>
    H43,

    /// <remarks/>
    H44,

    /// <remarks/>
    H45,

    /// <remarks/>
    H46,

    /// <remarks/>
    H47,

    /// <remarks/>
    H48,

    /// <remarks/>
    H49,

    /// <remarks/>
    H50,

    /// <remarks/>
    H51,

    /// <remarks/>
    H52,

    /// <remarks/>
    H53,

    /// <remarks/>
    H54,

    /// <remarks/>
    H55,

    /// <remarks/>
    H56,

    /// <remarks/>
    H57,

    /// <remarks/>
    H58,

    /// <remarks/>
    H59,

    /// <remarks/>
    H60,

    /// <remarks/>
    H61,

    /// <remarks/>
    H62,

    /// <remarks/>
    H63,

    /// <remarks/>
    H64,

    /// <remarks/>
    H65,

    /// <remarks/>
    H66,

    /// <remarks/>
    H67,

    /// <remarks/>
    H68,

    /// <remarks/>
    H69,

    /// <remarks/>
    H70,

    /// <remarks/>
    H71,

    /// <remarks/>
    H72,

    /// <remarks/>
    H73,

    /// <remarks/>
    H74,

    /// <remarks/>
    H75,

    /// <remarks/>
    H76,

    /// <remarks/>
    H77,

    /// <remarks/>
    H78,

    /// <remarks/>
    H79,

    /// <remarks/>
    H80,

    /// <remarks/>
    H81,

    /// <remarks/>
    H82,

    /// <remarks/>
    H83,

    /// <remarks/>
    H84,

    /// <remarks/>
    H85,

    /// <remarks/>
    H87,

    /// <remarks/>
    H88,

    /// <remarks/>
    H89,

    /// <remarks/>
    H90,

    /// <remarks/>
    H91,

    /// <remarks/>
    H92,

    /// <remarks/>
    H93,

    /// <remarks/>
    H94,

    /// <remarks/>
    H95,

    /// <remarks/>
    H96,

    /// <remarks/>
    H98,

    /// <remarks/>
    H99,

    /// <remarks/>
    HA,

    /// <remarks/>
    HAR,

    /// <remarks/>
    HBA,

    /// <remarks/>
    HBX,

    /// <remarks/>
    HC,

    /// <remarks/>
    HD,

    /// <remarks/>
    HDW,

    /// <remarks/>
    HE,

    /// <remarks/>
    HF,

    /// <remarks/>
    HGM,

    /// <remarks/>
    HH,

    /// <remarks/>
    HI,

    /// <remarks/>
    HIU,

    /// <remarks/>
    HJ,

    /// <remarks/>
    HK,

    /// <remarks/>
    HKM,

    /// <remarks/>
    HL,

    /// <remarks/>
    HLT,

    /// <remarks/>
    HM,

    /// <remarks/>
    HMQ,

    /// <remarks/>
    HMT,

    /// <remarks/>
    HN,

    /// <remarks/>
    HO,

    /// <remarks/>
    HP,

    /// <remarks/>
    HPA,

    /// <remarks/>
    HS,

    /// <remarks/>
    HT,

    /// <remarks/>
    HTZ,

    /// <remarks/>
    HUR,

    /// <remarks/>
    HY,

    /// <remarks/>
    IA,

    /// <remarks/>
    IC,

    /// <remarks/>
    IE,

    /// <remarks/>
    IF,

    /// <remarks/>
    II,

    /// <remarks/>
    IL,

    /// <remarks/>
    IM,

    /// <remarks/>
    INH,

    /// <remarks/>
    INK,

    /// <remarks/>
    INQ,

    /// <remarks/>
    IP,

    /// <remarks/>
    ISD,

    /// <remarks/>
    IT,

    /// <remarks/>
    IU,

    /// <remarks/>
    IV,

    /// <remarks/>
    J10,

    /// <remarks/>
    J12,

    /// <remarks/>
    J13,

    /// <remarks/>
    J14,

    /// <remarks/>
    J15,

    /// <remarks/>
    J16,

    /// <remarks/>
    J17,

    /// <remarks/>
    J18,

    /// <remarks/>
    J19,

    /// <remarks/>
    J2,

    /// <remarks/>
    J20,

    /// <remarks/>
    J21,

    /// <remarks/>
    J22,

    /// <remarks/>
    J23,

    /// <remarks/>
    J24,

    /// <remarks/>
    J25,

    /// <remarks/>
    J26,

    /// <remarks/>
    J27,

    /// <remarks/>
    J28,

    /// <remarks/>
    J29,

    /// <remarks/>
    J30,

    /// <remarks/>
    J31,

    /// <remarks/>
    J32,

    /// <remarks/>
    J33,

    /// <remarks/>
    J34,

    /// <remarks/>
    J35,

    /// <remarks/>
    J36,

    /// <remarks/>
    J38,

    /// <remarks/>
    J39,

    /// <remarks/>
    J40,

    /// <remarks/>
    J41,

    /// <remarks/>
    J42,

    /// <remarks/>
    J43,

    /// <remarks/>
    J44,

    /// <remarks/>
    J45,

    /// <remarks/>
    J46,

    /// <remarks/>
    J47,

    /// <remarks/>
    J48,

    /// <remarks/>
    J49,

    /// <remarks/>
    J50,

    /// <remarks/>
    J51,

    /// <remarks/>
    J52,

    /// <remarks/>
    J53,

    /// <remarks/>
    J54,

    /// <remarks/>
    J55,

    /// <remarks/>
    J56,

    /// <remarks/>
    J57,

    /// <remarks/>
    J58,

    /// <remarks/>
    J59,

    /// <remarks/>
    J60,

    /// <remarks/>
    J61,

    /// <remarks/>
    J62,

    /// <remarks/>
    J63,

    /// <remarks/>
    J64,

    /// <remarks/>
    J65,

    /// <remarks/>
    J66,

    /// <remarks/>
    J67,

    /// <remarks/>
    J68,

    /// <remarks/>
    J69,

    /// <remarks/>
    J70,

    /// <remarks/>
    J71,

    /// <remarks/>
    J72,

    /// <remarks/>
    J73,

    /// <remarks/>
    J74,

    /// <remarks/>
    J75,

    /// <remarks/>
    J76,

    /// <remarks/>
    J78,

    /// <remarks/>
    J79,

    /// <remarks/>
    J81,

    /// <remarks/>
    J82,

    /// <remarks/>
    J83,

    /// <remarks/>
    J84,

    /// <remarks/>
    J85,

    /// <remarks/>
    J87,

    /// <remarks/>
    J89,

    /// <remarks/>
    J90,

    /// <remarks/>
    J91,

    /// <remarks/>
    J92,

    /// <remarks/>
    J93,

    /// <remarks/>
    J94,

    /// <remarks/>
    J95,

    /// <remarks/>
    J96,

    /// <remarks/>
    J97,

    /// <remarks/>
    J98,

    /// <remarks/>
    J99,

    /// <remarks/>
    JB,

    /// <remarks/>
    JE,

    /// <remarks/>
    JG,

    /// <remarks/>
    JK,

    /// <remarks/>
    JM,

    /// <remarks/>
    JNT,

    /// <remarks/>
    JO,

    /// <remarks/>
    JOU,

    /// <remarks/>
    JPS,

    /// <remarks/>
    JR,

    /// <remarks/>
    JWL,

    /// <remarks/>
    K1,

    /// <remarks/>
    K10,

    /// <remarks/>
    K11,

    /// <remarks/>
    K12,

    /// <remarks/>
    K13,

    /// <remarks/>
    K14,

    /// <remarks/>
    K15,

    /// <remarks/>
    K16,

    /// <remarks/>
    K17,

    /// <remarks/>
    K18,

    /// <remarks/>
    K19,

    /// <remarks/>
    K2,

    /// <remarks/>
    K20,

    /// <remarks/>
    K21,

    /// <remarks/>
    K22,

    /// <remarks/>
    K23,

    /// <remarks/>
    K24,

    /// <remarks/>
    K25,

    /// <remarks/>
    K26,

    /// <remarks/>
    K27,

    /// <remarks/>
    K28,

    /// <remarks/>
    K3,

    /// <remarks/>
    K30,

    /// <remarks/>
    K31,

    /// <remarks/>
    K32,

    /// <remarks/>
    K33,

    /// <remarks/>
    K34,

    /// <remarks/>
    K35,

    /// <remarks/>
    K36,

    /// <remarks/>
    K37,

    /// <remarks/>
    K38,

    /// <remarks/>
    K39,

    /// <remarks/>
    K40,

    /// <remarks/>
    K41,

    /// <remarks/>
    K42,

    /// <remarks/>
    K43,

    /// <remarks/>
    K45,

    /// <remarks/>
    K46,

    /// <remarks/>
    K47,

    /// <remarks/>
    K48,

    /// <remarks/>
    K49,

    /// <remarks/>
    K5,

    /// <remarks/>
    K50,

    /// <remarks/>
    K51,

    /// <remarks/>
    K52,

    /// <remarks/>
    K53,

    /// <remarks/>
    K54,

    /// <remarks/>
    K55,

    /// <remarks/>
    K58,

    /// <remarks/>
    K59,

    /// <remarks/>
    K6,

    /// <remarks/>
    K60,

    /// <remarks/>
    K61,

    /// <remarks/>
    K62,

    /// <remarks/>
    K63,

    /// <remarks/>
    K64,

    /// <remarks/>
    K65,

    /// <remarks/>
    K66,

    /// <remarks/>
    K67,

    /// <remarks/>
    K68,

    /// <remarks/>
    K69,

    /// <remarks/>
    K70,

    /// <remarks/>
    K71,

    /// <remarks/>
    K73,

    /// <remarks/>
    K74,

    /// <remarks/>
    K75,

    /// <remarks/>
    K76,

    /// <remarks/>
    K77,

    /// <remarks/>
    K78,

    /// <remarks/>
    K79,

    /// <remarks/>
    K80,

    /// <remarks/>
    K81,

    /// <remarks/>
    K82,

    /// <remarks/>
    K83,

    /// <remarks/>
    K84,

    /// <remarks/>
    K85,

    /// <remarks/>
    K86,

    /// <remarks/>
    K87,

    /// <remarks/>
    K88,

    /// <remarks/>
    K89,

    /// <remarks/>
    K90,

    /// <remarks/>
    K91,

    /// <remarks/>
    K92,

    /// <remarks/>
    K93,

    /// <remarks/>
    K94,

    /// <remarks/>
    K95,

    /// <remarks/>
    K96,

    /// <remarks/>
    K97,

    /// <remarks/>
    K98,

    /// <remarks/>
    K99,

    /// <remarks/>
    KA,

    /// <remarks/>
    KAT,

    /// <remarks/>
    KB,

    /// <remarks/>
    KBA,

    /// <remarks/>
    KCC,

    /// <remarks/>
    KD,

    /// <remarks/>
    KDW,

    /// <remarks/>
    KEL,

    /// <remarks/>
    KF,

    /// <remarks/>
    KG,

    /// <remarks/>
    KGM,

    /// <remarks/>
    KGS,

    /// <remarks/>
    KHY,

    /// <remarks/>
    KHZ,

    /// <remarks/>
    KI,

    /// <remarks/>
    KIC,

    /// <remarks/>
    KIP,

    /// <remarks/>
    KJ,

    /// <remarks/>
    KJO,

    /// <remarks/>
    KL,

    /// <remarks/>
    KLK,

    /// <remarks/>
    KMA,

    /// <remarks/>
    KMH,

    /// <remarks/>
    KMK,

    /// <remarks/>
    KMQ,

    /// <remarks/>
    KMT,

    /// <remarks/>
    KNI,

    /// <remarks/>
    KNS,

    /// <remarks/>
    KNT,

    /// <remarks/>
    KO,

    /// <remarks/>
    KPA,

    /// <remarks/>
    KPH,

    /// <remarks/>
    KPO,

    /// <remarks/>
    KPP,

    /// <remarks/>
    KR,

    /// <remarks/>
    KS,

    /// <remarks/>
    KSD,

    /// <remarks/>
    KSH,

    /// <remarks/>
    KT,

    /// <remarks/>
    KTM,

    /// <remarks/>
    KTN,

    /// <remarks/>
    KUR,

    /// <remarks/>
    KVA,

    /// <remarks/>
    KVR,

    /// <remarks/>
    KVT,

    /// <remarks/>
    KW,

    /// <remarks/>
    KWH,

    /// <remarks/>
    KWO,

    /// <remarks/>
    KWT,

    /// <remarks/>
    KX,

    /// <remarks/>
    L10,

    /// <remarks/>
    L11,

    /// <remarks/>
    L12,

    /// <remarks/>
    L13,

    /// <remarks/>
    L14,

    /// <remarks/>
    L15,

    /// <remarks/>
    L16,

    /// <remarks/>
    L17,

    /// <remarks/>
    L18,

    /// <remarks/>
    L19,

    /// <remarks/>
    L2,

    /// <remarks/>
    L20,

    /// <remarks/>
    L21,

    /// <remarks/>
    L23,

    /// <remarks/>
    L24,

    /// <remarks/>
    L25,

    /// <remarks/>
    L26,

    /// <remarks/>
    L27,

    /// <remarks/>
    L28,

    /// <remarks/>
    L29,

    /// <remarks/>
    L30,

    /// <remarks/>
    L31,

    /// <remarks/>
    L32,

    /// <remarks/>
    L33,

    /// <remarks/>
    L34,

    /// <remarks/>
    L35,

    /// <remarks/>
    L36,

    /// <remarks/>
    L37,

    /// <remarks/>
    L38,

    /// <remarks/>
    L39,

    /// <remarks/>
    L40,

    /// <remarks/>
    L41,

    /// <remarks/>
    L42,

    /// <remarks/>
    L43,

    /// <remarks/>
    L44,

    /// <remarks/>
    L45,

    /// <remarks/>
    L46,

    /// <remarks/>
    L47,

    /// <remarks/>
    L48,

    /// <remarks/>
    L49,

    /// <remarks/>
    L50,

    /// <remarks/>
    L51,

    /// <remarks/>
    L52,

    /// <remarks/>
    L53,

    /// <remarks/>
    L54,

    /// <remarks/>
    L55,

    /// <remarks/>
    L56,

    /// <remarks/>
    L57,

    /// <remarks/>
    L58,

    /// <remarks/>
    L59,

    /// <remarks/>
    L60,

    /// <remarks/>
    L61,

    /// <remarks/>
    L62,

    /// <remarks/>
    L63,

    /// <remarks/>
    L64,

    /// <remarks/>
    L65,

    /// <remarks/>
    L66,

    /// <remarks/>
    L67,

    /// <remarks/>
    L68,

    /// <remarks/>
    L69,

    /// <remarks/>
    L70,

    /// <remarks/>
    L71,

    /// <remarks/>
    L72,

    /// <remarks/>
    L73,

    /// <remarks/>
    L74,

    /// <remarks/>
    L75,

    /// <remarks/>
    L76,

    /// <remarks/>
    L77,

    /// <remarks/>
    L78,

    /// <remarks/>
    L79,

    /// <remarks/>
    L80,

    /// <remarks/>
    L81,

    /// <remarks/>
    L82,

    /// <remarks/>
    L83,

    /// <remarks/>
    L84,

    /// <remarks/>
    L85,

    /// <remarks/>
    L86,

    /// <remarks/>
    L87,

    /// <remarks/>
    L88,

    /// <remarks/>
    L89,

    /// <remarks/>
    L90,

    /// <remarks/>
    L91,

    /// <remarks/>
    L92,

    /// <remarks/>
    L93,

    /// <remarks/>
    L94,

    /// <remarks/>
    L95,

    /// <remarks/>
    L96,

    /// <remarks/>
    L98,

    /// <remarks/>
    L99,

    /// <remarks/>
    LA,

    /// <remarks/>
    LAC,

    /// <remarks/>
    LBR,

    /// <remarks/>
    LBT,

    /// <remarks/>
    LC,

    /// <remarks/>
    LD,

    /// <remarks/>
    LE,

    /// <remarks/>
    LEF,

    /// <remarks/>
    LF,

    /// <remarks/>
    LH,

    /// <remarks/>
    LI,

    /// <remarks/>
    LJ,

    /// <remarks/>
    LK,

    /// <remarks/>
    LM,

    /// <remarks/>
    LN,

    /// <remarks/>
    LO,

    /// <remarks/>
    LP,

    /// <remarks/>
    LPA,

    /// <remarks/>
    LR,

    /// <remarks/>
    LS,

    /// <remarks/>
    LTN,

    /// <remarks/>
    LTR,

    /// <remarks/>
    LUB,

    /// <remarks/>
    LUM,

    /// <remarks/>
    LUX,

    /// <remarks/>
    LX,

    /// <remarks/>
    LY,

    /// <remarks/>
    M0,

    /// <remarks/>
    M1,

    /// <remarks/>
    M10,

    /// <remarks/>
    M11,

    /// <remarks/>
    M12,

    /// <remarks/>
    M13,

    /// <remarks/>
    M14,

    /// <remarks/>
    M15,

    /// <remarks/>
    M16,

    /// <remarks/>
    M17,

    /// <remarks/>
    M18,

    /// <remarks/>
    M19,

    /// <remarks/>
    M20,

    /// <remarks/>
    M21,

    /// <remarks/>
    M22,

    /// <remarks/>
    M23,

    /// <remarks/>
    M24,

    /// <remarks/>
    M25,

    /// <remarks/>
    M26,

    /// <remarks/>
    M27,

    /// <remarks/>
    M29,

    /// <remarks/>
    M30,

    /// <remarks/>
    M31,

    /// <remarks/>
    M32,

    /// <remarks/>
    M33,

    /// <remarks/>
    M34,

    /// <remarks/>
    M35,

    /// <remarks/>
    M36,

    /// <remarks/>
    M37,

    /// <remarks/>
    M4,

    /// <remarks/>
    M5,

    /// <remarks/>
    M7,

    /// <remarks/>
    M9,

    /// <remarks/>
    MA,

    /// <remarks/>
    MAH,

    /// <remarks/>
    MAL,

    /// <remarks/>
    MAM,

    /// <remarks/>
    MAR,

    /// <remarks/>
    MAW,

    /// <remarks/>
    MBE,

    /// <remarks/>
    MBF,

    /// <remarks/>
    MBR,

    /// <remarks/>
    MC,

    /// <remarks/>
    MCU,

    /// <remarks/>
    MD,

    /// <remarks/>
    MF,

    /// <remarks/>
    MGM,

    /// <remarks/>
    MHZ,

    /// <remarks/>
    MIK,

    /// <remarks/>
    MIL,

    /// <remarks/>
    MIN,

    /// <remarks/>
    MIO,

    /// <remarks/>
    MIU,

    /// <remarks/>
    MK,

    /// <remarks/>
    MLD,

    /// <remarks/>
    MLT,

    /// <remarks/>
    MMK,

    /// <remarks/>
    MMQ,

    /// <remarks/>
    MMT,

    /// <remarks/>
    MND,

    /// <remarks/>
    MON,

    /// <remarks/>
    MPA,

    /// <remarks/>
    MQ,

    /// <remarks/>
    MQH,

    /// <remarks/>
    MQS,

    /// <remarks/>
    MSK,

    /// <remarks/>
    MT,

    /// <remarks/>
    MTK,

    /// <remarks/>
    MTQ,

    /// <remarks/>
    MTR,

    /// <remarks/>
    MTS,

    /// <remarks/>
    MV,

    /// <remarks/>
    MVA,

    /// <remarks/>
    MWH,

    /// <remarks/>
    N1,

    /// <remarks/>
    N2,

    /// <remarks/>
    N3,

    /// <remarks/>
    NA,

    /// <remarks/>
    NAR,

    /// <remarks/>
    NB,

    /// <remarks/>
    NBB,

    /// <remarks/>
    NC,

    /// <remarks/>
    NCL,

    /// <remarks/>
    ND,

    /// <remarks/>
    NE,

    /// <remarks/>
    NEW,

    /// <remarks/>
    NF,

    /// <remarks/>
    NG,

    /// <remarks/>
    NH,

    /// <remarks/>
    NI,

    /// <remarks/>
    NIL,

    /// <remarks/>
    NIU,

    /// <remarks/>
    NJ,

    /// <remarks/>
    NL,

    /// <remarks/>
    NMI,

    /// <remarks/>
    NMP,

    /// <remarks/>
    NN,

    /// <remarks/>
    NPL,

    /// <remarks/>
    NPR,

    /// <remarks/>
    NPT,

    /// <remarks/>
    NQ,

    /// <remarks/>
    NR,

    /// <remarks/>
    NRL,

    /// <remarks/>
    NT,

    /// <remarks/>
    NTT,

    /// <remarks/>
    NU,

    /// <remarks/>
    NV,

    /// <remarks/>
    NX,

    /// <remarks/>
    NY,

    /// <remarks/>
    OA,

    /// <remarks/>
    ODE,

    /// <remarks/>
    OHM,

    /// <remarks/>
    ON,

    /// <remarks/>
    ONZ,

    /// <remarks/>
    OP,

    /// <remarks/>
    OT,

    /// <remarks/>
    OZ,

    /// <remarks/>
    OZA,

    /// <remarks/>
    OZI,

    /// <remarks/>
    P0,

    /// <remarks/>
    P1,

    /// <remarks/>
    P2,

    /// <remarks/>
    P3,

    /// <remarks/>
    P4,

    /// <remarks/>
    P5,

    /// <remarks/>
    P6,

    /// <remarks/>
    P7,

    /// <remarks/>
    P8,

    /// <remarks/>
    P9,

    /// <remarks/>
    PA,

    /// <remarks/>
    PAL,

    /// <remarks/>
    PB,

    /// <remarks/>
    PD,

    /// <remarks/>
    PE,

    /// <remarks/>
    PF,

    /// <remarks/>
    PFL,

    /// <remarks/>
    PG,

    /// <remarks/>
    PGL,

    /// <remarks/>
    PI,

    /// <remarks/>
    PK,

    /// <remarks/>
    PL,

    /// <remarks/>
    PLA,

    /// <remarks/>
    PM,

    /// <remarks/>
    PN,

    /// <remarks/>
    PO,

    /// <remarks/>
    PQ,

    /// <remarks/>
    PR,

    /// <remarks/>
    PS,

    /// <remarks/>
    PT,

    /// <remarks/>
    PTD,

    /// <remarks/>
    PTI,

    /// <remarks/>
    PTL,

    /// <remarks/>
    PU,

    /// <remarks/>
    PV,

    /// <remarks/>
    PW,

    /// <remarks/>
    PY,

    /// <remarks/>
    PZ,

    /// <remarks/>
    Q3,

    /// <remarks/>
    QA,

    /// <remarks/>
    QAN,

    /// <remarks/>
    QB,

    /// <remarks/>
    QD,

    /// <remarks/>
    QH,

    /// <remarks/>
    QK,

    /// <remarks/>
    QR,

    /// <remarks/>
    QT,

    /// <remarks/>
    QTD,

    /// <remarks/>
    QTI,

    /// <remarks/>
    QTL,

    /// <remarks/>
    QTR,

    /// <remarks/>
    R1,

    /// <remarks/>
    R4,

    /// <remarks/>
    R9,

    /// <remarks/>
    RA,

    /// <remarks/>
    RD,

    /// <remarks/>
    RG,

    /// <remarks/>
    RH,

    /// <remarks/>
    RK,

    /// <remarks/>
    RL,

    /// <remarks/>
    RM,

    /// <remarks/>
    RN,

    /// <remarks/>
    RO,

    /// <remarks/>
    RP,

    /// <remarks/>
    RPM,

    /// <remarks/>
    RPS,

    /// <remarks/>
    RS,

    /// <remarks/>
    RT,

    /// <remarks/>
    RU,

    /// <remarks/>
    S3,

    /// <remarks/>
    S4,

    /// <remarks/>
    S5,

    /// <remarks/>
    S6,

    /// <remarks/>
    S7,

    /// <remarks/>
    S8,

    /// <remarks/>
    SA,

    /// <remarks/>
    SAN,

    /// <remarks/>
    SCO,

    /// <remarks/>
    SCR,

    /// <remarks/>
    SD,

    /// <remarks/>
    SE,

    /// <remarks/>
    SEC,

    /// <remarks/>
    SET,

    /// <remarks/>
    SG,

    /// <remarks/>
    SHT,

    /// <remarks/>
    SIE,

    /// <remarks/>
    SK,

    /// <remarks/>
    SL,

    /// <remarks/>
    SMI,

    /// <remarks/>
    SN,

    /// <remarks/>
    SO,

    /// <remarks/>
    SP,

    /// <remarks/>
    SQ,

    /// <remarks/>
    SQR,

    /// <remarks/>
    SR,

    /// <remarks/>
    SS,

    /// <remarks/>
    SST,

    /// <remarks/>
    ST,

    /// <remarks/>
    STI,

    /// <remarks/>
    STK,

    /// <remarks/>
    STL,

    /// <remarks/>
    STN,

    /// <remarks/>
    SV,

    /// <remarks/>
    SW,

    /// <remarks/>
    SX,

    /// <remarks/>
    T0,

    /// <remarks/>
    T1,

    /// <remarks/>
    T3,

    /// <remarks/>
    T4,

    /// <remarks/>
    T5,

    /// <remarks/>
    T6,

    /// <remarks/>
    T7,

    /// <remarks/>
    T8,

    /// <remarks/>
    TA,

    /// <remarks/>
    TAH,

    /// <remarks/>
    TC,

    /// <remarks/>
    TD,

    /// <remarks/>
    TE,

    /// <remarks/>
    TF,

    /// <remarks/>
    TI,

    /// <remarks/>
    TIC,

    /// <remarks/>
    TIP,

    /// <remarks/>
    TJ,

    /// <remarks/>
    TK,

    /// <remarks/>
    TL,

    /// <remarks/>
    TMS,

    /// <remarks/>
    TN,

    /// <remarks/>
    TNE,

    /// <remarks/>
    TP,

    /// <remarks/>
    TPR,

    /// <remarks/>
    TQ,

    /// <remarks/>
    TQD,

    /// <remarks/>
    TR,

    /// <remarks/>
    TRL,

    /// <remarks/>
    TS,

    /// <remarks/>
    TSD,

    /// <remarks/>
    TSH,

    /// <remarks/>
    TT,

    /// <remarks/>
    TU,

    /// <remarks/>
    TV,

    /// <remarks/>
    TW,

    /// <remarks/>
    TY,

    /// <remarks/>
    U1,

    /// <remarks/>
    U2,

    /// <remarks/>
    UA,

    /// <remarks/>
    UB,

    /// <remarks/>
    UC,

    /// <remarks/>
    UD,

    /// <remarks/>
    UE,

    /// <remarks/>
    UF,

    /// <remarks/>
    UH,

    /// <remarks/>
    UM,

    /// <remarks/>
    VA,

    /// <remarks/>
    VI,

    /// <remarks/>
    VLT,

    /// <remarks/>
    VP,

    /// <remarks/>
    VQ,

    /// <remarks/>
    VS,

    /// <remarks/>
    W2,

    /// <remarks/>
    W4,

    /// <remarks/>
    WA,

    /// <remarks/>
    WB,

    /// <remarks/>
    WCD,

    /// <remarks/>
    WE,

    /// <remarks/>
    WEB,

    /// <remarks/>
    WEE,

    /// <remarks/>
    WG,

    /// <remarks/>
    WH,

    /// <remarks/>
    WHR,

    /// <remarks/>
    WI,

    /// <remarks/>
    WM,

    /// <remarks/>
    WR,

    /// <remarks/>
    WSD,

    /// <remarks/>
    WTT,

    /// <remarks/>
    WW,

    /// <remarks/>
    X1,

    /// <remarks/>
    YDK,

    /// <remarks/>
    YDQ,

    /// <remarks/>
    YL,

    /// <remarks/>
    YRD,

    /// <remarks/>
    YT,

    /// <remarks/>
    Z1,

    /// <remarks/>
    Z2,

    /// <remarks/>
    Z3,

    /// <remarks/>
    Z4,

    /// <remarks/>
    Z5,

    /// <remarks/>
    Z6,

    /// <remarks/>
    Z8,

    /// <remarks/>
    ZP,

    /// <remarks/>
    ZZ,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:8")]
public partial class QuantityType
{

    private MeasurementUnitCommonCodeContentType unitCodeField;

    private bool unitCodeFieldSpecified;

    private decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public MeasurementUnitCommonCodeContentType unitCode
    {
        get
        {
            return this.unitCodeField;
        }
        set
        {
            this.unitCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool unitCodeSpecified
    {
        get
        {
            return this.unitCodeFieldSpecified;
        }
        set
        {
            this.unitCodeFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
public partial class TransportContractDocumentType
{

    private IDType idField;

    /// <remarks/>
    public IDType ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("OriginLocation", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class OriginLocationType
{

    private IDType idField;

    private TextType nameField;

    /// <remarks/>
    public IDType ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public TextType Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("FinalDestinationLocation", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class FinalDestinationLocationType
{

    private IDType idField;

    private TextType nameField;

    /// <remarks/>
    public IDType ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public TextType Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("CustomsNote", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class CustomsNoteType
{

    private CodeType contentCodeField;

    private TextType contentField;

    private CodeType subjectCodeField;

    private CountryIDType countryIDField;

    /// <remarks/>
    public CodeType ContentCode
    {
        get
        {
            return this.contentCodeField;
        }
        set
        {
            this.contentCodeField = value;
        }
    }

    /// <remarks/>
    public TextType Content
    {
        get
        {
            return this.contentField;
        }
        set
        {
            this.contentField = value;
        }
    }

    /// <remarks/>
    public CodeType SubjectCode
    {
        get
        {
            return this.subjectCodeField;
        }
        set
        {
            this.subjectCodeField = value;
        }
    }

    /// <remarks/>
    public CountryIDType CountryID
    {
        get
        {
            return this.countryIDField;
        }
        set
        {
            this.countryIDField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:QualifiedDataType:7")]
public partial class CountryIDType
{

    private string schemeIDField;

    private AgencyIdentificationCodeContentType schemeAgencyIDField;

    private bool schemeAgencyIDFieldSpecified;

    private string schemeVersionIDField;

    private ISOTwoletterCountryCodeIdentifierContentType valueField;

    public CountryIDType()
    {
        this.schemeVersionIDField = "second edition 2006";
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string schemeID
    {
        get
        {
            return this.schemeIDField;
        }
        set
        {
            this.schemeIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public AgencyIdentificationCodeContentType schemeAgencyID
    {
        get
        {
            return this.schemeAgencyIDField;
        }
        set
        {
            this.schemeAgencyIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool schemeAgencyIDSpecified
    {
        get
        {
            return this.schemeAgencyIDFieldSpecified;
        }
        set
        {
            this.schemeAgencyIDFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
    public string schemeVersionID
    {
        get
        {
            return this.schemeVersionIDField;
        }
        set
        {
            this.schemeVersionIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public ISOTwoletterCountryCodeIdentifierContentType Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:identifierlist:standard:5:ISO316612A:SecondEdition2006VI-6")]
[System.Xml.Serialization.XmlRootAttribute("ISOTwoletterCountryCodeIdentifier", Namespace = "urn:un:unece:uncefact:identifierlist:standard:5:ISO316612A:SecondEdition2006VI-6", IsNullable = false)]
public enum ISOTwoletterCountryCodeIdentifierContentType
{

    /// <remarks/>
    AD,

    /// <remarks/>
    AE,

    /// <remarks/>
    AF,

    /// <remarks/>
    AG,

    /// <remarks/>
    AI,

    /// <remarks/>
    AL,

    /// <remarks/>
    AM,

    /// <remarks/>
    AO,

    /// <remarks/>
    AQ,

    /// <remarks/>
    AR,

    /// <remarks/>
    AS,

    /// <remarks/>
    AT,

    /// <remarks/>
    AU,

    /// <remarks/>
    AW,

    /// <remarks/>
    AX,

    /// <remarks/>
    AZ,

    /// <remarks/>
    BA,

    /// <remarks/>
    BB,

    /// <remarks/>
    BD,

    /// <remarks/>
    BE,

    /// <remarks/>
    BF,

    /// <remarks/>
    BG,

    /// <remarks/>
    BH,

    /// <remarks/>
    BI,

    /// <remarks/>
    BJ,

    /// <remarks/>
    BL,

    /// <remarks/>
    BM,

    /// <remarks/>
    BN,

    /// <remarks/>
    BO,

    /// <remarks/>
    BQ,

    /// <remarks/>
    BR,

    /// <remarks/>
    BS,

    /// <remarks/>
    BT,

    /// <remarks/>
    BV,

    /// <remarks/>
    BW,

    /// <remarks/>
    BY,

    /// <remarks/>
    BZ,

    /// <remarks/>
    CA,

    /// <remarks/>
    CC,

    /// <remarks/>
    CD,

    /// <remarks/>
    CF,

    /// <remarks/>
    CG,

    /// <remarks/>
    CH,

    /// <remarks/>
    CI,

    /// <remarks/>
    CK,

    /// <remarks/>
    CL,

    /// <remarks/>
    CM,

    /// <remarks/>
    CN,

    /// <remarks/>
    CO,

    /// <remarks/>
    CR,

    /// <remarks/>
    CU,

    /// <remarks/>
    CV,

    /// <remarks/>
    CW,

    /// <remarks/>
    CX,

    /// <remarks/>
    CY,

    /// <remarks/>
    CZ,

    /// <remarks/>
    DE,

    /// <remarks/>
    DJ,

    /// <remarks/>
    DK,

    /// <remarks/>
    DM,

    /// <remarks/>
    DO,

    /// <remarks/>
    DZ,

    /// <remarks/>
    EC,

    /// <remarks/>
    EE,

    /// <remarks/>
    EG,

    /// <remarks/>
    EH,

    /// <remarks/>
    ER,

    /// <remarks/>
    ES,

    /// <remarks/>
    ET,

    /// <remarks/>
    FI,

    /// <remarks/>
    FJ,

    /// <remarks/>
    FK,

    /// <remarks/>
    FM,

    /// <remarks/>
    FO,

    /// <remarks/>
    FR,

    /// <remarks/>
    GA,

    /// <remarks/>
    GB,

    /// <remarks/>
    GD,

    /// <remarks/>
    GE,

    /// <remarks/>
    GF,

    /// <remarks/>
    GG,

    /// <remarks/>
    GH,

    /// <remarks/>
    GI,

    /// <remarks/>
    GL,

    /// <remarks/>
    GM,

    /// <remarks/>
    GN,

    /// <remarks/>
    GP,

    /// <remarks/>
    GQ,

    /// <remarks/>
    GR,

    /// <remarks/>
    GS,

    /// <remarks/>
    GT,

    /// <remarks/>
    GU,

    /// <remarks/>
    GW,

    /// <remarks/>
    GY,

    /// <remarks/>
    HK,

    /// <remarks/>
    HM,

    /// <remarks/>
    HN,

    /// <remarks/>
    HR,

    /// <remarks/>
    HT,

    /// <remarks/>
    HU,

    /// <remarks/>
    ID,

    /// <remarks/>
    IE,

    /// <remarks/>
    IL,

    /// <remarks/>
    IM,

    /// <remarks/>
    IN,

    /// <remarks/>
    IO,

    /// <remarks/>
    IQ,

    /// <remarks/>
    IR,

    /// <remarks/>
    IS,

    /// <remarks/>
    IT,

    /// <remarks/>
    JE,

    /// <remarks/>
    JM,

    /// <remarks/>
    JO,

    /// <remarks/>
    JP,

    /// <remarks/>
    KE,

    /// <remarks/>
    KG,

    /// <remarks/>
    KH,

    /// <remarks/>
    KI,

    /// <remarks/>
    KM,

    /// <remarks/>
    KN,

    /// <remarks/>
    KP,

    /// <remarks/>
    KR,

    /// <remarks/>
    KW,

    /// <remarks/>
    KY,

    /// <remarks/>
    KZ,

    /// <remarks/>
    LA,

    /// <remarks/>
    LB,

    /// <remarks/>
    LC,

    /// <remarks/>
    LI,

    /// <remarks/>
    LK,

    /// <remarks/>
    LR,

    /// <remarks/>
    LS,

    /// <remarks/>
    LT,

    /// <remarks/>
    LU,

    /// <remarks/>
    LV,

    /// <remarks/>
    LY,

    /// <remarks/>
    MA,

    /// <remarks/>
    MC,

    /// <remarks/>
    MD,

    /// <remarks/>
    ME,

    /// <remarks/>
    MF,

    /// <remarks/>
    MG,

    /// <remarks/>
    MH,

    /// <remarks/>
    MK,

    /// <remarks/>
    ML,

    /// <remarks/>
    MM,

    /// <remarks/>
    MN,

    /// <remarks/>
    MO,

    /// <remarks/>
    MP,

    /// <remarks/>
    MQ,

    /// <remarks/>
    MR,

    /// <remarks/>
    MS,

    /// <remarks/>
    MT,

    /// <remarks/>
    MU,

    /// <remarks/>
    MV,

    /// <remarks/>
    MW,

    /// <remarks/>
    MX,

    /// <remarks/>
    MY,

    /// <remarks/>
    MZ,

    /// <remarks/>
    NA,

    /// <remarks/>
    NC,

    /// <remarks/>
    NE,

    /// <remarks/>
    NF,

    /// <remarks/>
    NG,

    /// <remarks/>
    NI,

    /// <remarks/>
    NL,

    /// <remarks/>
    NO,

    /// <remarks/>
    NP,

    /// <remarks/>
    NR,

    /// <remarks/>
    NU,

    /// <remarks/>
    NZ,

    /// <remarks/>
    OM,

    /// <remarks/>
    PA,

    /// <remarks/>
    PE,

    /// <remarks/>
    PF,

    /// <remarks/>
    PG,

    /// <remarks/>
    PH,

    /// <remarks/>
    PK,

    /// <remarks/>
    PL,

    /// <remarks/>
    PM,

    /// <remarks/>
    PN,

    /// <remarks/>
    PR,

    /// <remarks/>
    PS,

    /// <remarks/>
    PT,

    /// <remarks/>
    PW,

    /// <remarks/>
    PY,

    /// <remarks/>
    QA,

    /// <remarks/>
    RE,

    /// <remarks/>
    RO,

    /// <remarks/>
    RS,

    /// <remarks/>
    RU,

    /// <remarks/>
    RW,

    /// <remarks/>
    SA,

    /// <remarks/>
    SB,

    /// <remarks/>
    SC,

    /// <remarks/>
    SD,

    /// <remarks/>
    SE,

    /// <remarks/>
    SG,

    /// <remarks/>
    SH,

    /// <remarks/>
    SI,

    /// <remarks/>
    SJ,

    /// <remarks/>
    SK,

    /// <remarks/>
    SL,

    /// <remarks/>
    SM,

    /// <remarks/>
    SN,

    /// <remarks/>
    SO,

    /// <remarks/>
    SR,

    /// <remarks/>
    SS,

    /// <remarks/>
    ST,

    /// <remarks/>
    SV,

    /// <remarks/>
    SX,

    /// <remarks/>
    SY,

    /// <remarks/>
    SZ,

    /// <remarks/>
    TC,

    /// <remarks/>
    TD,

    /// <remarks/>
    TF,

    /// <remarks/>
    TG,

    /// <remarks/>
    TH,

    /// <remarks/>
    TJ,

    /// <remarks/>
    TK,

    /// <remarks/>
    TL,

    /// <remarks/>
    TM,

    /// <remarks/>
    TN,

    /// <remarks/>
    TO,

    /// <remarks/>
    TR,

    /// <remarks/>
    TT,

    /// <remarks/>
    TV,

    /// <remarks/>
    TW,

    /// <remarks/>
    TZ,

    /// <remarks/>
    UA,

    /// <remarks/>
    UG,

    /// <remarks/>
    UM,

    /// <remarks/>
    US,

    /// <remarks/>
    UY,

    /// <remarks/>
    UZ,

    /// <remarks/>
    VA,

    /// <remarks/>
    VC,

    /// <remarks/>
    VE,

    /// <remarks/>
    VG,

    /// <remarks/>
    VI,

    /// <remarks/>
    VN,

    /// <remarks/>
    VU,

    /// <remarks/>
    WF,

    /// <remarks/>
    WS,

    /// <remarks/>
    YE,

    /// <remarks/>
    YT,

    /// <remarks/>
    ZA,

    /// <remarks/>
    ZM,

    /// <remarks/>
    ZW,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("MasterConsignment", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class MasterConsignmentType
{

    private MeasureType includedTareGrossWeightMeasureField;

    private QuantityType consignmentItemQuantityField;

    private QuantityType packageQuantityField;

    private QuantityType totalPieceQuantityField;

    private IDType productIDField;

    private TransportContractDocumentType transportContractDocumentField;

    private OriginLocationType originLocationField;

    private FinalDestinationLocationType finalDestinationLocationField;

    private CustomsNoteType[] includedCustomsNoteField;

    private HouseConsignmentType[] includedHouseConsignmentField;

    /// <remarks/>
    public MeasureType IncludedTareGrossWeightMeasure
    {
        get
        {
            return this.includedTareGrossWeightMeasureField;
        }
        set
        {
            this.includedTareGrossWeightMeasureField = value;
        }
    }

    /// <remarks/>
    public QuantityType ConsignmentItemQuantity
    {
        get
        {
            return this.consignmentItemQuantityField;
        }
        set
        {
            this.consignmentItemQuantityField = value;
        }
    }

    /// <remarks/>
    public QuantityType PackageQuantity
    {
        get
        {
            return this.packageQuantityField;
        }
        set
        {
            this.packageQuantityField = value;
        }
    }

    /// <remarks/>
    public QuantityType TotalPieceQuantity
    {
        get
        {
            return this.totalPieceQuantityField;
        }
        set
        {
            this.totalPieceQuantityField = value;
        }
    }

    /// <remarks/>
    public IDType ProductID
    {
        get
        {
            return this.productIDField;
        }
        set
        {
            this.productIDField = value;
        }
    }

    /// <remarks/>
    public TransportContractDocumentType TransportContractDocument
    {
        get
        {
            return this.transportContractDocumentField;
        }
        set
        {
            this.transportContractDocumentField = value;
        }
    }

    /// <remarks/>
    public OriginLocationType OriginLocation
    {
        get
        {
            return this.originLocationField;
        }
        set
        {
            this.originLocationField = value;
        }
    }

    /// <remarks/>
    public FinalDestinationLocationType FinalDestinationLocation
    {
        get
        {
            return this.finalDestinationLocationField;
        }
        set
        {
            this.finalDestinationLocationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("IncludedCustomsNote")]
    public CustomsNoteType[] IncludedCustomsNote
    {
        get
        {
            return this.includedCustomsNoteField;
        }
        set
        {
            this.includedCustomsNoteField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("IncludedHouseConsignment")]
    public HouseConsignmentType[] IncludedHouseConsignment
    {
        get
        {
            return this.includedHouseConsignmentField;
        }
        set
        {
            this.includedHouseConsignmentField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("BusinessHeaderDocument", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class BusinessHeaderDocumentType
{

    private IDType idField;

    /// <remarks/>
    public IDType ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("RecipientParty", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class RecipientPartyType
{

    private IDType primaryIDField;

    /// <remarks/>
    public IDType PrimaryID
    {
        get
        {
            return this.primaryIDField;
        }
        set
        {
            this.primaryIDField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("SenderParty", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class SenderPartyType
{

    private IDType primaryIDField;

    /// <remarks/>
    public IDType PrimaryID
    {
        get
        {
            return this.primaryIDField;
        }
        set
        {
            this.primaryIDField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "iata:datamodel:3")]
[System.Xml.Serialization.XmlRootAttribute("ServiceProviderParty", Namespace = "iata:datamodel:3", IsNullable = false)]
public partial class ServiceProviderPartyType
{

    private IDType primaryIDField;

    /// <remarks/>
    public IDType PrimaryID
    {
        get
        {
            return this.primaryIDField;
        }
        set
        {
            this.primaryIDField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:IANA:CharacterSetCode:2007-05-14")]
[System.Xml.Serialization.XmlRootAttribute("CharacterSetCode", Namespace = "urn:un:unece:uncefact:codelist:standard:IANA:CharacterSetCode:2007-05-14", IsNullable = false)]
public enum CharacterSetCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3")]
    Item3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4")]
    Item4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5")]
    Item5,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6")]
    Item6,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("7")]
    Item7,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("8")]
    Item8,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("9")]
    Item9,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("10")]
    Item10,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("11")]
    Item11,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("12")]
    Item12,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("13")]
    Item13,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("14")]
    Item14,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("15")]
    Item15,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("16")]
    Item16,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("17")]
    Item17,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("18")]
    Item18,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("19")]
    Item19,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("20")]
    Item20,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("21")]
    Item21,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("22")]
    Item22,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("23")]
    Item23,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("24")]
    Item24,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("25")]
    Item25,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("26")]
    Item26,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("27")]
    Item27,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("28")]
    Item28,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("29")]
    Item29,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("30")]
    Item30,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("31")]
    Item31,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("32")]
    Item32,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("33")]
    Item33,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("34")]
    Item34,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("35")]
    Item35,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("36")]
    Item36,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("37")]
    Item37,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("38")]
    Item38,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("39")]
    Item39,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("40")]
    Item40,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("41")]
    Item41,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("42")]
    Item42,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("43")]
    Item43,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("44")]
    Item44,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("45")]
    Item45,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("46")]
    Item46,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("47")]
    Item47,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("48")]
    Item48,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("49")]
    Item49,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("50")]
    Item50,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("51")]
    Item51,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("52")]
    Item52,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("53")]
    Item53,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("54")]
    Item54,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("55")]
    Item55,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("56")]
    Item56,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("57")]
    Item57,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("58")]
    Item58,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("59")]
    Item59,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("60")]
    Item60,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("61")]
    Item61,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("62")]
    Item62,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("63")]
    Item63,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("64")]
    Item64,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("65")]
    Item65,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("66")]
    Item66,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("67")]
    Item67,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("68")]
    Item68,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("69")]
    Item69,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("70")]
    Item70,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("71")]
    Item71,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("72")]
    Item72,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("73")]
    Item73,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("74")]
    Item74,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("75")]
    Item75,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("76")]
    Item76,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("77")]
    Item77,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("78")]
    Item78,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("79")]
    Item79,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("80")]
    Item80,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("81")]
    Item81,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("82")]
    Item82,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("83")]
    Item83,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("84")]
    Item84,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("85")]
    Item85,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("86")]
    Item86,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("87")]
    Item87,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("88")]
    Item88,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("89")]
    Item89,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("90")]
    Item90,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("91")]
    Item91,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("92")]
    Item92,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("93")]
    Item93,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("94")]
    Item94,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("95")]
    Item95,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("96")]
    Item96,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("97")]
    Item97,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("98")]
    Item98,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("99")]
    Item99,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("100")]
    Item100,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("101")]
    Item101,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("102")]
    Item102,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("103")]
    Item103,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("104")]
    Item104,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("105")]
    Item105,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("106")]
    Item106,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("109")]
    Item109,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("110")]
    Item110,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("111")]
    Item111,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("112")]
    Item112,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("113")]
    Item113,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("114")]
    Item114,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("115")]
    Item115,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("116")]
    Item116,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("117")]
    Item117,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("118")]
    Item118,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("119")]
    Item119,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1000")]
    Item1000,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1001")]
    Item1001,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1002")]
    Item1002,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1003")]
    Item1003,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1004")]
    Item1004,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1005")]
    Item1005,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1006")]
    Item1006,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1007")]
    Item1007,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1008")]
    Item1008,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1009")]
    Item1009,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1010")]
    Item1010,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1011")]
    Item1011,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1012")]
    Item1012,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1013")]
    Item1013,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1014")]
    Item1014,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1015")]
    Item1015,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1016")]
    Item1016,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1017")]
    Item1017,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1018")]
    Item1018,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1019")]
    Item1019,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1020")]
    Item1020,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2000")]
    Item2000,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2001")]
    Item2001,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2002")]
    Item2002,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2003")]
    Item2003,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2004")]
    Item2004,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2005")]
    Item2005,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2006")]
    Item2006,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2007")]
    Item2007,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2008")]
    Item2008,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2009")]
    Item2009,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2010")]
    Item2010,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2011")]
    Item2011,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2012")]
    Item2012,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2013")]
    Item2013,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2014")]
    Item2014,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2015")]
    Item2015,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2016")]
    Item2016,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2017")]
    Item2017,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2018")]
    Item2018,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2019")]
    Item2019,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2020")]
    Item2020,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2021")]
    Item2021,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2022")]
    Item2022,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2023")]
    Item2023,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2024")]
    Item2024,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2025")]
    Item2025,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2026")]
    Item2026,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2027")]
    Item2027,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2028")]
    Item2028,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2029")]
    Item2029,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2030")]
    Item2030,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2031")]
    Item2031,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2032")]
    Item2032,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2033")]
    Item2033,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2034")]
    Item2034,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2035")]
    Item2035,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2036")]
    Item2036,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2037")]
    Item2037,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2038")]
    Item2038,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2039")]
    Item2039,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2040")]
    Item2040,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2041")]
    Item2041,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2042")]
    Item2042,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2043")]
    Item2043,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2044")]
    Item2044,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2045")]
    Item2045,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2046")]
    Item2046,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2047")]
    Item2047,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2048")]
    Item2048,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2049")]
    Item2049,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2050")]
    Item2050,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2051")]
    Item2051,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2052")]
    Item2052,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2053")]
    Item2053,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2054")]
    Item2054,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2055")]
    Item2055,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2056")]
    Item2056,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2057")]
    Item2057,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2058")]
    Item2058,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2059")]
    Item2059,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2060")]
    Item2060,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2061")]
    Item2061,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2062")]
    Item2062,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2063")]
    Item2063,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2064")]
    Item2064,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2065")]
    Item2065,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2066")]
    Item2066,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2067")]
    Item2067,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2068")]
    Item2068,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2069")]
    Item2069,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2070")]
    Item2070,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2071")]
    Item2071,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2072")]
    Item2072,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2073")]
    Item2073,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2074")]
    Item2074,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2075")]
    Item2075,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2076")]
    Item2076,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2077")]
    Item2077,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2078")]
    Item2078,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2079")]
    Item2079,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2080")]
    Item2080,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2081")]
    Item2081,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2082")]
    Item2082,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2083")]
    Item2083,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2084")]
    Item2084,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2085")]
    Item2085,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2086")]
    Item2086,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2087")]
    Item2087,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2088")]
    Item2088,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2089")]
    Item2089,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2090")]
    Item2090,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2091")]
    Item2091,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2092")]
    Item2092,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2093")]
    Item2093,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2094")]
    Item2094,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2095")]
    Item2095,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2096")]
    Item2096,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2097")]
    Item2097,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2098")]
    Item2098,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2099")]
    Item2099,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2100")]
    Item2100,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2101")]
    Item2101,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2102")]
    Item2102,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2103")]
    Item2103,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2104")]
    Item2104,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2105")]
    Item2105,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2106")]
    Item2106,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2107")]
    Item2107,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2250")]
    Item2250,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2251")]
    Item2251,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2252")]
    Item2252,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2253")]
    Item2253,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2254")]
    Item2254,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2255")]
    Item2255,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2256")]
    Item2256,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2257")]
    Item2257,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2258")]
    Item2258,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2259")]
    Item2259,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:IANA:MIMEMediaType:2009-09-01")]
[System.Xml.Serialization.XmlRootAttribute("MIMEMediaType", Namespace = "urn:un:unece:uncefact:codelist:standard:IANA:MIMEMediaType:2009-09-01", IsNullable = false)]
public enum MIMEMediaTypeContentType
{

    /// <remarks/>
    application,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application")]
    application1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application")]
    application2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application")]
    application3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/CSTAdata+xml")]
    applicationCSTAdataxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/EDI-Consent")]
    applicationEDIConsent,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/EDI-X12")]
    applicationEDIX12,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/EDIFACT")]
    applicationEDIFACT,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/H224")]
    applicationH224,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/activemessage")]
    applicationactivemessage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/andrew-inset")]
    applicationandrewinset,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/applefile")]
    applicationapplefile,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/atom+xml")]
    applicationatomxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/atomcat+xml")]
    applicationatomcatxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/atomicmail")]
    applicationatomicmail,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/atomsvc+xml")]
    applicationatomsvcxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/auth-policy+xml")]
    applicationauthpolicyxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/batch-SMTP")]
    applicationbatchSMTP,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/beep+xml")]
    applicationbeepxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/cals-1840")]
    applicationcals1840,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ccxml+xml")]
    applicationccxmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/cea-2018+xml")]
    applicationcea2018xml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/cellml+xml")]
    applicationcellmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/cnrp+xml")]
    applicationcnrpxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/commonground")]
    applicationcommonground,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/conference-info+xml")]
    applicationconferenceinfoxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/cpl+xml")]
    applicationcplxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/csta+xml")]
    applicationcstaxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/cybercash")]
    applicationcybercash,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/davmount+xml")]
    applicationdavmountxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/dca-rft")]
    applicationdcarft,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/dec-dx")]
    applicationdecdx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/dialog-info+xml")]
    applicationdialoginfoxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/dicom")]
    applicationdicom,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/dns")]
    applicationdns,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/dvcs")]
    applicationdvcs,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ecmascript")]
    applicationecmascript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/emma+xml")]
    applicationemmaxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/epp+xml")]
    applicationeppxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/eshop")]
    applicationeshop,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/example")]
    applicationexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/fastinfoset")]
    applicationfastinfoset,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/fastsoap")]
    applicationfastsoap,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/fits")]
    applicationfits,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/font-tdpfr")]
    applicationfonttdpfr,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/http")]
    applicationhttp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/hyperstudio")]
    applicationhyperstudio,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ibe-key-request+xml")]
    applicationibekeyrequestxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ibe-pkg-reply+xml")]
    applicationibepkgreplyxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ibe-pp-data")]
    applicationibeppdata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/iges")]
    applicationiges,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/im-iscomposing+xml")]
    applicationimiscomposingxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/index")]
    applicationindex,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/index.cmd")]
    applicationindexcmd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/index.obj")]
    applicationindexobj,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/index.response")]
    applicationindexresponse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/index.vnd")]
    applicationindexvnd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/iotp")]
    applicationiotp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ipfix")]
    applicationipfix,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ipp")]
    applicationipp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/isup")]
    applicationisup,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/javascript")]
    applicationjavascript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/json")]
    applicationjson,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/kpml-request+xml")]
    applicationkpmlrequestxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/kpml-response+xml")]
    applicationkpmlresponsexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/lost+xml")]
    applicationlostxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mac-binhex40")]
    applicationmacbinhex40,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/macwriteii")]
    applicationmacwriteii,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/marc")]
    applicationmarc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mathematica")]
    applicationmathematica,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-associated-procedure-description+xml")]
    applicationmbmsassociatedproceduredescriptionxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-deregister+xml")]
    applicationmbmsderegisterxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-envelope+xml")]
    applicationmbmsenvelopexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-msk+xml")]
    applicationmbmsmskxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-msk-response+xml")]
    applicationmbmsmskresponsexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-protection-description+xml")]
    applicationmbmsprotectiondescriptionxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-reception-report+xml")]
    applicationmbmsreceptionreportxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-register+xml")]
    applicationmbmsregisterxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-register-response+xml")]
    applicationmbmsregisterresponsexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbms-user-service-description+xml")]
    applicationmbmsuserservicedescriptionxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mbox")]
    applicationmbox,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/media_control+xml")]
    applicationmedia_controlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mediaservercontrol+xml")]
    applicationmediaservercontrolxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mikey")]
    applicationmikey,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/moss-keys")]
    applicationmosskeys,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/moss-signature")]
    applicationmosssignature,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mosskey-data")]
    applicationmosskeydata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mosskey-request")]
    applicationmosskeyrequest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mp21")]
    applicationmp21,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mp4")]
    applicationmp4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mpeg4-generic")]
    applicationmpeg4generic,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mpeg4-iod")]
    applicationmpeg4iod,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mpeg4-iod-xmt")]
    applicationmpeg4iodxmt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/msword")]
    applicationmsword,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/mxf")]
    applicationmxf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/nasdata")]
    applicationnasdata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/news-checkgroups")]
    applicationnewscheckgroups,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/news-groupinfo")]
    applicationnewsgroupinfo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/news-transmission")]
    applicationnewstransmission,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/nss")]
    applicationnss,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ocsp-request")]
    applicationocsprequest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ocsp-response")]
    applicationocspresponse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/octet-stream")]
    applicationoctetstream,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/oda")]
    applicationoda,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/oebps-package+xml")]
    applicationoebpspackagexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ogg")]
    applicationogg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/parityfec")]
    applicationparityfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/patch-ops-error+xml")]
    applicationpatchopserrorxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pdf")]
    applicationpdf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pgp-encrypted")]
    applicationpgpencrypted,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pgp-keys")]
    applicationpgpkeys,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pgp-signature")]
    applicationpgpsignature,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pidf+xml")]
    applicationpidfxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pidf-diff+xml")]
    applicationpidfdiffxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pkcs10")]
    applicationpkcs10,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pkcs7-mime")]
    applicationpkcs7mime,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pkcs7-signature")]
    applicationpkcs7signature,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pkix-cert")]
    applicationpkixcert,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pkix-crl")]
    applicationpkixcrl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pkix-pkipath")]
    applicationpkixpkipath,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pkixcmp")]
    applicationpkixcmp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/pls+xml")]
    applicationplsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/poc-settings+xml")]
    applicationpocsettingsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/postscript")]
    applicationpostscript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/prs.alvestrand.titrax-sheet")]
    applicationprsalvestrandtitraxsheet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/prs.cww")]
    applicationprscww,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/prs.nprend")]
    applicationprsnprend,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/prs.plucker")]
    applicationprsplucker,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/qsig")]
    applicationqsig,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/rdf+xml")]
    applicationrdfxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/reginfo+xml")]
    applicationreginfoxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/relax-ng-compact-syntax")]
    applicationrelaxngcompactsyntax,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/remote-printing")]
    applicationremoteprinting,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/resource-lists+xml")]
    applicationresourcelistsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/resource-lists-diff+xml")]
    applicationresourcelistsdiffxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/riscos")]
    applicationriscos,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/rlmi+xml")]
    applicationrlmixml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/rls-services+xml")]
    applicationrlsservicesxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/rtf")]
    applicationrtf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/rtx")]
    applicationrtx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/samlassertion+xml")]
    applicationsamlassertionxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/samlmetadata+xml")]
    applicationsamlmetadataxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/sbml+xml")]
    applicationsbmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/scvp-cv-request")]
    applicationscvpcvrequest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/scvp-cv-response")]
    applicationscvpcvresponse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/scvp-vp-request")]
    applicationscvpvprequest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/scvp-vp-response")]
    applicationscvpvpresponse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/sdp")]
    applicationsdp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/set-payment")]
    applicationsetpayment,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/set-payment-initiation")]
    applicationsetpaymentinitiation,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/set-registration")]
    applicationsetregistration,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/set-registration-initiation")]
    applicationsetregistrationinitiation,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/sgml")]
    applicationsgml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/sgml-open-catalog")]
    applicationsgmlopencatalog,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/shf+xml")]
    applicationshfxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/sieve")]
    applicationsieve,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/simple-filter+xml")]
    applicationsimplefilterxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/simple-message-summary")]
    applicationsimplemessagesummary,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/simpleSymbolContainer")]
    applicationsimpleSymbolContainer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/slate")]
    applicationslate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/smil (OBSOLETE)")]
    applicationsmilOBSOLETE,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/smil+xml")]
    applicationsmilxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/soap+fastinfoset")]
    applicationsoapfastinfoset,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/soap+xml")]
    applicationsoapxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/sparql-query")]
    applicationsparqlquery,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/sparql-results+xml")]
    applicationsparqlresultsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/spirits-event+xml")]
    applicationspiritseventxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/srgs")]
    applicationsrgs,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/srgs+xml")]
    applicationsrgsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ssml+xml")]
    applicationssmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/timestamp-query")]
    applicationtimestampquery,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/timestamp-reply")]
    applicationtimestampreply,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/tve-trigger")]
    applicationtvetrigger,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/ulpfec")]
    applicationulpfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vemmi")]
    applicationvemmi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3M.Post-it-Notes")]
    applicationvnd3MPostitNotes,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp.bsf+xml")]
    applicationvnd3gppbsfxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp.pic-bw-large")]
    applicationvnd3gpppicbwlarge,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp.pic-bw-small")]
    applicationvnd3gpppicbwsmall,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp.pic-bw-var")]
    applicationvnd3gpppicbwvar,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp.sms")]
    applicationvnd3gppsms,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp2.bcmcsinfo+xml")]
    applicationvnd3gpp2bcmcsinfoxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp2.sms")]
    applicationvnd3gpp2sms,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.3gpp2.tcap")]
    applicationvnd3gpp2tcap,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.FloGraphIt")]
    applicationvndFloGraphIt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.HandHeld-Entertainment+xml")]
    applicationvndHandHeldEntertainmentxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Kinar")]
    applicationvndKinar,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.MFER")]
    applicationvndMFER,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Mobius.DAF")]
    applicationvndMobiusDAF,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Mobius.DIS")]
    applicationvndMobiusDIS,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Mobius.MBK")]
    applicationvndMobiusMBK,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Mobius.MQY")]
    applicationvndMobiusMQY,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Mobius.MSL")]
    applicationvndMobiusMSL,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Mobius.PLC")]
    applicationvndMobiusPLC,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Mobius.TXF")]
    applicationvndMobiusTXF,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.Quark.QuarkXPress")]
    applicationvndQuarkQuarkXPress,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.RenLearn.rlprint")]
    applicationvndRenLearnrlprint,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.SimTech-MindMapper")]
    applicationvndSimTechMindMapper,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.accpac.simply.aso")]
    applicationvndaccpacsimplyaso,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.accpac.simply.imp")]
    applicationvndaccpacsimplyimp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.acucobol")]
    applicationvndacucobol,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.acucorp")]
    applicationvndacucorp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.adobe.partial-upload")]
    applicationvndadobepartialupload,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.adobe.xdp+xml")]
    applicationvndadobexdpxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.adobe.xfdf")]
    applicationvndadobexfdf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.aether.imp")]
    applicationvndaetherimp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.airzip.filesecure.azf")]
    applicationvndairzipfilesecureazf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.airzip.filesecure.azs")]
    applicationvndairzipfilesecureazs,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.americandynamics.acc")]
    applicationvndamericandynamicsacc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.amiga.ami")]
    applicationvndamigaami,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.anser-web-certificate-issue-initiation")]
    applicationvndanserwebcertificateissueinitiation,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.antix.game-component")]
    applicationvndantixgamecomponent,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.apple.installer+xml")]
    applicationvndappleinstallerxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.apple.mpegurl")]
    applicationvndapplempegurl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.arastra.swi (OBSOLETE)")]
    applicationvndarastraswiOBSOLETE,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.aristanetworks.swi")]
    applicationvndaristanetworksswi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.audiograph")]
    applicationvndaudiograph,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.autopackage")]
    applicationvndautopackage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.avistar+xml")]
    applicationvndavistarxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.blueice.multipass")]
    applicationvndblueicemultipass,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.bluetooth.ep.oob")]
    applicationvndbluetoothepoob,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.bmi")]
    applicationvndbmi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.businessobjects")]
    applicationvndbusinessobjects,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cab-jscript")]
    applicationvndcabjscript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.canon-cpdl")]
    applicationvndcanoncpdl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.canon-lips")]
    applicationvndcanonlips,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cendio.thinlinc.clientconf")]
    applicationvndcendiothinlincclientconf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.chemdraw+xml")]
    applicationvndchemdrawxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.chipnuts.karaoke-mmd")]
    applicationvndchipnutskaraokemmd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cinderella")]
    applicationvndcinderella,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cirpack.isdn-ext")]
    applicationvndcirpackisdnext,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.claymore")]
    applicationvndclaymore,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cloanto.rp9")]
    applicationvndcloantorp9,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.clonk.c4group")]
    applicationvndclonkc4group,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.commerce-battelle")]
    applicationvndcommercebattelle,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.commonspace")]
    applicationvndcommonspace,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.contact.cmsg")]
    applicationvndcontactcmsg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cosmocaller")]
    applicationvndcosmocaller,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.crick.clicker")]
    applicationvndcrickclicker,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.crick.clicker.keyboard")]
    applicationvndcrickclickerkeyboard,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.crick.clicker.palette")]
    applicationvndcrickclickerpalette,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.crick.clicker.template")]
    applicationvndcrickclickertemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.crick.clicker.wordbank")]
    applicationvndcrickclickerwordbank,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.criticaltools.wbs+xml")]
    applicationvndcriticaltoolswbsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ctc-posml")]
    applicationvndctcposml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ctct.ws+xml")]
    applicationvndctctwsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cups-pdf")]
    applicationvndcupspdf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cups-postscript")]
    applicationvndcupspostscript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cups-ppd")]
    applicationvndcupsppd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cups-raster")]
    applicationvndcupsraster,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cups-raw")]
    applicationvndcupsraw,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.curl")]
    applicationvndcurl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.cybank")]
    applicationvndcybank,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.data-vision.rdz")]
    applicationvnddatavisionrdz,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.denovo.fcselayout-link")]
    applicationvnddenovofcselayoutlink,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dir-bi.plate-dl-nosuffix")]
    applicationvnddirbiplatedlnosuffix,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dna")]
    applicationvnddna,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dolby.mobile.1")]
    applicationvnddolbymobile1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dolby.mobile.2")]
    applicationvnddolbymobile2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dpgraph")]
    applicationvnddpgraph,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dreamfactory")]
    applicationvnddreamfactory,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.esgcontainer")]
    applicationvnddvbesgcontainer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.ipdcdftnotifaccess")]
    applicationvnddvbipdcdftnotifaccess,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.ipdcesgaccess")]
    applicationvnddvbipdcesgaccess,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.ipdcroaming")]
    applicationvnddvbipdcroaming,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.iptv.alfec-base")]
    applicationvnddvbiptvalfecbase,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.iptv.alfec-enhancement")]
    applicationvnddvbiptvalfecenhancement,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.notif-aggregate-root+xml")]
    applicationvnddvbnotifaggregaterootxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.notif-container+xml")]
    applicationvnddvbnotifcontainerxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.notif-generic+xml")]
    applicationvnddvbnotifgenericxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.notif-ia-msglist+xml")]
    applicationvnddvbnotifiamsglistxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.notif-ia-registration-request+xml")]
    applicationvnddvbnotifiaregistrationrequestxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.notif-ia-registration-response+xml")]
    applicationvnddvbnotifiaregistrationresponsexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dvb.notif-init+xml")]
    applicationvnddvbnotifinitxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dxr")]
    applicationvnddxr,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.dynageo")]
    applicationvnddynageo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ecdis-update")]
    applicationvndecdisupdate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ecowin.chart")]
    applicationvndecowinchart,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ecowin.filerequest")]
    applicationvndecowinfilerequest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ecowin.fileupdate")]
    applicationvndecowinfileupdate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ecowin.series")]
    applicationvndecowinseries,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ecowin.seriesrequest")]
    applicationvndecowinseriesrequest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ecowin.seriesupdate")]
    applicationvndecowinseriesupdate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.emclient.accessrequest+xml")]
    applicationvndemclientaccessrequestxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.enliven")]
    applicationvndenliven,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.epson.esf")]
    applicationvndepsonesf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.epson.msf")]
    applicationvndepsonmsf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.epson.quickanime")]
    applicationvndepsonquickanime,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.epson.salt")]
    applicationvndepsonsalt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.epson.ssf")]
    applicationvndepsonssf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ericsson.quickcall")]
    applicationvndericssonquickcall,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.eszigno3+xml")]
    applicationvndeszigno3xml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.aoc+xml")]
    applicationvndetsiaocxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.cug+xml")]
    applicationvndetsicugxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.iptvcommand+xml")]
    applicationvndetsiiptvcommandxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.iptvdiscovery+xml")]
    applicationvndetsiiptvdiscoveryxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.iptvprofile+xml")]
    applicationvndetsiiptvprofilexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.iptvsad-bc+xml")]
    applicationvndetsiiptvsadbcxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.iptvsad-cod+xml")]
    applicationvndetsiiptvsadcodxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.iptvsad-npvr+xml")]
    applicationvndetsiiptvsadnpvrxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.iptvueprofile+xml")]
    applicationvndetsiiptvueprofilexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.mcid+xml")]
    applicationvndetsimcidxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.sci+xml")]
    applicationvndetsiscixml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.simservs+xml")]
    applicationvndetsisimservsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.etsi.tsl.der")]
    applicationvndetsitslder,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.eudora.data")]
    applicationvndeudoradata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ezpix-album")]
    applicationvndezpixalbum,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ezpix-package")]
    applicationvndezpixpackage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.f-secure.mobile")]
    applicationvndfsecuremobile,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fdf")]
    applicationvndfdf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fdsn.mseed")]
    applicationvndfdsnmseed,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fdsn.seed")]
    applicationvndfdsnseed,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ffsns")]
    applicationvndffsns,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fints")]
    applicationvndfints,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fluxtime.clip")]
    applicationvndfluxtimeclip,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.font-fontforge-sfd")]
    applicationvndfontfontforgesfd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.framemaker")]
    applicationvndframemaker,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.frogans.fnc")]
    applicationvndfrogansfnc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.frogans.ltf")]
    applicationvndfrogansltf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fsc.weblaunch")]
    applicationvndfscweblaunch,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujitsu.oasys")]
    applicationvndfujitsuoasys,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujitsu.oasys2")]
    applicationvndfujitsuoasys2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujitsu.oasys3")]
    applicationvndfujitsuoasys3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujitsu.oasysgp")]
    applicationvndfujitsuoasysgp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujitsu.oasysprs")]
    applicationvndfujitsuoasysprs,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujixerox.ART-EX")]
    applicationvndfujixeroxARTEX,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujixerox.ART4")]
    applicationvndfujixeroxART4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujixerox.HBPL")]
    applicationvndfujixeroxHBPL,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujixerox.ddd")]
    applicationvndfujixeroxddd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujixerox.docuworks")]
    applicationvndfujixeroxdocuworks,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fujixerox.docuworks.binder")]
    applicationvndfujixeroxdocuworksbinder,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fut-misnet")]
    applicationvndfutmisnet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.fuzzysheet")]
    applicationvndfuzzysheet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.genomatix.tuxedo")]
    applicationvndgenomatixtuxedo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.geogebra.file")]
    applicationvndgeogebrafile,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.geogebra.tool")]
    applicationvndgeogebratool,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.geometry-explorer")]
    applicationvndgeometryexplorer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.geonext")]
    applicationvndgeonext,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.geoplan")]
    applicationvndgeoplan,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.geospace")]
    applicationvndgeospace,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.globalplatform.card-content-mgt")]
    applicationvndglobalplatformcardcontentmgt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.globalplatform.card-content-mgt-response")]
    applicationvndglobalplatformcardcontentmgtresponse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.gmx (OBSOLETE)")]
    applicationvndgmxOBSOLETE,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.google-earth.kml+xml")]
    applicationvndgoogleearthkmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.google-earth.kmz")]
    applicationvndgoogleearthkmz,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.grafeq")]
    applicationvndgrafeq,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.gridmp")]
    applicationvndgridmp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.groove-account")]
    applicationvndgrooveaccount,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.groove-help")]
    applicationvndgroovehelp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.groove-identity-message")]
    applicationvndgrooveidentitymessage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.groove-injector")]
    applicationvndgrooveinjector,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.groove-tool-message")]
    applicationvndgroovetoolmessage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.groove-tool-template")]
    applicationvndgroovetooltemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.groove-vcard")]
    applicationvndgroovevcard,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hbci")]
    applicationvndhbci,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hcl-bireports")]
    applicationvndhclbireports,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hhe.lesson-player")]
    applicationvndhhelessonplayer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hp-HPGL")]
    applicationvndhpHPGL,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hp-PCL")]
    applicationvndhpPCL,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hp-PCLXL")]
    applicationvndhpPCLXL,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hp-hpid")]
    applicationvndhphpid,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hp-hps")]
    applicationvndhphps,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hp-jlyt")]
    applicationvndhpjlyt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.httphone")]
    applicationvndhttphone,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hydrostatix.sof-data")]
    applicationvndhydrostatixsofdata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.hzn-3d-crossword")]
    applicationvndhzn3dcrossword,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ibm.MiniPay")]
    applicationvndibmMiniPay,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ibm.afplinedata")]
    applicationvndibmafplinedata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ibm.electronic-media")]
    applicationvndibmelectronicmedia,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ibm.modcap")]
    applicationvndibmmodcap,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ibm.rights-management")]
    applicationvndibmrightsmanagement,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ibm.secure-container")]
    applicationvndibmsecurecontainer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.iccprofile")]
    applicationvndiccprofile,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.igloader")]
    applicationvndigloader,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.immervision-ivp")]
    applicationvndimmervisionivp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.immervision-ivu")]
    applicationvndimmervisionivu,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.informedcontrol.rms+xml")]
    applicationvndinformedcontrolrmsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.informix-visionary")]
    applicationvndinformixvisionary,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.intercon.formnet")]
    applicationvndinterconformnet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.intertrust.digibox")]
    applicationvndintertrustdigibox,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.intertrust.nncp")]
    applicationvndintertrustnncp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.intu.qbo")]
    applicationvndintuqbo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.intu.qfx")]
    applicationvndintuqfx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.iptc.g2.conceptitem+xml")]
    applicationvndiptcg2conceptitemxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.iptc.g2.knowledgeitem+xml")]
    applicationvndiptcg2knowledgeitemxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.iptc.g2.newsitem+xml")]
    applicationvndiptcg2newsitemxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.iptc.g2.packageitem+xml")]
    applicationvndiptcg2packageitemxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ipunplugged.rcprofile")]
    applicationvndipunpluggedrcprofile,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.irepository.package+xml")]
    applicationvndirepositorypackagexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.is-xpr")]
    applicationvndisxpr,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.jam")]
    applicationvndjam,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-directory-service")]
    applicationvndjapannetdirectoryservice,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-jpnstore-wakeup")]
    applicationvndjapannetjpnstorewakeup,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-payment-wakeup")]
    applicationvndjapannetpaymentwakeup,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-registration")]
    applicationvndjapannetregistration,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-registration-wakeup")]
    applicationvndjapannetregistrationwakeup,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-setstore-wakeup")]
    applicationvndjapannetsetstorewakeup,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-verification")]
    applicationvndjapannetverification,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.japannet-verification-wakeup")]
    applicationvndjapannetverificationwakeup,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.jcp.javame.midlet-rms")]
    applicationvndjcpjavamemidletrms,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.jisp")]
    applicationvndjisp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.joost.joda-archive")]
    applicationvndjoostjodaarchive,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kahootz")]
    applicationvndkahootz,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.karbon")]
    applicationvndkdekarbon,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.kchart")]
    applicationvndkdekchart,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.kformula")]
    applicationvndkdekformula,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.kivio")]
    applicationvndkdekivio,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.kontour")]
    applicationvndkdekontour,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.kpresenter")]
    applicationvndkdekpresenter,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.kspread")]
    applicationvndkdekspread,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kde.kword")]
    applicationvndkdekword,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kenameaapp")]
    applicationvndkenameaapp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kidspiration")]
    applicationvndkidspiration,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.koan")]
    applicationvndkoan,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.kodak-descriptor")]
    applicationvndkodakdescriptor,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.liberty-request+xml")]
    applicationvndlibertyrequestxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.llamagraphics.life-balance.desktop")]
    applicationvndllamagraphicslifebalancedesktop,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.llamagraphics.life-balance.exchange+xml")]
    applicationvndllamagraphicslifebalanceexchangexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.lotus-1-2-3")]
    applicationvndlotus123,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.lotus-approach")]
    applicationvndlotusapproach,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.lotus-freelance")]
    applicationvndlotusfreelance,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.lotus-notes")]
    applicationvndlotusnotes,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.lotus-organizer")]
    applicationvndlotusorganizer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.lotus-screencam")]
    applicationvndlotusscreencam,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.lotus-wordpro")]
    applicationvndlotuswordpro,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.macports.portpkg")]
    applicationvndmacportsportpkg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.marlin.drm.actiontoken+xml")]
    applicationvndmarlindrmactiontokenxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.marlin.drm.conftoken+xml")]
    applicationvndmarlindrmconftokenxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.marlin.drm.license+xml")]
    applicationvndmarlindrmlicensexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.marlin.drm.mdcf")]
    applicationvndmarlindrmmdcf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mcd")]
    applicationvndmcd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.medcalcdata")]
    applicationvndmedcalcdata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mediastation.cdkey")]
    applicationvndmediastationcdkey,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.meridian-slingshot")]
    applicationvndmeridianslingshot,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mfmp")]
    applicationvndmfmp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.micrografx.flo")]
    applicationvndmicrografxflo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.micrografx.igx")]
    applicationvndmicrografxigx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mif")]
    applicationvndmif,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.minisoft-hp3000-save")]
    applicationvndminisofthp3000save,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mitsubishi.misty-guard.trustweb")]
    applicationvndmitsubishimistyguardtrustweb,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mophun.application")]
    applicationvndmophunapplication,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mophun.certificate")]
    applicationvndmophuncertificate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.flexsuite")]
    applicationvndmotorolaflexsuite,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.flexsuite.adsi")]
    applicationvndmotorolaflexsuiteadsi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.flexsuite.fis")]
    applicationvndmotorolaflexsuitefis,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.flexsuite.gotap")]
    applicationvndmotorolaflexsuitegotap,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.flexsuite.kmr")]
    applicationvndmotorolaflexsuitekmr,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.flexsuite.ttc")]
    applicationvndmotorolaflexsuitettc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.flexsuite.wem")]
    applicationvndmotorolaflexsuitewem,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.motorola.iprm")]
    applicationvndmotorolaiprm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mozilla.xul+xml")]
    applicationvndmozillaxulxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-artgalry")]
    applicationvndmsartgalry,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-asf")]
    applicationvndmsasf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-cab-compressed")]
    applicationvndmscabcompressed,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-excel")]
    applicationvndmsexcel,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-fontobject")]
    applicationvndmsfontobject,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-htmlhelp")]
    applicationvndmshtmlhelp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-ims")]
    applicationvndmsims,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-lrm")]
    applicationvndmslrm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-playready.initiator+xml")]
    applicationvndmsplayreadyinitiatorxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-powerpoint")]
    applicationvndmspowerpoint,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-project")]
    applicationvndmsproject,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-tnef")]
    applicationvndmstnef,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-wmdrm.lic-chlg-req")]
    applicationvndmswmdrmlicchlgreq,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-wmdrm.lic-resp")]
    applicationvndmswmdrmlicresp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-wmdrm.meter-chlg-req")]
    applicationvndmswmdrmmeterchlgreq,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-wmdrm.meter-resp")]
    applicationvndmswmdrmmeterresp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-works")]
    applicationvndmsworks,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-wpl")]
    applicationvndmswpl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ms-xpsdocument")]
    applicationvndmsxpsdocument,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.mseq")]
    applicationvndmseq,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.msign")]
    applicationvndmsign,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.multiad.creator")]
    applicationvndmultiadcreator,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.multiad.creator.cif")]
    applicationvndmultiadcreatorcif,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.music-niff")]
    applicationvndmusicniff,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.musician")]
    applicationvndmusician,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.muvee.style")]
    applicationvndmuveestyle,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ncd.control")]
    applicationvndncdcontrol,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ncd.reference")]
    applicationvndncdreference,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nervana")]
    applicationvndnervana,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.netfpx")]
    applicationvndnetfpx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.neurolanguage.nlu")]
    applicationvndneurolanguagenlu,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.noblenet-directory")]
    applicationvndnoblenetdirectory,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.noblenet-sealer")]
    applicationvndnoblenetsealer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.noblenet-web")]
    applicationvndnoblenetweb,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.catalogs")]
    applicationvndnokiacatalogs,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.conml+wbxml")]
    applicationvndnokiaconmlwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.conml+xml")]
    applicationvndnokiaconmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.iSDS-radio-presets")]
    applicationvndnokiaiSDSradiopresets,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.iptv.config+xml")]
    applicationvndnokiaiptvconfigxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.landmark+wbxml")]
    applicationvndnokialandmarkwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.landmark+xml")]
    applicationvndnokialandmarkxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.landmarkcollection+xml")]
    applicationvndnokialandmarkcollectionxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.n-gage.ac+xml")]
    applicationvndnokiangageacxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.n-gage.data")]
    applicationvndnokiangagedata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.n-gage.symbian.install")]
    applicationvndnokiangagesymbianinstall,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.ncd")]
    applicationvndnokiancd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.pcd+wbxml")]
    applicationvndnokiapcdwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.pcd+xml")]
    applicationvndnokiapcdxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.radio-preset")]
    applicationvndnokiaradiopreset,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.nokia.radio-presets")]
    applicationvndnokiaradiopresets,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.novadigm.EDM")]
    applicationvndnovadigmEDM,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.novadigm.EDX")]
    applicationvndnovadigmEDX,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.novadigm.EXT")]
    applicationvndnovadigmEXT,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.chart")]
    applicationvndoasisopendocumentchart,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.chart-template")]
    applicationvndoasisopendocumentcharttemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.database")]
    applicationvndoasisopendocumentdatabase,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.formula")]
    applicationvndoasisopendocumentformula,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.formula-template")]
    applicationvndoasisopendocumentformulatemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.graphics")]
    applicationvndoasisopendocumentgraphics,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.graphics-template")]
    applicationvndoasisopendocumentgraphicstemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.image")]
    applicationvndoasisopendocumentimage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.image-template")]
    applicationvndoasisopendocumentimagetemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.presentation")]
    applicationvndoasisopendocumentpresentation,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.presentation-template")]
    applicationvndoasisopendocumentpresentationtemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.spreadsheet")]
    applicationvndoasisopendocumentspreadsheet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.spreadsheet-template")]
    applicationvndoasisopendocumentspreadsheettemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.text")]
    applicationvndoasisopendocumenttext,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.text-master")]
    applicationvndoasisopendocumenttextmaster,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.text-template")]
    applicationvndoasisopendocumenttexttemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oasis.opendocument.text-web")]
    applicationvndoasisopendocumenttextweb,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.obn")]
    applicationvndobn,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.olpc-sugar")]
    applicationvndolpcsugar,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma-scws-config")]
    applicationvndomascwsconfig,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma-scws-http-request")]
    applicationvndomascwshttprequest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma-scws-http-response")]
    applicationvndomascwshttpresponse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.associated-procedure-parameter+xml")]
    applicationvndomabcastassociatedprocedureparameterxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.drm-trigger+xml")]
    applicationvndomabcastdrmtriggerxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.imd+xml")]
    applicationvndomabcastimdxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.ltkm")]
    applicationvndomabcastltkm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.notification+xml")]
    applicationvndomabcastnotificationxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.provisioningtrigger")]
    applicationvndomabcastprovisioningtrigger,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.sgboot")]
    applicationvndomabcastsgboot,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.sgdd+xml")]
    applicationvndomabcastsgddxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.sgdu")]
    applicationvndomabcastsgdu,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.simple-symbol-container")]
    applicationvndomabcastsimplesymbolcontainer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.smartcard-trigger+xml")]
    applicationvndomabcastsmartcardtriggerxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.sprov+xml")]
    applicationvndomabcastsprovxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.bcast.stkm")]
    applicationvndomabcaststkm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.dcd")]
    applicationvndomadcd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.dcdc")]
    applicationvndomadcdc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.dd2+xml")]
    applicationvndomadd2xml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.drm.risd+xml")]
    applicationvndomadrmrisdxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.group-usage-list+xml")]
    applicationvndomagroupusagelistxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.poc.detailed-progress-report+xml")]
    applicationvndomapocdetailedprogressreportxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.poc.final-report+xml")]
    applicationvndomapocfinalreportxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.poc.groups+xml")]
    applicationvndomapocgroupsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.poc.invocation-descriptor+xml")]
    applicationvndomapocinvocationdescriptorxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.poc.optimized-progress-report+xml")]
    applicationvndomapocoptimizedprogressreportxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.push")]
    applicationvndomapush,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.oma.xcap-directory+xml")]
    applicationvndomaxcapdirectoryxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.omads-email+xml")]
    applicationvndomadsemailxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.omads-file+xml")]
    applicationvndomadsfilexml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.omads-folder+xml")]
    applicationvndomadsfolderxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.omaloc-supl-init")]
    applicationvndomalocsuplinit,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.openofficeorg.extension")]
    applicationvndopenofficeorgextension,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.osa.netdeploy")]
    applicationvndosanetdeploy,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.osgi.bundle")]
    applicationvndosgibundle,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.osgi.dp")]
    applicationvndosgidp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.otps.ct-kip+xml")]
    applicationvndotpsctkipxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.palm")]
    applicationvndpalm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.paos.xml")]
    applicationvndpaosxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.pg.format")]
    applicationvndpgformat,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.pg.osasli")]
    applicationvndpgosasli,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.piaccess.application-licence")]
    applicationvndpiaccessapplicationlicence,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.picsel")]
    applicationvndpicsel,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.poc.group-advertisement+xml")]
    applicationvndpocgroupadvertisementxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.pocketlearn")]
    applicationvndpocketlearn,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.powerbuilder6")]
    applicationvndpowerbuilder6,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.powerbuilder6-s")]
    applicationvndpowerbuilder6s,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.powerbuilder7")]
    applicationvndpowerbuilder7,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.powerbuilder7-s")]
    applicationvndpowerbuilder7s,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.powerbuilder75")]
    applicationvndpowerbuilder75,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.powerbuilder75-s")]
    applicationvndpowerbuilder75s,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.preminet")]
    applicationvndpreminet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.previewsystems.box")]
    applicationvndpreviewsystemsbox,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.proteus.magazine")]
    applicationvndproteusmagazine,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.publishare-delta-tree")]
    applicationvndpublisharedeltatree,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.pvi.ptid1")]
    applicationvndpviptid1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.pwg-multiplexed")]
    applicationvndpwgmultiplexed,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.pwg-xhtml-print+xml")]
    applicationvndpwgxhtmlprintxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.qualcomm.brew-app-res")]
    applicationvndqualcommbrewappres,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.rapid")]
    applicationvndrapid,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.realvnc.bed")]
    applicationvndrealvncbed,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.recordare.musicxml")]
    applicationvndrecordaremusicxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.recordare.musicxml+xml")]
    applicationvndrecordaremusicxmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.route66.link66+xml")]
    applicationvndroute66link66xml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ruckus.download")]
    applicationvndruckusdownload,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.s3sms")]
    applicationvnds3sms,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sbm.cid")]
    applicationvndsbmcid,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sbm.mid2")]
    applicationvndsbmmid2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.scribus")]
    applicationvndscribus,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.3df")]
    applicationvndsealed3df,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.csf")]
    applicationvndsealedcsf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.doc")]
    applicationvndsealeddoc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.eml")]
    applicationvndsealedeml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.mht")]
    applicationvndsealedmht,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.net")]
    applicationvndsealednet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.ppt")]
    applicationvndsealedppt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.tiff")]
    applicationvndsealedtiff,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealed.xls")]
    applicationvndsealedxls,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealedmedia.softseal.html")]
    applicationvndsealedmediasoftsealhtml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sealedmedia.softseal.pdf")]
    applicationvndsealedmediasoftsealpdf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.seemail")]
    applicationvndseemail,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sema")]
    applicationvndsema,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.semd")]
    applicationvndsemd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.semf")]
    applicationvndsemf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.shana.informed.formdata")]
    applicationvndshanainformedformdata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.shana.informed.formtemplate")]
    applicationvndshanainformedformtemplate,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.shana.informed.interchange")]
    applicationvndshanainformedinterchange,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.shana.informed.package")]
    applicationvndshanainformedpackage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.smaf")]
    applicationvndsmaf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.smart.teacher")]
    applicationvndsmartteacher,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.software602.filler.form+xml")]
    applicationvndsoftware602fillerformxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.software602.filler.form-xml-zip")]
    applicationvndsoftware602fillerformxmlzip,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.solent.sdkm+xml")]
    applicationvndsolentsdkmxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.spotfire.dxp")]
    applicationvndspotfiredxp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.spotfire.sfs")]
    applicationvndspotfiresfs,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sss-cod")]
    applicationvndssscod,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sss-dtf")]
    applicationvndsssdtf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sss-ntf")]
    applicationvndsssntf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.street-stream")]
    applicationvndstreetstream,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sun.wadl+xml")]
    applicationvndsunwadlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.sus-calendar")]
    applicationvndsuscalendar,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.svd")]
    applicationvndsvd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.swiftview-ics")]
    applicationvndswiftviewics,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.syncml+xml")]
    applicationvndsyncmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.syncml.dm+wbxml")]
    applicationvndsyncmldmwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.syncml.dm+xml")]
    applicationvndsyncmldmxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.syncml.dm.notification")]
    applicationvndsyncmldmnotification,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.syncml.ds.notification")]
    applicationvndsyncmldsnotification,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.tao.intent-module-archive")]
    applicationvndtaointentmodulearchive,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.tmobile-livetv")]
    applicationvndtmobilelivetv,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.trid.tpt")]
    applicationvndtridtpt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.triscape.mxs")]
    applicationvndtriscapemxs,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.trueapp")]
    applicationvndtrueapp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.truedoc")]
    applicationvndtruedoc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.ufdl")]
    applicationvndufdl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uiq.theme")]
    applicationvnduiqtheme,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.umajin")]
    applicationvndumajin,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.unity")]
    applicationvndunity,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uoml+xml")]
    applicationvnduomlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.alert")]
    applicationvnduplanetalert,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.alert-wbxml")]
    applicationvnduplanetalertwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.bearer-choice")]
    applicationvnduplanetbearerchoice,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.bearer-choice-wbxml")]
    applicationvnduplanetbearerchoicewbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.cacheop")]
    applicationvnduplanetcacheop,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.cacheop-wbxml")]
    applicationvnduplanetcacheopwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.channel")]
    applicationvnduplanetchannel,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.channel-wbxml")]
    applicationvnduplanetchannelwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.list")]
    applicationvnduplanetlist,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.list-wbxml")]
    applicationvnduplanetlistwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.listcmd")]
    applicationvnduplanetlistcmd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.listcmd-wbxml")]
    applicationvnduplanetlistcmdwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.uplanet.signal")]
    applicationvnduplanetsignal,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.vcx")]
    applicationvndvcx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.vd-study")]
    applicationvndvdstudy,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.vectorworks")]
    applicationvndvectorworks,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.vidsoft.vidconference")]
    applicationvndvidsoftvidconference,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.visio")]
    applicationvndvisio,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.visionary")]
    applicationvndvisionary,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.vividence.scriptfile")]
    applicationvndvividencescriptfile,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.vsf")]
    applicationvndvsf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wap.sic")]
    applicationvndwapsic,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wap.slc")]
    applicationvndwapslc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wap.wbxml")]
    applicationvndwapwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wap.wmlc")]
    applicationvndwapwmlc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wap.wmlscriptc")]
    applicationvndwapwmlscriptc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.webturbo")]
    applicationvndwebturbo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wfa.wsc")]
    applicationvndwfawsc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wmc")]
    applicationvndwmc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wmf.bootstrap")]
    applicationvndwmfbootstrap,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wordperfect")]
    applicationvndwordperfect,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wqd")]
    applicationvndwqd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wrq-hp3000-labelled")]
    applicationvndwrqhp3000labelled,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wt.stf")]
    applicationvndwtstf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wv.csp+wbxml")]
    applicationvndwvcspwbxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wv.csp+xml")]
    applicationvndwvcspxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.wv.ssp+xml")]
    applicationvndwvsspxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xara")]
    applicationvndxara,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xfdl")]
    applicationvndxfdl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xfdl.webform")]
    applicationvndxfdlwebform,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xmi+xml")]
    applicationvndxmixml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xmpie.cpkg")]
    applicationvndxmpiecpkg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xmpie.dpkg")]
    applicationvndxmpiedpkg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xmpie.plan")]
    applicationvndxmpieplan,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xmpie.ppkg")]
    applicationvndxmpieppkg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.xmpie.xlim")]
    applicationvndxmpiexlim,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yamaha.hv-dic")]
    applicationvndyamahahvdic,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yamaha.hv-script")]
    applicationvndyamahahvscript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yamaha.hv-voice")]
    applicationvndyamahahvvoice,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yamaha.openscoreformat")]
    applicationvndyamahaopenscoreformat,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yamaha.openscoreformat.osfpvg+xml")]
    applicationvndyamahaopenscoreformatosfpvgxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yamaha.smaf-audio")]
    applicationvndyamahasmafaudio,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yamaha.smaf-phrase")]
    applicationvndyamahasmafphrase,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.yellowriver-custom-menu")]
    applicationvndyellowrivercustommenu,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.zul")]
    applicationvndzul,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/vnd.zzazz.deck+xml")]
    applicationvndzzazzdeckxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/voicexml+xml")]
    applicationvoicexmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/watcherinfo+xml")]
    applicationwatcherinfoxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/whoispp-query")]
    applicationwhoisppquery,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/whoispp-response")]
    applicationwhoisppresponse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/wita")]
    applicationwita,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/wordperfect5.1")]
    applicationwordperfect51,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/wsdl+xml")]
    applicationwsdlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/wspolicy+xml")]
    applicationwspolicyxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/x400-bp")]
    applicationx400bp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xcap-att+xml")]
    applicationxcapattxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xcap-caps+xml")]
    applicationxcapcapsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xcap-el+xml")]
    applicationxcapelxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xcap-error+xml")]
    applicationxcaperrorxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xcap-ns+xml")]
    applicationxcapnsxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xcon-conference-info+xml")]
    applicationxconconferenceinfoxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xcon-conference-info-diff+xml")]
    applicationxconconferenceinfodiffxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xenc+xml")]
    applicationxencxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xhtml+xml")]
    applicationxhtmlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xhtml-voice+xml (Obsolete)")]
    applicationxhtmlvoicexmlObsolete,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xml")]
    applicationxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xml-dtd")]
    applicationxmldtd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xml-external-parsed-entity")]
    applicationxmlexternalparsedentity,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xmpp+xml")]
    applicationxmppxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xop+xml")]
    applicationxopxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/xv+xml")]
    applicationxvxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("application/zip")]
    applicationzip,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/32kadpcm")]
    audio32kadpcm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/3gpp")]
    audio3gpp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/3gpp2")]
    audio3gpp2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/AMR")]
    audioAMR,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/AMR-WB")]
    audioAMRWB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/ATRAC-ADVANCED-LOSSLESS")]
    audioATRACADVANCEDLOSSLESS,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/ATRAC-X")]
    audioATRACX,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/ATRAC3")]
    audioATRAC3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/BV16")]
    audioBV16,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/BV32")]
    audioBV32,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/CN")]
    audioCN,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/DAT12")]
    audioDAT12,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/DVI4")]
    audioDVI4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRC")]
    audioEVRC,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRC-QCP")]
    audioEVRCQCP,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRC0")]
    audioEVRC0,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRC1")]
    audioEVRC1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRCB")]
    audioEVRCB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRCB0")]
    audioEVRCB0,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRCB1")]
    audioEVRCB1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRCWB")]
    audioEVRCWB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRCWB0")]
    audioEVRCWB0,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/EVRCWB1")]
    audioEVRCWB1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G719")]
    audioG719,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G722")]
    audioG722,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G7221")]
    audioG7221,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G723")]
    audioG723,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G726-16")]
    audioG72616,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G726-24")]
    audioG72624,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G726-32")]
    audioG72632,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G726-40")]
    audioG72640,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G728")]
    audioG728,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G729")]
    audioG729,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G7291")]
    audioG7291,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G729D")]
    audioG729D,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/G729E")]
    audioG729E,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/GSM")]
    audioGSM,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/GSM-EFR")]
    audioGSMEFR,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/L16")]
    audioL16,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/L20")]
    audioL20,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/L24")]
    audioL24,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/L8")]
    audioL8,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/LPC")]
    audioLPC,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/MP4A-LATM")]
    audioMP4ALATM,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/MPA")]
    audioMPA,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/PCMA")]
    audioPCMA,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/PCMA-WB")]
    audioPCMAWB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/PCMU")]
    audioPCMU,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/PCMU-WB")]
    audioPCMUWB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/QCELP")]
    audioQCELP,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/RED")]
    audioRED,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/SMV")]
    audioSMV,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/SMV-QCP")]
    audioSMVQCP,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/SMV0")]
    audioSMV0,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/VDVI")]
    audioVDVI,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/VMR-WB")]
    audioVMRWB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/ac3")]
    audioac3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/amr-wb+")]
    audioamrwb,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/asc")]
    audioasc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/basic")]
    audiobasic,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/clearmode")]
    audioclearmode,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/dls")]
    audiodls,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/dsr-es201108")]
    audiodsres201108,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/dsr-es202050")]
    audiodsres202050,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/dsr-es202211")]
    audiodsres202211,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/dsr-es202212")]
    audiodsres202212,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/eac3")]
    audioeac3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/example")]
    audioexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/iLBC")]
    audioiLBC,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/mobile-xmf")]
    audiomobilexmf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/mp4")]
    audiomp4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/mpa-robust")]
    audiomparobust,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/mpeg")]
    audiompeg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/mpeg4-generic")]
    audiompeg4generic,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/ogg")]
    audioogg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/parityfec")]
    audioparityfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/prs.sid")]
    audioprssid,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/rtp-enc-aescm128")]
    audiortpencaescm128,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/rtp-midi")]
    audiortpmidi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/rtx")]
    audiortx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/sp-midi")]
    audiospmidi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/speex")]
    audiospeex,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/t140c")]
    audiot140c,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/t38")]
    audiot38,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/telephone-event")]
    audiotelephoneevent,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/tone")]
    audiotone,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/ulpfec")]
    audioulpfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.3gpp.iufp")]
    audiovnd3gppiufp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.4SB")]
    audiovnd4SB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.CELP")]
    audiovndCELP,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.audiokoz")]
    audiovndaudiokoz,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.cisco.nse")]
    audiovndcisconse,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.cmles.radio-events")]
    audiovndcmlesradioevents,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.cns.anp1")]
    audiovndcnsanp1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.cns.inf1")]
    audiovndcnsinf1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.digital-winds")]
    audiovnddigitalwinds,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dlna.adts")]
    audiovnddlnaadts,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.heaac.1")]
    audiovnddolbyheaac1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.heaac.2")]
    audiovnddolbyheaac2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.mlp")]
    audiovnddolbymlp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.mps")]
    audiovnddolbymps,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.pl2")]
    audiovnddolbypl2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.pl2x")]
    audiovnddolbypl2x,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.pl2z")]
    audiovnddolbypl2z,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dolby.pulse.1")]
    audiovnddolbypulse1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dra")]
    audiovnddra,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dts")]
    audiovnddts,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.dts.hd")]
    audiovnddtshd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.everad.plj")]
    audiovndeveradplj,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.hns.audio")]
    audiovndhnsaudio,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.lucent.voice")]
    audiovndlucentvoice,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.ms-playready.media.pya")]
    audiovndmsplayreadymediapya,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.nokia.mobile-xmf")]
    audiovndnokiamobilexmf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.nortel.vbk")]
    audiovndnortelvbk,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.nuera.ecelp4800")]
    audiovndnueraecelp4800,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.nuera.ecelp7470")]
    audiovndnueraecelp7470,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.nuera.ecelp9600")]
    audiovndnueraecelp9600,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.octel.sbc")]
    audiovndoctelsbc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.qcelp - DEPRECATED - Please use audio/qcelp")]
    audiovndqcelpDEPRECATEDPleaseuseaudioqcelp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.rhetorex.32kadpcm")]
    audiovndrhetorex32kadpcm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.sealedmedia.softseal.mpeg")]
    audiovndsealedmediasoftsealmpeg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vnd.vmx.cvsd")]
    audiovndvmxcvsd,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vorbis")]
    audiovorbis,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("audio/vorbis-config")]
    audiovorbisconfig,

    /// <remarks/>
    image,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image")]
    image1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image")]
    image2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/cgm")]
    imagecgm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/example")]
    imageexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/fits")]
    imagefits,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/g3fax")]
    imageg3fax,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/gif")]
    imagegif,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/ief")]
    imageief,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/jp2")]
    imagejp2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/jpeg")]
    imagejpeg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/jpm")]
    imagejpm,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/jpx")]
    imagejpx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/naplps")]
    imagenaplps,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/png")]
    imagepng,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/prs.btif")]
    imageprsbtif,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/prs.pti")]
    imageprspti,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/t38")]
    imaget38,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/tiff")]
    imagetiff,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/tiff-fx")]
    imagetifffx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.adobe.photoshop")]
    imagevndadobephotoshop,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.cns.inf2")]
    imagevndcnsinf2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.djvu")]
    imagevnddjvu,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.dwg")]
    imagevnddwg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.dxf")]
    imagevnddxf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.fastbidsheet")]
    imagevndfastbidsheet,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.fpx")]
    imagevndfpx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.fst")]
    imagevndfst,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.fujixerox.edmics-mmr")]
    imagevndfujixeroxedmicsmmr,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.fujixerox.edmics-rlc")]
    imagevndfujixeroxedmicsrlc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.globalgraphics.pgb")]
    imagevndglobalgraphicspgb,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.microsoft.icon")]
    imagevndmicrosofticon,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.mix")]
    imagevndmix,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.ms-modi")]
    imagevndmsmodi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.net-fpx")]
    imagevndnetfpx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.radiance")]
    imagevndradiance,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.sealed.png")]
    imagevndsealedpng,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.sealedmedia.softseal.gif")]
    imagevndsealedmediasoftsealgif,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.sealedmedia.softseal.jpg")]
    imagevndsealedmediasoftsealjpg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.svf")]
    imagevndsvf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.wap.wbmp")]
    imagevndwapwbmp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("image/vnd.xiff")]
    imagevndxiff,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/CPIM")]
    messageCPIM,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/delivery-status")]
    messagedeliverystatus,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/disposition-notification")]
    messagedispositionnotification,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/example")]
    messageexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/external-body")]
    messageexternalbody,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/global")]
    messageglobal,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/global-delivery-status")]
    messageglobaldeliverystatus,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/global-disposition-notification")]
    messageglobaldispositionnotification,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/global-headers")]
    messageglobalheaders,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/http")]
    messagehttp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/imdn+xml")]
    messageimdnxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/news (OBSOLETE)")]
    messagenewsOBSOLETE,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/partial")]
    messagepartial,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/rfc822")]
    messagerfc822,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/s-http")]
    messageshttp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/sip")]
    messagesip,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/sipfrag")]
    messagesipfrag,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/tracking-status")]
    messagetrackingstatus,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("message/vnd.si.simp")]
    messagevndsisimp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/example")]
    modelexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/iges")]
    modeliges,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/mesh")]
    modelmesh,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.dwf")]
    modelvnddwf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.flatland.3dml")]
    modelvndflatland3dml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.gdl")]
    modelvndgdl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.gs-gdl")]
    modelvndgsgdl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.gtw")]
    modelvndgtw,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.moml+xml")]
    modelvndmomlxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.mts")]
    modelvndmts,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.parasolid.transmit.binary")]
    modelvndparasolidtransmitbinary,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.parasolid.transmit.text")]
    modelvndparasolidtransmittext,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vnd.vtu")]
    modelvndvtu,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("model/vrml")]
    modelvrml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/alternative")]
    multipartalternative,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/appledouble")]
    multipartappledouble,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/byteranges")]
    multipartbyteranges,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/digest")]
    multipartdigest,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/encrypted")]
    multipartencrypted,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/example")]
    multipartexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/form-data")]
    multipartformdata,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/header-set")]
    multipartheaderset,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/mixed")]
    multipartmixed,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/parallel")]
    multipartparallel,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/related")]
    multipartrelated,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/report")]
    multipartreport,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/signed")]
    multipartsigned,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("multipart/voice-message")]
    multipartvoicemessage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/RED")]
    textRED,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/calendar")]
    textcalendar,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/css")]
    textcss,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/csv")]
    textcsv,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/directory")]
    textdirectory,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/dns")]
    textdns,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/ecmascript (obsolete)")]
    textecmascriptobsolete,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/enriched")]
    textenriched,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/example")]
    textexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/html")]
    texthtml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/javascript (obsolete)")]
    textjavascriptobsolete,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/parityfec")]
    textparityfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/plain")]
    textplain,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/prs.fallenstein.rst")]
    textprsfallensteinrst,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/prs.lines.tag")]
    textprslinestag,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/rfc822-headers")]
    textrfc822headers,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/richtext")]
    textrichtext,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/rtf")]
    textrtf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/rtp-enc-aescm128")]
    textrtpencaescm128,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/rtx")]
    textrtx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/sgml")]
    textsgml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/t140")]
    textt140,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/tab-separated-values")]
    texttabseparatedvalues,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/troff")]
    texttroff,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/ulpfec")]
    textulpfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/uri-list")]
    texturilist,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.DMClientScript")]
    textvndDMClientScript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.IPTC.NITF")]
    textvndIPTCNITF,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.IPTC.NewsML")]
    textvndIPTCNewsML,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.abc")]
    textvndabc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.curl")]
    textvndcurl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.esmertec.theme-descriptor")]
    textvndesmertecthemedescriptor,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.fly")]
    textvndfly,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.fmi.flexstor")]
    textvndfmiflexstor,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.graphviz")]
    textvndgraphviz,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.in3d.3dml")]
    textvndin3d3dml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.in3d.spot")]
    textvndin3dspot,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.latex-z")]
    textvndlatexz,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.motorola.reflex")]
    textvndmotorolareflex,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.ms-mediapackage")]
    textvndmsmediapackage,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.net2phone.commcenter.command")]
    textvndnet2phonecommcentercommand,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.si.uricatalogue")]
    textvndsiuricatalogue,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.sun.j2me.app-descriptor")]
    textvndsunj2meappdescriptor,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.trolltech.linguist")]
    textvndtrolltechlinguist,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.wap.si")]
    textvndwapsi,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.wap.sl")]
    textvndwapsl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.wap.wml")]
    textvndwapwml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/vnd.wap.wmlscript")]
    textvndwapwmlscript,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/xml")]
    textxml,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("text/xml-external-parsed-entity")]
    textxmlexternalparsedentity,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/3gpp")]
    video3gpp,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/3gpp-tt")]
    video3gpptt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/3gpp2")]
    video3gpp2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/BMPEG")]
    videoBMPEG,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/BT656")]
    videoBT656,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/CelB")]
    videoCelB,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/DV")]
    videoDV,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/H261")]
    videoH261,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/H263")]
    videoH263,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/H263-1998")]
    videoH2631998,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/H263-2000")]
    videoH2632000,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/H264")]
    videoH264,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/JPEG")]
    videoJPEG,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/MJ2")]
    videoMJ2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/MP1S")]
    videoMP1S,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/MP2P")]
    videoMP2P,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/MP2T")]
    videoMP2T,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/MP4V-ES")]
    videoMP4VES,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/MPV")]
    videoMPV,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/SMPTE292M")]
    videoSMPTE292M,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/example")]
    videoexample,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/jpeg2000")]
    videojpeg2000,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/mp4")]
    videomp4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/mpeg")]
    videompeg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/mpeg4-generic")]
    videompeg4generic,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/nv")]
    videonv,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/ogg")]
    videoogg,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/parityfec")]
    videoparityfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/pointer")]
    videopointer,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/quicktime")]
    videoquicktime,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/raw")]
    videoraw,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/rtp-enc-aescm128")]
    videortpencaescm128,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/rtx")]
    videortx,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/ulpfec")]
    videoulpfec,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vc1")]
    videovc1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.CCTV")]
    videovndCCTV,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.dlna.mpeg-tts")]
    videovnddlnampegtts,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.fvt")]
    videovndfvt,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.hns.video")]
    videovndhnsvideo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.iptvforum.1dparityfec-1010")]
    videovndiptvforum1dparityfec1010,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.iptvforum.1dparityfec-2005")]
    videovndiptvforum1dparityfec2005,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.iptvforum.2dparityfec-1010")]
    videovndiptvforum2dparityfec1010,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.iptvforum.2dparityfec-2005")]
    videovndiptvforum2dparityfec2005,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.iptvforum.ttsavc")]
    videovndiptvforumttsavc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.iptvforum.ttsmpeg2")]
    videovndiptvforumttsmpeg2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.motorola.video")]
    videovndmotorolavideo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.motorola.videop")]
    videovndmotorolavideop,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.mpegurl")]
    videovndmpegurl,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.ms-playready.media.pyv")]
    videovndmsplayreadymediapyv,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.nokia.interleaved-multimedia")]
    videovndnokiainterleavedmultimedia,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.nokia.videovoip")]
    videovndnokiavideovoip,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.objectvideo")]
    videovndobjectvideo,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.sealed.mpeg1")]
    videovndsealedmpeg1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.sealed.mpeg4")]
    videovndsealedmpeg4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.sealed.swf")]
    videovndsealedswf,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.sealedmedia.softseal.mov")]
    videovndsealedmediasoftsealmov,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("video/vnd.vivo")]
    videovndvivo,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:5:ISO42173A:2009-09-09")]
[System.Xml.Serialization.XmlRootAttribute("ISO3AlphaCurrencyCode", Namespace = "urn:un:unece:uncefact:codelist:standard:5:ISO42173A:2009-09-09", IsNullable = false)]
public enum ISO3AlphaCurrencyCodeContentType
{

    /// <remarks/>
    AED,

    /// <remarks/>
    AFN,

    /// <remarks/>
    ALL,

    /// <remarks/>
    AMD,

    /// <remarks/>
    ANG,

    /// <remarks/>
    AOA,

    /// <remarks/>
    ARS,

    /// <remarks/>
    AUD,

    /// <remarks/>
    AWG,

    /// <remarks/>
    AZN,

    /// <remarks/>
    BAM,

    /// <remarks/>
    BBD,

    /// <remarks/>
    BDT,

    /// <remarks/>
    BGN,

    /// <remarks/>
    BHD,

    /// <remarks/>
    BIF,

    /// <remarks/>
    BMD,

    /// <remarks/>
    BND,

    /// <remarks/>
    BOB,

    /// <remarks/>
    BOV,

    /// <remarks/>
    BRL,

    /// <remarks/>
    BSD,

    /// <remarks/>
    BTN,

    /// <remarks/>
    BWP,

    /// <remarks/>
    BYR,

    /// <remarks/>
    BZD,

    /// <remarks/>
    CAD,

    /// <remarks/>
    CDF,

    /// <remarks/>
    CHE,

    /// <remarks/>
    CHF,

    /// <remarks/>
    CHW,

    /// <remarks/>
    CLF,

    /// <remarks/>
    CLP,

    /// <remarks/>
    CNY,

    /// <remarks/>
    COP,

    /// <remarks/>
    COU,

    /// <remarks/>
    CRC,

    /// <remarks/>
    CUC,

    /// <remarks/>
    CUP,

    /// <remarks/>
    CVE,

    /// <remarks/>
    CZK,

    /// <remarks/>
    DJF,

    /// <remarks/>
    DKK,

    /// <remarks/>
    DOP,

    /// <remarks/>
    DZD,

    /// <remarks/>
    EEK,

    /// <remarks/>
    EGP,

    /// <remarks/>
    ERN,

    /// <remarks/>
    ETB,

    /// <remarks/>
    EUR,

    /// <remarks/>
    FJD,

    /// <remarks/>
    FKP,

    /// <remarks/>
    GBP,

    /// <remarks/>
    GEL,

    /// <remarks/>
    GHS,

    /// <remarks/>
    GIP,

    /// <remarks/>
    GMD,

    /// <remarks/>
    GNF,

    /// <remarks/>
    GTQ,

    /// <remarks/>
    GWP,

    /// <remarks/>
    GYD,

    /// <remarks/>
    HKD,

    /// <remarks/>
    HNL,

    /// <remarks/>
    HRK,

    /// <remarks/>
    HTG,

    /// <remarks/>
    HUF,

    /// <remarks/>
    IDR,

    /// <remarks/>
    ILS,

    /// <remarks/>
    INR,

    /// <remarks/>
    IQD,

    /// <remarks/>
    IRR,

    /// <remarks/>
    ISK,

    /// <remarks/>
    JMD,

    /// <remarks/>
    JOD,

    /// <remarks/>
    JPY,

    /// <remarks/>
    KES,

    /// <remarks/>
    KGS,

    /// <remarks/>
    KHR,

    /// <remarks/>
    KMF,

    /// <remarks/>
    KPW,

    /// <remarks/>
    KRW,

    /// <remarks/>
    KWD,

    /// <remarks/>
    KYD,

    /// <remarks/>
    KZT,

    /// <remarks/>
    LAK,

    /// <remarks/>
    LBP,

    /// <remarks/>
    LKR,

    /// <remarks/>
    LRD,

    /// <remarks/>
    LSL,

    /// <remarks/>
    LTL,

    /// <remarks/>
    LVL,

    /// <remarks/>
    LYD,

    /// <remarks/>
    MAD,

    /// <remarks/>
    MDL,

    /// <remarks/>
    MGA,

    /// <remarks/>
    MKD,

    /// <remarks/>
    MMK,

    /// <remarks/>
    MNT,

    /// <remarks/>
    MOP,

    /// <remarks/>
    MRO,

    /// <remarks/>
    MUR,

    /// <remarks/>
    MVR,

    /// <remarks/>
    MWK,

    /// <remarks/>
    MXN,

    /// <remarks/>
    MXV,

    /// <remarks/>
    MYR,

    /// <remarks/>
    MZN,

    /// <remarks/>
    NAD,

    /// <remarks/>
    NGN,

    /// <remarks/>
    NIO,

    /// <remarks/>
    NOK,

    /// <remarks/>
    NPR,

    /// <remarks/>
    NZD,

    /// <remarks/>
    OMR,

    /// <remarks/>
    PAB,

    /// <remarks/>
    PEN,

    /// <remarks/>
    PGK,

    /// <remarks/>
    PHP,

    /// <remarks/>
    PKR,

    /// <remarks/>
    PLN,

    /// <remarks/>
    PYG,

    /// <remarks/>
    QAR,

    /// <remarks/>
    RON,

    /// <remarks/>
    RSD,

    /// <remarks/>
    RUB,

    /// <remarks/>
    RWF,

    /// <remarks/>
    SAR,

    /// <remarks/>
    SBD,

    /// <remarks/>
    SCR,

    /// <remarks/>
    SDG,

    /// <remarks/>
    SEK,

    /// <remarks/>
    SGD,

    /// <remarks/>
    SHP,

    /// <remarks/>
    SLL,

    /// <remarks/>
    SOS,

    /// <remarks/>
    SRD,

    /// <remarks/>
    STD,

    /// <remarks/>
    SVC,

    /// <remarks/>
    SYP,

    /// <remarks/>
    SZL,

    /// <remarks/>
    THB,

    /// <remarks/>
    TJS,

    /// <remarks/>
    TMT,

    /// <remarks/>
    TND,

    /// <remarks/>
    TOP,

    /// <remarks/>
    TRY,

    /// <remarks/>
    TTD,

    /// <remarks/>
    TWD,

    /// <remarks/>
    TZS,

    /// <remarks/>
    UAH,

    /// <remarks/>
    UGX,

    /// <remarks/>
    USD,

    /// <remarks/>
    USN,

    /// <remarks/>
    USS,

    /// <remarks/>
    UYI,

    /// <remarks/>
    UYU,

    /// <remarks/>
    UZS,

    /// <remarks/>
    VEF,

    /// <remarks/>
    VND,

    /// <remarks/>
    VUV,

    /// <remarks/>
    WST,

    /// <remarks/>
    XAF,

    /// <remarks/>
    XAG,

    /// <remarks/>
    XAU,

    /// <remarks/>
    XBA,

    /// <remarks/>
    XBB,

    /// <remarks/>
    XBC,

    /// <remarks/>
    XBD,

    /// <remarks/>
    XCD,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("XCD")]
    XCD1,

    /// <remarks/>
    XDR,

    /// <remarks/>
    XFU,

    /// <remarks/>
    XOF,

    /// <remarks/>
    XPD,

    /// <remarks/>
    XPF,

    /// <remarks/>
    XPT,

    /// <remarks/>
    XTS,

    /// <remarks/>
    XXX,

    /// <remarks/>
    YER,

    /// <remarks/>
    ZAR,

    /// <remarks/>
    ZMK,

    /// <remarks/>
    ZWL,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:6:0133:40106")]
[System.Xml.Serialization.XmlRootAttribute("CharacterSetEncodingCode", Namespace = "urn:un:unece:uncefact:codelist:standard:6:0133:40106", IsNullable = false)]
public enum CharacterSetEncodingCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1")]
    Item1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2")]
    Item2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3")]
    Item3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4")]
    Item4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5")]
    Item5,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6")]
    Item6,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("7")]
    Item7,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("8")]
    Item8,

    /// <remarks/>
    zzz,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:DeliveryTermsCode:2000")]
[System.Xml.Serialization.XmlRootAttribute("DeliveryTermsCode", Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:DeliveryTermsCode:2000", IsNullable = false)]
public enum DeliveryTermsCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1")]
    Item1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2")]
    Item2,

    /// <remarks/>
    CFR,

    /// <remarks/>
    CIF,

    /// <remarks/>
    CIP,

    /// <remarks/>
    CPT,

    /// <remarks/>
    DAF,

    /// <remarks/>
    DDP,

    /// <remarks/>
    DDU,

    /// <remarks/>
    DEQ,

    /// <remarks/>
    DES,

    /// <remarks/>
    EXW,

    /// <remarks/>
    FAS,

    /// <remarks/>
    FCA,

    /// <remarks/>
    FOB,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:PackageTypeCode:2006")]
[System.Xml.Serialization.XmlRootAttribute("PackageTypeCode", Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:PackageTypeCode:2006", IsNullable = false)]
public enum PackageTypeCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("43")]
    Item43,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1A")]
    Item1A,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1B")]
    Item1B,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1D")]
    Item1D,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1G")]
    Item1G,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1W")]
    Item1W,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2C")]
    Item2C,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3A")]
    Item3A,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3H")]
    Item3H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4A")]
    Item4A,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4B")]
    Item4B,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4C")]
    Item4C,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4D")]
    Item4D,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4F")]
    Item4F,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4G")]
    Item4G,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4H")]
    Item4H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5H")]
    Item5H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5L")]
    Item5L,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5M")]
    Item5M,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6H")]
    Item6H,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6P")]
    Item6P,

    /// <remarks/>
    AA,

    /// <remarks/>
    AB,

    /// <remarks/>
    AC,

    /// <remarks/>
    AD,

    /// <remarks/>
    AE,

    /// <remarks/>
    AF,

    /// <remarks/>
    AG,

    /// <remarks/>
    AH,

    /// <remarks/>
    AI,

    /// <remarks/>
    AJ,

    /// <remarks/>
    AM,

    /// <remarks/>
    AP,

    /// <remarks/>
    AT,

    /// <remarks/>
    AV,

    /// <remarks/>
    BA,

    /// <remarks/>
    BB,

    /// <remarks/>
    BC,

    /// <remarks/>
    BD,

    /// <remarks/>
    BE,

    /// <remarks/>
    BF,

    /// <remarks/>
    BG,

    /// <remarks/>
    BH,

    /// <remarks/>
    BI,

    /// <remarks/>
    BJ,

    /// <remarks/>
    BK,

    /// <remarks/>
    BL,

    /// <remarks/>
    BM,

    /// <remarks/>
    BN,

    /// <remarks/>
    BO,

    /// <remarks/>
    BP,

    /// <remarks/>
    BQ,

    /// <remarks/>
    BR,

    /// <remarks/>
    BS,

    /// <remarks/>
    BT,

    /// <remarks/>
    BU,

    /// <remarks/>
    BV,

    /// <remarks/>
    BW,

    /// <remarks/>
    BX,

    /// <remarks/>
    BY,

    /// <remarks/>
    BZ,

    /// <remarks/>
    CA,

    /// <remarks/>
    CB,

    /// <remarks/>
    CC,

    /// <remarks/>
    CD,

    /// <remarks/>
    CE,

    /// <remarks/>
    CF,

    /// <remarks/>
    CG,

    /// <remarks/>
    CH,

    /// <remarks/>
    CI,

    /// <remarks/>
    CJ,

    /// <remarks/>
    CK,

    /// <remarks/>
    CL,

    /// <remarks/>
    CM,

    /// <remarks/>
    CN,

    /// <remarks/>
    CO,

    /// <remarks/>
    CP,

    /// <remarks/>
    CQ,

    /// <remarks/>
    CR,

    /// <remarks/>
    CS,

    /// <remarks/>
    CT,

    /// <remarks/>
    CU,

    /// <remarks/>
    CV,

    /// <remarks/>
    CW,

    /// <remarks/>
    CX,

    /// <remarks/>
    CY,

    /// <remarks/>
    CZ,

    /// <remarks/>
    DA,

    /// <remarks/>
    DB,

    /// <remarks/>
    DC,

    /// <remarks/>
    DG,

    /// <remarks/>
    DH,

    /// <remarks/>
    DI,

    /// <remarks/>
    DJ,

    /// <remarks/>
    DK,

    /// <remarks/>
    DL,

    /// <remarks/>
    DM,

    /// <remarks/>
    DN,

    /// <remarks/>
    DP,

    /// <remarks/>
    DR,

    /// <remarks/>
    DS,

    /// <remarks/>
    DT,

    /// <remarks/>
    DU,

    /// <remarks/>
    DV,

    /// <remarks/>
    DW,

    /// <remarks/>
    DX,

    /// <remarks/>
    DY,

    /// <remarks/>
    EC,

    /// <remarks/>
    ED,

    /// <remarks/>
    EE,

    /// <remarks/>
    EF,

    /// <remarks/>
    EG,

    /// <remarks/>
    EH,

    /// <remarks/>
    EI,

    /// <remarks/>
    EN,

    /// <remarks/>
    FC,

    /// <remarks/>
    FD,

    /// <remarks/>
    FI,

    /// <remarks/>
    FL,

    /// <remarks/>
    FO,

    /// <remarks/>
    FP,

    /// <remarks/>
    FR,

    /// <remarks/>
    FT,

    /// <remarks/>
    FW,

    /// <remarks/>
    FX,

    /// <remarks/>
    GB,

    /// <remarks/>
    GI,

    /// <remarks/>
    GR,

    /// <remarks/>
    GU,

    /// <remarks/>
    GZ,

    /// <remarks/>
    HA,

    /// <remarks/>
    HB,

    /// <remarks/>
    HC,

    /// <remarks/>
    HG,

    /// <remarks/>
    HR,

    /// <remarks/>
    IA,

    /// <remarks/>
    IB,

    /// <remarks/>
    IC,

    /// <remarks/>
    ID,

    /// <remarks/>
    IE,

    /// <remarks/>
    IF,

    /// <remarks/>
    IG,

    /// <remarks/>
    IH,

    /// <remarks/>
    IK,

    /// <remarks/>
    IL,

    /// <remarks/>
    IN,

    /// <remarks/>
    IZ,

    /// <remarks/>
    JC,

    /// <remarks/>
    JG,

    /// <remarks/>
    JR,

    /// <remarks/>
    JT,

    /// <remarks/>
    JY,

    /// <remarks/>
    KG,

    /// <remarks/>
    LG,

    /// <remarks/>
    LT,

    /// <remarks/>
    LV,

    /// <remarks/>
    LZ,

    /// <remarks/>
    MB,

    /// <remarks/>
    MC,

    /// <remarks/>
    MR,

    /// <remarks/>
    MS,

    /// <remarks/>
    MT,

    /// <remarks/>
    MW,

    /// <remarks/>
    MX,

    /// <remarks/>
    NA,

    /// <remarks/>
    NE,

    /// <remarks/>
    NF,

    /// <remarks/>
    NG,

    /// <remarks/>
    NS,

    /// <remarks/>
    NT,

    /// <remarks/>
    NU,

    /// <remarks/>
    NV,

    /// <remarks/>
    OK,

    /// <remarks/>
    PA,

    /// <remarks/>
    PB,

    /// <remarks/>
    PC,

    /// <remarks/>
    PD,

    /// <remarks/>
    PE,

    /// <remarks/>
    PF,

    /// <remarks/>
    PG,

    /// <remarks/>
    PH,

    /// <remarks/>
    PI,

    /// <remarks/>
    PJ,

    /// <remarks/>
    PK,

    /// <remarks/>
    PL,

    /// <remarks/>
    PN,

    /// <remarks/>
    PO,

    /// <remarks/>
    PR,

    /// <remarks/>
    PT,

    /// <remarks/>
    PU,

    /// <remarks/>
    PV,

    /// <remarks/>
    PX,

    /// <remarks/>
    PY,

    /// <remarks/>
    PZ,

    /// <remarks/>
    QA,

    /// <remarks/>
    QB,

    /// <remarks/>
    QC,

    /// <remarks/>
    QD,

    /// <remarks/>
    QF,

    /// <remarks/>
    QG,

    /// <remarks/>
    QH,

    /// <remarks/>
    QJ,

    /// <remarks/>
    QK,

    /// <remarks/>
    QL,

    /// <remarks/>
    QM,

    /// <remarks/>
    QN,

    /// <remarks/>
    QP,

    /// <remarks/>
    QQ,

    /// <remarks/>
    QR,

    /// <remarks/>
    QS,

    /// <remarks/>
    RD,

    /// <remarks/>
    RG,

    /// <remarks/>
    RJ,

    /// <remarks/>
    RK,

    /// <remarks/>
    RL,

    /// <remarks/>
    RO,

    /// <remarks/>
    RT,

    /// <remarks/>
    RZ,

    /// <remarks/>
    SA,

    /// <remarks/>
    SB,

    /// <remarks/>
    SC,

    /// <remarks/>
    SD,

    /// <remarks/>
    SE,

    /// <remarks/>
    SH,

    /// <remarks/>
    SI,

    /// <remarks/>
    SK,

    /// <remarks/>
    SL,

    /// <remarks/>
    SM,

    /// <remarks/>
    SO,

    /// <remarks/>
    SP,

    /// <remarks/>
    SS,

    /// <remarks/>
    ST,

    /// <remarks/>
    SU,

    /// <remarks/>
    SV,

    /// <remarks/>
    SW,

    /// <remarks/>
    SX,

    /// <remarks/>
    SY,

    /// <remarks/>
    SZ,

    /// <remarks/>
    TB,

    /// <remarks/>
    TC,

    /// <remarks/>
    TD,

    /// <remarks/>
    TI,

    /// <remarks/>
    TK,

    /// <remarks/>
    TL,

    /// <remarks/>
    TN,

    /// <remarks/>
    TO,

    /// <remarks/>
    TR,

    /// <remarks/>
    TS,

    /// <remarks/>
    TU,

    /// <remarks/>
    TV,

    /// <remarks/>
    TY,

    /// <remarks/>
    TZ,

    /// <remarks/>
    UC,

    /// <remarks/>
    VA,

    /// <remarks/>
    VG,

    /// <remarks/>
    VI,

    /// <remarks/>
    VK,

    /// <remarks/>
    VL,

    /// <remarks/>
    VN,

    /// <remarks/>
    VO,

    /// <remarks/>
    VP,

    /// <remarks/>
    VQ,

    /// <remarks/>
    VR,

    /// <remarks/>
    VY,

    /// <remarks/>
    WA,

    /// <remarks/>
    WB,

    /// <remarks/>
    WC,

    /// <remarks/>
    WD,

    /// <remarks/>
    WF,

    /// <remarks/>
    WG,

    /// <remarks/>
    WH,

    /// <remarks/>
    WJ,

    /// <remarks/>
    WK,

    /// <remarks/>
    WL,

    /// <remarks/>
    WM,

    /// <remarks/>
    WN,

    /// <remarks/>
    WP,

    /// <remarks/>
    WQ,

    /// <remarks/>
    WR,

    /// <remarks/>
    WS,

    /// <remarks/>
    WT,

    /// <remarks/>
    WU,

    /// <remarks/>
    WV,

    /// <remarks/>
    WW,

    /// <remarks/>
    WX,

    /// <remarks/>
    WY,

    /// <remarks/>
    WZ,

    /// <remarks/>
    XA,

    /// <remarks/>
    XB,

    /// <remarks/>
    XC,

    /// <remarks/>
    XD,

    /// <remarks/>
    XF,

    /// <remarks/>
    XG,

    /// <remarks/>
    XH,

    /// <remarks/>
    XJ,

    /// <remarks/>
    XK,

    /// <remarks/>
    YA,

    /// <remarks/>
    YB,

    /// <remarks/>
    YC,

    /// <remarks/>
    YD,

    /// <remarks/>
    YF,

    /// <remarks/>
    YG,

    /// <remarks/>
    YH,

    /// <remarks/>
    YJ,

    /// <remarks/>
    YK,

    /// <remarks/>
    YL,

    /// <remarks/>
    YM,

    /// <remarks/>
    YN,

    /// <remarks/>
    YP,

    /// <remarks/>
    YQ,

    /// <remarks/>
    YR,

    /// <remarks/>
    YS,

    /// <remarks/>
    YT,

    /// <remarks/>
    YV,

    /// <remarks/>
    YW,

    /// <remarks/>
    YX,

    /// <remarks/>
    YY,

    /// <remarks/>
    YZ,

    /// <remarks/>
    ZA,

    /// <remarks/>
    ZB,

    /// <remarks/>
    ZC,

    /// <remarks/>
    ZD,

    /// <remarks/>
    ZF,

    /// <remarks/>
    ZG,

    /// <remarks/>
    ZH,

    /// <remarks/>
    ZJ,

    /// <remarks/>
    ZK,

    /// <remarks/>
    ZL,

    /// <remarks/>
    ZM,

    /// <remarks/>
    ZN,

    /// <remarks/>
    ZP,

    /// <remarks/>
    ZQ,

    /// <remarks/>
    ZR,

    /// <remarks/>
    ZS,

    /// <remarks/>
    ZT,

    /// <remarks/>
    ZU,

    /// <remarks/>
    ZV,

    /// <remarks/>
    ZW,

    /// <remarks/>
    ZX,

    /// <remarks/>
    ZY,

    /// <remarks/>
    ZZ,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:PartyRoleCode:D09A")]
[System.Xml.Serialization.XmlRootAttribute("PartyRoleCode", Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:PartyRoleCode:D09A", IsNullable = false)]
public enum PartyRoleCodeContentType
{

    /// <remarks/>
    AA,

    /// <remarks/>
    AB,

    /// <remarks/>
    AE,

    /// <remarks/>
    AF,

    /// <remarks/>
    AG,

    /// <remarks/>
    AH,

    /// <remarks/>
    AI,

    /// <remarks/>
    AJ,

    /// <remarks/>
    AK,

    /// <remarks/>
    AL,

    /// <remarks/>
    AM,

    /// <remarks/>
    AN,

    /// <remarks/>
    AO,

    /// <remarks/>
    AP,

    /// <remarks/>
    AQ,

    /// <remarks/>
    AR,

    /// <remarks/>
    AS,

    /// <remarks/>
    AT,

    /// <remarks/>
    AU,

    /// <remarks/>
    AV,

    /// <remarks/>
    AW,

    /// <remarks/>
    AX,

    /// <remarks/>
    AY,

    /// <remarks/>
    AZ,

    /// <remarks/>
    B1,

    /// <remarks/>
    B2,

    /// <remarks/>
    BA,

    /// <remarks/>
    BB,

    /// <remarks/>
    BC,

    /// <remarks/>
    BD,

    /// <remarks/>
    BE,

    /// <remarks/>
    BF,

    /// <remarks/>
    BG,

    /// <remarks/>
    BH,

    /// <remarks/>
    BI,

    /// <remarks/>
    BJ,

    /// <remarks/>
    BK,

    /// <remarks/>
    BL,

    /// <remarks/>
    BM,

    /// <remarks/>
    BN,

    /// <remarks/>
    BO,

    /// <remarks/>
    BP,

    /// <remarks/>
    BQ,

    /// <remarks/>
    BS,

    /// <remarks/>
    BT,

    /// <remarks/>
    BU,

    /// <remarks/>
    BV,

    /// <remarks/>
    BW,

    /// <remarks/>
    BX,

    /// <remarks/>
    BY,

    /// <remarks/>
    BZ,

    /// <remarks/>
    C1,

    /// <remarks/>
    C2,

    /// <remarks/>
    CA,

    /// <remarks/>
    CB,

    /// <remarks/>
    CC,

    /// <remarks/>
    CD,

    /// <remarks/>
    CE,

    /// <remarks/>
    CF,

    /// <remarks/>
    CG,

    /// <remarks/>
    CH,

    /// <remarks/>
    CI,

    /// <remarks/>
    CJ,

    /// <remarks/>
    CK,

    /// <remarks/>
    CL,

    /// <remarks/>
    CM,

    /// <remarks/>
    CN,

    /// <remarks/>
    CNX,

    /// <remarks/>
    CNY,

    /// <remarks/>
    CNZ,

    /// <remarks/>
    CO,

    /// <remarks/>
    COA,

    /// <remarks/>
    COB,

    /// <remarks/>
    COC,

    /// <remarks/>
    COD,

    /// <remarks/>
    COE,

    /// <remarks/>
    COF,

    /// <remarks/>
    COG,

    /// <remarks/>
    COH,

    /// <remarks/>
    COI,

    /// <remarks/>
    COJ,

    /// <remarks/>
    COK,

    /// <remarks/>
    COL,

    /// <remarks/>
    COM,

    /// <remarks/>
    CON,

    /// <remarks/>
    COO,

    /// <remarks/>
    COP,

    /// <remarks/>
    COQ,

    /// <remarks/>
    COR,

    /// <remarks/>
    COS,

    /// <remarks/>
    COT,

    /// <remarks/>
    COU,

    /// <remarks/>
    COV,

    /// <remarks/>
    COW,

    /// <remarks/>
    COX,

    /// <remarks/>
    COY,

    /// <remarks/>
    COZ,

    /// <remarks/>
    CP,

    /// <remarks/>
    CPA,

    /// <remarks/>
    CPB,

    /// <remarks/>
    CPC,

    /// <remarks/>
    CPD,

    /// <remarks/>
    CPE,

    /// <remarks/>
    CPF,

    /// <remarks/>
    CPG,

    /// <remarks/>
    CPH,

    /// <remarks/>
    CPI,

    /// <remarks/>
    CPJ,

    /// <remarks/>
    CPK,

    /// <remarks/>
    CPL,

    /// <remarks/>
    CPM,

    /// <remarks/>
    CPN,

    /// <remarks/>
    CPO,

    /// <remarks/>
    CQ,

    /// <remarks/>
    CR,

    /// <remarks/>
    CS,

    /// <remarks/>
    CT,

    /// <remarks/>
    CU,

    /// <remarks/>
    CV,

    /// <remarks/>
    CW,

    /// <remarks/>
    CX,

    /// <remarks/>
    CY,

    /// <remarks/>
    CZ,

    /// <remarks/>
    DA,

    /// <remarks/>
    DB,

    /// <remarks/>
    DC,

    /// <remarks/>
    DCP,

    /// <remarks/>
    DCQ,

    /// <remarks/>
    DCR,

    /// <remarks/>
    DCS,

    /// <remarks/>
    DCT,

    /// <remarks/>
    DCU,

    /// <remarks/>
    DCV,

    /// <remarks/>
    DCW,

    /// <remarks/>
    DCX,

    /// <remarks/>
    DCY,

    /// <remarks/>
    DCZ,

    /// <remarks/>
    DD,

    /// <remarks/>
    DDA,

    /// <remarks/>
    DDB,

    /// <remarks/>
    DDC,

    /// <remarks/>
    DDD,

    /// <remarks/>
    DDE,

    /// <remarks/>
    DDF,

    /// <remarks/>
    DDG,

    /// <remarks/>
    DDH,

    /// <remarks/>
    DDI,

    /// <remarks/>
    DDJ,

    /// <remarks/>
    DDK,

    /// <remarks/>
    DDL,

    /// <remarks/>
    DDM,

    /// <remarks/>
    DDN,

    /// <remarks/>
    DDO,

    /// <remarks/>
    DDP,

    /// <remarks/>
    DDQ,

    /// <remarks/>
    DDR,

    /// <remarks/>
    DDS,

    /// <remarks/>
    DDT,

    /// <remarks/>
    DDU,

    /// <remarks/>
    DDV,

    /// <remarks/>
    DDW,

    /// <remarks/>
    DDX,

    /// <remarks/>
    DDY,

    /// <remarks/>
    DDZ,

    /// <remarks/>
    DE,

    /// <remarks/>
    DEA,

    /// <remarks/>
    DEB,

    /// <remarks/>
    DEC,

    /// <remarks/>
    DED,

    /// <remarks/>
    DEE,

    /// <remarks/>
    DEF,

    /// <remarks/>
    DEG,

    /// <remarks/>
    DEH,

    /// <remarks/>
    DEI,

    /// <remarks/>
    DEJ,

    /// <remarks/>
    DEK,

    /// <remarks/>
    DEL,

    /// <remarks/>
    DEM,

    /// <remarks/>
    DEN,

    /// <remarks/>
    DEO,

    /// <remarks/>
    DEP,

    /// <remarks/>
    DEQ,

    /// <remarks/>
    DER,

    /// <remarks/>
    DES,

    /// <remarks/>
    DET,

    /// <remarks/>
    DEU,

    /// <remarks/>
    DEV,

    /// <remarks/>
    DEW,

    /// <remarks/>
    DEX,

    /// <remarks/>
    DEY,

    /// <remarks/>
    DEZ,

    /// <remarks/>
    DF,

    /// <remarks/>
    DFA,

    /// <remarks/>
    DFB,

    /// <remarks/>
    DFC,

    /// <remarks/>
    DFD,

    /// <remarks/>
    DFE,

    /// <remarks/>
    DFF,

    /// <remarks/>
    DFG,

    /// <remarks/>
    DFH,

    /// <remarks/>
    DFI,

    /// <remarks/>
    DFJ,

    /// <remarks/>
    DFK,

    /// <remarks/>
    DFL,

    /// <remarks/>
    DFM,

    /// <remarks/>
    DFN,

    /// <remarks/>
    DFO,

    /// <remarks/>
    DFP,

    /// <remarks/>
    DFQ,

    /// <remarks/>
    DFR,

    /// <remarks/>
    DFS,

    /// <remarks/>
    DFT,

    /// <remarks/>
    DG,

    /// <remarks/>
    DH,

    /// <remarks/>
    DI,

    /// <remarks/>
    DJ,

    /// <remarks/>
    DK,

    /// <remarks/>
    DL,

    /// <remarks/>
    DM,

    /// <remarks/>
    DN,

    /// <remarks/>
    DO,

    /// <remarks/>
    DP,

    /// <remarks/>
    DQ,

    /// <remarks/>
    DR,

    /// <remarks/>
    DS,

    /// <remarks/>
    DT,

    /// <remarks/>
    DU,

    /// <remarks/>
    DV,

    /// <remarks/>
    DW,

    /// <remarks/>
    DX,

    /// <remarks/>
    DY,

    /// <remarks/>
    DZ,

    /// <remarks/>
    EA,

    /// <remarks/>
    EB,

    /// <remarks/>
    EC,

    /// <remarks/>
    ED,

    /// <remarks/>
    EE,

    /// <remarks/>
    EF,

    /// <remarks/>
    EG,

    /// <remarks/>
    EH,

    /// <remarks/>
    EI,

    /// <remarks/>
    EJ,

    /// <remarks/>
    EK,

    /// <remarks/>
    EL,

    /// <remarks/>
    EM,

    /// <remarks/>
    EN,

    /// <remarks/>
    EO,

    /// <remarks/>
    EP,

    /// <remarks/>
    EQ,

    /// <remarks/>
    ER,

    /// <remarks/>
    ES,

    /// <remarks/>
    ET,

    /// <remarks/>
    EU,

    /// <remarks/>
    EV,

    /// <remarks/>
    EW,

    /// <remarks/>
    EX,

    /// <remarks/>
    EY,

    /// <remarks/>
    EZ,

    /// <remarks/>
    FA,

    /// <remarks/>
    FB,

    /// <remarks/>
    FC,

    /// <remarks/>
    FD,

    /// <remarks/>
    FE,

    /// <remarks/>
    FF,

    /// <remarks/>
    FG,

    /// <remarks/>
    FH,

    /// <remarks/>
    FI,

    /// <remarks/>
    FJ,

    /// <remarks/>
    FK,

    /// <remarks/>
    FL,

    /// <remarks/>
    FM,

    /// <remarks/>
    FN,

    /// <remarks/>
    FO,

    /// <remarks/>
    FP,

    /// <remarks/>
    FQ,

    /// <remarks/>
    FR,

    /// <remarks/>
    FS,

    /// <remarks/>
    FT,

    /// <remarks/>
    FU,

    /// <remarks/>
    FV,

    /// <remarks/>
    FW,

    /// <remarks/>
    FX,

    /// <remarks/>
    FY,

    /// <remarks/>
    FZ,

    /// <remarks/>
    GA,

    /// <remarks/>
    GB,

    /// <remarks/>
    GC,

    /// <remarks/>
    GD,

    /// <remarks/>
    GE,

    /// <remarks/>
    GF,

    /// <remarks/>
    GH,

    /// <remarks/>
    GI,

    /// <remarks/>
    GJ,

    /// <remarks/>
    GK,

    /// <remarks/>
    GL,

    /// <remarks/>
    GM,

    /// <remarks/>
    GN,

    /// <remarks/>
    GO,

    /// <remarks/>
    GP,

    /// <remarks/>
    GQ,

    /// <remarks/>
    GR,

    /// <remarks/>
    GS,

    /// <remarks/>
    GT,

    /// <remarks/>
    GU,

    /// <remarks/>
    GV,

    /// <remarks/>
    GW,

    /// <remarks/>
    GX,

    /// <remarks/>
    GY,

    /// <remarks/>
    GZ,

    /// <remarks/>
    HA,

    /// <remarks/>
    HB,

    /// <remarks/>
    HC,

    /// <remarks/>
    HD,

    /// <remarks/>
    HE,

    /// <remarks/>
    HF,

    /// <remarks/>
    HG,

    /// <remarks/>
    HH,

    /// <remarks/>
    HI,

    /// <remarks/>
    HJ,

    /// <remarks/>
    HK,

    /// <remarks/>
    HL,

    /// <remarks/>
    HM,

    /// <remarks/>
    HN,

    /// <remarks/>
    HO,

    /// <remarks/>
    HP,

    /// <remarks/>
    HQ,

    /// <remarks/>
    HR,

    /// <remarks/>
    HS,

    /// <remarks/>
    HT,

    /// <remarks/>
    HU,

    /// <remarks/>
    HV,

    /// <remarks/>
    HW,

    /// <remarks/>
    HX,

    /// <remarks/>
    HY,

    /// <remarks/>
    HZ,

    /// <remarks/>
    I1,

    /// <remarks/>
    I2,

    /// <remarks/>
    IB,

    /// <remarks/>
    IC,

    /// <remarks/>
    ID,

    /// <remarks/>
    IE,

    /// <remarks/>
    IF,

    /// <remarks/>
    IG,

    /// <remarks/>
    IH,

    /// <remarks/>
    II,

    /// <remarks/>
    IJ,

    /// <remarks/>
    IL,

    /// <remarks/>
    IM,

    /// <remarks/>
    IN,

    /// <remarks/>
    IO,

    /// <remarks/>
    IP,

    /// <remarks/>
    IQ,

    /// <remarks/>
    IR,

    /// <remarks/>
    IS,

    /// <remarks/>
    IT,

    /// <remarks/>
    IU,

    /// <remarks/>
    IV,

    /// <remarks/>
    IW,

    /// <remarks/>
    IX,

    /// <remarks/>
    IY,

    /// <remarks/>
    IZ,

    /// <remarks/>
    JA,

    /// <remarks/>
    JB,

    /// <remarks/>
    JC,

    /// <remarks/>
    JD,

    /// <remarks/>
    JE,

    /// <remarks/>
    JF,

    /// <remarks/>
    JG,

    /// <remarks/>
    JH,

    /// <remarks/>
    LA,

    /// <remarks/>
    LB,

    /// <remarks/>
    LC,

    /// <remarks/>
    LD,

    /// <remarks/>
    LE,

    /// <remarks/>
    LF,

    /// <remarks/>
    LG,

    /// <remarks/>
    LH,

    /// <remarks/>
    LI,

    /// <remarks/>
    LJ,

    /// <remarks/>
    LK,

    /// <remarks/>
    LL,

    /// <remarks/>
    LM,

    /// <remarks/>
    LN,

    /// <remarks/>
    LO,

    /// <remarks/>
    LP,

    /// <remarks/>
    LQ,

    /// <remarks/>
    LR,

    /// <remarks/>
    LS,

    /// <remarks/>
    LT,

    /// <remarks/>
    LU,

    /// <remarks/>
    LV,

    /// <remarks/>
    MA,

    /// <remarks/>
    MAD,

    /// <remarks/>
    MDR,

    /// <remarks/>
    MF,

    /// <remarks/>
    MG,

    /// <remarks/>
    MI,

    /// <remarks/>
    MP,

    /// <remarks/>
    MR,

    /// <remarks/>
    MS,

    /// <remarks/>
    MT,

    /// <remarks/>
    N2,

    /// <remarks/>
    NI,

    /// <remarks/>
    OA,

    /// <remarks/>
    OB,

    /// <remarks/>
    OC,

    /// <remarks/>
    OD,

    /// <remarks/>
    OE,

    /// <remarks/>
    OF,

    /// <remarks/>
    OG,

    /// <remarks/>
    OH,

    /// <remarks/>
    OI,

    /// <remarks/>
    OJ,

    /// <remarks/>
    OK,

    /// <remarks/>
    OL,

    /// <remarks/>
    OM,

    /// <remarks/>
    ON,

    /// <remarks/>
    OO,

    /// <remarks/>
    OP,

    /// <remarks/>
    OQ,

    /// <remarks/>
    OR,

    /// <remarks/>
    OS,

    /// <remarks/>
    OT,

    /// <remarks/>
    OU,

    /// <remarks/>
    OV,

    /// <remarks/>
    OW,

    /// <remarks/>
    OX,

    /// <remarks/>
    OY,

    /// <remarks/>
    OZ,

    /// <remarks/>
    P1,

    /// <remarks/>
    P2,

    /// <remarks/>
    P3,

    /// <remarks/>
    P4,

    /// <remarks/>
    PA,

    /// <remarks/>
    PB,

    /// <remarks/>
    PC,

    /// <remarks/>
    PD,

    /// <remarks/>
    PE,

    /// <remarks/>
    PF,

    /// <remarks/>
    PG,

    /// <remarks/>
    PH,

    /// <remarks/>
    PI,

    /// <remarks/>
    PJ,

    /// <remarks/>
    PK,

    /// <remarks/>
    PM,

    /// <remarks/>
    PN,

    /// <remarks/>
    PO,

    /// <remarks/>
    PQ,

    /// <remarks/>
    PR,

    /// <remarks/>
    PS,

    /// <remarks/>
    PT,

    /// <remarks/>
    PW,

    /// <remarks/>
    PX,

    /// <remarks/>
    PY,

    /// <remarks/>
    PZ,

    /// <remarks/>
    RA,

    /// <remarks/>
    RB,

    /// <remarks/>
    RE,

    /// <remarks/>
    RF,

    /// <remarks/>
    RH,

    /// <remarks/>
    RI,

    /// <remarks/>
    RL,

    /// <remarks/>
    RM,

    /// <remarks/>
    RP,

    /// <remarks/>
    RS,

    /// <remarks/>
    RV,

    /// <remarks/>
    RW,

    /// <remarks/>
    SB,

    /// <remarks/>
    SE,

    /// <remarks/>
    SF,

    /// <remarks/>
    SG,

    /// <remarks/>
    SI,

    /// <remarks/>
    SN,

    /// <remarks/>
    SO,

    /// <remarks/>
    SR,

    /// <remarks/>
    SS,

    /// <remarks/>
    ST,

    /// <remarks/>
    SU,

    /// <remarks/>
    SX,

    /// <remarks/>
    SY,

    /// <remarks/>
    SZ,

    /// <remarks/>
    TA,

    /// <remarks/>
    TB,

    /// <remarks/>
    TC,

    /// <remarks/>
    TCP,

    /// <remarks/>
    TCR,

    /// <remarks/>
    TD,

    /// <remarks/>
    TE,

    /// <remarks/>
    TF,

    /// <remarks/>
    TG,

    /// <remarks/>
    TH,

    /// <remarks/>
    TI,

    /// <remarks/>
    TJ,

    /// <remarks/>
    TK,

    /// <remarks/>
    TL,

    /// <remarks/>
    TM,

    /// <remarks/>
    TN,

    /// <remarks/>
    TO,

    /// <remarks/>
    TP,

    /// <remarks/>
    TQ,

    /// <remarks/>
    TR,

    /// <remarks/>
    TS,

    /// <remarks/>
    TT,

    /// <remarks/>
    TU,

    /// <remarks/>
    TV,

    /// <remarks/>
    TW,

    /// <remarks/>
    TX,

    /// <remarks/>
    TY,

    /// <remarks/>
    TZ,

    /// <remarks/>
    UA,

    /// <remarks/>
    UB,

    /// <remarks/>
    UC,

    /// <remarks/>
    UD,

    /// <remarks/>
    UE,

    /// <remarks/>
    UF,

    /// <remarks/>
    UG,

    /// <remarks/>
    UH,

    /// <remarks/>
    UHP,

    /// <remarks/>
    UI,

    /// <remarks/>
    UJ,

    /// <remarks/>
    UK,

    /// <remarks/>
    UL,

    /// <remarks/>
    UM,

    /// <remarks/>
    UN,

    /// <remarks/>
    UO,

    /// <remarks/>
    UP,

    /// <remarks/>
    UQ,

    /// <remarks/>
    UR,

    /// <remarks/>
    US,

    /// <remarks/>
    UT,

    /// <remarks/>
    UU,

    /// <remarks/>
    UV,

    /// <remarks/>
    UW,

    /// <remarks/>
    UX,

    /// <remarks/>
    UY,

    /// <remarks/>
    UZ,

    /// <remarks/>
    VA,

    /// <remarks/>
    VB,

    /// <remarks/>
    VC,

    /// <remarks/>
    VE,

    /// <remarks/>
    VF,

    /// <remarks/>
    VG,

    /// <remarks/>
    VH,

    /// <remarks/>
    VI,

    /// <remarks/>
    VJ,

    /// <remarks/>
    VK,

    /// <remarks/>
    VL,

    /// <remarks/>
    VM,

    /// <remarks/>
    VN,

    /// <remarks/>
    VO,

    /// <remarks/>
    VP,

    /// <remarks/>
    VQ,

    /// <remarks/>
    VR,

    /// <remarks/>
    VS,

    /// <remarks/>
    VT,

    /// <remarks/>
    VU,

    /// <remarks/>
    VV,

    /// <remarks/>
    VW,

    /// <remarks/>
    VX,

    /// <remarks/>
    VY,

    /// <remarks/>
    VZ,

    /// <remarks/>
    WA,

    /// <remarks/>
    WB,

    /// <remarks/>
    WC,

    /// <remarks/>
    WD,

    /// <remarks/>
    WE,

    /// <remarks/>
    WF,

    /// <remarks/>
    WG,

    /// <remarks/>
    WH,

    /// <remarks/>
    WI,

    /// <remarks/>
    WJ,

    /// <remarks/>
    WK,

    /// <remarks/>
    WL,

    /// <remarks/>
    WM,

    /// <remarks/>
    WN,

    /// <remarks/>
    WO,

    /// <remarks/>
    WP,

    /// <remarks/>
    WPA,

    /// <remarks/>
    WQ,

    /// <remarks/>
    WR,

    /// <remarks/>
    WS,

    /// <remarks/>
    WT,

    /// <remarks/>
    WU,

    /// <remarks/>
    WV,

    /// <remarks/>
    WW,

    /// <remarks/>
    WX,

    /// <remarks/>
    WY,

    /// <remarks/>
    WZ,

    /// <remarks/>
    ZZZ,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:TransportModeCode:2")]
[System.Xml.Serialization.XmlRootAttribute("TransportModeCode", Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:TransportModeCode:2", IsNullable = false)]
public enum TransportModeCodeContentType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("0")]
    Item0,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1")]
    Item1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2")]
    Item2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3")]
    Item3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4")]
    Item4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5")]
    Item5,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6")]
    Item6,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("7")]
    Item7,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("8")]
    Item8,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("9")]
    Item9,
}
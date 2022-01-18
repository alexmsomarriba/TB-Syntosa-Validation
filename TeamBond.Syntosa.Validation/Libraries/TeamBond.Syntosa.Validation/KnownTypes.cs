namespace TeamBond.Syntosa.Validation
{
    /// <summary>
    /// Represents the unique identifiers for TeamBond Modules
    /// </summary>
    public struct Module
    {
        /// <summary>
        /// The TeamBond Module constant for the TeamBond module
        /// </summary>
        public const string TeamBond = "E63F21D6-0143-4B2F-817D-C47672E1E9B2";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond domains.
    /// </summary>
    public struct Domain
    {
        /// <summary>
        /// The TeamBond domain constant for TeamBond Domain
        /// </summary>
        public const string TeamBond = "D6C4959D-9406-4F59-9CFB-0C216504BD2A";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond type units
    /// </summary>
    public struct TypeUnit
    {
        /// <summary>
        /// The TeamBond type unit constant for String.
        /// </summary>
        public const string String = "B1EF4918-AAD5-403D-B2B9-F7620746A5B3";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond type functions.
    /// </summary>
    public struct TypeFunction
    {
        /// <summary>
        /// The TeamBond type function constant for Edges.
        /// </summary>
        public const string Edges = "91411741-9D1D-4597-A39F-FBF2E6D87A23";

        /// <summary>
        /// The TeamBond type function constant for Edge Labels.
        /// </summary>
        public const string EdgeLabels = "8EF61291-E316-4B36-B74A-30A9C488D8D0";

        /// <summary>
        /// The TeamBond type function constant for Edge properties.
        /// </summary>
        public const string EdgeProperties = "40B2A56A-0D11-4774-8855-CF558AAAD0CE";

        /// <summary>
        /// The TeamBond type function constant for Global Properties.
        /// </summary>
        public const string GlobalProperties = "4A81B607-B1ED-40F4-919A-8EFE276FC487";

        /// <summary>
        /// The TeamBond type function constant for Private Properties.
        /// </summary>
        public const string PrivateProperties = "701358CD-4BF3-4016-B34F-E950E0839BAC";

        /// <summary>
        /// The TeamBond type function constant for Record Statuses
        /// </summary>
        public const string RecordStatuses = "2A1902E4-1E11-4505-93A7-488EF5CD7606";

        /// <summary>
        /// The TeamBond type function constant for Workforce Management.
        /// </summary>
        public const string WorkforceManagement = "3BC7A7DE-8E41-4734-8B2A-728572AB45DC";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond global property types.
    /// </summary>
    public struct GlobalPropertyTypes
    {
        /// <summary>
        /// The employer name global property.
        /// </summary>
        public const string EmployerName = "78A89356-E313-4614-8970-45EB0919CDF4";

        /// <summary>
        /// The employer address global property.
        /// </summary>
        public const string EmployerAddress = "429FDC74-6D7A-4D14-A01A-FD58C01751BD";

        /// <summary>
        /// The employer contact name global property.
        /// </summary>
        public const string EmployerContactName = "E2EB9E35-4D9D-4CB3-9960-E9B2D1152709";

        /// <summary>
        /// The division acronym global property.
        /// </summary>
        public const string DivisionAcronym = "B8AB0B30-DD6B-464F-B9D6-61624D4134F4";

        /// <summary>
        /// The division lead name global property.
        /// </summary>
        public const string DivisionLeadName = "C90AEB37-468E-4B86-AA78-99D79456130D";

        /// <summary>
        /// The division lead contact email global property.
        /// </summary>
        public const string DivisionLeadContactEmail = "EA474C84-07D3-45DA-B9B1-7794A2392D03";

        /// <summary>
        /// The team acronym global property.
        /// </summary>
        public const string TeamAcronym = "52E72F04-4D04-4E1C-A332-2D9A5F91BC01";

        /// <summary>
        /// The team lead name global property.
        /// </summary>
        public const string TeamLeadName = "4315445A-FC40-4196-B7AD-7EA2B670C777";

        /// <summary>
        /// The division lead contact email global property.
        /// </summary>
        public const string TeamLeadContactEmail = "891F60E6-DEFB-4F72-86AE-08B723324E8A";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond edge label types.
    /// </summary>
    public struct EdgeLabelTypes
    {
        /// <summary>
        /// The employer edge label type.
        /// </summary>
        public const string Employer = "61E61847-4CB6-42D8-89B9-9C22878B2882";

        /// <summary>
        /// The division edge label type.
        /// </summary>
        public const string Division = "33C92673-FBE4-48BD-A0E1-8F38029F2ED2";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond edge types.
    /// </summary>
    public struct EdgeTypes
    {
        /// <summary>
        /// The TeamBond edge type constant for the Assigned To edge type.
        /// </summary>
        public const string AssignedTo = "A9D48CF6-FD05-4047-BB21-FF327B8E3D1B";

        /// <summary>
        /// The TeamBond edge type constant for the Belongs To edge type.
        /// </summary>
        public const string BelongsTo = "07BC1A0C-E659-4EC2-9110-9642461B3DF2";

        /// <summary>
        /// The TeamBond edge type constant for the Connected To edge type.
        /// </summary>
        public const string ConnectedTo = "DB3A0146-BFF6-494F-BB74-0D68DFA26D73";

        /// <summary>
        /// The TeamBond edge type constant for the Depends On edge type.
        /// </summary>
        public const string DependsOn = "9973E243-2C0D-47CA-A01C-FEEBBA47ABD2";

        /// <summary>
        /// The TeamBond edge type constant for the for the Enabled By edge type.
        /// </summary>
        public const string EnabledBy = "C0E6C076-57E4-4A00-9DEA-7E9628251497";

        /// <summary>
        /// The TeamBond edge type constant for the for the Member Of edge type.
        /// </summary>
        public const string MemberOf = "58F70FEA-1970-41B3-A790-06B215840E6B";
    }

    /// <summary>
    ///  Represents the unique identifiers for the TeamBond record statuses.
    /// </summary>
    public struct RecordStatuses
    {
        /// <summary>
        /// The TeamBond record status constant for the for the Active record status.
        /// </summary>
        public const string Active = "4586C01B-BF70-4924-9801-64F81B81F8D5";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond workforce management types.
    /// </summary>
    public struct WorkforceManagement
    {
        /// <summary>
        /// The TeamBond workforce management type constant for the for the broker type.
        /// </summary>
        public const string Broker = "FA6BFCDE-06A7-4EA1-8EEE-2EE5CC67D9A4";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the candidate type.
        /// </summary>
        public const string Candidate = "D31FAA1E-E515-490F-838E-E2FA5CC18E0C";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the division type.
        /// </summary>
        public const string Division = "21A66A6E-DAF9-4F70-8055-AAFE5F27EB91";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the employee type.
        /// </summary>
        public const string Employee = "FB9D70BE-E721-451D-82E7-8529400ECEA1";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the employer type.
        /// </summary>
        public const string Employer = "095CF019-70F0-42E8-8D89-316A9DE0FCAC";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the job type.
        /// </summary>
        public const string Job = "8C81D001-5BA9-4A16-8128-628425F2D3F2";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the job posting type.
        /// </summary>
        public const string JobPosting = "C7B4E18E-2C5D-4F64-A3DF-08E39FEAD839";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the outside candidate type.
        /// </summary>
        public const string OutsideCandidate = "6479D64D-B6A8-43CE-BB80-CFBAC1FFFC07";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the resume type.
        /// </summary>
        public const string Resume = "339C15B8-88C9-4913-9243-5B67ED6B25B4";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the worker type.
        /// </summary>
        public const string Worker = "668A9537-705C-42DE-AFF7-1E6430DD395C";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the portfolio type.
        /// </summary>
        public const string Portfolio = "38EA0B8B-6410-4356-B7CD-EDE99667D334";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the department type.
        /// </summary>
        public const string Department = "868B2485-4BE3-421A-8EA5-05A4BB26B300";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the team type.
        /// </summary>
        public const string Team = "C0ABB9CE-D688-4CCF-90E8-35D0211829B0";
    }
}
namespace TeamBond.Syntosa.Validation
{
    using System;

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
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond edge types.
    /// </summary>
    public struct EdgeTypes
    {
        /// <summary>
        /// The TeamBond edge type constant for the Assigned To edge type.
        /// </summary>
        public const string AssignedTo = "a9d48cf6-fd05-4047-bb21-ff327b8e3d1b";

        /// <summary>
        /// The TeamBond edge type constant for the Belongs To edge type.
        /// </summary>
        public const string BelongsTo = "07bc1a0c-e659-4ec2-9110-9642461b3df2";

        /// <summary>
        /// The TeamBond edge type constant for the Connected To edge type.
        /// </summary>
        public const string ConnectedTo = "db3a0146-bff6-494f-bb74-0d68dfa26d73";

        /// <summary>
        /// The TeamBond edge type constant for the Depends On edge type.
        /// </summary>
        public const string DependsOn = "9973e243-2c0d-47ca-a01c-feebba47abd2";

        /// <summary>
        /// The TeamBond edge type constant for the for the Enabled By edge type.
        /// </summary>
        public const string EnabledBy = "c0e6c076-57e4-4a00-9dea-7e9628251497";

        /// <summary>
        /// The TeamBond edge type constant for the for the Member Of edge type.
        /// </summary>
        public const string MemberOf = "58f70fea-1970-41b3-a790-06b215840e6b";
    }

    /// <summary>
    ///  Represents the unique identifiers for the TeamBond record statuses.
    /// </summary>
    public struct RecordStatuses
    {
        /// <summary>
        /// The TeamBond record status constant for the for the Active record status.
        /// </summary>
        public const string Active = "4586c01b-bf70-4924-9801-64f81b81f8d5";
    }

    /// <summary>
    /// Represents the unique identifiers for the TeamBond workforce management types.
    /// </summary>
    public struct WorkforceManagement
    {
        /// <summary>
        /// The TeamBond workforce management type constant for the for the broker type.
        /// </summary>
        public const string Broker = "fa6bfcde-06a7-4ea1-8eee-2ee5cc67d9a4";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the candidate type.
        /// </summary>
        public const string Candidate = "d31faa1e-e515-490f-838e-e2fa5cc18e0c";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the division type.
        /// </summary>
        public const string Division = "21a66a6e-daf9-4f70-8055-aafe5f27eb91";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the employee type.
        /// </summary>
        public const string Employee = "fb9d70be-e721-451d-82e7-8529400ecea1";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the employer type.
        /// </summary>
        public const string Employer = "095cf019-70f0-42e8-8d89-316a9de0fcac";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the job type.
        /// </summary>
        public const string Job = "8c81d001-5ba9-4a16-8128-628425f2d3f2";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the job posting type.
        /// </summary>
        public const string JobPosting = "c7b4e18e-2c5d-4f64-a3df-08e39fead839";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the outside candidate type.
        /// </summary>
        public const string OutsideCandidate = "6479d64d-b6a8-43ce-bb80-cfbac1fffc07";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the resume type.
        /// </summary>
        public const string Resume = "339c15b8-88c9-4913-9243-5b67ed6b25b4";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the worker type.
        /// </summary>
        public const string Worker = "668a9537-705c-42de-aff7-1e6430dd395c";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the portfolio type.
        /// </summary>
        public const string Portfolio = "38ea0b8b-6410-4356-b7cd-ede99667d334";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the department type.
        /// </summary>
        public const string Department = "868b2485-4be3-421a-8ea5-05a4bb26b300";

        /// <summary>
        /// The TeamBond workforce management type constant for the for the team type.
        /// </summary>
        public const string Team = "c0abb9ce-d688-4ccf-90e8-35d0211829b0";
    }
}
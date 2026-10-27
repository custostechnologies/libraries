using System;
using System.Xml.Serialization;

using Microsoft.Xrm.Sdk;

using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Attributes;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class EmploymentApplicationDto
    : NotifiableDto<EmploymentApplicationDto>, IQueryDto
    {
        public override string TargetAppDisplayName => "manageHR";

        public override string SourceEntityLogicalName =>
            EntityLogicalNames.EmploymentApplication;

        [DataverseLink(
            srcName: SynStaffMemberFields.ForcePlugin,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public override DateTime? ForcePlugin { get; set; }

        [IgnoreOnCreate]
        //[IgnoreOnUpdate]
        [DataverseLink(
            srcName: EmploymentApplicationFields.EmploymentApplicationId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null)]
        public Guid EmploymentApplicationId { get; set; }

        public override Guid EntityId
        {
            get => EmploymentApplicationId;
            set => EmploymentApplicationId = value;
        }

        [ApplicationField("pgrowth_webformoptionvalue")]
        [DataverseLink(
            srcName: "pgrowth_webformoptionvalue",
            srcType: typeof(int?),
            trgName: null,
            trgType: null)]
        public int WebFormOptionValue { get; set; }

        // PERSONAL DETAILS

        [ApplicationField(EmploymentApplicationFields.Salutation)]
        [DataverseLink(
    srcName: EmploymentApplicationFields.Salutation,
    srcType: typeof(OptionSetValue),
    trgName: null,
    trgType: null)]
        public Salutation Salutation { get; set; }

        [ApplicationField(EmploymentApplicationFields.GivenNames)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.GivenNames,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? GivenNames { get; set; }

        [ApplicationField(EmploymentApplicationFields.PreferredName)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.PreferredName,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? PreferredName { get; set; }

        [ApplicationField(EmploymentApplicationFields.LastName)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.LastName,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? LastName { get; set; }

        [ApplicationField(EmploymentApplicationFields.DateOfBirth)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.DateOfBirth,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? DateOfBirth { get; set; }

        [ApplicationField(EmploymentApplicationFields.Name)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.Name,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Name { get; set; }

        // CONTACT DETAILS
        [ApplicationField(EmploymentApplicationFields.PersonalEmailAddress)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.PersonalEmailAddress,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? PersonalEmailAddress { get; set; }

        [ApplicationField(EmploymentApplicationFields.MobileNumber)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.MobileNumber,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? MobileNumber { get; set; }

        [ApplicationField(EmploymentApplicationFields.HomePhoneNumber)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.HomePhoneNumber,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? HomePhoneNumber { get; set; }

        [ApplicationField(EmploymentApplicationFields.WorkPhoneNumber)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.WorkPhoneNumber,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? WorkPhoneNumber { get; set; }
        // RESIDENTIAL ADDRESS
        [ApplicationField(EmploymentApplicationFields.AddressLine1)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.AddressLine1,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? AddressLine1 { get; set; }

        [ApplicationField(EmploymentApplicationFields.Suburb)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.Suburb,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.State)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.State,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public StateTerritory? State { get; set; }

        [ApplicationField(EmploymentApplicationFields.Postcode)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.Postcode,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Postcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Country)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.Country,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Country { get; set; }
        // CHURCH DETAILS
        [ApplicationField(EmploymentApplicationFields.ChurchAttending)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.ChurchAttending,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? ChurchAttending { get; set; }

        [ApplicationField(EmploymentApplicationFields.Denomination)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.Denomination,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public Denomination? Denomination { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchAddressLine1)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ChurchAddressLine1,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ChurchAddressLine1 { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchSuburb)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ChurchSuburb,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ChurchSuburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchState)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ChurchState,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public StateTerritory? ChurchState { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchPostcode)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ChurchPostcode,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ChurchPostcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchCountry)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ChurchCountry,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ChurchCountry { get; set; }
        // APPLICATION METADATA
        [ApplicationField(EmploymentApplicationFields.ApplicationType)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.ApplicationType,
        srcType: typeof(OptionSetValue),
        trgName: null,
        trgType: null)]
        public ApplicationType? ApplicationType { get; set; }

        [ApplicationField(EmploymentApplicationFields.CMEAType)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CMEAType,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public CMEAType? CMEAType { get; set; }

        [ApplicationField(EmploymentApplicationFields.SubmissionDate)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.SubmissionDate,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? SubmissionDate { get; set; }
        // LOOKUPS
        [ApplicationField(EmploymentApplicationFields.CoreJobPosition)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.CoreJobPosition,
        srcType: typeof(EntityReference),
        trgName: null,
        trgType: null)]
        public EntityReference? CoreJobPosition { get; set; }

        [XmlIgnore]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ParentJobOpportunity,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public EntityReference? ParentJobOpportunity { get; set; }
        // QUALIFICATIONS
        [ApplicationField(EmploymentApplicationFields.EducationalQualifications)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.EducationalQualifications,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? EducationalQualifications { get; set; }

        [ApplicationField(EmploymentApplicationFields.GraduationYear)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.GraduationYear,
            srcType: typeof(int?),
            trgName: null,
            trgType: null)]
        public int GraduationYear { get; set; }

        [ApplicationField(EmploymentApplicationFields.NESAAccreditationNumber)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.NESAAccreditationNumber,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? NESAAccreditationNumber { get; set; }

        [ApplicationField(EmploymentApplicationFields.NESAAccreditationStatus)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.NESAAccreditationStatus,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public NESAAccreditation? NESAAccreditationStatus { get; set; }
        // QUALIFICATIONS
        [ApplicationField(EmploymentApplicationFields.MajorTeachingSubjectArea)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.MajorTeachingSubjectArea,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? MajorTeachingSubjectArea { get; set; }

        [ApplicationField(EmploymentApplicationFields.MinorTeachingSubjectArea)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.MinorTeachingSubjectArea,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? MinorTeachingSubjectArea { get; set; }

        [ApplicationField(EmploymentApplicationFields.OtherQualifiedSubjects)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.OtherQualifiedSubjects,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? OtherQualifiedSubjects { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentTeachingLoad)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentTeachingLoad,
            srcType: typeof(decimal?),
            trgName: null,
            trgType: null)]
        public decimal CurrentTeachingLoad { get; set; }

        [ApplicationField(EmploymentApplicationFields.NESAAccreditationDateAchieved)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.NESAAccreditationDateAchieved,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? NESAAccreditationDateAchieved { get; set; }

        [ApplicationField(EmploymentApplicationFields.ISTAAAccreditationNumber)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ISTAAAccreditationNumber,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ISTAAAccreditationNumber { get; set; }

        [ApplicationField(EmploymentApplicationFields.ISTAAAccreditationDateAchieved)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ISTAAAccreditationDateAchieved,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? ISTAAAccreditationDateAchieved { get; set; }

        [ApplicationField(EmploymentApplicationFields.HasAccomplishedTeacher)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.HasAccomplishedTeacher,
            srcType: typeof(bool?),
            trgName: null,
            trgType: null)]
        public bool HasAccomplishedTeacher { get; set; }
        // FIRST AID/CPR
        [DataverseLink(
        srcName: EmploymentApplicationFields.CPRQualificationYN,
        srcType: typeof(bool?),
        trgName: null,
        trgType: null)]
        public bool CPRQualificationYN { get; set; }

        [ApplicationField(EmploymentApplicationFields.NameOfCPRQualification)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.NameOfCPRQualification,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? NameOfCPRQualification { get; set; }

        [ApplicationField(EmploymentApplicationFields.IssueDateOfCPRQualification)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.IssueDateOfCPRQualification,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? IssueDateOfCPRQualification { get; set; }

        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentFirstAidYN,
            srcType: typeof(bool?),
            trgName: null,
            trgType: null)]
        public bool CurrentFirstAidYN { get; set; }

        [ApplicationField(EmploymentApplicationFields.NameOfFirstAidQualification)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.NameOfFirstAidQualification,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? NameOfFirstAidQualification { get; set; }

        [ApplicationField(EmploymentApplicationFields.IssueDateOfFirstAidQualification)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.IssueDateOfFirstAidQualification,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? IssueDateOfFirstAidQualification { get; set; }
        // CURRENT EMPLOYMENT
        [ApplicationField(EmploymentApplicationFields.CurrentEmployerName)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.CurrentEmployerName,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? CurrentEmployerName { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentManagerPrincipalName)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentManagerPrincipalName,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentManagerPrincipalName { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentPositions)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentPositions,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmploymentPositions { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentResponsibilities)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentResponsibilities,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmploymentResponsibilities { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentStartDate)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentStartDate,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? CurrentEmploymentStartDate { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentEndDate)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentEndDate,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? CurrentEmploymentEndDate { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentReasonForLeaving)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentReasonForLeaving,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmploymentReasonForLeaving { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentBaseSalary)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentBaseSalary,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmploymentBaseSalary { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentCurrentAllowances)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentCurrentAllowances,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmploymentCurrentAllowances { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentPastVariations)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmploymentPastVariations,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmploymentPastVariations { get; set; }
        [ApplicationField(EmploymentApplicationFields.CurrentEmployerAddressLine1)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.CurrentEmployerAddressLine1,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? CurrentEmployerAddressLine1 { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmployerSuburb)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmployerSuburb,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmployerSuburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmployerState)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmployerState,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public StateTerritory? CurrentEmployerState { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmployerPostcode)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmployerPostcode,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmployerPostcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmployerCountry)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.CurrentEmployerCountry,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CurrentEmployerCountry { get; set; }
        // MINISTER
        [ApplicationField(EmploymentApplicationFields.NameMinisterOfReligion)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.NameMinisterOfReligion,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? NameMinisterOfReligion { get; set; }

        [ApplicationField(EmploymentApplicationFields.PhoneNumberMinisterOfReligion)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.PhoneNumberMinisterOfReligion,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? PhoneNumberMinisterOfReligion { get; set; }

        [ApplicationField(EmploymentApplicationFields.EmailAddressMinisterOfReligion)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.EmailAddressMinisterOfReligion,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? EmailAddressMinisterOfReligion { get; set; }
        // REFEREEE 1
        [ApplicationField(EmploymentApplicationFields.Referee1Name)]
        [DataverseLink(EmploymentApplicationFields.Referee1Name, typeof(string), null, null)]
        public string? Referee1Name { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1Occupation)]
        [DataverseLink(EmploymentApplicationFields.Referee1Occupation, typeof(string), null, null)]
        public string? Referee1Occupation { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1RelationshipToApplicant)]
        [DataverseLink(EmploymentApplicationFields.Referee1RelationshipToApplicant, typeof(string), null, null)]
        public string? Referee1RelationshipToApplicant { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1PhoneNumber)]
        [DataverseLink(EmploymentApplicationFields.Referee1PhoneNumber, typeof(string), null, null)]
        public string? Referee1PhoneNumber { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1AddressLine1)]
        [DataverseLink(EmploymentApplicationFields.Referee1AddressLine1, typeof(string), null, null)]
        public string? Referee1AddressLine1 { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1Suburb)]
        [DataverseLink(EmploymentApplicationFields.Referee1Suburb, typeof(string), null, null)]
        public string? Referee1Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1State)]
        [DataverseLink(EmploymentApplicationFields.Referee1State, typeof(OptionSetValue), null, null)]
        public StateTerritory? Referee1State { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1Postcode)]
        [DataverseLink(EmploymentApplicationFields.Referee1Postcode, typeof(string), null, null)]
        public string? Referee1Postcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1Country)]
        [DataverseLink(EmploymentApplicationFields.Referee1Country, typeof(string), null, null)]
        public string? Referee1Country { get; set; }
        // REFEREE 2
        [ApplicationField(EmploymentApplicationFields.Referee1Name)]
        [DataverseLink(EmploymentApplicationFields.Referee2Name, typeof(string), null, null)]
        public string? Referee2Name { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2Occupation)]
        [DataverseLink(EmploymentApplicationFields.Referee2Occupation, typeof(string), null, null)]
        public string? Referee2Occupation { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2RelationshipToApplicant)]
        [DataverseLink(EmploymentApplicationFields.Referee2RelationshipToApplicant, typeof(string), null, null)]
        public string? Referee2RelationshipToApplicant { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2PhoneNumber)]
        [DataverseLink(EmploymentApplicationFields.Referee2PhoneNumber, typeof(string), null, null)]
        public string? Referee2PhoneNumber { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2AddressLine1)]
        [DataverseLink(EmploymentApplicationFields.Referee2AddressLine1, typeof(string), null, null)]
        public string? Referee2AddressLine1 { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2Suburb)]
        [DataverseLink(EmploymentApplicationFields.Referee2Suburb, typeof(string), null, null)]
        public string? Referee2Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2State)]
        [DataverseLink(EmploymentApplicationFields.Referee2State, typeof(OptionSetValue), null, null)]
        public StateTerritory? Referee2State { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2Postcode)]
        [DataverseLink(EmploymentApplicationFields.Referee2Postcode, typeof(string), null, null)]
        public string? Referee2Postcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2Country)]
        [DataverseLink(EmploymentApplicationFields.Referee2Country, typeof(string), null, null)]
        public string? Referee2Country { get; set; }
        // REFEREE 3
        [ApplicationField(EmploymentApplicationFields.Referee2Name)]
        [DataverseLink(EmploymentApplicationFields.Referee3Name, typeof(string), null, null)]
        public string? Referee3Name { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3Occupation)]
        [DataverseLink(EmploymentApplicationFields.Referee3Occupation, typeof(string), null, null)]
        public string? Referee3Occupation { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3RelationshipToApplicant)]
        [DataverseLink(EmploymentApplicationFields.Referee3RelationshipToApplicant, typeof(string), null, null)]
        public string? Referee3RelationshipToApplicant { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3PhoneNumber)]
        [DataverseLink(EmploymentApplicationFields.Referee3PhoneNumber, typeof(string), null, null)]
        public string? Referee3PhoneNumber { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3AddressLine1)]
        [DataverseLink(EmploymentApplicationFields.Referee3AddressLine1, typeof(string), null, null)]
        public string? Referee3AddressLine1 { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3Suburb)]
        [DataverseLink(EmploymentApplicationFields.Referee3Suburb, typeof(string), null, null)]
        public string? Referee3Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3State)]
        [DataverseLink(EmploymentApplicationFields.Referee3State, typeof(OptionSetValue), null, null)]
        public StateTerritory? Referee3State { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3Postcode)]
        [DataverseLink(EmploymentApplicationFields.Referee3Postcode, typeof(string), null, null)]
        public string? Referee3Postcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3Country)]
        [DataverseLink(EmploymentApplicationFields.Referee3Country, typeof(string), null, null)]
        public string? Referee3Country { get; set; }
        // RESIDENCY
        [ApplicationField(EmploymentApplicationFields.IsPermanentResident)]
        [DataverseLink(
    srcName: EmploymentApplicationFields.IsPermanentResident,
    srcType: typeof(bool?),
    trgName: null,
    trgType: null)]
        public bool IsPermanentResident { get; set; }

        [ApplicationField(EmploymentApplicationFields.HasWorkVisa)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.HasWorkVisa,
            srcType: typeof(bool?),
            trgName: null,
            trgType: null)]
        public bool HasWorkVisa { get; set; }

        [ApplicationField(EmploymentApplicationFields.VisaWorkApprovalDetails)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.VisaWorkApprovalDetails,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? VisaWorkApprovalDetails { get; set; }
        // MEMBERSHIPS
        //     [ApplicationField(EmploymentApplicationFields.ProfessionalMembershipDetails)]
        [DataverseLink(
            srcName: EmploymentApplicationFields.ProfessionalMembershipDetails,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ProfessionalMembershipDetails { get; set; }

        // WWCC
        [ApplicationField(EmploymentApplicationFields.WWCCNumber)]
        [DataverseLink(
        srcName: EmploymentApplicationFields.WWCCNumber,
        srcType: typeof(string),
        trgName: null,
        trgType: null)]
        public string? WWCCNumber { get; set; }
    }

}
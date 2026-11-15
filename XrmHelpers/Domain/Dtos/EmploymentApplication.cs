using System;
using System.Xml.Serialization;
using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Domain.Attributes;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class EmploymentApplication
    {
        [ApplicationField("pgrowth_webformoptionvalue")]
        public int WebFormOptionValue { get; set; }

        [ApplicationField(EmploymentApplicationFields.AddressLine1)]
        public string? AddressLine1 { get; set; }

        //[ApplicationField(EmploymentApplicationFields.ApplicantDeclarationId)]
        //public string? ApplicantDeclarationId { get; set; }

        //[ApplicationField(EmploymentApplicationFields.ApplicationFolderPath)]
        //public string? ApplicationFolderPath { get; set; }

        //[ApplicationField(EmploymentApplicationFields.ApplicationKey)]
        //public string? ApplicationKey { get; set; }

        [ApplicationField(EmploymentApplicationFields.ApplicationType)]
        public ApplicationType? ApplicationType { get; set; }

        //[ApplicationField(EmploymentApplicationFields.CertifierOfCPRQualification)]
        //public string? CertifierOfCPRQualification { get; set; }

        //[ApplicationField(EmploymentApplicationFields.CertifierOfFirstAidQualification)]
        //public string? CertifierOfFirstAidQualification { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchAttending)]
        public string? ChurchAttending { get; set; }

        //[ApplicationField(EmploymentApplicationFields.CitizenshipCountry)]
        //public string? CitizenshipCountry { get; set; }

        [ApplicationField(EmploymentApplicationFields.CMEAType)]
        public CMEAType? CMEAType { get; set; }

        //[ApplicationField(EmploymentApplicationFields.CommunityAndChurchInvolvement)]
        //public string? CommunityAndChurchInvolvement { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchAddressLine1)]
        public string? ChurchAddressLine1 { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchSuburb)]
        public string? ChurchSuburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchState)]
        public StateTerritory? ChurchState { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchCountry)]
        public string? ChurchCountry { get; set; }

        [ApplicationField(EmploymentApplicationFields.ChurchPostcode)]
        public string? ChurchPostcode { get; set; }

        //[ApplicationField(EmploymentApplicationFields.CoreJobPosition)]
        public EntityReference? CoreJobPosition { get; set; }

        //[ApplicationField(EmploymentApplicationFields.CountryOfCitizenship)]
        //public string? CountryOfCitizenship { get; set; }

        //[ApplicationField(EmploymentApplicationFields.CountryOfCitizenshipText)]
        //public string? CountryOfCitizenshipText { get; set; }

        public bool CPRQualificationYN { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerAddressLine1)]
        public string? CurrentEmployerAddressLine1 { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerName)]
        public string? CurrentEmployerName { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerPostcode)]
        public string? CurrentEmployerPostcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmployerSuburb)]
        public string? CurrentEmployerSuburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.CurrentEmployerCountry)]
        public string? CurrentEmployerCountry { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerState)]
        public StateTerritory? CurrentEmployerState { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentBaseSalary)]
        public string? CurrentEmploymentBaseSalary { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentCurrentAllowances)]
        public string? CurrentEmploymentCurrentAllowances { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentEndDate)]
        public DateTime? CurrentEmploymentEndDate { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentPastVariations)]
        public string? CurrentEmploymentPastVariations { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentPositions)]
        public string? CurrentEmploymentPositions { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentReasonForLeaving)]
        public string? CurrentEmploymentReasonForLeaving { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentResponsibilities)]
        public string? CurrentEmploymentResponsibilities { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentStartDate)]
        public DateTime? CurrentEmploymentStartDate { get; set; }

        public bool CurrentFirstAidYN { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentManagerPrincipalName)]
        public string? CurrentManagerPrincipalName { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentTeachingLoad)]
        public decimal CurrentTeachingLoad { get; set; }


        [ApplicationField(EmploymentApplicationFields.DateOfBirth)]
        public DateTime? DateOfBirth { get; set; }


        [ApplicationField(EmploymentApplicationFields.Denomination)]
        public Denomination? Denomination { get; set; }

        //[ApplicationField(EmploymentApplicationFields.EducationalPhilosophy)]
        //public string? EducationalPhilosophy { get; set; }

        [ApplicationField(EmploymentApplicationFields.EducationalQualifications)]
        public string? EducationalQualifications { get; set; }


        [ApplicationField(EmploymentApplicationFields.EmailAddressMinisterOfReligion)]
        public string? EmailAddressMinisterOfReligion { get; set; }

        //[ApplicationField(EmploymentApplicationFields.ExtraCurricularInvolvement)]
        //public string? ExtraCurricularInvolvement { get; set; }

        [ApplicationField(EmploymentApplicationFields.GivenNames)]
        public string? GivenNames { get; set; }


        [ApplicationField(EmploymentApplicationFields.GraduationYear)]
        public int GraduationYear { get; set; }


        [ApplicationField(EmploymentApplicationFields.IsAlum)]
        public bool IsAlum { get; set; }


        //[ApplicationField(EmploymentApplicationFields.HealthConditionsDetails)]
        //public string? HealthConditionsDetails { get; set; }

        //[ApplicationField(EmploymentApplicationFields.HealthConditionsYN)]
        //public string? HealthConditionsYN { get; set; }


        [ApplicationField(EmploymentApplicationFields.HomePhoneNumber)]
        public string? HomePhoneNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.IssueDateOfCPRQualification)]
        public DateTime? IssueDateOfCPRQualification { get; set; }


        [ApplicationField(EmploymentApplicationFields.IssueDateOfFirstAidQualification)]
        public DateTime? IssueDateOfFirstAidQualification { get; set; }


        [ApplicationField(EmploymentApplicationFields.ISTAAAccreditationDateAchieved)]
        public DateTime? ISTAAAccreditationDateAchieved { get; set; }


        [ApplicationField(EmploymentApplicationFields.ISTAAAccreditationNumber)]
        public string? ISTAAAccreditationNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.HasAccomplishedTeacher)]
        public bool HasAccomplishedTeacher { get; set; }

        [ApplicationField(EmploymentApplicationFields.LastName)]
        public string? LastName { get; set; }


        [ApplicationField(EmploymentApplicationFields.MajorTeachingSubjectArea)]
        public string? MajorTeachingSubjectArea { get; set; }


        [ApplicationField(EmploymentApplicationFields.MinorTeachingSubjectArea)]
        public string? MinorTeachingSubjectArea { get; set; }


        [ApplicationField(EmploymentApplicationFields.MobileNumber)]
        public string? MobileNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.NameMinisterOfReligion)]
        public string? NameMinisterOfReligion { get; set; }


        [ApplicationField(EmploymentApplicationFields.NameOfCPRQualification)]
        public string? NameOfCPRQualification { get; set; }


        [ApplicationField(EmploymentApplicationFields.NameOfFirstAidQualification)]
        public string? NameOfFirstAidQualification { get; set; }


        [ApplicationField(EmploymentApplicationFields.NESAAccreditationDateAchieved)]
        public DateTime? NESAAccreditationDateAchieved { get; set; }


        [ApplicationField(EmploymentApplicationFields.NESAAccreditationNumber)]
        public string? NESAAccreditationNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.NESAAccreditationStatus)]
        public NESAAccreditation? NESAAccreditationStatus { get; set; }

        //[ApplicationField(EmploymentApplicationFields.OtherQualifications)]
        //public string? OtherQualifications { get; set; }

        [ApplicationField(EmploymentApplicationFields.OtherQualifiedSubjects)]
        public string? OtherQualifiedSubjects { get; set; }


        //[ApplicationField(EmploymentApplicationFields.PassportNumber)]
        //public string? PassportNumber { get; set; }

        //[ApplicationField(EmploymentApplicationFields.PastEmploymentDetails)]
        //public string? PastEmploymentDetails { get; set; }

        //[ApplicationField(EmploymentApplicationFields.PastoralCareInterestSkills)]
        //public string? PastoralCareInterestSkills { get; set; }


        [ApplicationField(EmploymentApplicationFields.PersonalEmailAddress)]
        public string? PersonalEmailAddress { get; set; }


        [ApplicationField(EmploymentApplicationFields.PhoneNumberMinisterOfReligion)]
        public string? PhoneNumberMinisterOfReligion { get; set; }


        [ApplicationField(EmploymentApplicationFields.Postcode)]
        public string? Postcode { get; set; }



        [ApplicationField(EmploymentApplicationFields.PreferredName)]
        public string? PreferredName { get; set; }


        //[ApplicationField(EmploymentApplicationFields.PreviousWorkersCompClaimsDetails)]
        //public string? PreviousWorkersCompClaimsDetails { get; set; }


        //[ApplicationField(EmploymentApplicationFields.PreviousWorkersCompClaimsYN)]
        //public string? PreviousWorkersCompClaimsYN { get; set; }


        //[ApplicationField(EmploymentApplicationFields.ProcessId)]
        //public string? ProcessId { get; set; }


        [ApplicationField(EmploymentApplicationFields.ProfessionalMembershipDetails)]
        public string? ProfessionalMembershipDetails { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1AddressLine1)]
        public string? Referee1AddressLine1 { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1Name)]
        public string? Referee1Name { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1Occupation)]
        public string? Referee1Occupation { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1PhoneNumber)]
        public string? Referee1PhoneNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1Postcode)]
        public string? Referee1Postcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1Suburb)]
        public string? Referee1Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1Country)]
        public string? Referee1Country { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee1RelationshipToApplicant)]
        public string? Referee1RelationshipToApplicant { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1State)]
        public StateTerritory? Referee1State { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2AddressLine1)]
        public string? Referee2AddressLine1 { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2Name)]
        public string? Referee2Name { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2Occupation)]
        public string? Referee2Occupation { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2PhoneNumber)]
        public string? Referee2PhoneNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2Postcode)]
        public string? Referee2Postcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2Suburb)]
        public string? Referee2Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2Country)]
        public string? Referee2Country { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee2RelationshipToApplicant)]
        public string? Referee2RelationshipToApplicant { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2State)]
        public StateTerritory? Referee2State { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3AddressLine1)]
        public string? Referee3AddressLine1 { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3Name)]
        public string? Referee3Name { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3Occupation)]
        public string? Referee3Occupation { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3PhoneNumber)]
        public string? Referee3PhoneNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3Postcode)]
        public string? Referee3Postcode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3Suburb)]
        public string? Referee3Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.Referee3Country)]
        public string? Referee3Country { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3RelationshipToApplicant)]
        public string? Referee3RelationshipToApplicant { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3State)]
        public StateTerritory? Referee3State { get; set; }


        [ApplicationField(EmploymentApplicationFields.Salutation)]
        public Salutation Salutation { get; set; }

        [ApplicationField(EmploymentApplicationFields.State)]
        public StateTerritory? State { get; set; }

        [ApplicationField(EmploymentApplicationFields.Suburb)]
        public string? Suburb { get; set; }

        [ApplicationField(EmploymentApplicationFields.Country)]
        public string? Country { get; set; }

        [ApplicationField(EmploymentApplicationFields.SubmissionDate)]
        public DateTime? SubmissionDate { get; set; }


        [ApplicationField(EmploymentApplicationFields.Name)]
        public string? Name { get; set; }


        [ApplicationField(EmploymentApplicationFields.VisaWorkApprovalDetails)]
        public string? VisaWorkApprovalDetails { get; set; }


        [ApplicationField(EmploymentApplicationFields.IsPermanentResident)]
        public bool IsPermanentResident { get; set; }


        [ApplicationField(EmploymentApplicationFields.HasWorkVisa)]
        public bool HasWorkVisa { get; set; }


        [ApplicationField(EmploymentApplicationFields.WorkPhoneNumber)]
        public string? WorkPhoneNumber { get; set; }


        //[ApplicationField(EmploymentApplicationFields.WWCCExpiryDate)]
        //public DateTime? WWCCExpiryDate { get; set; }


        [ApplicationField(EmploymentApplicationFields.WWCCNumber)]
        public string? WWCCNumber { get; set; }

        // settable entity references and Guid's
        public EntityReference? ParentJobOpportunity { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ApplicationFieldAttribute : Attribute
    {
        public string FieldName { get; }
        public ApplicationFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
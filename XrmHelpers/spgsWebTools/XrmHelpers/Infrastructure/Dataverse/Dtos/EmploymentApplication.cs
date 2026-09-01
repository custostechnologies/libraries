using System;
using System.Xml.Serialization;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    public class EmploymentApplication
    {
        [ApplicationField("pgrowth_positionappliedfor")]
        public int JobOpportunityCode { get; set; }

        [ApplicationField(EmploymentApplicationFields.Address)]
        public string? Address { get; set; }

        [ApplicationField(EmploymentApplicationFields.ApplicantDeclarationId)]
        public string? ApplicantDeclarationId { get; set; }


        [ApplicationField(EmploymentApplicationFields.ApplicationFolderPath)]
        public string? ApplicationFolderPath { get; set; }


        [ApplicationField(EmploymentApplicationFields.ApplicationKey)]
        public string? ApplicationKey { get; set; }


        [ApplicationField(EmploymentApplicationFields.ApplicationType)]
        public ApplicationType? ApplicationType { get; set; }


        [ApplicationField(EmploymentApplicationFields.CertifierOfCPRQualification)]
        public string? CertifierOfCPRQualification { get; set; }


        [ApplicationField(EmploymentApplicationFields.CertifierOfFirstAidQualification)]
        public string? CertifierOfFirstAidQualification { get; set; }


        [ApplicationField(EmploymentApplicationFields.ChurchAttending)]
        public string? ChurchAttending { get; set; }


        [ApplicationField(EmploymentApplicationFields.CitizenshipCountry)]
        public string? CitizenshipCountry { get; set; }


        [ApplicationField(EmploymentApplicationFields.CMEAType)]
        public CMEAType? CMEAType { get; set; }


        [ApplicationField(EmploymentApplicationFields.CommunityAndChurchInvolvement)]
        public string? CommunityAndChurchInvolvement { get; set; }


        [ApplicationField(EmploymentApplicationFields.ChurchAddress)]
        public string? ChurchAddress { get; set; }


        //[ApplicationField(EmploymentApplicationFields.CoreJobPosition)]
        public EntityReference? CoreJobPosition { get; set; }


        [ApplicationField(EmploymentApplicationFields.CountryOfCitizenship)]
        public string? CountryOfCitizenship { get; set; }


        [ApplicationField(EmploymentApplicationFields.CountryOfCitizenshipText)]
        public string? CountryOfCitizenshipText { get; set; }

        public bool CPRQualificationYN { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerAddress)]
        public string? CurrentEmployerAddress { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerName)]
        public string? CurrentEmployerName { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerPostCode)]
        public string? CurrentEmployerPostCode { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmployerState)]
        public StateTerritory? CurrentEmployerState { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentBaseSalary)]
        public int CurrentEmploymentBaseSalary { get; set; }


        [ApplicationField(EmploymentApplicationFields.CurrentEmploymentCurrentAllowances)]
        public int CurrentEmploymentCurrentAllowances { get; set; }


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


        [ApplicationField(EmploymentApplicationFields.EducationalPhilosophy)]
        public string? EducationalPhilosophy { get; set; }


        [ApplicationField(EmploymentApplicationFields.EducationalQualifications)]
        public string? EducationalQualifications { get; set; }


        [ApplicationField(EmploymentApplicationFields.EmailAddressMinisterOfReligion)]
        public string? EmailAddressMinisterOfReligion { get; set; }


        [ApplicationField(EmploymentApplicationFields.ExtraCurricularInvolvement)]
        public string? ExtraCurricularInvolvement { get; set; }


        [ApplicationField(EmploymentApplicationFields.GivenNames)]
        public string? GivenNames { get; set; }


        [ApplicationField(EmploymentApplicationFields.GraduationYear)]
        public int GraduationYear { get; set; }


        [ApplicationField(EmploymentApplicationFields.IsAlum)]
        public bool IsAlum { get; set; }


        [ApplicationField(EmploymentApplicationFields.HealthConditionsDetails)]
        public string? HealthConditionsDetails { get; set; }


        [ApplicationField(EmploymentApplicationFields.HealthConditionsYN)]
        public string? HealthConditionsYN { get; set; }


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


        [ApplicationField(EmploymentApplicationFields.OtherQualifications)]
        public string? OtherQualifications { get; set; }


        [ApplicationField(EmploymentApplicationFields.OtherQualifiedSubjects)]
        public string? OtherQualifiedSubjects { get; set; }


        [ApplicationField(EmploymentApplicationFields.PassportNumber)]
        public string? PassportNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.PastEmploymentDetails)]
        public string? PastEmploymentDetails { get; set; }


        [ApplicationField(EmploymentApplicationFields.PastoralCareInterestSkills)]
        public string? PastoralCareInterestSkills { get; set; }


        [ApplicationField(EmploymentApplicationFields.PersonalEmailAddress)]
        public string? PersonalEmailAddress { get; set; }


        [ApplicationField(EmploymentApplicationFields.PhoneNumberMinisterOfReligion)]
        public string? PhoneNumberMinisterOfReligion { get; set; }


        [ApplicationField(EmploymentApplicationFields.PostCode)]
        public int PostCode { get; set; }


        [ApplicationField(EmploymentApplicationFields.PostCodeText)]
        public string? PostCodeText { get; set; }


        [ApplicationField(EmploymentApplicationFields.PreferredName)]
        public string? PreferredName { get; set; }


        [ApplicationField(EmploymentApplicationFields.PreviousWorkersCompClaimsDetails)]
        public string? PreviousWorkersCompClaimsDetails { get; set; }


        [ApplicationField(EmploymentApplicationFields.PreviousWorkersCompClaimsYN)]
        public string? PreviousWorkersCompClaimsYN { get; set; }


        [ApplicationField(EmploymentApplicationFields.ProcessId)]
        public string? ProcessId { get; set; }


        [ApplicationField(EmploymentApplicationFields.ProfessionalMembershipDetails)]
        public string? ProfessionalMembershipDetails { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1Address)]
        public string? Referee1Address { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1Name)]
        public string? Referee1Name { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1Occupation)]
        public string? Referee1Occupation { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1PhoneNumber)]
        public string? Referee1PhoneNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1PostCode)]
        public int Referee1PostCode { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1RelationshipToApplicant)]
        public string? Referee1RelationshipToApplicant { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee1State)]
        public StateTerritory? Referee1State { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2Address)]
        public string? Referee2Address { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2Name)]
        public string? Referee2Name { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2Occupation)]
        public string? Referee2Occupation { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2PhoneNumber)]
        public string? Referee2PhoneNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2PostCode)]
        public int Referee2PostCode { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2RelationshipToApplicant)]
        public string? Referee2RelationshipToApplicant { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee2State)]
        public string? Referee2State { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3Address)]
        public string? Referee3Address { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3Name)]
        public string? Referee3Name { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3Occupation)]
        public string? Referee3Occupation { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3PhoneNumber)]
        public string? Referee3PhoneNumber { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3PostCode)]
        public int Referee3PostCode { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3RelationshipToApplicant)]
        public string? Referee3RelationshipToApplicant { get; set; }


        [ApplicationField(EmploymentApplicationFields.Referee3State)]
        public StateTerritory? Referee3State { get; set; }


        [ApplicationField(EmploymentApplicationFields.Salutation)]
        public Salutation Salutation { get; set; }


        [ApplicationField(EmploymentApplicationFields.State)]
        public StateTerritory? State { get; set; }


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


        [ApplicationField(EmploymentApplicationFields.WWCCExpiryDate)]
        public DateTime? WWCCExpiryDate { get; set; }


        [ApplicationField(EmploymentApplicationFields.WWCCNumber)]
        public string? WWCCNumber { get; set; }

        // settable entity references and Guid's
        public EntityReference? ParentJobOpportunity { get; set; }
        public EntityReference? OwningBusinessUnit { get; set; }
        public EntityReference? OwningTeam { get; set; }
        public EntityReference? OwningUser { get; set; }
        public EntityReference? JobPosition { get; set; }
        public string? StageId { get; set; }
        public string? TraversedPath { get; set; }

        // reference-only system fields
        public Status? Status { get; set; }
        public StatusReason? StatusReason { get; set; }
        public EntityReference? ModifiedBy { get; }
        public DateTime? ModifiedOn { get; }
        public EntityReference? CreatedBy { get; }
        public string? CreatedOn { get; }
        public Guid? OwnerId { get; }
        public Guid? EmploymentApplicationId { get; set; }
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
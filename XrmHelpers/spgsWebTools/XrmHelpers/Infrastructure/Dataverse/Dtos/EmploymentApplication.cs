using System;
using System.Xml.Serialization;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    [XmlRoot("root")]
    public class EmploymentApplication
    {
        [XmlElement("pgrowth_positionappliedfor")]
        public int JobOpportunityCode { get; set; }

        [XmlElement(EmploymentApplicationFields.Address)]
        public string? Address { get; set; }

        [XmlElement(EmploymentApplicationFields.ApplicantDeclarationId)]
        public string? ApplicantDeclarationId { get; set; }


        [XmlElement(EmploymentApplicationFields.ApplicationFolderPath)]
        public string? ApplicationFolderPath { get; set; }


        [XmlElement(EmploymentApplicationFields.ApplicationKey)]
        public string? ApplicationKey { get; set; }


        [XmlElement(EmploymentApplicationFields.ApplicationType)]
        public ApplicationType ApplicationType { get; set; }


        [XmlElement(EmploymentApplicationFields.CertifierOfCPRQualification)]
        public string? CertifierOfCPRQualification { get; set; }


        [XmlElement(EmploymentApplicationFields.CertifierOfFirstAidQualification)]
        public string? CertifierOfFirstAidQualification { get; set; }


        [XmlElement(EmploymentApplicationFields.ChurchAttending)]
        public string? ChurchAttending { get; set; }


        [XmlElement(EmploymentApplicationFields.CitizenshipCountry)]
        public string? CitizenshipCountry { get; set; }


        [XmlElement(EmploymentApplicationFields.CMEAType)]
        public CMEAType CMEAType { get; set; }


        [XmlElement(EmploymentApplicationFields.CommunityAndChurchInvolvement)]
        public string? CommunityAndChurchInvolvement { get; set; }


        [XmlElement(EmploymentApplicationFields.ChurchAddress)]
        public string? ChurchAddress { get; set; }


        //[XmlElement(EmploymentApplicationFields.CoreJobPosition)]
        public EntityReference? CoreJobPosition { get; set; }


        [XmlElement(EmploymentApplicationFields.CountryOfCitizenship)]
        public string? CountryOfCitizenship { get; set; }


        [XmlElement(EmploymentApplicationFields.CountryOfCitizenshipText)]
        public string? CountryOfCitizenshipText { get; set; }

        public bool CPRQualificationYN { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmployerAddress)]
        public string? CurrentEmployerAddress { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmployerName)]
        public string? CurrentEmployerName { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmployerPostCode)]
        public string? CurrentEmployerPostCode { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmployerState)]
        public StateTerritory CurrentEmployerState { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentBaseSalary)]
        public int CurrentEmploymentBaseSalary { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentCurrentAllowances)]
        public int CurrentEmploymentCurrentAllowances { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentEndDate)]
        public DateTime? CurrentEmploymentEndDate { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentPastVariations)]
        public string? CurrentEmploymentPastVariations { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentPositions)]
        public string? CurrentEmploymentPositions { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentReasonForLeaving)]
        public string? CurrentEmploymentReasonForLeaving { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentResponsibilities)]
        public string? CurrentEmploymentResponsibilities { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentEmploymentStartDate)]
        public DateTime? CurrentEmploymentStartDate { get; set; }

        public bool CurrentFirstAidYN { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentManagerPrincipalName)]
        public string? CurrentManagerPrincipalName { get; set; }


        [XmlElement(EmploymentApplicationFields.CurrentTeachingLoad)]
        public decimal CurrentTeachingLoad { get; set; }


        [XmlElement(EmploymentApplicationFields.DateOfBirth)]
        public DateTime? DateOfBirth { get; set; }


        [XmlElement(EmploymentApplicationFields.Denomination)]
        public Denomination Denomination { get; set; }


        [XmlElement(EmploymentApplicationFields.EducationalPhilosophy)]
        public string? EducationalPhilosophy { get; set; }


        [XmlElement(EmploymentApplicationFields.EducationalQualifications)]
        public string? EducationalQualifications { get; set; }


        [XmlElement(EmploymentApplicationFields.EmailAddressMinisterOfReligion)]
        public string? EmailAddressMinisterOfReligion { get; set; }


        [XmlElement(EmploymentApplicationFields.ExtraCurricularInvolvement)]
        public string? ExtraCurricularInvolvement { get; set; }


        [XmlElement(EmploymentApplicationFields.GivenNames)]
        public string? GivenNames { get; set; }


        [XmlElement(EmploymentApplicationFields.GraduationYear)]
        public int GraduationYear { get; set; }


        [XmlElement(EmploymentApplicationFields.IsAlum)]
        public bool IsAlum { get; set; }


        [XmlElement(EmploymentApplicationFields.HealthConditionsDetails)]
        public string? HealthConditionsDetails { get; set; }


        [XmlElement(EmploymentApplicationFields.HealthConditionsYN)]
        public string? HealthConditionsYN { get; set; }


        [XmlElement(EmploymentApplicationFields.HomePhoneNumber)]
        public string? HomePhoneNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.IssueDateOfCPRQualification)]
        public DateTime? IssueDateOfCPRQualification { get; set; }


        [XmlElement(EmploymentApplicationFields.IssueDateOfFirstAidQualification)]
        public DateTime? IssueDateOfFirstAidQualification { get; set; }


        [XmlElement(EmploymentApplicationFields.ISTAAAccreditationDateAchieved)]
        public DateTime? ISTAAAccreditationDateAchieved { get; set; }


        [XmlElement(EmploymentApplicationFields.ISTAAAccreditationNumber)]
        public string? ISTAAAccreditationNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.HasAccomplishedTeacher)]
        public bool HasAccomplishedTeacher { get; set; }

        [XmlElement(EmploymentApplicationFields.LastName)]
        public string? LastName { get; set; }


        [XmlElement(EmploymentApplicationFields.MajorTeachingSubjectArea)]
        public string? MajorTeachingSubjectArea { get; set; }


        [XmlElement(EmploymentApplicationFields.MinorTeachingSubjectArea)]
        public string? MinorTeachingSubjectArea { get; set; }


        [XmlElement(EmploymentApplicationFields.MobileNumber)]
        public string? MobileNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.NameMinisterOfReligion)]
        public string? NameMinisterOfReligion { get; set; }


        [XmlElement(EmploymentApplicationFields.NameOfCPRQualification)]
        public string? NameOfCPRQualification { get; set; }


        [XmlElement(EmploymentApplicationFields.NameOfFirstAidQualification)]
        public string? NameOfFirstAidQualification { get; set; }


        [XmlElement(EmploymentApplicationFields.NESAAccreditationDateAchieved)]
        public DateTime? NESAAccreditationDateAchieved { get; set; }


        [XmlElement(EmploymentApplicationFields.NESAAccreditationNumber)]
        public string? NESAAccreditationNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.NESAAccreditationStatus)]
        public NESAAccreditation NESAAccreditationStatus { get; set; }


        [XmlElement(EmploymentApplicationFields.OtherQualifications)]
        public string? OtherQualifications { get; set; }


        [XmlElement(EmploymentApplicationFields.OtherQualifiedSubjects)]
        public string? OtherQualifiedSubjects { get; set; }


        [XmlElement(EmploymentApplicationFields.PassportNumber)]
        public string? PassportNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.PastEmploymentDetails)]
        public string? PastEmploymentDetails { get; set; }


        [XmlElement(EmploymentApplicationFields.PastoralCareInterestSkills)]
        public string? PastoralCareInterestSkills { get; set; }


        [XmlElement(EmploymentApplicationFields.PersonalEmailAddress)]
        public string? PersonalEmailAddress { get; set; }


        [XmlElement(EmploymentApplicationFields.PhoneNumberMinisterOfReligion)]
        public string? PhoneNumberMinisterOfReligion { get; set; }


        [XmlElement(EmploymentApplicationFields.PostCode)]
        public int PostCode { get; set; }

        
        [XmlElement(EmploymentApplicationFields.PostCodeText)]
        public string? PostCodeText { get; set; }


        [XmlElement(EmploymentApplicationFields.PreferredName)]
        public string? PreferredName { get; set; }


        [XmlElement(EmploymentApplicationFields.PreviousWorkersCompClaimsDetails)]
        public string? PreviousWorkersCompClaimsDetails { get; set; }


        [XmlElement(EmploymentApplicationFields.PreviousWorkersCompClaimsYN)]
        public string? PreviousWorkersCompClaimsYN { get; set; }


        [XmlElement(EmploymentApplicationFields.ProcessId)]
        public string? ProcessId { get; set; }


        [XmlElement(EmploymentApplicationFields.ProfessionalMembershipDetails)]
        public string? ProfessionalMembershipDetails { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee1Address)]
        public string? Referee1Address { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee1Name)]
        public string? Referee1Name { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee1Occupation)]
        public string? Referee1Occupation { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee1PhoneNumber)]
        public string? Referee1PhoneNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee1PostCode)]
        public int Referee1PostCode { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee1RelationshipToApplicant)]
        public string? Referee1RelationshipToApplicant { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee1State)]
        public StateTerritory Referee1State { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee2Address)]
        public string? Referee2Address { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee2Name)]
        public string? Referee2Name { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee2Occupation)]
        public string? Referee2Occupation { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee2PhoneNumber)]
        public string? Referee2PhoneNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee2PostCode)]
        public int Referee2PostCode { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee2RelationshipToApplicant)]
        public string? Referee2RelationshipToApplicant { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee2State)]
        public string? Referee2State { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee3Address)]
        public string? Referee3Address { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee3Name)]
        public string? Referee3Name { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee3Occupation)]
        public string? Referee3Occupation { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee3PhoneNumber)]
        public string? Referee3PhoneNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee3PostCode)]
        public int Referee3PostCode { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee3RelationshipToApplicant)]
        public string? Referee3RelationshipToApplicant { get; set; }


        [XmlElement(EmploymentApplicationFields.Referee3State)]
        public StateTerritory Referee3State { get; set; }


        [XmlElement(EmploymentApplicationFields.Salutation)]
        public Salutation Salutation { get; set; }


        [XmlElement(EmploymentApplicationFields.State)]
        public StateTerritory State { get; set; }


        [XmlElement(EmploymentApplicationFields.SubmissionDate)]
        public DateTime? SubmissionDate { get; set; }


        [XmlElement(EmploymentApplicationFields.Name)]
        public string? Name { get; set; }


        [XmlElement(EmploymentApplicationFields.VisaWorkApprovalDetails)]
        public string? VisaWorkApprovalDetails { get; set; }


        [XmlElement(EmploymentApplicationFields.IsPermanentResident)]
        public bool IsPermanentResident { get; set; }


        [XmlElement(EmploymentApplicationFields.HasWorkVisa)]
        public bool HasWorkVisa { get; set; }


        [XmlElement(EmploymentApplicationFields.WorkPhoneNumber)]
        public string? WorkPhoneNumber { get; set; }


        [XmlElement(EmploymentApplicationFields.WWCCExpiryDate)]
        public DateTime? WWCCExpiryDate { get; set; }


        [XmlElement(EmploymentApplicationFields.WWCCNumber)]
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
        public Status Status { get; set; }
        public StatusReason StatusReason { get; set; }
        public EntityReference? ModifiedBy { get; }
        public DateTime? ModifiedOn { get; }
        public EntityReference? CreatedBy { get; }
        public string? CreatedOn { get; }
        public Guid OwnerId { get; }
        public Guid? EmploymentApplicationId { get; set; }
    }
}
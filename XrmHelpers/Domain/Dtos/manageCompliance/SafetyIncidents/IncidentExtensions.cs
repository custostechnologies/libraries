using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    public static class IncidentExtensions
    {
        public static (
            int severityScore,
            string logMessage
        ) SeverityScore(
            IncidentType incidentType,
            bool ambulanceCalled,
            AdmittedToHospital admittedToHospital,
            bool lossOfTime,
            IncidentCategory incidentCategory,
            InjuryBodyPart[]? injuryBodyParts,
            InjuryType[]? injuryTypes
        )
        {
            int score = 0;

            if (incidentType == IncidentType.ResultingInInjury) score++;
            if (ambulanceCalled) score++;
            if (admittedToHospital == AdmittedToHospital.Yes) score++;
            if (lossOfTime) score++;

            var categoryFactor = EnumExtensions.GetSeverityFactor(incidentCategory);
            int bodyPartFactor = 0;

            if (injuryBodyParts != null)
            {
                foreach (var bodyPart in injuryBodyParts)
                {
                    bodyPartFactor += EnumExtensions.GetSeverityFactor(bodyPart);
                }
            }

            int injuryTypeFactor = 0;

            if (injuryTypes != null)
            {
                foreach (var injuryType in injuryTypes)
                {
                    injuryTypeFactor += EnumExtensions.GetSeverityFactor(injuryType);
                }
            }

            score += categoryFactor + bodyPartFactor + injuryTypeFactor; 
            var message = 
                $"SeverityScore components: " + 
                $"incidentType: {(incidentType == IncidentType.ResultingInInjury ? "+1" : "+0")}" +
                $"ambulanceCalled: {(ambulanceCalled ? "+1" : "+0")}" + 
                $"admittedToHospital: {(admittedToHospital == AdmittedToHospital.Yes ? "+1" : "+0")}" +
                $"lossOfTime: {(lossOfTime ? "+1" : "+0")}" + 
                $"categoryFactor: +{categoryFactor}" + 
                $"bodyPartFactor: +{bodyPartFactor}" +
                $"injuryTypeFactor: +{injuryTypeFactor}";

            return (score, message);
        }
    }
}
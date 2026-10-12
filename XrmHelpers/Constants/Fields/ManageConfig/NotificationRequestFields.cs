namespace spgsWebTools.XrmHelpers.Constants
{
    public static class NotificationRequestFields
    {
        // Identity
        public const string NotificationRequestId = "mthomas_notificationrequestid";

        // Core Fields
        public const string RequestTitle = "mthomas_requesttitle";
        public const string NotificationType = "mthomas_notificationtype";
        public const string AdaptiveCardTemplate = "mthomas_adaptivecardtemplate";
        public const string EmailTemplate = "mthomas_emailtemplate";
        public const string ExportTemplate = "mthomas_exporttemplate";

        // Recipient Information
        public const string RecipientString = "mthomas_recipientstring";
        public const string ReceiverId = "mthomas_receiverid";
        public const string CCString = "mthomas_ccstring";
        public const string ChatId = "mthomas_chatid";
        public const string MessageId = "mthomas_messageid";
        public const string SendFrom = "pgrowth_sendfrom";
        public const string Importance = "pgrowth_importance";
        public const string Subject = "pgrowth_subject";
        public const string OperationType = "pgrowth_operationtype";

        // Related Records
        public const string RelatedRecordId = "mthomas_relatedrecordid";
        public const string NewNotificationRequestFlow = "mthomas_newnotificationrequestflow";

        // Input Data
        public const string InputJson = "mthomas_inputjson";
        public const string InFlowInputs = "mthomas_inflowinputs";
        public const string InFlowInputsObject = "mthomas_inflowinputsobject";
        public const string InParentFlowInstanceId = "mthomas_inparentflowinstanceid";
        public const string HTMLDocument = "pgrowth_htmldocument";

        // Status
        public const string StateCode = "statecode";
        public const string StatusCode = "statuscode";
    }
}
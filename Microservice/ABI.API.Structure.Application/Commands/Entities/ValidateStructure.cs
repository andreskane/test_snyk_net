using System.Collections.Generic;

namespace ABI.API.Structure.Application.Commands.Entities
{
    public class ValidateStructure
    {
        public bool Valid { get; set; }

        public List<ValidateError> Errors { get; set; }

        public ValidateStructure()
        {
            Valid = true;
            Errors = new List<ValidateError>();
        }

        public void AddValidateError(
            int nodeId,
            string nodeCode,
            string nodeName,
            int nodeLevelId,
            string nodeLevelName,
            string error,
            string typeError,
            int priority,
            object classError)
        {
            var errorItem = new ValidateError
            {
                NodeId = nodeId,
                NodeCode = nodeCode,
                NodeName = nodeName,
                NodeLevelId = nodeLevelId,
                NodeLevelName = nodeLevelName,
                TypeError = typeError,
                Error = error,
                Priority = priority,
                ClassError = classError
            };

            Errors.Add(errorItem);

        }

    }

    public class ValidateError
    {
        public int NodeId { get; set; }
        public string NodeCode { get; set; }
        public string NodeName { get; set; }
        public int NodeLevelId { get; set; }
        public string NodeLevelName { get; set; }
        public string Error { get; set; }
        public string TypeError { get; set; }
        public int Priority { get; set; }
        public object ClassError { get; set; }
    }
}

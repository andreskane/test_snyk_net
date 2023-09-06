using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class ShortNameTransformation : TransformationBase
    {

        public override async  Task<object> DoTransform(object value)
        {

            return await Task.FromResult( GetShortName(value.ToString(), 15));

        }

        #region Methods Private

        /// <summary>
        /// Gets the short name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="numberOfInitials">The number of initials.</param>
        /// <returns></returns>
        private string GetShortName(string value, int numberOfInitials = 3)
        {
            var initials = string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                var words = value.Split(' ');

                for (int i = numberOfInitials, f = 0; i > 0 && f < words.Length; i--, f++)
                {
                    if (f < words.Length - 1)
                    {
                        initials += this.GetWordInitials(words[f], 1);
                    }
                    else
                    {
                        initials += this.GetWordInitials(words[f], i);
                    }
                }
            }

            return initials;
        }

        /// <summary>
        /// Gets the word initials.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="numberOfInitials">The number of initials.</param>
        /// <returns></returns>
        private string GetWordInitials(string word, int numberOfInitials)
        {
            var initials = string.Empty;

            if (numberOfInitials > word.Length)
                initials = word.Substring(0, word.Length);
            else
                initials = word.Substring(0, numberOfInitials);

            return initials.ToUpper();

        }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace FaladorTradingSystems.Twitter
{
    public class TwitterEngine
    {
        #region constructor

        public TwitterEngine()
        {
            Auth.SetUserCredentials(ProjectParameters.TwitterConsumerAPI,
                ProjectParameters.TwitterConsumerSecret,
                ProjectParameters.TwitterAccessAPI,
                ProjectParameters.TwitterAccessSecret);
        }

        #endregion 
    }
}

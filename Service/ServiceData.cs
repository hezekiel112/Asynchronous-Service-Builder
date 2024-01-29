namespace HezekDev.ServiceBuilder.Service {
    public struct ServiceData {
        public string Name {
            get;
        }

        public short Delay {
            get;
        }

        public string Description {
            get;
        }

        /// <summary>
        /// use this to override the delay as 500ms
        /// </summary>
        public bool UseMin_DefaultDelay {
            get;
            set;
        }

        /// <summary>
        /// use this to override the delay as 500ms
        /// </summary>
        public bool UseMax_DefaultDelay {
            get;
            set;
        }
        public bool IsOneTimeExecution {
            get;
            set;
        }

        public EServiceToken Token = EServiceToken.SERVICE_IS_SLEEPING;

        public bool SetToken(EServiceToken token) {
            if (Token != token)
                Token = token;

            return Token == token;
        }

        public ServiceData(string name, string description, short delay = 0, bool isOneTimeExecution = false) {
            Name = name;
            Description = description;
            Delay = delay;
            IsOneTimeExecution = isOneTimeExecution;
        }
    }

}
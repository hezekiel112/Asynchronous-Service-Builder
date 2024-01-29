namespace HezekDev.ServiceBuilder.Service {
    /// <summary>
    /// struct servant à définir les informations d'un token de service
    /// </summary>
    public readonly struct ServiceTokenModal {
        public EServiceToken Token {
            get;
        }

        /// <summary>
        /// retourne token sous un format base16 hexa
        /// </summary>
        /// <returns></returns>
        public byte GetB16ID() {
            return (byte) Token;
        }

        /// <summary>
        /// retourne la valeur du type de token
        /// </summary>
        /// <returns></returns>
        public EServiceToken GetToken() {
            return Token;
        }

        public ServiceTokenModal(EServiceToken token) {
            Token = token;
        }
    }

}
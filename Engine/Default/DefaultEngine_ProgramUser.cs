namespace HezekDev.ServiceBuilder {
    public static class DefaultEngine_ProgramUser {
        public static void Save(ServiceExecutionTrace[] valueToSave, bool autoSave = true) {
            ProgramUser.Singleton.SerializeJSON(valueToSave, out string savedValue);

            if (autoSave)
                ProgramUser.Singleton.SaveJSON(savedValue);
        }
    }
}
using System.Text.Json;
using System.IO;
using Microsoft.VisualBasic;
using System.Text;

namespace HezekDev.ServiceBuilder {
    public sealed class ProgramUser {

        public static ProgramUser Singleton {
            get;
        } = new ProgramUser();

        public bool IsSaveFileExist => File.Exists($"{Constants.ABSOLUTE_FILE_SAVE_PATH}.json");

        public short CurrentSession {
            get;set;
        }

        readonly JsonSerializerOptions JsonConfig = new JsonSerializerOptions() {
            WriteIndented = true,
        };

        public void SerializeJSON(ServiceExecutionTrace[] valueToSerialize, out string serializedValue) {
            serializedValue = JsonSerializer.Serialize(valueToSerialize, JsonConfig);
        }

        public void SaveJSON(string serializedValue) {
            using (StreamWriter writer = new StreamWriter($"session.json", append: true)) {
                // Utiliser WriteLine pour ajouter la nouvelle ligne
                writer.WriteLine(serializedValue);
            }
        }
    }
}
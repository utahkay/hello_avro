//<auto-generated />
namespace ps.platform.test
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Microsoft.Hadoop.Avro;

    /// <summary>
    /// Used to serialize and deserialize Avro record ps.platform.test.User2.
    /// </summary>
    [DataContract(Namespace = "ps.platform.test")]
    public partial class User2
    {
        private const string JsonSchema = @"{""type"":""record"",""name"":""ps.platform.test.User2"",""fields"":[{""name"":""id"",""type"":""int""},{""name"":""name"",""type"":""string""},{""name"":""email"",""type"":[""string"",""null""]}]}";

        /// <summary>
        /// Gets the schema.
        /// </summary>
        public static string Schema
        {
            get
            {
                return JsonSchema;
            }
        }
      
        /// <summary>
        /// Gets or sets the id field.
        /// </summary>
        [DataMember]
        public int id { get; set; }
              
        /// <summary>
        /// Gets or sets the name field.
        /// </summary>
        [DataMember]
        public string name { get; set; }
              
        /// <summary>
        /// Gets or sets the email field.
        /// </summary>
        [DataMember]
        public string email { get; set; }
                
        /// <summary>
        /// Initializes a new instance of the <see cref="User2"/> class.
        /// </summary>
        public User2()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User2"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        public User2(int id, string name, string email)
        {
            this.id = id;
            this.name = name;
            this.email = email;
        }
    }
}

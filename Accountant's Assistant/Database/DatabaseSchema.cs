using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant_s_Assistant.Database
{
    class DatabaseSchema
    {
        public static string EmployerSchema = @"{
  ""$schema"": ""http://json-schema.org/draft-04/schema#"",
  ""type"": ""object"",
  ""properties"": {
    ""Employer"": {
      ""type"": ""array"",
      ""items"": [
        {
          ""type"": ""object"",
          ""properties"": {
            ""Id"": {
              ""type"": ""string""
            },
            ""Name"": {
              ""type"": ""string""
            },
            ""Address"": {
              ""type"": ""object"",
              ""properties"": {
                ""Street"": {
                  ""type"": ""string""
                },
                ""City"": {
                  ""type"": ""string""
                },
                ""PostalCode"": {
                  ""type"": ""string""
                }
              },
              ""required"": [
                ""Street"",
                ""City"",
                ""PostalCode""
              ]
            },
            ""VAT"": {
              ""type"": ""string""
            },
            ""Director"": {
              ""type"": ""string""
            }
          },
          ""required"": [
            ""Id"",
            ""Name"",
            ""Address"",
            ""VAT"",
            ""Director""
          ]
        },
        {
          ""type"": ""object"",
          ""properties"": {
            ""Id"": {
              ""type"": ""string""
            },
            ""Name"": {
              ""type"": ""string""
            },
            ""Address"": {
              ""type"": ""object"",
              ""properties"": {
                ""Street"": {
                  ""type"": ""string""
                },
                ""City"": {
                  ""type"": ""string""
                },
                ""PostalCode"": {
                  ""type"": ""string""
                }
              },
              ""required"": [
                ""Street"",
                ""City"",
                ""PostalCode""
              ]
            },
            ""VAT"": {
              ""type"": ""string""
            },
            ""Director"": {
              ""type"": ""string""
            }
          },
          ""required"": [
            ""Id"",
            ""Name"",
            ""Address"",
            ""VAT"",
            ""Director""
          ]
        }
      ]
    }
  },
  ""required"": [
    ""Employer""
  ]
}";

        public static string EmpolyeeSchema  = @"{
  ""$schema"": ""http://json-schema.org/draft-04/schema#"",
  ""type"": ""object"",
  ""properties"": {
    ""Employee"": {
      ""type"": ""array"",
      ""items"": [
        {
          ""type"": ""object"",
          ""properties"": {
            ""Id"": {
              ""type"": ""string""
            },
            ""Name"": {
              ""type"": ""string""
            },
            ""Address"": {
              ""type"": ""object"",
              ""properties"": {
                ""Street"": {
                  ""type"": ""string""
                },
                ""City"": {
                  ""type"": ""string""
                },
                ""PostalCode"": {
                  ""type"": ""string""
                }
              },
              ""required"": [
                ""Street"",
                ""City"",
                ""PostalCode""
              ]
            },
            ""VAT"": {
              ""type"": ""string""
            },
            ""Birthday"": {
              ""type"": ""string""
            }
          },
          ""required"": [
            ""Id"",
            ""Name"",
            ""Address"",
            ""VAT"",
            ""Birthday""
          ]
        },
        {
          ""type"": ""object"",
          ""properties"": {
            ""Id"": {
              ""type"": ""string""
            },
            ""Name"": {
              ""type"": ""string""
            },
            ""Address"": {
              ""type"": ""object"",
              ""properties"": {
                ""Street"": {
                  ""type"": ""string""
                },
                ""City"": {
                  ""type"": ""string""
                },
                ""PostalCode"": {
                  ""type"": ""string""
                }
              },
              ""required"": [
                ""Street"",
                ""City"",
                ""PostalCode""
              ]
            },
            ""VAT"": {
              ""type"": ""string""
            },
            ""Birthday"": {
              ""type"": ""string""
            }
          },
          ""required"": [
            ""Id"",
            ""Name"",
            ""Address"",
            ""VAT"",
            ""Birthday""
          ]
        }
      ]
    }
  },
  ""required"": [
    ""Employee""
  ]
}";
    }
}

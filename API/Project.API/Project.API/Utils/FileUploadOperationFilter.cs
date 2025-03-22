using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace Project.API.Utils
{
	public class FileUploadOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (context.ApiDescription.ActionDescriptor.DisplayName.Contains("UploadImage"))
			{
				operation.RequestBody = new OpenApiRequestBody
				{
					Content = new Dictionary<string, OpenApiMediaType>
					{
						["multipart/form-data"] = new OpenApiMediaType
						{
							Schema = new OpenApiSchema
							{
								Type = "object",
								Properties = new Dictionary<string, OpenApiSchema>
								{
									["file"] = new OpenApiSchema { Type = "string", Format = "binary" },
									["fileName"] = new OpenApiSchema { Type = "string" },
									["title"] = new OpenApiSchema { Type = "string" }
								}
							}
						}
					}
				};
			}
		}
	}

}

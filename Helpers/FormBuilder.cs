using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helpers
{
    public class FormBuilder
    {
        public string html;
        public int columns = 2;
        public List<string> exceptions = new List<string>();


        public void build(object targetClass)
        {
            var lines = new List<string>();
            string[] propertyNames = targetClass.GetType().GetProperties().Select(p => p.Name).ToArray();
            var targetType = this.GetType();

            var elements = targetClass.GetType().GetProperties();
            var currentCol = 0;
            foreach (var prop in elements)
            {
                if (!exceptions.Contains(prop.Name))
                {
                    if (currentCol == 0)
                    {
                        lines.Add(@"<div class=""row"">");
                    }

                    lines.Add(@"<div class=""col"">");
                    currentCol++;

                    var nameWithSpaces = Regex.Split(prop.Name, @"(?<!^)(?=[A-Z])");

                    nameWithSpaces[0] = nameWithSpaces[0].ToUpper()[0] + nameWithSpaces[0].Substring(1);

                    var labelText = String.Join(" ", nameWithSpaces);

                    if (labelText.EndsWith(" Id"))
                    {
                        labelText = labelText.Substring(0, labelText.Length - 3);
                    }

                    lines.Add(String.Format(@"<label for=""{1}"">{0}</label>", labelText, prop.Name));

                    var t = Type.GetTypeCode(prop.PropertyType);

                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        t = Type.GetTypeCode(Nullable.GetUnderlyingType(prop.PropertyType));
                    }

                    switch (t)
                    {
                        case TypeCode.Decimal:
                            lines.Add(String.Format(@"<input type=""number"" min=0 step=""0.01"" class=""form-control"" id=""{0}"" name=""{0}"" />", prop.Name));
                            break;
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                            if (prop.Name.EndsWith("Id"))
                            {
                                lines.Add(String.Format(@"<select class=""form-select"" id=""{0}"" name=""{0}"">", prop.Name));
                                lines.Add(String.Format(@"@foreach (var tt in Model.{0}Types)", prop.Name.Substring(0,prop.Name.Length-2)));
                                lines.Add("{");
                                lines.Add(@"<option value=""@tt.Key"">@tt.Value</option>");
                                lines.Add("}");
                                lines.Add(@"</select>");
                            }
                            else
                            {
                                lines.Add(String.Format(@"<input type=""text"" class=""form-control"" id=""{0}"" name=""{0}"" />", prop.Name));
                            }
                            break;
                        case TypeCode.DateTime:
                            lines.Add(String.Format(@"<input type=""date"" class=""form-control"" id=""{0}"" name=""{0}"" />", prop.Name));
                            break;
                        default:
                            if (prop.Name.ToLower().EndsWith("email"))
                            {
                                lines.Add(String.Format(@"<input type=""email"" class=""form-control"" id=""{0}"" name=""{0}"" />", prop.Name));
                            }
                            else
                            {
                                lines.Add(String.Format(@"<input type=""text"" class=""form-control"" id=""{0}"" name=""{0}"" />", prop.Name));
                            }
                            break;
                    }

                    lines.Add(@"</div>");
                    if (currentCol == columns)
                    {
                        lines.Add(@"</div>");
                        currentCol = 0;
                    }
                }
            }

            html = String.Join("\r\n", lines);
            /*
             * 
             * <select class="form-select" id="gender" name="gender">
                        @foreach (var tt in Model.genderTypes)
                        {
                            <option value="@tt.Key">@tt.Value</option>
                        }
                    </select>

            <div class="row">
                <div class="col">
                    <label for="firstName">First Name</label>
                    <input type="text" class="form-control" id="firstName" name="firstName" />
                </div>
                <div class="col">
                    <label for="lastName">Last Name</label>
                    <input type="text" class="form-control" id="lastName" name="lastName" />
                </div>
            </div>

            try
            {
                foreach (var prop in propertyNames)
                {
                    if (prop.ToLower() != "password")
                    {
                        var value = me.GetType().GetProperty(prop).GetValue(t);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                
            }*/
        }
    }
}

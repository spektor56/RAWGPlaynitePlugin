/* 
 * RAWG Video Games Database API
 *
 *  The largest open video games database.  ### Why build on RAWG - More than 350,000 games for 50 platforms including mobiles. - Rich metadata: tags, genres, developers, publishers, individual creators, official websites, release dates, Metacritic ratings. - Where to buy: links to digital distribution services - Similar games based on visual similarity. - Player activity data: Steam average playtime and RAWG player counts and ratings. - Actively developing and constantly getting better by user contribution and our algorithms.  ### Terms of Use - Free for personal use as long as you attribute RAWG as the source of the data and/or images and add an active hyperlink from every page where the data of RAWG is used. - Free for commercial use for startups and hobby projects with not more than 100,000 monthly active users or 500,000 page views per month. If your project is larger than that, email us at [api@rawg.io](mailto:api@rawg.io) for commercial terms. - No cloning. It would not be cool if you used our API to launch a clone of RAWG. We know it is not always easy to say what is a duplicate and what isn't. Drop us a line at [api@rawg.io](mailto:api@rawg.io) if you are in doubt, and we will talk it through. - Every API request should have a User-Agent header with your app name. If you don’t provide it, we may ban your requests.  __[Read more](https://rawg.io/apidocs)__. 
 *
 * OpenAPI spec version: v1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// PersonSingle
    /// </summary>
    [DataContract]
    public partial class PersonSingle :  IEquatable<PersonSingle>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSingle" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected PersonSingle() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSingle" /> class.
        /// </summary>
        /// <param name="name">name (required).</param>
        /// <param name="description">description.</param>
        public PersonSingle(string name = default(string), string description = default(string))
        {
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new InvalidDataException("name is a required property for PersonSingle and cannot be null");
            }
            else
            {
                this.Name = name;
            }
            this.Description = description;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; private set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Slug
        /// </summary>
        [DataMember(Name="slug", EmitDefaultValue=false)]
        public string Slug { get; private set; }

        /// <summary>
        /// Gets or Sets Image
        /// </summary>
        [DataMember(Name="image", EmitDefaultValue=false)]
        public string Image { get; private set; }

        /// <summary>
        /// Gets or Sets ImageBackground
        /// </summary>
        [DataMember(Name="image_background", EmitDefaultValue=false)]
        public string ImageBackground { get; private set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets GamesCount
        /// </summary>
        [DataMember(Name="games_count", EmitDefaultValue=false)]
        public int? GamesCount { get; private set; }

        /// <summary>
        /// Gets or Sets ReviewsCount
        /// </summary>
        [DataMember(Name="reviews_count", EmitDefaultValue=false)]
        public int? ReviewsCount { get; private set; }

        /// <summary>
        /// Gets or Sets Rating
        /// </summary>
        [DataMember(Name="rating", EmitDefaultValue=false)]
        public string Rating { get; private set; }

        /// <summary>
        /// Gets or Sets RatingTop
        /// </summary>
        [DataMember(Name="rating_top", EmitDefaultValue=false)]
        public int? RatingTop { get; private set; }

        /// <summary>
        /// Gets or Sets Updated
        /// </summary>
        [DataMember(Name="updated", EmitDefaultValue=false)]
        public DateTime? Updated { get; private set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PersonSingle {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Slug: ").Append(Slug).Append("\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  ImageBackground: ").Append(ImageBackground).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  GamesCount: ").Append(GamesCount).Append("\n");
            sb.Append("  ReviewsCount: ").Append(ReviewsCount).Append("\n");
            sb.Append("  Rating: ").Append(Rating).Append("\n");
            sb.Append("  RatingTop: ").Append(RatingTop).Append("\n");
            sb.Append("  Updated: ").Append(Updated).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as PersonSingle);
        }

        /// <summary>
        /// Returns true if PersonSingle instances are equal
        /// </summary>
        /// <param name="input">Instance of PersonSingle to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PersonSingle input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Slug == input.Slug ||
                    (this.Slug != null &&
                    this.Slug.Equals(input.Slug))
                ) && 
                (
                    this.Image == input.Image ||
                    (this.Image != null &&
                    this.Image.Equals(input.Image))
                ) && 
                (
                    this.ImageBackground == input.ImageBackground ||
                    (this.ImageBackground != null &&
                    this.ImageBackground.Equals(input.ImageBackground))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.GamesCount == input.GamesCount ||
                    (this.GamesCount != null &&
                    this.GamesCount.Equals(input.GamesCount))
                ) && 
                (
                    this.ReviewsCount == input.ReviewsCount ||
                    (this.ReviewsCount != null &&
                    this.ReviewsCount.Equals(input.ReviewsCount))
                ) && 
                (
                    this.Rating == input.Rating ||
                    (this.Rating != null &&
                    this.Rating.Equals(input.Rating))
                ) && 
                (
                    this.RatingTop == input.RatingTop ||
                    (this.RatingTop != null &&
                    this.RatingTop.Equals(input.RatingTop))
                ) && 
                (
                    this.Updated == input.Updated ||
                    (this.Updated != null &&
                    this.Updated.Equals(input.Updated))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Slug != null)
                    hashCode = hashCode * 59 + this.Slug.GetHashCode();
                if (this.Image != null)
                    hashCode = hashCode * 59 + this.Image.GetHashCode();
                if (this.ImageBackground != null)
                    hashCode = hashCode * 59 + this.ImageBackground.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.GamesCount != null)
                    hashCode = hashCode * 59 + this.GamesCount.GetHashCode();
                if (this.ReviewsCount != null)
                    hashCode = hashCode * 59 + this.ReviewsCount.GetHashCode();
                if (this.Rating != null)
                    hashCode = hashCode * 59 + this.Rating.GetHashCode();
                if (this.RatingTop != null)
                    hashCode = hashCode * 59 + this.RatingTop.GetHashCode();
                if (this.Updated != null)
                    hashCode = hashCode * 59 + this.Updated.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // Name (string) minLength
            if(this.Name != null && this.Name.Length < 1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, length must be greater than 1.", new [] { "Name" });
            }

            // Slug (string) minLength
            if(this.Slug != null && this.Slug.Length < 1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Slug, length must be greater than 1.", new [] { "Slug" });
            }

            // Slug (string) pattern
            Regex regexSlug = new Regex(@"^[-a-zA-Z0-9_]+$", RegexOptions.CultureInvariant);
            if (false == regexSlug.Match(this.Slug).Success)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Slug, must match a pattern of " + regexSlug, new [] { "Slug" });
            }

            // ImageBackground (string) minLength
            if(this.ImageBackground != null && this.ImageBackground.Length < 1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ImageBackground, length must be greater than 1.", new [] { "ImageBackground" });
            }

            // Description (string) minLength
            if(this.Description != null && this.Description.Length < 1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Description, length must be greater than 1.", new [] { "Description" });
            }

            yield break;
        }
    }

}
namespace TeamBond.Syntosa.Validation.Core.CodeWriter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The json type.
    /// </summary>
    public class JsonType
    {
        /// <summary>
        /// The generator config.
        /// </summary>
        private readonly IJsonClassGeneratorConfig generatorConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonType" /> class.
        /// </summary>
        /// <param name="generator">
        /// The generator.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        public JsonType(IJsonClassGeneratorConfig generator, JToken token)
            : this(generator)
        {
            this.Type = GetFirstTypeEnum(token);

            if (this.Type == SupportedJsonTypes.Array)
            {
                var array = (JArray)token;
                this.InternalType = GetCommonType(generator, array.ToArray());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonType" /> class.
        /// </summary>
        /// <param name="generator">
        /// The generator.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        internal JsonType(IJsonClassGeneratorConfig generator, SupportedJsonTypes type)
            : this(generator)
        {
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonType" /> class.
        /// </summary>
        /// <param name="generator">
        /// The generator.
        /// </param>
        private JsonType(IJsonClassGeneratorConfig generator)
        {
            this.generatorConfig = generator;
        }

        /// <summary>
        /// Gets the assigned name.
        /// </summary>
        public string AssignedName { get; private set; }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        public IList<FieldInfo> Fields { get; internal set; }

        /// <summary>
        /// Gets the internal type.
        /// </summary>
        public JsonType InternalType { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether is root.
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// Gets a value indicating whether must cache.
        /// </summary>
        public bool MustCache
        {
            get
            {
                switch (this.Type)
                {
                    case SupportedJsonTypes.Array: return true;
                    case SupportedJsonTypes.Object: return true;
                    case SupportedJsonTypes.Anything: return true;
                    case SupportedJsonTypes.Dictionary: return true;
                    case SupportedJsonTypes.NonConstrained: return true;
                    default: return false;
                }
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public SupportedJsonTypes Type { get; }

        /// <summary>
        /// The get common type.
        /// </summary>
        /// <param name="generator">
        /// The generator.
        /// </param>
        /// <param name="tokens">
        /// The tokens.
        /// </param>
        /// <returns>
        /// The <see cref="JsonType" />.
        /// </returns>
        public static JsonType GetCommonType(IJsonClassGeneratorConfig generator, JToken[] tokens)
        {
            if (tokens is null || tokens.Length <= 0)
            {
                return new JsonType(generator, SupportedJsonTypes.NonConstrained);
            }

            var common = new JsonType(generator, tokens[0]).MaybeMakeNullable(generator);

            for (var i = 1; i < tokens.Length; i++)
            {
                var current = new JsonType(generator, tokens[i]);
                common = common.GetCommonType(current);
            }

            return common;
        }

        /// <summary>
        /// The assign name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public void AssignName(string name)
        {
            this.AssignedName = name;
        }

        /// <summary>
        /// The get common type.
        /// </summary>
        /// <param name="type2">
        /// The type 2.
        /// </param>
        /// <returns>
        /// The <see cref="JsonType" />.
        /// </returns>
        public JsonType GetCommonType(JsonType type2)
        {
            var commonType = this.GetCommonTypeEnum(this.Type, type2.Type);

            if (commonType == SupportedJsonTypes.Array)
            {
                if (type2.Type == SupportedJsonTypes.NullableSomething)
                {
                    return this;
                }

                if (this.Type == SupportedJsonTypes.NullableSomething)
                {
                    return type2;
                }

                var commonInternalType = this.InternalType.GetCommonType(type2.InternalType)
                    .MaybeMakeNullable(this.generatorConfig);

                if (commonInternalType != this.InternalType)
                {
                    return new JsonType(this.generatorConfig, SupportedJsonTypes.Array)
                               {
                                   InternalType = commonInternalType
                               };
                }
            }

            if (this.Type == commonType)
            {
                return this;
            }

            return new JsonType(this.generatorConfig, commonType).MaybeMakeNullable(this.generatorConfig);
        }

        /// <summary>
        /// The get inner most type.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonType" />.
        /// </returns>
        public JsonType GetInnerMostType()
        {
            if (this.Type != SupportedJsonTypes.Array)
            {
                throw new InvalidOperationException();
            }

            if (this.InternalType.Type != SupportedJsonTypes.Array)
            {
                return this.InternalType;
            }

            return this.InternalType.GetInnerMostType();
        }

        /// <summary>
        /// The get j token type.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetJTokenType()
        {
            switch (this.Type)
            {
                case SupportedJsonTypes.Boolean:
                case SupportedJsonTypes.Integer:
                case SupportedJsonTypes.Long:
                case SupportedJsonTypes.Float:
                case SupportedJsonTypes.Date:
                case SupportedJsonTypes.NullableBoolean:
                case SupportedJsonTypes.NullableInteger:
                case SupportedJsonTypes.NullableLong:
                case SupportedJsonTypes.NullableFloat:
                case SupportedJsonTypes.String:
                    return "JValue";
                case SupportedJsonTypes.Array:
                    return "JArray";
                case SupportedJsonTypes.Dictionary:
                    return "JObject";
                case SupportedJsonTypes.Object:
                    return "JObject";
                default:
                    return "JToken";
            }
        }

        /// <summary>
        /// The get reader name.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetReaderName()
        {
            if (this.Type == SupportedJsonTypes.Anything || this.Type == SupportedJsonTypes.NullableSomething
                                                         || this.Type == SupportedJsonTypes.NonConstrained)
            {
                return "ReadObject";
            }

            if (this.Type == SupportedJsonTypes.Object)
            {
                return $"ReadStronglyTypedObjects<{this.Type}>";
            }

            if (this.Type == SupportedJsonTypes.Array)
            {
                return $"ReadArray<{this.InternalType.GetTypeName()}>";
            }

            return $"Read{Enum.GetName(typeof(SupportedJsonTypes), this.Type)}";
        }

        /// <summary>
        /// The get type name.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetTypeName()
        {
            return this.generatorConfig.CodeWriter.GetTypeName(this, this.generatorConfig);
        }

        /// <summary>
        /// The get null.
        /// </summary>
        /// <param name="generator">
        /// The generator.
        /// </param>
        /// <returns>
        /// The <see cref="JsonType" />.
        /// </returns>
        internal static JsonType GetNull(IJsonClassGeneratorConfig generator)
        {
            return new JsonType(generator, SupportedJsonTypes.NullableSomething);
        }

        /// <summary>
        /// The maybe make nullable.
        /// </summary>
        /// <param name="generator">
        /// The generator.
        /// </param>
        /// <returns>
        /// The <see cref="JsonType" />.
        /// </returns>
        internal JsonType MaybeMakeNullable(IJsonClassGeneratorConfig generator)
        {
            if (!generator.AlwaysUseNullableValues)
            {
                return this;
            }

            return this.GetCommonType(GetNull(generator));
        }

        /// <summary>
        /// The get first type enum.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="SupportedJsonTypes" />.
        /// </returns>
        private static SupportedJsonTypes GetFirstTypeEnum(JToken token)
        {
            var type = token.Type;

            if (type == JTokenType.Integer)
            {
                if ((long)((JValue)token).Value < int.MaxValue)
                {
                    return SupportedJsonTypes.Integer;
                }

                return SupportedJsonTypes.Long;
            }

            switch (type)
            {
                case JTokenType.Array: return SupportedJsonTypes.Array;
                case JTokenType.Boolean: return SupportedJsonTypes.Boolean;
                case JTokenType.Float: return SupportedJsonTypes.Float;
                case JTokenType.Null: return SupportedJsonTypes.NullableSomething;
                case JTokenType.Undefined: return SupportedJsonTypes.NullableSomething;
                case JTokenType.String: return SupportedJsonTypes.String;
                case JTokenType.Object: return SupportedJsonTypes.Object;
                case JTokenType.Date: return SupportedJsonTypes.Date;

                default: return SupportedJsonTypes.Anything;
            }
        }

        /// <summary>
        /// The is null.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        private static bool IsNull(SupportedJsonTypes type)
        {
            return type == SupportedJsonTypes.NullableSomething;
        }

        /// <summary>
        /// The get common type enum.
        /// </summary>
        /// <param name="type1">
        /// The type 1.
        /// </param>
        /// <param name="type2">
        /// The type 2.
        /// </param>
        /// <returns>
        /// The <see cref="SupportedJsonTypes" />.
        /// </returns>
        private SupportedJsonTypes GetCommonTypeEnum(SupportedJsonTypes type1, SupportedJsonTypes type2)
        {
            if (type1 == SupportedJsonTypes.NonConstrained)
            {
                return type2;
            }

            if (type2 == SupportedJsonTypes.NonConstrained)
            {
                return type1;
            }

            switch (type1)
            {
                case SupportedJsonTypes.Boolean:
                    if (IsNull(type2))
                    {
                        return SupportedJsonTypes.NullableBoolean;
                    }

                    if (type2 == SupportedJsonTypes.Boolean)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.NullableBoolean:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Boolean)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.Integer:
                    if (IsNull(type2))
                    {
                        return SupportedJsonTypes.NullableInteger;
                    }

                    if (type2 == SupportedJsonTypes.Float)
                    {
                        return SupportedJsonTypes.Float;
                    }

                    if (type2 == SupportedJsonTypes.Long)
                    {
                        return SupportedJsonTypes.Long;
                    }

                    if (type2 == SupportedJsonTypes.Integer)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.NullableInteger:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Float)
                    {
                        return SupportedJsonTypes.NullableFloat;
                    }

                    if (type2 == SupportedJsonTypes.Long)
                    {
                        return SupportedJsonTypes.NullableLong;
                    }

                    if (type2 == SupportedJsonTypes.Integer)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.Float:
                    if (IsNull(type2))
                    {
                        return SupportedJsonTypes.NullableFloat;
                    }

                    if (type2 == SupportedJsonTypes.Float)
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Integer)
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Long)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.NullableFloat:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Float)
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Integer)
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Long)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.Long:
                    if (IsNull(type2))
                    {
                        return SupportedJsonTypes.NullableLong;
                    }

                    if (type2 == SupportedJsonTypes.Float)
                    {
                        return SupportedJsonTypes.Float;
                    }

                    if (type2 == SupportedJsonTypes.Integer)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.NullableLong:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Float)
                    {
                        return SupportedJsonTypes.NullableFloat;
                    }

                    if (type2 == SupportedJsonTypes.Integer)
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Long)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.Date:
                    if (IsNull(type2))
                    {
                        return SupportedJsonTypes.NullableDate;
                    }

                    if (type2 == SupportedJsonTypes.Date)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.NullableDate:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Date)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.NullableSomething:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.String)
                    {
                        return SupportedJsonTypes.String;
                    }

                    if (type2 == SupportedJsonTypes.Integer)
                    {
                        return SupportedJsonTypes.NullableInteger;
                    }

                    if (type2 == SupportedJsonTypes.Float)
                    {
                        return SupportedJsonTypes.NullableFloat;
                    }

                    if (type2 == SupportedJsonTypes.Long)
                    {
                        return SupportedJsonTypes.NullableLong;
                    }

                    if (type2 == SupportedJsonTypes.Boolean)
                    {
                        return SupportedJsonTypes.NullableBoolean;
                    }

                    if (type2 == SupportedJsonTypes.Date)
                    {
                        return SupportedJsonTypes.NullableDate;
                    }

                    if (type2 == SupportedJsonTypes.Array)
                    {
                        return SupportedJsonTypes.Array;
                    }

                    if (type2 == SupportedJsonTypes.Object)
                    {
                        return SupportedJsonTypes.Object;
                    }

                    break;
                case SupportedJsonTypes.Dictionary:
                    throw new ArgumentException();
                case SupportedJsonTypes.Array:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.Array)
                    {
                        return type1;
                    }

                    break;
                case SupportedJsonTypes.String:
                    if (IsNull(type2))
                    {
                        return type1;
                    }

                    if (type2 == SupportedJsonTypes.String)
                    {
                        return type1;
                    }

                    break;
            }

            return SupportedJsonTypes.Anything;
        }
    }
}
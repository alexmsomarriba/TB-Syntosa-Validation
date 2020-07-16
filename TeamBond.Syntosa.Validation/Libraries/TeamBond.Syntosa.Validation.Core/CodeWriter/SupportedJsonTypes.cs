namespace TeamBond.Syntosa.Validation.Core.CodeWriter
{
    /// <summary>
    /// The supported json types.
    /// </summary>
    public enum SupportedJsonTypes
    {
        /// <summary>
        /// The anything.
        /// </summary>
        Anything,

        /// <summary>
        /// The string.
        /// </summary>
        String,

        /// <summary>
        /// The boolean.
        /// </summary>
        Boolean,

        /// <summary>
        /// The integer.
        /// </summary>
        Integer,

        /// <summary>
        /// The long.
        /// </summary>
        Long,

        /// <summary>
        /// The float.
        /// </summary>
        Float,

        /// <summary>
        /// The date.
        /// </summary>
        Date,

        /// <summary>
        /// The nullable integer.
        /// </summary>
        NullableInteger,

        /// <summary>
        /// The nullable long.
        /// </summary>
        NullableLong,

        /// <summary>
        /// The nullable float.
        /// </summary>
        NullableFloat,

        /// <summary>
        /// The nullable boolean.
        /// </summary>
        NullableBoolean,

        /// <summary>
        /// The nullable date.
        /// </summary>
        NullableDate,

        /// <summary>
        /// The object.
        /// </summary>
        Object,

        /// <summary>
        /// The array.
        /// </summary>
        Array,

        /// <summary>
        /// The dictionary.
        /// </summary>
        Dictionary,

        /// <summary>
        /// The nullable something.
        /// </summary>
        NullableSomething,

        /// <summary>
        /// The non constrained.
        /// </summary>
        NonConstrained
    }
}
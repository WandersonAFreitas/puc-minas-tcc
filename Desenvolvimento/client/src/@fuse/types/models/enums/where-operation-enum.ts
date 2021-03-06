export enum WhereOperationEnum {
    Equal = 'eq',
    NotEqual = 'ne',
    LessThan = 'lt',
    LessThanOrEqual = 'le',
    GreaterThan = 'gt',
    GreaterThanOrEqual = 'ge',
    BeginsWith = 'bw',
    NotBeginsWith = 'bn',
    In = 'in',
    NotIn = 'ni',
    EndWith = 'ew',
    NotEndWith = 'en',
    Contains = 'cn',
    NotContains = 'nc',
    Null = 'nu',
    NotNull = 'nn',
}

export const WHERE_OPERATION_FILTER = {
    eq: WhereOperationEnum.Equal,
    ne: WhereOperationEnum.NotEqual,
    lt: WhereOperationEnum.LessThan,
    le: WhereOperationEnum.LessThanOrEqual,
    gt: WhereOperationEnum.GreaterThan,
    ge: WhereOperationEnum.GreaterThanOrEqual,
    bw: WhereOperationEnum.BeginsWith,
    bn: WhereOperationEnum.NotBeginsWith,
    in: WhereOperationEnum.In,
    ni: WhereOperationEnum.NotIn,
    ew: WhereOperationEnum.EndWith,
    en: WhereOperationEnum.NotEndWith,
    cn: WhereOperationEnum.Contains,
    nc: WhereOperationEnum.NotContains,
    nu: WhereOperationEnum.Null,
    nn: WhereOperationEnum.NotNull,
};

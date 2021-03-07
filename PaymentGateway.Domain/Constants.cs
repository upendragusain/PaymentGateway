﻿using System;

namespace PaymentGateway.Domain
{
    public static class Constants
    {
        public const decimal CHARGE_AMOUNT_MIN = 0.0001m;
        public const decimal CHARGE_AMOUNT_MAX = 99_999_999;
    }
}

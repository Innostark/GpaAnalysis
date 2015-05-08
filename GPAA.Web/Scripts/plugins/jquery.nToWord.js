/* Copyright (c) 2014 Zeeshan Qureshi (zeeshan@cr3ativ3mind.com) http://www.cr3ativ3mind.com
 * Licensed under Free for all
 * Use commercial for non-commercial but keep the comment section.
 *
 * Version : 0.1
 *
 * Requires: jQuery 1.2+
 * it supports two languages [Arabic - English] need to put ar for Arabic or en for English upon calling the function e.g.: nToWord(number:"",language"ar"); - nToWord(number:"",language"en");
 * it supports upto 999 999 999 999 but can be extended easily
 */
(function(jQuery) {
    $.nToWord = function(options) {
        var settings = $.extend({
            number: null,
            language: null
        }, options);
        var ones = ["", "One", "Two", "Three", "Four", "Five", "Six",
            "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve",
            "Thirteen", "Fourteen", "Fifteen", "Sixteen",
            "Seventeen", "Eighteen", "Nineteen"
        ];
        var tens = ["", "Ten", "Twenty", "Thirty", "Forty", "Fifty",
            "Sixty", "Seventy", "Eighty", "Ninety"
        ];
        var hunds = "Hundred";
        var thao = "Thousand";
        var mil = "Million";
        var bil = "Billion";
        var onesA = ["", "واحد", "اثنين", "ثلاث", "اربع", "خمس", "ست",
            "سبع", "ثماني", "تسع", "عشرة", "احدى عشرة", "اثنا عشرة",
            "ثلاثة عشرة", "اربعة عشرة", "خمسة عشرة", "ستة عشرة",
            "سبعة عشرة", "ثمانية عشرة", "تسعة عشرة"
        ];
        var tensA = ["", "عشرة", "عشرون", "ثلاثون", "اربعون", "خمسون",
            "ستون", "سبعون", "ثمانون", "تسعون"
        ];
        var hundsA = ["", "مائة", "مائتين"];
        var thaoA = ["", "الف", "الفين", "الاف"];
        var milA = ["", "مليون", "مليونين", "ملايين"];
        var bilA = ["", "بليون", "بليونين", "بلايين"];
        var word = null;
        var intNum = parseInt(options.number);
        var lang = options.language + "";
        if (lang === "ar") {
            if (intNum < 20) {
                word = onesA[intNum];
            } else if ((intNum >= 20) && (intNum <= 99)) {
                word = n99a(intNum);
            } else if ((intNum >= 100) && (intNum <= 999)) {
                word = n999a(intNum);
            } else if ((intNum >= 1000) && (intNum <= 999999)) {
                word = n9999a(intNum);
            } else if ((intNum >= 1000000) && (intNum <= 999999999)) {
                word = n9a(intNum);
            } else if ((intNum >= 1000000000) && (intNum <=
                999999999999)) {
                word = n12a(intNum);
            }

            function n99a(num) {
                var numberL20 = num % 10;
                var numberM20 = Math.floor(num / 10);
                var x = "";
                if (numberL20 == 0) {
                    x = tensA[numberM20];
                } else {
                    x = onesA[numberL20] + " و " + tensA[numberM20];
                }
                return x;
            }

            function n999a(num) {
                var numberL20 = num % 100;
                var numberM20 = Math.floor(num / 100);
                var text = "";
                if (numberL20 == 0) {
                    if (numberM20 > 2) {
                        text = onesA[numberM20] + hundsA[1];
                    } else {
                        text = hundsA[numberM20];
                    }
                } else {
                    if (numberL20 < 20) {
                        if (numberM20 > 2) {
                            text = onesA[numberM20] + hundsA[1] +
                                " و " + onesA[numberL20];
                        } else {
                            if (numberM20 == 1) {
                                text = hundsA[1] + " و " + onesA[
                                    numberL20];
                            } else if (numberM20 == 2) {
                                text = hundsA[2] + " و " + onesA[
                                    numberL20];
                            }
                        }
                    } else if ((numberL20 >= 20) && (numberL20 <=
                        99)) {
                        if (numberM20 > 2) {
                            text = onesA[numberM20] + hundsA[1] +
                                " و " + n99a(numberL20);
                        } else {
                            if (numberM20 == 1) {
                                text = hundsA[1] + " و " + n99a(
                                    numberL20);
                            } else if (numberM20 == 2) {
                                text = hundsA[2] + " و " + n99a(
                                    numberL20);
                            }
                        }
                        //text =	onesA[numberM20]+hundsA[1]+" و "+n99( numberL20 );
                    }
                }
                return text;
            }

            function n9999a(num) {
                var numDivided = Math.floor(num / 1000);
                var seconPart = num - (numDivided * 1000);
                var text = "";
                var text2 = "";

                if (numDivided < 20) {
                    if (numDivided > 2 ) {
                        text = onesA[numDivided] + " " + thaoA[3];
                    } else {
                        if (numDivided == 1) {
                            text = thaoA[1];
                        } else if(numDivided == 2) {
                            text = thaoA[2];
							
                        } 
                    }
                } 
				else if (numDivided <= 99) {
                    text = n99a(numDivided) + " " + thaoA[1];
                } else if (numDivided <= 999) {
                    text = n999a(numDivided) + " " + thaoA[1];
                }
                if (seconPart <= 9) {
                    if (seconPart != 0) {
                        text2 = " و " + onesA[seconPart];
                    } else {
                        text2 = " " + onesA[seconPart];
                    }
                } else if (seconPart <= 99) {
                    text2 = " و " + n99a(seconPart);
                } else if (seconPart <= 999) {
                    text2 = " و " + n999a(seconPart);
                }
                return text + text2;
            }

            function n9a(num) {
                var numDivided = Math.floor(num / 1000000);
                var seconPart = num - (numDivided * 1000000);
                var text = "";
                var text2 = "";
                if (numDivided < 20) {
                    if (numDivided > 2) {
                        text = onesA[numDivided] + " " + milA[3];
                    } else {
                        if (numDivided == 1) {
                            text = milA[1];
                        } else {
                            text = milA[2];
                        }
                    }
                } else if (numDivided <= 99) {
                    text = n99a(numDivided) + " " + milA[1];
                } else if (numDivided <= 999) {
                    text = n999a(numDivided) + " " + milA[1];
                }
                if (seconPart <= 9) {
                    if (seconPart != 0) {
                        text2 = " و " + onesA[seconPart];
                    } else {
                        text2 = " " + onesA[seconPart];
                    }
                } else if (seconPart <= 99) {
                    text2 = " و " + n99a(seconPart);
                } else if (seconPart <= 999) {
                    text2 = " و " + n999a(seconPart);
                } else if (seconPart <= 999999) {
                    text2 = " و " + n9999a(seconPart);
                }
                return text + text2;
            }

            function n12a(num) {
                var numDivided = Math.floor(num / 1000000000);
                var seconPart = num - (numDivided * 1000000000);
                var text = "";
                var text2 = "";
                if (numDivided < 20) {
                    if (numDivided > 2) {
                        text = onesA[numDivided] + " " + bilA[3];
                    } else {
                        if (numDivided == 1) {
                            text = bilA[1];
                        } else {
                            text = bilA[2];
                        }
                    }
                } else if (numDivided <= 99) {
                    text = n99a(numDivided) + " " + bilA[1];
                } else if (numDivided <= 999) {
                    text = n999a(numDivided) + " " + bilA[1];
                }
                if (seconPart <= 9) {
                    if (seconPart != 0) {
                        text2 = " و " + onesA[seconPart];
                    } else {
                        text2 = " " + onesA[seconPart];
                    }
                } else if (seconPart <= 99) {
                    text2 = " و " + n99a(seconPart);
                } else if (seconPart <= 999) {
                    text2 = " و " + n999a(seconPart);
                } else if (seconPart <= 999999) {
                    text2 = " و " + n9999a(seconPart);
                } else if (seconPart <= 999999999) {
                    text2 = " و " + n9a(seconPart);
                }
                return text + text2;
            }
            return word + "  ريالا فقط لا غير  ";
        } else if (lang === "en") {
            if (intNum < 20) {
                word = ones[intNum];
            } else if ((intNum >= 20) && (intNum <= 99)) {
                word = n99(intNum);
            } else if ((intNum >= 100) && (intNum <= 999)) {
                word = n999(intNum);
            } else if ((intNum >= 1000) && (intNum <= 999999)) {
                word = n9999(intNum);
            } else if ((intNum >= 1000000) && (intNum <= 999999999)) {
                word = n9(intNum);
            } else if ((intNum >= 1000000000) && (intNum <=
                999999999999)) {
                word = n12(intNum);
            }

            function n99(num) {
                var numberL20 = num % 10;
                var numberM20 = Math.floor(num / 10);
                var x = "";
                if (numberL20 == 0) {
                    x = tens[numberM20];
                } else {
                    x = tens[numberM20] + " " + ones[numberL20];
                }
                return x;
            }

            function n999(num) {
                var numberL20 = num % 100;
                var numberM20 = Math.floor(num / 100);
                var text = "";
                if (numberL20 == 0) {
                    text = ones[numberM20] + " " + hunds;
                } else {
                    if (numberL20 < 20) {
                        text = ones[numberM20] + " " + hunds +
                            " And " + ones[numberL20];
                    } else if ((numberL20 >= 20) && (numberL20 <=
                        99)) {
                        text = ones[numberM20] + " " + hunds +
                            " And " + n99(numberL20);
                        //text =	onesA[numberM20]+hundsA[1]+" و "+n99( numberL20 );
                    }
                }
                return text;
            }

            function n9999(num) {
                var numDivided = Math.floor(num / 1000);
                var seconPart = num - (numDivided * 1000);
                //var g = ((num / 1000) + "").split(".");
                // var seconPart = parseInt(g[1]);
                //var seconPart = g;
                var text = "";
                var text2 = "";
                if (numDivided < 20) {
                    text = ones[numDivided] + " " + thao;
                } else if (numDivided <= 99) {
                    text = n99(numDivided) + "  " + thao;
                } else if (numDivided <= 999) {
                    text = n999(numDivided) + "  " + thao;
                }
                if (seconPart <= 9) {
                    if (seconPart != 0) {
                        text2 = " And " + ones[seconPart];
                    } else {
                        text2 = " " + ones[seconPart];
                    }
                } else if (seconPart <= 99) {
                    text2 = " And " + n99(seconPart);
                } else if (seconPart <= 999) {
                    text2 = " And " + n999(seconPart);
                }
                return text + text2;
            }

            function n9(num) {
                var numDivided = Math.floor(num / 1000000);
                var seconPart = num - (numDivided * 1000000);
                //var g = ((num / 1000000) + "").split(".");
                // var seconPart = parseInt(g[1]);
                var text = "";
                var text2 = "";
                if (numDivided < 20) {
                    text = ones[numDivided] + " " + mil;
                } else if (numDivided <= 99) {
                    text = n99(numDivided) + " " + mil;
                } else if (numDivided <= 999) {
                    text = n999(numDivided) + " " + mil;
                }
                if (seconPart <= 9) {
                    if (seconPart != 0) {
                        text2 = " And " + ones[seconPart];
                    } else {
                        text2 = " " + ones[seconPart];
                    }
                } else if (seconPart <= 99) {
                    text2 = " And " + n99(seconPart);
                } else if (seconPart <= 999) {
                    text2 = " And " + n999(seconPart);
                } else if (seconPart <= 999999) {
                    text2 = " And " + n9999(seconPart);
                }
                return text + text2;
            }

            function n12(num) {
                var numDivided = Math.floor(num / 1000000000);
                var seconPart = num - (numDivided * 1000000000);
                //var g = ((num / 1000000000) + "").split(".");
                //var seconPart = parseInt(g[1]);
                var text = "";
                var text2 = "";
                if (numDivided < 20) {
                    text = ones[numDivided] + " " + bil;
                } else if (numDivided <= 99) {
                    text = n99(numDivided) + " " + bil;
                } else if (numDivided <= 999) {
                    text = n999(numDivided) + " " + bil;
                }
                if (seconPart <= 9) {
                    if (seconPart != 0) {
                        text2 = " And " + ones[seconPart];
                    } else {
                        text2 = " " + ones[seconPart];
                    }
                } else if (seconPart <= 99) {
                    text2 = " And " + n99(seconPart);
                } else if (seconPart <= 999) {
                    text2 = " And " + n999(seconPart);
                } else if (seconPart <= 999999) {
                    text2 = " And " + n9999(seconPart);
                } else if (seconPart <= 999999999) {
                    text2 = " And " + n9(seconPart);
                }
                return text + text2;
            }
            return word + "  Riyals Only  ";
        }
    }
})(jQuery);
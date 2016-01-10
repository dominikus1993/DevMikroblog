module Application.Utils {
    enum DateOfSeconds {
        Second = 1,
        Minute = 60,
        Hour = 3600,
        Day = 86400,
        Month = 2592000,
        Year = 31536000
    }

    export class DateUtils {
        static dateAgo(date: Date): string {
            const seconds = (Date.now() - date.getTime()) / 1000;
            if (seconds < DateOfSeconds.Minute) {
                return this.endingIfIsSecond(this.floatToInt(seconds));
            }
            else if (seconds >= DateOfSeconds.Minute && seconds < DateOfSeconds.Hour) {
                return this.endingIfIsMinute(this.floatToInt(seconds / DateOfSeconds.Minute));
            }
            else if (seconds >= DateOfSeconds.Hour && seconds < DateOfSeconds.Day) {
                return this.endingIfIsHour(this.floatToInt(seconds / DateOfSeconds.Hour));
            } else if (seconds >= DateOfSeconds.Day && seconds < DateOfSeconds.Month) {
                return this.endingIfDay(this.floatToInt(seconds / DateOfSeconds.Day));
            } else {
                return this.endingIfMonth(this.floatToInt(seconds / DateOfSeconds.Month));
            }

        }

        private static endingIfIsSecond(seconds: number) {
            return this.defaultEnding(seconds, "sekund");
        }


        private static endingIfIsMinute(minutes: number) {
            return this.defaultEnding(minutes, "minut");
        }

        private static endingIfIsHour(hours: number) {
            return this.defaultEnding(hours, "godzin");
        }

        private static endingIfDay(days: number) {
            if (days === 1) {
                return `${days} dzień temu`;
            } else {
                return `${days} dni temu`;
            }
        }

        private static endingIfMonth(days: number) {
            if (days === 1) {
                return `${days} miesiąc temu`;
            } else if (days > 1 && days < 5) {
                return `${days} miesiące temu`;
            } else {
                return `${days} miesiący temu`;
            }
        }

        private static defaultEnding(time: number, text: string) {
            if (time === 1) {
                return `${time} ${text}a temu`;
            }
            else if (time > 1 && time < 5) {
                return `${time} ${text}y temu`;
            } else {
                return `${time} ${text} temu`;
            }
        }

        private static floatToInt(num: number) {
            return num | 0;
        }
    }

    export class TagUtils {
        static parseTag(text: string) {
            const tagRegEx = /(?:(#)([a-z]+))/gi;
            return text.replace(tagRegEx, "<a href='#!/Tag/$2'>$1$2</a>");
        }
    }

    export class HtmlUtils {
        static stripHtml(text: string) {
            const regex = /(<([^>]+)>)/ig;
            return text.replace(regex, "");
        }
    }
}
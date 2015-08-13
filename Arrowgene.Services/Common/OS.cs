﻿namespace Arrowgene.Services.Common
{
    using System;

    /// <summary>
    /// Dealing with OS
    /// </summary>
    public static class OS
    {
        /// <summary>
        /// TODO SUMMARY
        /// </summary>
        public enum OsVersion
        {
            /// <summary>UNKNOWN</summary>
            UNKNOWN = 1,
            /// <summary>WIN_3_1</summary>
            WIN_3_1,
            /// <summary>WIN_95</summary>
            WIN_95,
            /// <summary>WIN_98</summary>
            WIN_98,
            /// <summary>WIN_ME</summary>
            WIN_ME,
            /// <summary>WIN_NT_3_5</summary>
            WIN_NT_3_5,
            /// <summary>WIN_NT_4</summary>
            WIN_NT_4,
            /// <summary>WIN_2000</summary>
            WIN_2000,
            /// <summary>WIN_XP</summary>
            WIN_XP,
            /// <summary>WIN_2003</summary>
            WIN_2003,
            /// <summary>WIN_VISTA</summary>
            WIN_VISTA,
            /// <summary>WIN_2008</summary>
            WIN_2008,
            /// <summary>WIN_7</summary>
            WIN_7,
            /// <summary>WIN_2008_R2</summary>
            WIN_2008_R2,
            /// <summary>WIN_8</summary>
            WIN_8,
            /// <summary>WIN_8_1</summary>
            WIN_8_1,
            /// <summary>WIN_10</summary>
            WIN_10,
            /// <summary>WIN_CE</summary>
            WIN_CE,
            /// <summary>UNIX</summary>
            UNIX,
            /// <summary>XBOX</summary>
            XBOX,
            /// <summary>MAX_OSX</summary>
            MAX_OSX
        };

        /// <summary>
        /// Returns version of OS.
        /// </summary>
        /// <returns>
        /// Returns <see cref="OsVersion"/>.
        /// </returns>
        /// <remarks>
        /// In order to detect cetain windows versions,
        /// it is necessary to add a custom .manifest file to the project.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dn481241%28v=vs.85%29.aspx
        /// Otherwise win 8.1 will be reconized as win 8.0 for example.
        /// </remarks>
        public static OsVersion GetOperatingSystemVersion()
        {
            int major = Environment.OSVersion.Version.Major;
            int minor = Environment.OSVersion.Version.Minor;
            PlatformID platformId = Environment.OSVersion.Platform;
            OsVersion osVersion = OsVersion.UNKNOWN;

            switch (platformId)
            {
                case PlatformID.Win32S:
                    osVersion = OsVersion.WIN_3_1;
                    break;

                case PlatformID.Win32Windows:
                    switch (major)
                    {
                        case 4:
                            switch (minor)
                            {
                                case 0:
                                    osVersion = OsVersion.WIN_95;
                                    break;
                                case 10:
                                    osVersion = OsVersion.WIN_98;
                                    break;
                                case 90:
                                    osVersion = OsVersion.WIN_ME;
                                    break;
                            }
                            break;
                    }
                    break;

                case PlatformID.Win32NT:
                    switch (major)
                    {
                        case 3:
                            osVersion = OsVersion.WIN_NT_3_5;
                            break;

                        case 4:
                            osVersion = OsVersion.WIN_NT_4;
                            break;

                        case 5:
                            switch (minor)
                            {
                                case 0:
                                    osVersion = OsVersion.WIN_2000;
                                    break;
                                case 1:
                                    osVersion = OsVersion.WIN_XP;
                                    break;
                                case 2:
                                    osVersion = OsVersion.WIN_2003;
                                    break;
                            }
                            break;

                        case 6:
                            switch (minor)
                            {
                                case 0:
                                    osVersion = OsVersion.WIN_VISTA;
                                    break;
                                case 1:
                                    osVersion = OsVersion.WIN_7;
                                    break;
                                case 2:
                                    osVersion = OsVersion.WIN_8;
                                    break;
                                case 3:
                                    osVersion = OsVersion.WIN_8_1;
                                    break;
                            }
                            break;
                    }
                    break;

                case PlatformID.WinCE:
                    osVersion = OsVersion.WIN_CE;
                    break;

                case PlatformID.Unix:
                    osVersion = OsVersion.UNIX;
                    break;

                case PlatformID.Xbox:
                    osVersion = OsVersion.XBOX;
                    break;

                case PlatformID.MacOSX:
                    osVersion = OsVersion.MAX_OSX;
                    break;
            }

            return osVersion;
        }
    }
}

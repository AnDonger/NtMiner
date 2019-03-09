﻿using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace NTMiner {
    public static class NtMinerRegistry {
        private const string AutoRunSubKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        public static void SetAutoBoot(string valueName, bool isAutoBoot, string otherParams = null) {
            string exeFileFullName = Process.GetCurrentProcess().MainModule.FileName;
            if (isAutoBoot == true) {
                string value = exeFileFullName;
                if (!string.IsNullOrEmpty(otherParams)) {
                    value = value + " " + otherParams;
                }
                Windows.Registry.SetValue(Registry.CurrentUser, AutoRunSubKey, valueName, value);
            }
            else {
                Windows.Registry.DeleteValue(Registry.CurrentUser, AutoRunSubKey, valueName);
            }
        }

        public const string NtMinerRegistrySubKey = @".DEFAULT\Software\NTMiner";

        #region IsShowInTaskbar
        public static bool GetIsShowInTaskbar() {
            object isAutoBootValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "IsShowInTaskbar");
            return isAutoBootValue == null || isAutoBootValue.ToString() == "True";
        }

        public static void SetIsShowInTaskbar(bool value) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "IsShowInTaskbar", value);
        }
        #endregion

        #region IsShowNotifyIcon
        public static bool GetIsShowNotifyIcon() {
            object isAutoBootValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "IsShowNotifyIcon");
            return isAutoBootValue == null || isAutoBootValue.ToString() == "True";
        }

        public static void SetIsShowNotifyIcon(bool value) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "IsShowNotifyIcon", value);
        }
        #endregion

        #region IsShowDaemonNotifyIcon
        public static bool GetIsShowDaemonNotifyIcon() {
            object isAutoBootValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "IsShowDaemonNotifyIcon");
            return isAutoBootValue != null && isAutoBootValue.ToString() == "True";
        }

        public static void SetIsShowDaemonNotifyIcon(bool value) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "IsShowDaemonNotifyIcon", value);
        }
        #endregion

        #region Location
        public static string GetLocation() {
            object locationValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "Location");
            if (locationValue != null) {
                return (string)locationValue;
            }
            return string.Empty;
        }

        public static void SetLocation(string location) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "Location", location);
        }
        #endregion

        #region Arguments
        public static string GetArguments() {
            object argumentsValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "Arguments");
            if (argumentsValue != null) {
                return (string)argumentsValue;
            }
            return string.Empty;
        }

        public static void SetArguments(string arguments) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "Arguments", arguments);
        }
        #endregion

        #region IsAutoBoot
        public static bool GetIsAutoBoot() {
            object isAutoBootValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "IsAutoBoot");
            return isAutoBootValue != null && isAutoBootValue.ToString() == "True";
        }

        public static void SetIsAutoBoot(bool value) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "IsAutoBoot", value);
        }
        #endregion

        #region CurrentVersion
        public static string GetCurrentVersion() {
            string currentVersion = "1.0.0.0";
            object currentVersionValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "CurrentVersion");
            if (currentVersionValue != null) {
                currentVersion = (string)currentVersionValue;
            }
            if (string.IsNullOrEmpty(currentVersion)) {
                return "1.0.0.0";
            }
            return currentVersion;
        }

        public static void SetCurrentVersion(string version) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "CurrentVersion", version);
        }
        #endregion

        #region CurrentVersionTag
        public static string GetCurrentVersionTag() {
            string currentVersionTag = string.Empty;
            object currentVersionTagValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "CurrentVersionTag");
            if (currentVersionTagValue != null) {
                currentVersionTag = (string)currentVersionTagValue;
            }
            return currentVersionTag;
        }

        public static void SetCurrentVersionTag(string versionTag) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "CurrentVersionTag", versionTag);
        }
        #endregion

        #region UpdaterVersion
        public static string GetUpdaterVersion() {
            string updaterVersion = "NTMinerUpdater.exe";
            object updaterVersionValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "UpdaterVersion");
            if (updaterVersionValue != null) {
                updaterVersion = (string)updaterVersionValue;
            }
            if (string.IsNullOrEmpty(updaterVersion)) {
                return "NTMinerUpdater.exe";
            }
            return updaterVersion;
        }

        public static void SetUpdaterVersion(string version) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "UpdaterVersion", version);
        }
        #endregion

        #region MinerServerHost
        public const string MinerServerHost = "server.ntminer.com";
        public static string GetMinerServerHost() {
            object minerServerHostValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "MinerServerHost");
            if (minerServerHostValue == null) {
                return MinerServerHost;
            }
            return (string)minerServerHostValue;
        }

        public static void SetMinerServerHost(string host) {
            if (string.IsNullOrEmpty(host)) {
                host = MinerServerHost;
            }
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "MinerServerHost", host);
        }
        #endregion

        #region Language
        public static string GetLanguage() {
            string langCode = string.Empty;
            object languageValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "Language");
            if (languageValue == null) {
                // 如果本机语言是中文则默认是中文
                if (System.Globalization.CultureInfo.InstalledUICulture.Name == "zh-CN") {
                    langCode = "zh-CN";
                }
            }
            else {
                langCode = (string)languageValue;
            }
            return langCode;
        }

        public static void SetLanguage(string langCode) {
            Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "Language", langCode);
        }
        #endregion

        #region GetClientId
        public static Guid GetClientId() {
            Guid id;
            object clientIdValue = Windows.Registry.GetValue(Registry.Users, NtMinerRegistrySubKey, "ClientId");
            if (clientIdValue == null) {
                id = Guid.NewGuid();
                Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "ClientId", id.ToString());
            }
            else if (!Guid.TryParse((string)clientIdValue, out id)) {
                id = Guid.NewGuid();
                Windows.Registry.SetValue(Registry.Users, NtMinerRegistrySubKey, "ClientId", id.ToString());
            }
            return id;
        }
        #endregion
    }
}

﻿using Aliyun.OSS;
using NTMiner.Core.MinerServer;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NTMiner.Controllers {
    // 注意该控制器不能重命名
    public class FileUrlController : ApiControllerBase, IFileUrlController {
        [Role.Public]
        [HttpPost]
        public string NTMinerUrl([FromBody]NTMinerUrlRequest request) {
            if (request == null || string.IsNullOrEmpty(request.FileName)) {
                return string.Empty;
            }
            var req = new GeneratePresignedUriRequest("ntminer", request.FileName, SignHttpMethod.Get) {
                Expiration = DateTime.Now.AddMinutes(10)
            };
            var uri = WebApiRoot.OssClient.GeneratePresignedUri(req);
            return uri.ToString();
        }

        [Role.Public]
        [HttpPost]
        public List<NTMinerFileData> NTMinerFiles() {
            var list = WebApiRoot.NTMinerFileSet.GetAll();
            return list;
        }

        [Role.Admin]
        [HttpPost]
        public ResponseBase AddOrUpdateNTMinerFile([FromBody]DataRequest<NTMinerFileData> request) {
            if (request == null || request.Data == null) {
                return ResponseBase.InvalidInput("参数错误");
            }
            try {
                WebApiRoot.NTMinerFileSet.AddOrUpdate(request.Data);
                return ResponseBase.Ok();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return ResponseBase.ServerError(e.Message);
            }
        }

        [Role.Admin]
        [HttpPost]
        public ResponseBase RemoveNTMinerFile([FromBody]DataRequest<Guid> request) {
            if (request == null) {
                return ResponseBase.InvalidInput("参数错误");
            }
            try {
                WebApiRoot.NTMinerFileSet.RemoveById(request.Data);
                return ResponseBase.Ok();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return ResponseBase.ServerError(e.Message);
            }
        }

        [Role.Public]
        [HttpPost]
        public string NTMinerUpdaterUrl() {
            try {
                string fileName;
                if (!VirtualRoot.LocalAppSettingSet.TryGetAppSetting(NTKeyword.NTMinerUpdaterFileNameAppSettingKey, out IAppSetting setting)) {
                    fileName = NTKeyword.NTMinerUpdaterFileName;
                }
                else {
                    fileName = (string)setting.Value;
                }
                var req = new GeneratePresignedUriRequest("ntminer", $"tools/{fileName}", SignHttpMethod.Get);
                var uri = WebApiRoot.OssClient.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return string.Empty;
            }
        }

        [Role.Public]
        [HttpPost]
        public string MinerClientFinderUrl() {
            try {
                string fileName;
                if (!VirtualRoot.LocalAppSettingSet.TryGetAppSetting(NTKeyword.MinerClientFinderFileNameAppSettingKey, out IAppSetting setting)) {
                    fileName = NTKeyword.MinerClientFinderFileName;
                }
                else {
                    fileName = (string)setting.Value;
                }
                var req = new GeneratePresignedUriRequest("ntminer", $"tools/{fileName}", SignHttpMethod.Get);
                var uri = WebApiRoot.OssClient.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return string.Empty;
            }
        }

        [Role.Public]
        [HttpPost]
        public string LiteDbExplorerUrl() {
            try {
                var req = new GeneratePresignedUriRequest("ntminer", "tools/LiteDBExplorerPortable.zip", SignHttpMethod.Get);
                var uri = WebApiRoot.OssClient.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return string.Empty;
            }
        }

        [Role.Public]
        [HttpPost]
        public string AtikmdagPatcherUrl() {
            try {
                string fileName;
                if (!VirtualRoot.LocalAppSettingSet.TryGetAppSetting(NTKeyword.MinerClientFinderFileNameAppSettingKey, out IAppSetting setting)) {
                    fileName = NTKeyword.AtikmdagPatcherFileName;
                }
                else {
                    fileName = (string)setting.Value;
                }
                var req = new GeneratePresignedUriRequest("ntminer", $"tools/{fileName}", SignHttpMethod.Get);
                var uri = WebApiRoot.OssClient.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return string.Empty;
            }
        }

        [Role.Public]
        [HttpPost]
        public string SwitchRadeonGpuUrl() {
            try {
                string fileName;
                if (!VirtualRoot.LocalAppSettingSet.TryGetAppSetting(NTKeyword.MinerClientFinderFileNameAppSettingKey, out IAppSetting setting)) {
                    fileName = NTKeyword.SwitchRadeonGpuFileName;
                }
                else {
                    fileName = (string)setting.Value;
                }
                var req = new GeneratePresignedUriRequest("ntminer", $"tools/{fileName}", SignHttpMethod.Get);
                var uri = WebApiRoot.OssClient.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return string.Empty;
            }
        }

        [Role.Public]
        [HttpPost]
        public string PackageUrl([FromBody]PackageUrlRequest request) {
            try {
                if (request == null || string.IsNullOrEmpty(request.Package)) {
                    return string.Empty;
                }
                var req = new GeneratePresignedUriRequest("ntminer", $"packages/{request.Package}", SignHttpMethod.Get) {
                    Expiration = DateTime.Now.AddMinutes(10)
                };
                var uri = WebApiRoot.OssClient.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (Exception e) {
                Logger.ErrorDebugLine(e);
                return string.Empty;
            }
        }
    }
}

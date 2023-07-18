import { AppState } from "../AppState.js";
import { api } from "./AxiosService.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";

class KeepsService {

  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
    logger.log(AppState.keeps)
  }

  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    AppState.activeKeep = res.data
    AppState.activeKeep = AppState.keeps.find(k => k.id == keepId)
    AppState.activeKeep.views++
    logger.log('[ACTIVE KEEP SELECTED] =>', AppState.activeKeep)
  }
}

export const keepsService = new KeepsService();
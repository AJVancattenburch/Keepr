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

  async createKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    AppState.keeps.push(new Keep(res.data))
    logger.log('[NEW KEEP CREATED] =>', res.data)
  }

  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
  }

  async searchKeeps(searchTerm) {
    logger.log('[SEARCHING FOR KEEPS CONTAINING THE WORD(S)] =>', searchTerm)
    const res = await api.get(`api/keeps?query=${searchTerm}`)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

}

export const keepsService = new KeepsService();
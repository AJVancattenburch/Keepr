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
}

export const keepsService = new KeepsService();
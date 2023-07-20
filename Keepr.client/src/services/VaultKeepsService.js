import { AppState } from "../AppState.js"
import { VaultHasKeep } from "../models/VaultKeep.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  
  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.vaultHasKeeps = res.data.map(vk => new VaultHasKeep(vk))
  }
}

export const vaultKeepsService = new VaultKeepsService()
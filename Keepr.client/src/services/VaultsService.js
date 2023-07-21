import { AppState } from '../AppState'
import { Vault } from '../models/Vault.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'

class VaultsService {

  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    AppState.myVaults.unshift(new Vault(res.data))
    AppState.userVaults.unshift(new Vault(res.data))
    logger.log('[NEW VAULT CREATED] =>', res.data)
  }

  async getMyVaults(accountId) {
    const res = await api.get(`/api/profiles/${accountId}/vaults`)
    AppState.myVaults = res.data.map(v => new Vault(v))
    logger.log('[MY VAULTS] =>', AppState.myVaults)
  }

  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = res.data
  }

  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.userVaults = res.data.map(v => new Vault(v))
    logger.log('[ACTIVE PROFILE PUBLIC VAULTS] =>', AppState.userVaults)
  }

  async deleteVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`)
    const foundVault = AppState.myVaults.find(v => v.id == vaultId)
    AppState.myVaults.splice(foundVault, 1)
  }
}

export const vaultsService = new VaultsService()